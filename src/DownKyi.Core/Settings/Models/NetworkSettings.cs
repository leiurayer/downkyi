using DownKyi.Core.Aria2cNet.Server;

namespace DownKyi.Core.Settings.Models
{
    /// <summary>
    /// 网络
    /// </summary>
    public class NetworkSettings
    {
        public AllowStatus IsLiftingOfRegion { get; set; }
        public int AriaListenPort { get; set; }
        public AriaConfigLogLevel AriaLogLevel { get; set; }
        public int AriaMaxConcurrentDownloads { get; set; }
        public int AriaSplit { get; set; }
        public int AriaMaxOverallDownloadLimit { get; set; }
        public int AriaMaxDownloadLimit { get; set; }
        public AriaConfigFileAllocation AriaFileAllocation { get; set; }

        public AllowStatus IsAriaHttpProxy { get; set; }
        public string AriaHttpProxy { get; set; }
        public int AriaHttpProxyListenPort { get; set; }
    }
}
