using DownKyi.Core.FileName;
using System.Collections.Generic;

namespace DownKyi.Core.Settings.Models
{
    /// <summary>
    /// 视频
    /// </summary>
    public class VideoSettings
    {
        public VideoCodecs VideoCodecs { get; set; } // AVC or HEVC
        public int Quality { get; set; } // 画质
        public int AudioQuality { get; set; } // 音质
        public AllowStatus IsTranscodingFlvToMp4 { get; set; } // 是否将flv转为mp4
        public string SaveVideoRootPath { get; set; } // 视频保存路径
        public List<string> HistoryVideoRootPaths { get; set; } // 历史视频保存路径
        public AllowStatus IsUseSaveVideoRootPath { get; set; } // 是否使用默认视频保存路径
        public List<FileNamePart> FileNameParts { get; set; } // 文件命名格式
    }
}
