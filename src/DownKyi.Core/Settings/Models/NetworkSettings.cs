using DownKyi.Core.Aria2cNet.Server;

namespace DownKyi.Core.Settings.Models
{
    /// <summary>
    /// 网络
    /// </summary>
    public class NetworkSettings
    {
        public AllowStatus IsLiftingOfRegion { get; set; } = AllowStatus.NONE;

        public Downloader Downloader { get; set; } = Downloader.NOT_SET;
        public int MaxCurrentDownloads { get; set; } = -1;

        #region built-in
        public int Split { get; set; } = -1;
        public AllowStatus IsHttpProxy { get; set; } = AllowStatus.NONE;
        public string HttpProxy { get; set; } = null;
        public int HttpProxyListenPort { get; set; } = -1;
        #endregion

        #region Aria
        public int AriaListenPort { get; set; } = -1;
        public AriaConfigLogLevel AriaLogLevel { get; set; } = AriaConfigLogLevel.NOT_SET;
        public int AriaSplit { get; set; } = -1;
        public int AriaMaxOverallDownloadLimit { get; set; } = -1;
        public int AriaMaxDownloadLimit { get; set; } = -1;
        public AriaConfigFileAllocation AriaFileAllocation { get; set; } = AriaConfigFileAllocation.NOT_SET;

        public AllowStatus IsAriaHttpProxy { get; set; } = AllowStatus.NONE;
        public string AriaHttpProxy { get; set; } = null;
        public int AriaHttpProxyListenPort { get; set; } = -1;
        #endregion
    }
}
