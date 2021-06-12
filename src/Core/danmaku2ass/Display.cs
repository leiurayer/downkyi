using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core.danmaku2ass
{
    /// <summary>
    /// 显示方式
    /// </summary>
    public class Display
    {
        public Config Config;
        public Danmaku Danmaku;
        public int LineIndex;

        public int FontSize;
        public bool IsScaled;
        public int MaxLength;
        public int Width;
        public int Height;

        public Tuple<int, int> Horizontal;
        public Tuple<int, int> Vertical;

        public int Duration;
        public int Leave;

        protected Display() { }

        public Display(Config config, Danmaku danmaku)
        {
            Config = config;
            Danmaku = danmaku;
            LineIndex = 0;

            IsScaled = SetIsScaled();
            FontSize = SetFontSize();
            MaxLength = SetMaxLength();
            Width = SetWidth();
            Height = SetHeight();

            Horizontal = SetHorizontal();
            Vertical = SetVertical();

            Duration = SetDuration();
            Leave = SetLeave();
        }

        /// <summary>
        /// 根据弹幕样式自动创建对应的 Display 类
        /// </summary>
        /// <returns></returns>
        public static Display Factory(Config config, Danmaku danmaku)
        {
            Dictionary<string, Display> dict = new Dictionary<string, Display>
            {
                { "scroll", new ScrollDisplay(config, danmaku) },
                { "top", new TopDisplay(config, danmaku) },
                { "bottom", new BottomDisplay(config, danmaku) }
            };
            return dict[danmaku.Style];
        }

        /// <summary>
        /// 字体大小
        /// 按用户自定义的字体大小来缩放
        /// </summary>
        /// <returns></returns>
        protected int SetFontSize()
        {
            if (IsScaled)
            {
                Console.WriteLine($"{Danmaku.SizeRatio}");
            }
            return Utils.IntCeiling(Config.BaseFontSize * Danmaku.SizeRatio);
        }

        /// <summary>
        /// 字体是否被缩放过
        /// </summary>
        /// <returns></returns>
        protected bool SetIsScaled()
        {
            return !Math.Round(Danmaku.SizeRatio, 2).Equals(1.0);
            //return Danmaku.SizeRatio.Equals(1.0f);
        }

        /// <summary>
        /// 最长的行字符数
        /// </summary>
        /// <returns></returns>
        protected int SetMaxLength()
        {
            string[] lines = Danmaku.Content.Split('\n');
            int maxLength = 0;
            foreach (string line in lines)
            {
                int length = Utils.DisplayLength(line);
                if (maxLength < length)
                {
                    maxLength = length;
                }
            }
            return maxLength;
        }

        /// <summary>
        /// 整条字幕宽度
        /// </summary>
        /// <returns></returns>
        protected int SetWidth()
        {
            float charCount = MaxLength;// / 2;
            return Utils.IntCeiling(FontSize * charCount);
        }

        /// <summary>
        /// 整条字幕高度
        /// </summary>
        /// <returns></returns>
        protected int SetHeight()
        {
            int lineCount = Danmaku.Content.Split('\n').Length;
            return lineCount * FontSize;
        }

        /// <summary>
        /// 出现和消失的水平坐标位置
        /// 默认在屏幕中间
        /// </summary>
        /// <returns></returns>
        protected virtual Tuple<int, int> SetHorizontal()
        {
            int x = (int)Math.Floor(Config.ScreenWidth / 2.0);
            return Tuple.Create(x, x);
        }

        /// <summary>
        /// 出现和消失的垂直坐标位置
        /// 默认在屏幕中间
        /// </summary>
        /// <returns></returns>
        protected virtual Tuple<int, int> SetVertical()
        {
            int y = (int)Math.Floor(Config.ScreenHeight / 2.0);
            return Tuple.Create(y, y);
        }

        /// <summary>
        /// 整条字幕的显示时间
        /// </summary>
        /// <returns></returns>
        protected virtual int SetDuration()
        {
            int baseDuration = 3 + Config.TuneDuration;
            if (baseDuration <= 0)
            {
                baseDuration = 0;
            }
            float charCount = MaxLength / 2;

            int value;
            if (charCount < 6)
            {
                value = baseDuration + 1;
            }
            else if (charCount < 12)
            {
                value = baseDuration + 2;
            }
            else
            {
                value = baseDuration + 3;
            }
            return value;
        }

        /// <summary>
        /// 离开碰撞时间
        /// </summary>
        /// <returns></returns>
        protected virtual int SetLeave()
        {
            return (int)(Danmaku.Start + Duration);
        }

        /// <summary>
        /// 按照新的行号重新布局
        /// </summary>
        /// <param name="lineIndex"></param>
        public void Relayout(int lineIndex)
        {
            LineIndex = lineIndex;
            Horizontal = SetHorizontal();
            Vertical = SetVertical();
        }

    }

    /// <summary>
    /// 顶部
    /// </summary>
    public class TopDisplay : Display
    {
        public TopDisplay(Config config, Danmaku danmaku) : base(config, danmaku)
        {
            Console.WriteLine("TopDisplay constructor.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Tuple<int, int> SetVertical()
        {
            // 这里 y 坐标为 0 就是最顶行了
            int y = LineIndex * Config.BaseFontSize;
            return Tuple.Create(y, y);
        }
    }

    /// <summary>
    /// 底部
    /// </summary>
    public class BottomDisplay : Display
    {
        public BottomDisplay(Config config, Danmaku danmaku) : base(config, danmaku)
        {
            Console.WriteLine("BottomDisplay constructor.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Tuple<int, int> SetVertical()
        {
            // 要让字幕不超出底部，减去高度
            int y = Config.ScreenHeight - (LineIndex * Config.BaseFontSize) - Height;
            // 再减去自定义的底部边距
            y -= Config.BottomMargin;
            return Tuple.Create(y, y);
        }
    }

    /// <summary>
    /// 滚动
    /// </summary>
    public class ScrollDisplay : Display
    {
        public int Distance;
        public int Speed;

        public ScrollDisplay(Config config, Danmaku danmaku) : base()
        {
            Console.WriteLine("ScrollDisplay constructor.");

            Config = config;
            Danmaku = danmaku;
            LineIndex = 0;

            IsScaled = SetIsScaled();
            FontSize = SetFontSize();
            MaxLength = SetMaxLength();
            Width = SetWidth();
            Height = SetHeight();

            Horizontal = SetHorizontal();
            Vertical = SetVertical();

            Distance = SetDistance();
            Speed = SetSpeed();

            Duration = SetDuration();
            Leave = SetLeave();
        }

        /// <summary>
        /// ASS 的水平位置参考点是整条字幕文本的中点
        /// </summary>
        /// <returns></returns>
        protected override Tuple<int, int> SetHorizontal()
        {
            int x1 = Config.ScreenWidth + (int)Math.Floor(Width / 2.0);
            int x2 = 0 - (int)Math.Floor(Width / 2.0);
            return Tuple.Create(x1, x2);
        }

        protected override Tuple<int, int> SetVertical()
        {
            int baseFontSize = Config.BaseFontSize;

            // 垂直位置，按基准字体大小算每一行的高度
            int y = (LineIndex + 1) * baseFontSize;

            // 个别弹幕可能字体比基准要大，所以最上的一行还要避免挤出顶部屏幕
            // 坐标不能小于字体大小
            if (y < FontSize)
            {
                y = FontSize;
            }
            return Tuple.Create(y, y);
        }

        /// <summary>
        /// 字幕坐标点的移动距离
        /// </summary>
        /// <returns></returns>
        protected int SetDistance()
        {
            Tuple<int, int> x = Horizontal;
            return x.Item1 - x.Item2;
        }

        /// <summary>
        /// 字幕每个字的移动的速度
        /// </summary>
        /// <returns></returns>
        protected int SetSpeed()
        {
            // 基准时间，就是每个字的移动时间
            // 12 秒加上用户自定义的微调
            int baseDuration = 12 + Config.TuneDuration;
            if (baseDuration <= 0)
            {
                baseDuration = 1;
            }
            return Utils.IntCeiling(Config.ScreenWidth / baseDuration);
        }

        /// <summary>
        /// 计算每条弹幕的显示时长，同步方式
        /// 每个弹幕的滚动速度都一样，辨认度好，适合观看剧集类视频。
        /// </summary>
        /// <returns></returns>
        public int SyncDuration()
        {
            return Distance / Speed;
        }

        /// <summary>
        /// 计算每条弹幕的显示时长，异步方式
        /// 每个弹幕的滚动速度都不一样，动态调整，辨认度低，适合观看 MTV 类视频。
        /// </summary>
        /// <returns></returns>
        public int AsyncDuration()
        {
            int baseDuration = 6 + Config.TuneDuration;
            if (baseDuration <= 0)
            {
                baseDuration = 0;
            }
            float charCount = MaxLength / 2;

            int value;
            if (charCount < 6)
            {
                value = (int)(baseDuration + charCount);
            }
            else if (charCount < 12)
            {
                value = baseDuration + (int)(charCount / 2);
            }
            else if (charCount < 24)
            {
                value = baseDuration + (int)(charCount / 3);
            }
            else
            {
                value = baseDuration + 10;
            }
            return value;
        }

        /// <summary>
        /// 整条字幕的移动时间
        /// </summary>
        /// <returns></returns>
        protected override int SetDuration()
        {
            string methodName = Config.LayoutAlgorithm.Substring(0, 1).ToUpper() + Config.LayoutAlgorithm.Substring(1);
            methodName += "Duration";
            MethodInfo method = typeof(ScrollDisplay).GetMethod(methodName);
            if (method != null)
            {
                return (int)method.Invoke(this, null);
            }
            return 0;
        }

        /// <summary>
        /// 离开碰撞时间
        /// </summary>
        /// <returns></returns>
        protected override int SetLeave()
        {
            // 对于滚动样式弹幕来说，就是最后一个字符离开最右边缘的时间
            // 坐标是字幕中点，在屏幕外和内各有半个字幕宽度
            // 也就是跑过一个字幕宽度的路程
            float duration = Width / Speed;
            return (int)(Danmaku.Start + duration);
        }

    }

}
