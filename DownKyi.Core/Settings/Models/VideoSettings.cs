using System.Collections.Generic;

namespace DownKyi.Core.Settings.Models
{
    /// <summary>
    /// 视频
    /// </summary>
    public class VideoSettings
    {
        public VideoCodecs VideoCodecs { get; set; }
        public int Quality { get; set; }
        public int AudioQuality { get; set; }
        public AllowStatus IsTranscodingFlvToMp4 { get; set; }
        public string SaveVideoRootPath { get; set; }
        public List<string> HistoryVideoRootPaths { get; set; }
        public AllowStatus IsUseSaveVideoRootPath { get; set; }
    }
}
