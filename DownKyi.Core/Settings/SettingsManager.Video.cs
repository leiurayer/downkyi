using System;
using System.Collections.Generic;
using System.IO;

namespace DownKyi.Core.Settings
{
    public partial class SettingsManager
    {
        // 设置优先下载的视频编码
        private readonly VideoCodecs videoCodecs = VideoCodecs.AVC;

        // 设置优先下载画质
        private readonly int quality = 120;

        // 设置优先下载音质
        private readonly int audioQuality = 30280;

        // 是否在下载的视频前增加序号
        private readonly AllowStatus isAddOrder = AllowStatus.NO;

        // 是否下载flv视频后转码为mp4
        private readonly AllowStatus isTranscodingFlvToMp4 = AllowStatus.YES;

        // 默认下载目录
        private readonly string saveVideoRootPath = Path.Combine(Environment.CurrentDirectory, "Media");

        // 历史下载目录
        private readonly List<string> historyVideoRootPaths = new List<string>();

        // 是否使用默认下载目录，如果是，则每次点击下载选中项时不再询问下载目录
        private readonly AllowStatus isUseSaveVideoRootPath = AllowStatus.NO;

        // 是否为不同视频分别创建文件夹
        private readonly AllowStatus isCreateFolderForMedia = AllowStatus.YES;

        // 是否在下载视频的同时下载弹幕
        private readonly AllowStatus isDownloadDanmaku = AllowStatus.YES;

        // 是否在下载视频的同时下载封面
        private readonly AllowStatus isDownloadCover = AllowStatus.YES;

        /// <summary>
        /// 获取优先下载的视频编码
        /// </summary>
        /// <returns></returns>
        public VideoCodecs GetVideoCodecs()
        {
            appSettings = GetSettings();
            if (appSettings.Video.VideoCodecs == 0)
            {
                // 第一次获取，先设置默认值
                SetVideoCodecs(videoCodecs);
                return videoCodecs;
            }
            return appSettings.Video.VideoCodecs;
        }

        /// <summary>
        /// 设置优先下载的视频编码
        /// </summary>
        /// <param name="videoCodecs"></param>
        /// <returns></returns>
        public bool SetVideoCodecs(VideoCodecs videoCodecs)
        {
            appSettings.Video.VideoCodecs = videoCodecs;
            return SetSettings();
        }

        /// <summary>
        /// 获取优先下载画质
        /// </summary>
        /// <returns></returns>
        public int GetQuality()
        {
            appSettings = GetSettings();
            if (appSettings.Video.Quality == 0)
            {
                // 第一次获取，先设置默认值
                SetQuality(quality);
                return quality;
            }
            return appSettings.Video.Quality;
        }

        /// <summary>
        /// 设置优先下载画质
        /// </summary>
        /// <param name="quality"></param>
        /// <returns></returns>
        public bool SetQuality(int quality)
        {
            appSettings.Video.Quality = quality;
            return SetSettings();
        }

        /// <summary>
        /// 获取优先下载音质
        /// </summary>
        /// <returns></returns>
        public int GetAudioQuality()
        {
            appSettings = GetSettings();
            if (appSettings.Video.AudioQuality == 0)
            {
                // 第一次获取，先设置默认值
                SetAudioQuality(audioQuality);
                return audioQuality;
            }
            return appSettings.Video.AudioQuality;
        }

        /// <summary>
        /// 设置优先下载音质
        /// </summary>
        /// <param name="quality"></param>
        /// <returns></returns>
        public bool SetAudioQuality(int quality)
        {
            appSettings.Video.AudioQuality = quality;
            return SetSettings();
        }

        /// <summary>
        /// 获取是否给视频增加序号
        /// </summary>
        /// <returns></returns>
        public AllowStatus IsAddOrder()
        {
            appSettings = GetSettings();
            if (appSettings.Video.IsAddOrder == 0)
            {
                // 第一次获取，先设置默认值
                IsAddOrder(isAddOrder);
                return isAddOrder;
            }
            return appSettings.Video.IsAddOrder;
        }

        /// <summary>
        /// 设置是否给视频增加序号
        /// </summary>
        /// <param name="isAddOrder"></param>
        /// <returns></returns>
        public bool IsAddOrder(AllowStatus isAddOrder)
        {
            appSettings.Video.IsAddOrder = isAddOrder;
            return SetSettings();
        }

        /// <summary>
        /// 获取是否下载flv视频后转码为mp4
        /// </summary>
        /// <returns></returns>
        public AllowStatus IsTranscodingFlvToMp4()
        {
            appSettings = GetSettings();
            if (appSettings.Video.IsTranscodingFlvToMp4 == 0)
            {
                // 第一次获取，先设置默认值
                IsTranscodingFlvToMp4(isTranscodingFlvToMp4);
                return isTranscodingFlvToMp4;
            }
            return appSettings.Video.IsTranscodingFlvToMp4;
        }

        /// <summary>
        /// 设置是否下载flv视频后转码为mp4
        /// </summary>
        /// <param name="isTranscodingFlvToMp4"></param>
        /// <returns></returns>
        public bool IsTranscodingFlvToMp4(AllowStatus isTranscodingFlvToMp4)
        {
            appSettings.Video.IsTranscodingFlvToMp4 = isTranscodingFlvToMp4;
            return SetSettings();
        }

