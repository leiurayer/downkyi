using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.BiliUtils
{
    public static class Constant
    {
        /// <summary>
        /// 音质id及含义
        /// </summary>
        public static Dictionary<int, string> AudioQuality { get; } = new Dictionary<int, string>()
        {
            { 30216, "64K" },
            { 30232, "132K" },
            { 30280, "192K" }
        };

        /// <summary>
        /// 音质id及含义
        /// </summary>
        public static Dictionary<string, int> AudioQualityId { get; } = new Dictionary<string, int>()
        {
            { "64K", 30216 },
            { "132K", 30232 },
            { "192K", 30280 }
        };

    }
}
