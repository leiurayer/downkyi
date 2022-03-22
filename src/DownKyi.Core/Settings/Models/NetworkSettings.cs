using DownKyi.Core.Aria2cNet.Server;

namespace DownKyi.Core.Settings.Models
{
    /// <summary>
    /// 网络
    /// </summary>
    public class NetworkSettings
    {
        public AllowStatus IsLiftingOfRegion { get; set; } = AllowStatus.NONE;
        public int AriaListenPort { get; set; } = -1;
        public AriaConfigLogLevel AriaLogLevel { get; set; } = AriaConfigLogLevel.NOT_SET;
        public int AriaMaxConcurrentDownloads { get; set; } = -1;
        public int AriaSplit { get; set; } = -1;
        public int AriaMaxOverallDownloadLimit { get; set; } = -1;
        public int AriaMaxDownloadLimit { get; set; } = -1;
        public AriaConfigFileAllocation AriaFileAllocation { get; set; } = AriaConfigFileAllocation.NOT_SET;

        public AllowStatus IsAriaHttpProxy { get; set; } = AllowStatus.NONE;
        public string AriaHttpProxy { get; set; } = null;
        public int AriaHttpProxyListenPort { get; set; } = -1;
    }
}
