using System;
using System.Text.RegularExpressions;

namespace Core
{
    public static class Common
    {
        // 配置文件所在路径
        public static readonly string ConfigPath = "./Config/";

        // 日志、历史等文件所在路径
        public static readonly string RecordPath = "./Config/";

        /// <summary>
        /// 判断字符串是否以http或https开头，且域名为bilibili.com
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUrl(string input)
        {
            if (input.StartsWith("http://") || input.StartsWith("https://"))
            {
                if (input.Contains("www.bilibili.com")) { return true; }
                else { return false; }
            }
            else { return false; }
        }


        /// <summary>
        /// 判断是否为avid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsAvid(string input)
        {
            if (input.StartsWith("av"))
            {
                bool isInt = Regex.IsMatch(input.Remove(0, 2), @"^\d+$");
                return isInt;
            }
            return false;
        }

        /// <summary>
        /// 判断是否为bvid
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsBvid(string input)
        {
            if (input.StartsWith("BV") && input.Length == 12)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否是用户空间的url
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUserSpaceUrl(string input)
        {
            if (input.StartsWith("http://") || input.StartsWith("https://"))
            {
                if (input.Contains("space.bilibili.com"))
                {
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }

        /// <summary>
        /// 判断是否是用户id
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsUserId(string input)
        {
            string inputLower = input.ToLower();
            if (inputLower.StartsWith("uid:"))
            {
                string uid = inputLower.TrimStart(new char[] { 'u', 'i', 'd', ':' });
                return IsInt(uid);
            }
            else if (inputLower.StartsWith("uid"))
            {
                string uid = inputLower.TrimStart(new char[] { 'u', 'i', 'd' });
                return IsInt(uid);
            }
            else { return false; }
        }

        /// <summary>
        /// 获取用户id
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static long GetUserId(string url)
        {
            string[] strList = url.Split('?');
            string baseUrl = strList[0];

            var match = Regex.Match(baseUrl, @"\d+");
            if (match.Success)
            {
                return GetInt(match.Value);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 从url中解析id，包括bvid和番剧的id
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetVideoId(string url)
        {
            string[] strList = url.Split('?');
            string baseUrl = strList[0];

            string[] str2List = baseUrl.Split('/');
            string id = str2List[str2List.Length - 1];

            if (id == "")
            {
                // 字符串末尾
                return str2List[str2List.Length - 2];
            }

            return id;
        }

        /// <summary>
        /// 判断是视频还是番剧
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static VideoType GetVideoType(string url)
        {
            if (url.ToLower().Contains("/video/bv"))
            {
                return VideoType.VIDEO;
            }

            if (url.ToLower().Contains("/video/av"))
            {
                return VideoType.VIDEO_AV;
            }

            if (url.ToLower().Contains("/bangumi/play/ss"))
            {
                return VideoType.BANGUMI_SEASON;
            }

            if (url.ToLower().Contains("/bangumi/play/ep"))
            {
                return VideoType.BANGUMI_EPISODE;
            }

            if (url.ToLower().Contains("/bangumi/media/md"))
            {
                return VideoType.BANGUMI_MEDIA;
            }

            if (url.ToLower().Contains("/cheese/play/ss"))
            {
                return VideoType.CHEESE_SEASON;
            }

            if (url.ToLower().Contains("/cheese/play/ep"))
            {
                return VideoType.CHEESE_EPISODE;
            }

            return VideoType.NONE;
        }

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

        private static long GetInt(string value)
        {
            if (IsInt(value))
            {
                return long.Parse(value);
            }
            else
            {
                return -1;
            }
        }

        private static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^\d+$");
        }

    }


    /// <summary>
    /// 支持的视频类型
    /// </summary>
    public enum VideoType
    {
        NONE,
        VIDEO, // 对应 /video/BV
        VIDEO_AV, // 对应 /video/av
        // BANGUMI, // BANGUMI细分为以下三个部分
        BANGUMI_SEASON, // 对应 /bangumi/play/ss
        BANGUMI_EPISODE, // 对应 /bangumi/play/ep
        BANGUMI_MEDIA, // 对应 /bangumi/media/md
        CHEESE_SEASON, // 对应 /cheese/play/ss
        CHEESE_EPISODE // 对应 /cheese/play/ep
    }

    /// <summary>
    /// 线程
    /// </summary>
    public enum ThreadStatus
    {
        MAIN_UI, // 主线程
        START_PARSE, // 开始解析url
        ADD_ALL_TO_DOWNLOAD,
        START_DOWNLOAD
    }

    /// <summary>
    /// 视频的编码格式，flv也视为编码放这里
    /// </summary>
    [Serializable]
    public enum VideoCodec
    {
        NONE = 1,
        AVC,
        HEVC,
        FLV
    }

}
