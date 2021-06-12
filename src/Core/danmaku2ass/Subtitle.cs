using System;
using System.Collections.Generic;

namespace Core.danmaku2ass
{
    /// <summary>
    /// 字幕
    /// </summary>
    public class Subtitle
    {
        public Danmaku Danmaku;
        public Display Display;
        public float Offset;

        public float Start;
        public float End;
        public string Color;
        public Dictionary<string, int> Position;
        public string StartMarkup;
        public string EndMarkup;
        public string ColorMarkup;
        public string BorderMarkup;
        public string FontSizeMarkup;
        public string StyleMarkup;
        public string LayerMarkup;
        public string ContentMarkup;
        public string Text;

        public Subtitle(Danmaku danmaku, Display display, float offset = 0)
        {
            Danmaku = danmaku;
            Display = display;
            Offset = offset;

            Start = SetStart();
            End = SetEnd();
            Color = SetColor();
            Position = SetPosition();
            StartMarkup = SetStartMarkup();
            EndMarkup = SetEndMarkup();
            ColorMarkup = SetColorMarkup();
            BorderMarkup = SetBorderMarkup();
            FontSizeMarkup = SetFontSizeMarkup();
            StyleMarkup = SetStyleMarkup();
            LayerMarkup = SetLayerMarkup();
            ContentMarkup = SetContentMarkup();
            Text = SetText();
        }

        protected float SetStart()
        {
            return Danmaku.Start + Offset;
        }

        protected float SetEnd()
        {
            return Start + Display.Duration;
        }

        protected string SetColor()
        {
            return Utils.Int2bgr(Danmaku.Color);
        }

        protected Dictionary<string, int> SetPosition()
        {
            Tuple<int, int> x = Display.Horizontal;
            Tuple<int, int> y = Display.Vertical;

            Dictionary<string, int> value = new Dictionary<string, int>
            {
                { "x1", x.Item1 },
                { "x2", x.Item2 },
                { "y1", y.Item1 },
                { "y2", y.Item2 }
            };
            return value;
        }

        protected string SetStartMarkup()
        {
            return Utils.Second2hms(Start);
        }

        protected string SetEndMarkup()
        {
            return Utils.Second2hms(End);
        }

        protected string SetColorMarkup()
        {
            // 白色不需要加特别标记
            if (Color == "FFFFFF")
            {
                return "";
            }
            return "\\c&H" + Color;
        }

        protected string SetBorderMarkup()
        {
            // 暗色加个亮色边框，方便阅读
            if (Utils.IsDark(Danmaku.Color))
            {
                //return "\\3c&HFFFFFF";
                return "\\3c&H000000";
            }
            else
            {
                return "\\3c&H000000";
            }
            //return "";
        }

        protected string SetFontSizeMarkup()
        {
            if (Display.IsScaled)
            {
                return $"\\fs{Display.FontSize}";
            }
            return "";
        }

        protected string SetStyleMarkup()
        {
            if (Danmaku.Style == "scroll")
            {
                return $"\\move({Position["x1"]}, {Position["y1"]}, {Position["x2"]}, {Position["y2"]})";
            }
            return $"\\a6\\pos({Position["x1"]}, {Position["y1"]})";
        }

        protected string SetLayerMarkup()
        {
            if (Danmaku.Style != "scroll")
            {
                return "-2";
            }
            return "-1";
        }

        protected string SetContentMarkup()
        {
            string markup = StyleMarkup + ColorMarkup + BorderMarkup + FontSizeMarkup;
            string content = Utils.CorrectTypos(Danmaku.Content);
            return $"{{{markup}}}{content}";
        }

        protected string SetText()
        {
            return $"Dialogue: {LayerMarkup},{StartMarkup},{EndMarkup},Danmaku,,0000,0000,0000,,{ContentMarkup}";
        }

    }
}
