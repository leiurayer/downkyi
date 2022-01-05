using DownKyi.Core.BiliApi.BiliUtils;
using System;
using System.Collections.Generic;

namespace DownKyi.Models
{
    [Serializable]
    public class DownloadBase
    {
        public DownloadBase()
        {
            // 唯一id
            Uuid = Guid.NewGuid().ToString("N");

            // 初始化需要下载的内容
            NeedDownloadContent = new Dictionary<string, bool>
            {
                { "downloadAudio", true },
                { "downloadVideo", true },
                { "downloadDanmaku", true },
                { "downloadSubtitle", true },
                { "downloadCover", true }
            };
        }

        // 此条下载项的id
        public string Uuid { get; }

        // 需要下载的内容
        public Dictionary<string, bool> NeedDownloadContent { get; private set; }

        // 视频的id
        public string Bvid { get; set; }
        public long Avid { get; set; }
        public long Cid { get; set; }
        public long EpisodeId { get; set; }

        // 视频封面的url
        public string CoverUrl { get; set; }

        // 视频page的封面的url
        public string PageCoverUrl { get; set; }

        // 分区id
        public int ZoneId { get; set; }

        // 视频序号
        public int Order { get; set; }

        // 视频主标题
        public string MainTitle { get; set; }

        // 视频标题
        public string Name { get; set; }

        // 时长
        public string Duration { get; set; }

        // 视频编码名称，AVC、HEVC
        public string VideoCodecName { get; set; }

        // 视频画质
        public Quality Resolution { get; set; }

        // 音频编码
        public Quality AudioCodec { get; set; }

        // 文件路径，不包含扩展名，所有内容均以此路径下载
        public string FilePath { get; set; }

        // 文件大小
        public string FileSize { get; set; }

    }
}
