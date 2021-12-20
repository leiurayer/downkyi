using System.Text.RegularExpressions;

namespace DownKyi.Core.Utils
{
    public static class Format
    {

        /// <summary>
        /// 格式化Duration时间
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static string FormatDuration(long duration)
        {
            string formatDuration;
            if (duration / 60 > 0)
            {
                long dur = duration / 60;
                if (dur / 60 > 0)
                {
                    formatDuration = $"{dur / 60}h{dur % 60}m{duration % 60}s";
                }
                else
                {
                    formatDuration = $"{duration / 60}m{duration % 60}s";
                }
            }
            else
            {
                formatDuration = $"{duration}s";
            }
            return formatDuration;
        }

        /// <summary>
        /// 格式化Duration时间，格式为00:00:00
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static string FormatDuration2(long duration)
        {
            string formatDuration;
            if (duration / 60 > 0)
            {
                long dur = duration / 60;
                if (dur / 60 > 0)
                {
                    formatDuration = string.Format("{0:D2}", dur / 60) + ":" + string.Format("{0:D2}", dur % 60) + ":" + string.Format("{0:D2}", duration % 60);
                }
                else
                {
                    formatDuration = "00:" + string.Format("{0:D2}", duration / 60) + ":" + string.Format("{0:D2}", duration % 60);
                }
            }
            else
            {
                formatDuration = "00:00:" + string.Format("{0:D2}", duration);
            }
            return formatDuration;
        }

        /// <summary>
        /// 格式化Duration时间，格式为00:00
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static string FormatDuration3(long duration)
        {
            string formatDuration;
            if (duration / 60 > 0)
            {
                long dur = duration / 60;
                if (dur / 60 > 0)
                {
                    formatDuration = string.Format("{0:D2}", dur / 60) + ":" + string.Format("{0:D2}", dur % 60) + ":" + string.Format("{0:D2}", duration % 60);
                }
                else
                {
                    formatDuration = string.Format("{0:D2}", duration / 60) + ":" + string.Format("{0:D2}", duration % 60);
                }
            }
            else
            {
                formatDuration = "00:" + string.Format("{0:D2}", duration);
            }
            return formatDuration;
        }

        /// <summary>
        /// 格式化数字，超过10000的数字将单位改为万，超过100000000的数字将单位改为亿，并保留1位小数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string FormatNumber(long number)
        {
            if (number > 99999999)
            {
                return (number / 100000000.0f).ToString("F1") + "亿";
            }

            if (number > 9999)
            {
                return (number / 10000.0f).ToString("F1") + "万";
            }
            else
            {
                return number.ToString();
            }
        }

        /// <summary>
        /// 格式化网速
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static string FormatSpeed(float speed)
        {
            string formatSpeed;
            if (speed <= 0)
            {
                formatSpeed = "0B/s";
            }
            else if (speed < 1024)
            {
                formatSpeed = speed.ToString("#.##") + "B/s";
            }
            else if (speed < 1024 * 1024)
            {
                formatSpeed = (speed / 1024).ToString("#.##") + "KB/s";
            }
            else
            {
                formatSpeed = (speed / 1024 / 1024).ToString("#.##") + "MB/s";
            }
            return formatSpeed;
        }

        /// <summary>
        /// 格式化字节大小，可用于文件大小的显示
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static string FormatFileSize(long fileSize)
        {
            string formatFileSize;
            if (fileSize <= 0)
            {
                formatFileSize = "0B";
            }
            else if (fileSize < 1024)
            {
                formatFileSize = fileSize.ToString() + "B";
            }
            else if (fileSize < 1024 * 1024)
            {
                formatFileSize = (fileSize / 1024.0).ToString("#.##") + "KB";
            }
            else if (fileSize < 1024 * 1024 * 1024)
            {
                formatFileSize = (fileSize / 1024.0 / 1024.0).ToString("#.##") + "MB";
            }
            else
            {
                formatFileSize = (fileSize / 1024.0 / 1024.0 / 1024.0).ToString("#.##") + "GB";
            }
            return formatFileSize;
        }

        /// <summary>
        /// 去除非法字符
        /// </summary>
        /// <param name="originName"></param>
        /// <returns></returns>
        public static string FormatFileName(string originName)
        {
            string destName = originName;
            // Windows中不能作为文件名的字符
            destName = destName.Replace("\\", " ");
            destName = destName.Replace("/", " ");
            destName = destName.Replace(":", " ");
            destName = destName.Replace("*", " ");
            destName = destName.Replace("?", " ");
            destName = destName.Replace("\"", " ");
            destName = destName.Replace("<", " ");
            destName = destName.Replace(">", " ");
            destName = destName.Replace("|", " ");

            // 转义字符
            destName = destName.Replace("\a", "");
            destName = destName.Replace("\b", "");
            destName = destName.Replace("\f", "");
            destName = destName.Replace("\n", "");
            destName = destName.Replace("\r", "");
            destName = destName.Replace("\t", "");
            destName = destName.Replace("\v", "");

            // 控制字符
            destName = Regex.Replace(destName, @"\p{C}+", string.Empty);

            return destName.Trim();
        }

    }
}
