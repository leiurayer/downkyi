namespace Core.settings
{
    public partial class Settings
    {
        // 默认下载完成后的操作
        private readonly AfterDownloadOperation afterDownload = AfterDownloadOperation.NONE;

        // 是否监听剪贴板
        private readonly ALLOW_STATUS isListenClipboard = ALLOW_STATUS.YES;

        // 视频详情页面是否自动解析
        private readonly ALLOW_STATUS isAutoParseVideo = ALLOW_STATUS.NO;

        // 下载完成列表排序
        private readonly DownloadFinishedSort finishedSort = DownloadFinishedSort.DOWNLOAD;


        /// <summary>
        /// 获取下载完成后的操作
        /// </summary>
        /// <returns></returns>
        public AfterDownloadOperation GetAfterDownloadOperation()
        {
            if (settingsEntity.AfterDownload == 0)
            {
                // 第一次获取，先设置默认值
                SetAfterDownloadOperation(afterDownload);
                return afterDownload;
            }
            return settingsEntity.AfterDownload;
        }

        /// <summary>
        /// 设置下载完成后的操作
        /// </summary>
        /// <param name="afterDownload"></param>
        /// <returns></returns>
        public bool SetAfterDownloadOperation(AfterDownloadOperation afterDownload)
        {
            settingsEntity.AfterDownload = afterDownload;
            return SetEntity();
        }

        /// <summary>
        /// 是否监听剪贴板
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS IsListenClipboard()
        {
            if (settingsEntity.IsListenClipboard == 0)
            {
                // 第一次获取，先设置默认值
                IsListenClipboard(isListenClipboard);
                return isListenClipboard;
            }
            return settingsEntity.IsListenClipboard;
        }

        /// <summary>
        /// 是否监听剪贴板
        /// </summary>
        /// <param name="isListen"></param>
        /// <returns></returns>
        public bool IsListenClipboard(ALLOW_STATUS isListen)
        {
            settingsEntity.IsListenClipboard = isListen;
            return SetEntity();
        }

        /// <summary>
        /// 视频详情页面是否自动解析
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS IsAutoParseVideo()
        {
            if (settingsEntity.IsAutoParseVideo == 0)
            {
                // 第一次获取，先设置默认值
                IsAutoParseVideo(isAutoParseVideo);
                return isAutoParseVideo;
            }
            return settingsEntity.IsAutoParseVideo;
        }

        /// <summary>
        /// 视频详情页面是否自动解析
        /// </summary>
        /// <param name="IsAuto"></param>
        /// <returns></returns>
        public bool IsAutoParseVideo(ALLOW_STATUS IsAuto)
        {
            settingsEntity.IsAutoParseVideo = IsAuto;
            return SetEntity();
        }

        /// <summary>
        /// 获取下载完成列表排序
        /// </summary>
        /// <returns></returns>
        public DownloadFinishedSort GetDownloadFinishedSort()
        {
            if (settingsEntity.DownloadFinishedSort == 0)
            {
                // 第一次获取，先设置默认值
                SetDownloadFinishedSort(finishedSort);
                return finishedSort;
            }
            return settingsEntity.DownloadFinishedSort;
        }

        /// <summary>
        /// 设置下载完成列表排序
        /// </summary>
        /// <param name="finishedSort"></param>
        /// <returns></returns>
        public bool SetDownloadFinishedSort(DownloadFinishedSort finishedSort)
        {
            settingsEntity.DownloadFinishedSort = finishedSort;
            return SetEntity();
        }

    }
}
