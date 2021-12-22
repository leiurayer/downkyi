using System;
using System.Linq;
using System.Text;

namespace DownKyi.Core.Danmaku2Ass
{
    internal static class Utils
    {
        /// <summary>
        /// 向上取整，返回int类型
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int IntCeiling(float number)
        {
            return (int)Math.Ceiling(number);
        }

        /// <summary>
        /// 字符长度，1个汉字当2个英文
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int DisplayLength(string text)
        {
            return Encoding.Default.GetBytes(text).Length;
        }

        /// <summary>
        /// 修正一些评论者的拼写错误
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string CorrectTypos(string text)
        {
            text = text.Replace("/n", "\\N");
            text = text.Replace("&gt;", ">");
            text = text.Replace("&lt;", "<");
            return text;
        }

        /// <summary>
        /// 秒数转 时:分:秒 格式
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string Second2hms(float seconds)
        {
            if (seconds < 0)
            {
                return "0:00:00.00";
            }

            int i = (int)Math.Floor(seconds / 1.0);
            int dec = (int)(Math.Round(seconds % 1.0f, 2) * 100);
            if (dec >= 100)
            {
                dec = 99;
            }

            int min = (int)Math.Floor(i / 60.0);
            int second = (int)(i % 60.0f);

            int hour = (int)Math.Floor(min / 60.0);
            min = (int)Math.Floor(min % 60.0f);

            return $"{hour:D}:{min:D2}:{second:D2}.{dec:D2}";
        }

        /// <summary>
        /// 时:分:秒 格式转 秒数
        /// </summary>
        /// <param name="hms"></param>
        /// <returns></returns>
        public static float Hms2second(string hms)
        {
            string[] numbers = hms.Split(':');
            float seconds = 0;

            for (int i = 0; i < numbers.Length; i++)
            {
                seconds += (float)(float.Parse(numbers[numbers.Length - i - 1]) * Math.Pow(60, i));
            }
            return seconds;
        }

        /// <summary>
        /// 同Hms2second(string hms)，不过可以用 +/- 符号来连接多个
        /// 即 3:00-2:30 相当于 30 秒
        /// </summary>
        /// <param name="xhms"></param>
        /// <returns></returns>
        public static float Xhms2second(string xhms)
        {
            string[] args = xhms.Replace("+", " +").Replace("-", " -").Split(' ');
            float result = 0;
            foreach (string hms in args)
            {
                result += Hms2second(hms);
            }
            return result;
        }

        /// <summary>
        /// 颜色值，整型转 RGB
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        public static string Int2rgb(int integer)
        {
            return integer.ToString("X").PadLeft(6, '0'); ;
        }

        /// <summary>
        /// 颜色值，整型转 BGR
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        public static string Int2bgr(int integer)
        {
            string rgb = Int2rgb(integer);
            string bgr = rgb.Substring(4, 2) + rgb.Substring(2, 2) + rgb.Substring(0, 2);
            return bgr;
        }

        /// <summary>
        /// 颜色值，整型转 HLS
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        public static float[] Int2hls(int integer)
        {
            string rgb = Int2rgb(integer);
            int[] rgb_decimals = { 0, 0, 0 };
            rgb_decimals[0] = int.Parse(rgb.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            rgb_decimals[1] = int.Parse(rgb.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            rgb_decimals[2] = int.Parse(rgb.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            int[] rgb_coordinates = { 0, 0, 0 };
            rgb_coordinates[0] = (int)Math.Floor(rgb_decimals[0] / 255.0);
            rgb_coordinates[1] = (int)Math.Floor(rgb_decimals[1] / 255.0);
            rgb_coordinates[2] = (int)Math.Floor(rgb_decimals[2] / 255.0);
            float[] hls_corrdinates = Rgb2hls(rgb_coordinates);

            float[] hls = { 0, 0, 0 };
            hls[0] = hls_corrdinates[0] * 360;
            hls[1] = hls_corrdinates[1] * 100;
            hls[2] = hls_corrdinates[2] * 100;
            return hls;
        }

        /// <summary>
        /// HLS: Hue, Luminance, Saturation
        /// H: position in the spectrum
        /// L: color lightness
        /// S: color saturation
        /// </summary>
        /// <param name="rgb"></param>
        /// <returns></returns>
        private static float[] Rgb2hls(int[] rgb)
        {
            float[] hls = { 0, 0, 0 };
            int maxc = rgb.Max();
            int minc = rgb.Min();
            hls[1] = (minc + maxc) / 2.0f;
            if (minc == maxc)
            {
                return hls;
            }

            if (hls[1] <= 0.5)
            {
                hls[2] = (maxc - minc) / (maxc + minc);
            }
            else
            {
                hls[2] = (maxc - minc) / (2.0f - maxc - minc);
            }
            float rc = (maxc - rgb[0]) / (maxc - minc);
            float gc = (maxc - rgb[1]) / (maxc - minc);
            float bc = (maxc - rgb[2]) / (maxc - minc);
            if (rgb[0] == maxc)
            {
                hls[0] = bc - gc;
            }
            else if (rgb[1] == maxc)
            {
                hls[0] = 2.0f + rc - bc;
            }
            else
            {
                hls[0] = 4.0f + gc - rc;
            }
            hls[0] = (hls[0] / 6.0f) % 1.0f;
            return hls;
        }

        /// <summary>
        /// 是否属于暗色
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        public static bool IsDark(int integer)
        {
            if (integer == 0)
            {
                return true;
            }

            float[] hls = Int2hls(integer);
            float hue = hls[0];
            float lightness = hls[1];

            // HSL 色轮见
            // http://zh.wikipedia.org/zh-cn/HSL和HSV色彩空间
            // 以下的数值都是我的主观判断认为是暗色
            if ((hue > 30 && hue < 210) && lightness < 33)
            {
                return true;
            }
            if ((hue < 30 || hue > 210) && lightness < 66)
            {
                return true;
            }
            return false;
        }
    }
}
