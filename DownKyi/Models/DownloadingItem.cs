using DownKyi.Core.BiliApi.VideoStream.Models;
using Prism.Mvvm;

namespace DownKyi.Models
{
    public class DownloadingItem : BindableBase
    {
        public PlayUrl PlayUrl { get; set; }

        // 此条下载项的id
        public string Uuid { get; }

        // 文件路径，不包含扩展名，所有内容均以此路径下载
        public string FilePath { get; set; }

        // Aria相关
        public string Gid { get; set; }

        // 视频类别
        public PlayStreamType PlayStreamType { get; set; }

        // 视频的id
        public string Bvid { get; set; }
        public long Avid { get; set; }
        public long Cid { get; set; }
        public long EpisodeId { get; set; }

        // 视频封面的url
        public string CoverUrl { get; set; }

        // 视频序号
        private int order;
        public int Order
        {
            get { return order; }
            set { SetProperty(ref order, value); }
        }

        // 视频主标题
        private string mainTitle;
        public string MainTitle
        {
            get { return mainTitle; }
            set { SetProperty(ref mainTitle, value); }
        }

        // 视频标题
        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        // 音频编码
        public int AudioCodecId { get; set; }
        private string audioCodecName;
        public string AudioCodecName
        {
            get { return audioCodecName; }
            set { SetProperty(ref audioCodecName, value); }
        }

        // 视频编码
        public int VideoCodecId { get; set; }
        private string videoCodecName;
        public string VideoCodecName
        {
            get { return videoCodecName; }
            set { SetProperty(ref videoCodecName, value); }
        }

        // 下载内容（音频、视频、弹幕、字幕、封面）
        private string downloadContent;
        public string DownloadContent
        {
            get { return downloadContent; }
            set { SetProperty(ref downloadContent, value); }
        }

        // 下载状态
        public DownloadStatus DownloadStatus { get; set; }
        private string downloadStatusTitle;
        public string DownloadStatusTitle
        {
            get { return downloadStatusTitle; }
            set { SetProperty(ref downloadStatusTitle, value); }
        }

    }
}
