using DownKyi.Core.BiliApi.VideoStream;
using System;
using System.Collections.Generic;

namespace DownKyi.Models
{
    [Serializable]
    public class Downloading// : DownloadBase
    {
        public Downloading() : base()
        {
            // 初始化下载的文件列表
            DownloadFiles = new Dictionary<string, string>();
        }

        // Aria相关
        public string Gid { get; set; }

        // 下载的文件
        public Dictionary<string, string> DownloadFiles { get; set; }

        // 视频类别
        public PlayStreamType PlayStreamType { get; set; }

        // 下载状态
        public DownloadStatus DownloadStatus { get; set; }

        // 正在下载内容（音频、视频、弹幕、字幕、封面）
        public string DownloadContent { get; set; }

        // 下载状态显示
        public string DownloadStatusTitle { get; set; }

        // 下载进度
        public float Progress { get; set; }

        //  已下载大小/文件大小
        public string DownloadingFileSize { get; set; }

        // 下载的最高速度
        public long MaxSpeed { get; set; }

        //  下载速度
        public string SpeedDisplay { get; set; }

    }
}