        /// <summary>
        /// 获取下载目录
        /// </summary>
        /// <returns></returns>
        public string GetSaveVideoRootPath()
        {
            appSettings = GetSettings();
            if (appSettings.Video.SaveVideoRootPath == null)
            {
                // 第一次获取，先设置默认值
                SetSaveVideoRootPath(saveVideoRootPath);
                return saveVideoRootPath;
            }
            return appSettings.Video.SaveVideoRootPath;
        }

        /// <summary>
        /// 设置下载目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool SetSaveVideoRootPath(string path)
        {
            appSettings.Video.SaveVideoRootPath = path;
            return SetSettings();
        }

        /// <summary>
        /// 获取历史下载目录
        /// </summary>
        /// <returns></returns>
        public List<string> GetHistoryVideoRootPaths()
        {
            appSettings = GetSettings();
            if (appSettings.Video.HistoryVideoRootPaths == null)
            {
                // 第一次获取，先设置默认值
                SetHistoryVideoRootPaths(historyVideoRootPaths);
                return historyVideoRootPaths;
            }
            return appSettings.Video.HistoryVideoRootPaths;
        }

        /// <summary>
        /// 设置历史下载目录
        /// </summary>
        /// <param name="historyPaths"></param>
        /// <returns></returns>
        public bool SetHistoryVideoRootPaths(List<string> historyPaths)
        {
            appSettings.Video.HistoryVideoRootPaths = historyPaths;
            return SetSettings();
        }

        /// <summary>
        /// 获取是否使用默认下载目录
        /// </summary>
        /// <returns></returns>
        public AllowStatus IsUseSaveVideoRootPath()
        {
            appSettings = GetSettings();
            if (appSettings.Video.IsUseSaveVideoRootPath == 0)
            {
                // 第一次获取，先设置默认值
                IsUseSaveVideoRootPath(isUseSaveVideoRootPath);
                return isUseSaveVideoRootPath;
            }
            return appSettings.Video.IsUseSaveVideoRootPath;
        }

        /// <summary>
        /// 设置是否使用默认下载目录
        /// </summary>
        /// <param name="isUseSaveVideoRootPath"></param>
        /// <returns></returns>
        public bool IsUseSaveVideoRootPath(AllowStatus isUseSaveVideoRootPath)
        {
            appSettings.Video.IsUseSaveVideoRootPath = isUseSaveVideoRootPath;
            return SetSettings();
        }

        /// <summary>
        /// 获取是否为不同视频分别创建文件夹
        /// </summary>
        /// <returns></returns>
        public AllowStatus IsCreateFolderForMedia()
        {
            appSettings = GetSettings();
            if (appSettings.Video.IsCreateFolderForMedia == 0)
            {
                // 第一次获取，先设置默认值
                IsCreateFolderForMedia(isCreateFolderForMedia);
                return isCreateFolderForMedia;
            }
            return appSettings.Video.IsCreateFolderForMedia;
        }

        /// <summary>
        /// 设置是否为不同视频分别创建文件夹
        /// </summary>
        /// <param name="isCreateFolderForMedia"></param>
        /// <returns></returns>
        public bool IsCreateFolderForMedia(AllowStatus isCreateFolderForMedia)
        {
            appSettings.Video.IsCreateFolderForMedia = isCreateFolderForMedia;
            return SetSettings();
        }

        /// <summary>
        /// 获取是否在下载视频的同时下载弹幕
        /// </summary>
        /// <returns></returns>
        public AllowStatus IsDownloadDanmaku()
        {
            appSettings = GetSettings();
            if (appSettings.Video.IsDownloadDanmaku == 0)
            {
                // 第一次获取，先设置默认值
                IsDownloadDanmaku(isDownloadDanmaku);
                return isDownloadDanmaku;
            }
            return appSettings.Video.IsDownloadDanmaku;
        }

        /// <summary>
        /// 设置是否在下载视频的同时下载弹幕
        /// </summary>
        /// <param name="isDownloadDanmaku"></param>
        /// <returns></returns>
        public bool IsDownloadDanmaku(AllowStatus isDownloadDanmaku)
        {
            appSettings.Video.IsDownloadDanmaku = isDownloadDanmaku;
            return SetSettings();
        }

        /// <summary>
        /// 获取是否在下载视频的同时下载封面
        /// </summary>
        /// <returns></returns>
        public AllowStatus IsDownloadCover()
        {
            appSettings = GetSettings();
            if (appSettings.Video.IsDownloadCover == 0)
            {
                // 第一次获取，先设置默认值
                IsDownloadCover(isDownloadCover);
                return isDownloadCover;
            }
            return appSettings.Video.IsDownloadCover;
        }

        /// <summary>
        /// 设置是否在下载视频的同时下载封面
        /// </summary>
        /// <param name="isDownloadCover"></param>
        /// <returns></returns>
        public bool IsDownloadCover(AllowStatus isDownloadCover)
        {
            appSettings.Video.IsDownloadCover = isDownloadCover;
            return SetSettings();
        }

    }
}
