using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.BiliUtils
{
    public static class Constant
    {
        private static readonly List<Quality> resolutions = new List<Quality>
        {
            new Quality { Name = "HDR 真彩", Id = 125 },
            new Quality { Name = "4K 超清", Id = 120 },
            new Quality { Name = "1080P 60帧", Id = 116 },
            new Quality { Name = "1080P 高码率", Id = 112 },
            new Quality { Name = "1080P 高清", Id = 80 },
            new Quality { Name = "720P 60帧", Id = 74 },
            new Quality { Name = "720P 高清", Id = 64 },
            new Quality { Name = "480P 清晰", Id = 32 },
            new Quality { Name = "360P 流畅", Id = 16 },
        };

        private static readonly List<Quality> qualities = new List<Quality>
        {
            new Quality { Name = "64K", Id = 30216 },
            new Quality { Name = "132K", Id = 30232 },
            new Quality { Name = "192K", Id = 30280 },
        };

        /// <summary>
        /// 获取支持的视频画质
        /// </summary>
        /// <returns></returns>
        public static List<Quality> GetResolutions()
        {
            return resolutions;
        }

        /// <summary>
        /// 获取支持的视频音质
        /// </summary>
        /// <returns></returns>
        public static List<Quality> GetAudioQualities()
        {
            return qualities;
        }

    }
}
