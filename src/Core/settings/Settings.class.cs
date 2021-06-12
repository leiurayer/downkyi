using Core.aria2cNet.server;

namespace Core.settings
{
    public class SettingsEntity
    {
        public UserInfoForSetting UserInfo { get; set; }

        // 基本
        public AfterDownloadOperation AfterDownload { get; set; }
        public ALLOW_STATUS IsListenClipboard { get; set; }
        public ALLOW_STATUS IsAutoParseVideo { get; set; }
        public DownloadFinishedSort DownloadFinishedSort { get; set; }

        // 网络
        public ALLOW_STATUS IsLiftingOfRegion { get; set; }
        public int AriaListenPort { get; set; }
        public AriaConfigLogLevel AriaLogLevel { get; set; }
        public int AriaMaxConcurrentDownloads { get; set; }
        public int AriaSplit { get; set; }
        public int AriaMaxOverallDownloadLimit { get; set; }
        public int AriaMaxDownloadLimit { get; set; }
        public AriaConfigFileAllocation AriaFileAllocation { get; set; }

        public ALLOW_STATUS IsAriaHttpProxy { get; set; }
        public string AriaHttpProxy { get; set; }
        public int AriaHttpProxyListenPort { get; set; }

        // 视频
        public VideoCodecs VideoCodecs { get; set; }
        public int Quality { get; set; }
        public ALLOW_STATUS IsAddOrder { get; set; }
        public ALLOW_STATUS IsTranscodingFlvToMp4 { get; set; }
        public string SaveVideoRootPath { get; set; }
        public ALLOW_STATUS IsUseSaveVideoRootPath { get; set; }
        public ALLOW_STATUS IsCreateFolderForMedia { get; set; }
        public ALLOW_STATUS IsDownloadDanmaku { get; set; }
        public ALLOW_STATUS IsDownloadCover { get; set; }

        // 弹幕
        public ALLOW_STATUS DanmakuTopFilter { get; set; }
        public ALLOW_STATUS DanmakuBottomFilter { get; set; }
        public ALLOW_STATUS DanmakuScrollFilter { get; set; }
        public ALLOW_STATUS IsCustomDanmakuResolution { get; set; }
        public int DanmakuScreenWidth { get; set; }
        public int DanmakuScreenHeight { get; set; }
        public string DanmakuFontName { get; set; }
        public int DanmakuFontSize { get; set; }
        public int DanmakuLineCount { get; set; }
        public DanmakuLayoutAlgorithm DanmakuLayoutAlgorithm { get; set; }

        // 关于
        public ALLOW_STATUS IsReceiveBetaVersion { get; set; }
        public ALLOW_STATUS AutoUpdateWhenLaunch { get; set; }
    }

    public class UserInfoForSetting
    {
        public long Mid { get; set; }
        public string Name { get; set; }
        public bool IsLogin { get; set; } // 是否登录
        public bool IsVip { get; set; } // 是否为大会员，未登录时为false
    }

    public enum AfterDownloadOperation
    {
        NONE = 1, OPEN_FOLDER, CLOSE_APP, CLOSE_SYSTEM
    }

    public enum ALLOW_STATUS
    {
        NONE = 0, YES, NO
    }

    public enum DownloadFinishedSort
    {
        DOWNLOAD = 0, NUMBER
    }

    public enum VideoCodecs
    {
        NONE = 0, AVC, HEVC
    }

    public enum DanmakuLayoutAlgorithm
    {
        NONE = 0, ASYNC, SYNC
    }

}
