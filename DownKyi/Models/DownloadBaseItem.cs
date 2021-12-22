using DownKyi.Core.BiliApi.BiliUtils;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace DownKyi.Models
{
    public class DownloadBaseItem : BindableBase
    {
        public DownloadBaseItem()
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

        private DrawingImage zoneImage;
        public DrawingImage ZoneImage
        {
            get => zoneImage;
            set => SetProperty(ref zoneImage, value);
        }

        // 视频序号
        private int order;
        public int Order
        {
            get => order;
            set => SetProperty(ref order, value);
        }

        // 视频主标题
        private string mainTitle;
        public string MainTitle
        {
            get => mainTitle;
            set => SetProperty(ref mainTitle, value);
        }

        // 视频标题
        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        // 时长
        private string duration;
        public string Duration
        {
            get => duration;
            set => SetProperty(ref duration, value);
        }

        // 视频编码
        // "hev1.2.4.L156.90"
        // "avc1.640034"
        //public string VideoCodecId { get; set; }

        // 视频编码名称，AVC、HEVC
        private string videoCodecName;
        public string VideoCodecName
        {
            get => videoCodecName;
            set => SetProperty(ref videoCodecName, value);
        }

        // 视频画质
        private Quality resolution;
        public Quality Resolution
        {
            get => resolution;
            set => SetProperty(ref resolution, value);
        }

        // 音频编码
        private Quality audioCodec;
        public Quality AudioCodec
        {
            get => audioCodec;
            set => SetProperty(ref audioCodec, value);
        }

        // 文件路径，不包含扩展名，所有内容均以此路径下载
        public string FilePath { get; set; }

        // 文件大小
        private string fileSize;
        public string FileSize
        {
            get => fileSize;
            set => SetProperty(ref fileSize, value);
        }

    }
}
