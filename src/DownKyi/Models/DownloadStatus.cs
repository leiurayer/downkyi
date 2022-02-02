namespace DownKyi.Models
{
    public enum DownloadStatus
    {
        NOT_STARTED,       // 未开始，从未开始下载
        WAIT_FOR_DOWNLOAD, // 等待下载，下载过，但是启动本次下载周期未开始，如重启程序后未开始
        PAUSE_STARTED,     // 暂停启动下载
        PAUSE,             // 暂停
        //PAUSE_TO_WAIT,     // 暂停后等待
        DOWNLOADING,       // 下载中
        DOWNLOAD_SUCCEED,  // 下载成功
        DOWNLOAD_FAILED,   // 下载失败
    }
}
