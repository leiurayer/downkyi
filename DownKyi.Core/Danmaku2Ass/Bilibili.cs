using DownKyi.Core.BiliApi.Danmaku;
using System.Collections.Generic;

namespace DownKyi.Core.Danmaku2Ass
{
    public class Bilibili
    {
        private static Bilibili instance;

        private readonly Dictionary<string, bool> config = new Dictionary<string, bool>
        {
            { "top_filter", false },
            { "bottom_filter", false },
            { "scroll_filter", false }
        };

        private readonly Dictionary<int, string> mapping = new Dictionary<int, string>
        {
            { 0, "none" }, // 保留项
            { 1, "scroll" },
            { 2, "scroll" },
            { 3, "scroll" },
            { 4, "bottom" },
            { 5, "top" },
            { 6, "scroll" }, // 逆向滚动弹幕，还是当滚动处理
            { 7, "none" }, // 高级弹幕，暂时不要考虑
            { 8, "none" }, // 代码弹幕，暂时不要考虑
            { 9, "none" }, // BAS弹幕，暂时不要考虑
            { 10, "none" }, // 未知，暂时不要考虑
            { 11, "none" }, // 保留项
            { 12, "none" }, // 保留项
            { 13, "none" }, // 保留项
            { 14, "none" }, // 保留项
            { 15, "none" }, // 保留项
        };

        // 弹幕标准字体大小
        private readonly int normalFontSize = 25;

        /// <summary>
        /// 获取Bilibili实例
        /// </summary>
        /// <returns></returns>
        public static Bilibili GetInstance()
        {
            if (instance == null)
            {
                instance = new Bilibili();
            }

            return instance;
        }

        /// <summary>
        /// 隐藏Bilibili()方法，必须使用单例模式
        /// </summary>
        private Bilibili() { }

        /// <summary>
        /// 是否屏蔽顶部弹幕
        /// </summary>
        /// <param name="isFilter"></param>
        /// <returns></returns>
        public Bilibili SetTopFilter(bool isFilter)
        {
            config["top_filter"] = isFilter;
            return this;
        }

        /// <summary>
        /// 是否屏蔽底部弹幕
        /// </summary>
        /// <param name="isFilter"></param>
        /// <returns></returns>
        public Bilibili SetBottomFilter(bool isFilter)
        {
            config["bottom_filter"] = isFilter;
            return this;
        }

        /// <summary>
        /// 是否屏蔽滚动弹幕
        /// </summary>
        /// <param name="isFilter"></param>
        /// <returns></returns>
        public Bilibili SetScrollFilter(bool isFilter)
        {
            config["scroll_filter"] = isFilter;
            return this;
        }

        public void Create(long avid, long cid, Config subtitleConfig, string assFile)
        {
            // 弹幕转换
            var biliDanmakus = DanmakuProtobuf.GetAllDanmakuProto(avid, cid);

            // 按弹幕出现顺序排序
            biliDanmakus.Sort((x, y) => { return x.Progress.CompareTo(y.Progress); });

            var danmakus = new List<Danmaku>();
            foreach (var biliDanmaku in biliDanmakus)
            {
                var danmaku = new Danmaku
                {
                    // biliDanmaku.Progress单位是毫秒，所以除以1000，单位变为秒
                    Start = biliDanmaku.Progress / 1000.0f,
                    Style = mapping[biliDanmaku.Mode],
                    Color = (int)biliDanmaku.Color,
                    Commenter = biliDanmaku.MidHash,
                    Content = biliDanmaku.Content,
                    SizeRatio = 1.0f * biliDanmaku.Fontsize / normalFontSize
                };

                danmakus.Add(danmaku);
            }

            // 弹幕预处理
            Producer producer = new Producer(config, danmakus);
            producer.StartHandle();

            // 字幕生成
            var keepedDanmakus = producer.KeepedDanmakus;
            var studio = new Studio(subtitleConfig, keepedDanmakus);
            studio.StartHandle();
            studio.CreateAssFile(assFile);
        }

        public Dictionary<string, int> GetResolution(int quality)
        {
            var resolution = new Dictionary<string, int>
            {
                { "width", 0 },
                { "height", 0 }
            };

            switch (quality)
            {
                // 240P 极速（仅mp4方式）
                case 6:
                    break;
                // 360P 流畅
                case 16:
                    break;
                // 480P 清晰
                case 32:
                    break;
                // 720P 高清（登录）
                case 64:
                    break;
                // 	720P60 高清（大会员）
                case 74:
                    break;
                // 1080P 高清（登录）
                case 80:
                    break;
                // 1080P+ 高清（大会员）
                case 112:
                    break;
                // 1080P60 高清（大会员）
                case 116:
                    break;
                // 4K 超清（大会员）（需要fourk=1）
                case 120:
                    break;
            }
            return resolution;
        }
    }
}
