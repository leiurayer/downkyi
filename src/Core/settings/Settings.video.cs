namespace Core.settings
{
    public partial class Settings
    {
        // 设置优先下载的视频编码
        private readonly VideoCodecs videoCodecs = VideoCodecs.AVC;

        // 设置优先下载画质
        private readonly int quality = 120;

        // 是否在下载的视频前增加序号
        private readonly ALLOW_STATUS isAddOrder = ALLOW_STATUS.NO;

        // 是否下载flv视频后转码为mp4
        private readonly ALLOW_STATUS isTranscodingFlvToMp4 = ALLOW_STATUS.YES;

        // 默认下载目录
        private readonly string saveVideoRootPath = "./Media";

        // 是否使用默认下载目录，如果是，则每次点击下载选中项时不再询问下载目录
        private readonly ALLOW_STATUS isUseSaveVideoRootPath = ALLOW_STATUS.NO;

        // 是否为不同视频分别创建文件夹
        private readonly ALLOW_STATUS isCreateFolderForMedia = ALLOW_STATUS.YES;

        // 是否在下载视频的同时下载弹幕
        private readonly ALLOW_STATUS isDownloadDanmaku = ALLOW_STATUS.YES;

        // 是否在下载视频的同时下载封面
        private readonly ALLOW_STATUS isDownloadCover = ALLOW_STATUS.YES;


        /// <summary>
        /// 获取优先下载的视频编码
        /// </summary>
        /// <returns></returns>
        public VideoCodecs GetVideoCodecs()
        {
            if (settingsEntity.VideoCodecs == 0)
            {
                // 第一次获取，先设置默认值
                SetVideoCodecs(videoCodecs);
                return videoCodecs;
            }
            return settingsEntity.VideoCodecs;
        }

        /// <summary>
        /// 设置优先下载的视频编码
        /// </summary>
        /// <param name="videoCodecs"></param>
        /// <returns></returns>
        public bool SetVideoCodecs(VideoCodecs videoCodecs)
        {
            settingsEntity.VideoCodecs = videoCodecs;
            return SetEntity();
        }

        /// <summary>
        /// 获取优先下载画质
        /// </summary>
        /// <returns></returns>
        public int GetQuality()
        {
            if (settingsEntity.Quality == 0)
            {
                // 第一次获取，先设置默认值
                SetQuality(quality);
                return quality;
            }
            return settingsEntity.Quality;
        }

        /// <summary>
        /// 设置优先下载画质
        /// </summary>
        /// <param name="quality"></param>
        /// <returns></returns>
        public bool SetQuality(int quality)
        {
            settingsEntity.Quality = quality;
            return SetEntity();
        }

        /// <summary>
        /// 获取是否给视频增加序号
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS IsAddOrder()
        {
            if (settingsEntity.IsAddOrder == 0)
            {
                // 第一次获取，先设置默认值
                IsAddOrder(isAddOrder);
                return isAddOrder;
            }
            return settingsEntity.IsAddOrder;
        }

        /// <summary>
        /// 设置是否给视频增加序号
        /// </summary>
        /// <param name="isAddOrder"></param>
        /// <returns></returns>
        public bool IsAddOrder(ALLOW_STATUS isAddOrder)
        {
            settingsEntity.IsAddOrder = isAddOrder;
            return SetEntity();
        }

        /// <summary>
        /// 获取是否下载flv视频后转码为mp4
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS IsTranscodingFlvToMp4()
        {
            if (settingsEntity.IsTranscodingFlvToMp4 == 0)
            {
                // 第一次获取，先设置默认值
                IsTranscodingFlvToMp4(isTranscodingFlvToMp4);
                return isTranscodingFlvToMp4;
            }
            return settingsEntity.IsTranscodingFlvToMp4;
        }

        /// <summary>
        /// 设置是否下载flv视频后转码为mp4
        /// </summary>
        /// <param name="isTranscodingFlvToMp4"></param>
        /// <returns></returns>
        public bool IsTranscodingFlvToMp4(ALLOW_STATUS isTranscodingFlvToMp4)
        {
            settingsEntity.IsTranscodingFlvToMp4 = isTranscodingFlvToMp4;
            return SetEntity();
        }

        /// <summary>
        /// 获取下载目录
        /// </summary>
        /// <returns></returns>
        public string GetSaveVideoRootPath()
        {
            if (settingsEntity.SaveVideoRootPath == null)
            {
                // 第一次获取，先设置默认值
                SetSaveVideoRootPath(saveVideoRootPath);
                return saveVideoRootPath;
            }
            return settingsEntity.SaveVideoRootPath;
        }

        /// <summary>
        /// 设置下载目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool SetSaveVideoRootPath(string path)
        {
            settingsEntity.SaveVideoRootPath = path;
            return SetEntity();
        }

        /// <summary>
        /// 获取是否使用默认下载目录
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS IsUseSaveVideoRootPath()
        {
            if (settingsEntity.IsUseSaveVideoRootPath == 0)
            {
                // 第一次获取，先设置默认值
                IsUseSaveVideoRootPath(isUseSaveVideoRootPath);
                return isUseSaveVideoRootPath;
            }
            return settingsEntity.IsUseSaveVideoRootPath;
        }

        /// <summary>
        /// 设置是否使用默认下载目录
        /// </summary>
        /// <param name="isUseSaveVideoRootPath"></param>
        /// <returns></returns>
        public bool IsUseSaveVideoRootPath(ALLOW_STATUS isUseSaveVideoRootPath)
        {
            settingsEntity.IsUseSaveVideoRootPath = isUseSaveVideoRootPath;
            return SetEntity();
        }

        /// <summary>
        /// 获取是否为不同视频分别创建文件夹
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS IsCreateFolderForMedia()
        {
            if (settingsEntity.IsCreateFolderForMedia == 0)
            {
                // 第一次获取，先设置默认值
                IsCreateFolderForMedia(isCreateFolderForMedia);
                return isCreateFolderForMedia;
            }
            return settingsEntity.IsCreateFolderForMedia;
        }

        /// <summary>
        /// 设置是否为不同视频分别创建文件夹
        /// </summary>
        /// <param name="isCreateFolderForMedia"></param>
        /// <returns></returns>
        public bool IsCreateFolderForMedia(ALLOW_STATUS isCreateFolderForMedia)
        {
            settingsEntity.IsCreateFolderForMedia = isCreateFolderForMedia;
            return SetEntity();
        }

        /// <summary>
        /// 获取是否在下载视频的同时下载弹幕
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS IsDownloadDanmaku()
        {
            if (settingsEntity.IsDownloadDanmaku == 0)
            {
                // 第一次获取，先设置默认值
                IsDownloadDanmaku(isDownloadDanmaku);
                return isDownloadDanmaku;
            }
            return settingsEntity.IsDownloadDanmaku;
        }

        /// <summary>
        /// 设置是否在下载视频的同时下载弹幕
        /// </summary>
        /// <param name="isDownloadDanmaku"></param>
        /// <returns></returns>
        public bool IsDownloadDanmaku(ALLOW_STATUS isDownloadDanmaku)
        {
            settingsEntity.IsDownloadDanmaku = isDownloadDanmaku;
            return SetEntity();
        }

        /// <summary>
        /// 获取是否在下载视频的同时下载封面
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS IsDownloadCover()
        {
            if (settingsEntity.IsDownloadCover == 0)
            {
                // 第一次获取，先设置默认值
                IsDownloadCover(isDownloadCover);
                return isDownloadCover;
            }
            return settingsEntity.IsDownloadCover;
        }

        /// <summary>
        /// 设置是否在下载视频的同时下载封面
        /// </summary>
        /// <param name="isDownloadCover"></param>
        /// <returns></returns>
        public bool IsDownloadCover(ALLOW_STATUS isDownloadCover)
        {
            settingsEntity.IsDownloadCover = isDownloadCover;
            return SetEntity();
        }






    }
}
