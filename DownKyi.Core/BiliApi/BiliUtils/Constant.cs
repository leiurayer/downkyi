using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.BiliUtils
{
    public static class Constant
    {
        private static readonly List<Quality> resolutions = new List<Quality>
        {
            new Quality { Name = "超高清 8K", Id = 127 },
            new Quality { Name = "杜比视界", Id = 126 },
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

        private static readonly List<Quality> codecIds = new List<Quality>
        {
            new Quality { Name = "H.264/AVC", Id = 7 },
            new Quality { Name = "H.265/HEVC", Id = 12 },
            new Quality { Name = "AV1", Id = 13 },
        };

        private static readonly List<Quality> qualities = new List<Quality>
        {
            new Quality { Name = "64K", Id = 30216 },
            new Quality { Name = "132K", Id = 30232 },
            new Quality { Name = "192K", Id = 30280 },
            new Quality { Name = "Dolby Atmos", Id = 30250 },
            new Quality { Name = "Hi-Res无损", Id = 30251 },
        };

        /// <summary>
        /// 获取支持的视频画质
        /// </summary>
        /// <returns></returns>
        public static List<Quality> GetResolutions()
        {
            // 使用深复制，
            // 保证外部修改list后，
            // 不会影响其他调用处
            return new List<Quality>(resolutions);
        }

        /// <summary>
        /// 获取视频编码代码
        /// </summary>
        /// <returns></returns>
        public static List<Quality> GetCodecIds()
        {
            // 使用深复制，
            // 保证外部修改list后，
            // 不会影响其他调用处
            return new List<Quality>(codecIds);
        }

        /// <summary>
        /// 获取支持的视频音质
        /// </summary>
        /// <returns></returns>
        public static List<Quality> GetAudioQualities()
        {
            // 使用深复制，
            // 保证外部修改list后，
            // 不会影响其他调用处
            return new List<Quality>(qualities);
        }

    }
}
