using DownKyi.Core.Aria2cNet.Server;

namespace DownKyi.Core.Settings
{
    public partial class SettingsManager
    {
        // 是否开启解除地区限制
        private readonly AllowStatus isLiftingOfRegion = AllowStatus.YES;

        // Aria服务器端口号
        private readonly int ariaListenPort = 6800;

        // Aria日志等级
        private readonly AriaConfigLogLevel ariaLogLevel = AriaConfigLogLevel.INFO;

        // Aria最大同时下载数(任务数)
        private readonly int ariaMaxConcurrentDownloads = 3;

        // Aria单文件最大线程数
        private readonly int ariaSplit = 5;

        // Aria下载速度限制
        private readonly int ariaMaxOverallDownloadLimit = 0;

        // Aria下载单文件速度限制
        private readonly int ariaMaxDownloadLimit = 0;

        // Aria文件预分配
        private readonly AriaConfigFileAllocation ariaFileAllocation = AriaConfigFileAllocation.NONE;

        // Aria HttpProxy代理
        private readonly AllowStatus isAriaHttpProxy = AllowStatus.NO;
        private readonly string ariaHttpProxy = "";
        private readonly int ariaHttpProxyListenPort = 0;

        /// <summary>
        /// 获取是否解除地区限制
        /// </summary>
        /// <returns></returns>
        public AllowStatus IsLiftingOfRegion()
        {
            appSettings = GetSettings();
            if (appSettings.Network.IsLiftingOfRegion == AllowStatus.NONE)
            {
                // 第一次获取，先设置默认值
                IsLiftingOfRegion(isLiftingOfRegion);
                return isLiftingOfRegion;
            }
            return appSettings.Network.IsLiftingOfRegion;
        }

        /// <summary>
        /// 设置是否解除地区限制
        /// </summary>
        /// <param name="isLiftingOfRegion"></param>
        /// <returns></returns>
        public bool IsLiftingOfRegion(AllowStatus isLiftingOfRegion)
        {
            appSettings.Network.IsLiftingOfRegion = isLiftingOfRegion;
            return SetSettings();
        }

        /// <summary>
        /// 获取Aria服务器的端口号
        /// </summary>
        /// <returns></returns>
        public int GetAriaListenPort()
        {
            appSettings = GetSettings();
            if (appSettings.Network.AriaListenPort == -1)
            {
                // 第一次获取，先设置默认值
                SetAriaListenPort(ariaListenPort);
                return ariaListenPort;
            }
            return appSettings.Network.AriaListenPort;
        }

        /// <summary>
        /// 设置Aria服务器的端口号
        /// </summary>
        /// <param name="ariaListenPort"></param>
        /// <returns></returns>
        public bool SetAriaListenPort(int ariaListenPort)
        {
            appSettings.Network.AriaListenPort = ariaListenPort;
            return SetSettings();
        }

        /// <summary>
        /// 获取Aria日志等级
        /// </summary>
        /// <returns></returns>
        public AriaConfigLogLevel GetAriaLogLevel()
        {
            appSettings = GetSettings();
            if (appSettings.Network.AriaLogLevel == AriaConfigLogLevel.NOT_SET)
            {
                // 第一次获取，先设置默认值
                SetAriaLogLevel(ariaLogLevel);
                return ariaLogLevel;
            }
            return appSettings.Network.AriaLogLevel;
        }

        /// <summary>
        /// 设置Aria日志等级
        /// </summary>
        /// <param name="ariaLogLevel"></param>
        /// <returns></returns>
        public bool SetAriaLogLevel(AriaConfigLogLevel ariaLogLevel)
        {
            appSettings.Network.AriaLogLevel = ariaLogLevel;
            return SetSettings();
        }

        /// <summary>
        /// 获取Aria最大同时下载数(任务数)
        /// </summary>
        /// <returns></returns>
        public int GetAriaMaxConcurrentDownloads()
        {
            appSettings = GetSettings();
            if (appSettings.Network.AriaMaxConcurrentDownloads == -1)
            {
                // 第一次获取，先设置默认值
                SetAriaMaxConcurrentDownloads(ariaMaxConcurrentDownloads);
                return ariaMaxConcurrentDownloads;
            }
            return appSettings.Network.AriaMaxConcurrentDownloads;
        }

        /// <summary>
        /// 设置Aria最大同时下载数(任务数)
        /// </summary>
        /// <param name="ariaMaxConcurrentDownloads"></param>
        /// <returns></returns>
        public bool SetAriaMaxConcurrentDownloads(int ariaMaxConcurrentDownloads)
        {
            appSettings.Network.AriaMaxConcurrentDownloads = ariaMaxConcurrentDownloads;
            return SetSettings();
        }

        /// <summary>
        /// 获取Aria单文件最大线程数
        /// </summary>
        /// <returns></returns>
        public int GetAriaSplit()
        {
            appSettings = GetSettings();
            if (appSettings.Network.AriaSplit == -1)
            {
                // 第一次获取，先设置默认值
                SetAriaSplit(ariaSplit);
                return ariaSplit;
            }
            return appSettings.Network.AriaSplit;
        }

        /// <summary>
        /// 设置Aria单文件最大线程数
        /// </summary>
        /// <param name="ariaSplit"></param>
        /// <returns></returns>
        public bool SetAriaSplit(int ariaSplit)
        {
            appSettings.Network.AriaSplit = ariaSplit;
            return SetSettings();
        }

        /// <summary>
        /// 获取Aria下载速度限制
        /// </summary>
        /// <returns></returns>
        public int GetAriaMaxOverallDownloadLimit()
        {
            appSettings = GetSettings();
            if (appSettings.Network.AriaMaxOverallDownloadLimit == -1)
            {
                // 第一次获取，先设置默认值
                SetAriaMaxOverallDownloadLimit(ariaMaxOverallDownloadLimit);
                return ariaMaxOverallDownloadLimit;
            }
            return appSettings.Network.AriaMaxOverallDownloadLimit;
        }

        /// <summary>
        /// 设置Aria下载速度限制
        /// </summary>
        /// <param name="ariaMaxOverallDownloadLimit"></param>
        /// <returns></returns>
        public bool SetAriaMaxOverallDownloadLimit(int ariaMaxOverallDownloadLimit)
        {
            appSettings.Network.AriaMaxOverallDownloadLimit = ariaMaxOverallDownloadLimit;
            return SetSettings();
        }

        /// <summary>
        /// 获取Aria下载单文件速度限制
        /// </summary>
        /// <returns></returns>
        public int GetAriaMaxDownloadLimit()
        {
            appSettings = GetSettings();
            if (appSettings.Network.AriaMaxDownloadLimit == -1)
            {
                // 第一次获取，先设置默认值
                SetAriaMaxDownloadLimit(ariaMaxDownloadLimit);
                return ariaMaxDownloadLimit;
            }
            return appSettings.Network.AriaMaxDownloadLimit;
        }

        /// <summary>
        /// 设置Aria下载单文件速度限制
        /// </summary>
        /// <param name="ariaMaxDownloadLimit"></param>
        /// <returns></returns>
        public bool SetAriaMaxDownloadLimit(int ariaMaxDownloadLimit)
        {
            appSettings.Network.AriaMaxDownloadLimit = ariaMaxDownloadLimit;
            return SetSettings();
        }

        /// <summary>
        /// 获取Aria文件预分配
        /// </summary>
        /// <returns></returns>
        public AriaConfigFileAllocation GetAriaFileAllocation()
        {
            appSettings = GetSettings();
            if (appSettings.Network.AriaFileAllocation == AriaConfigFileAllocation.NOT_SET)
            {
                // 第一次获取，先设置默认值
                SetAriaFileAllocation(ariaFileAllocation);
                return ariaFileAllocation;
            }
            return appSettings.Network.AriaFileAllocation;
        }

        /// <summary>
        /// 设置Aria文件预分配
        /// </summary>
        /// <param name="ariaFileAllocation"></param>
        /// <returns></returns>
        public bool SetAriaFileAllocation(AriaConfigFileAllocation ariaFileAllocation)
        {
            appSettings.Network.AriaFileAllocation = ariaFileAllocation;
            return SetSettings();
        }

        /// <summary>
        /// 获取是否开启Aria http代理
        /// </summary>
        /// <returns></returns>
        public AllowStatus IsAriaHttpProxy()
        {
            appSettings = GetSettings();
            if (appSettings.Network.IsAriaHttpProxy == AllowStatus.NONE)
            {
                // 第一次获取，先设置默认值
                IsAriaHttpProxy(isAriaHttpProxy);
                return isAriaHttpProxy;
            }
            return appSettings.Network.IsAriaHttpProxy;
        }

        /// <summary>
        /// 设置是否开启Aria http代理
        /// </summary>
        /// <param name="isAriaHttpProxy"></param>
        /// <returns></returns>
        public bool IsAriaHttpProxy(AllowStatus isAriaHttpProxy)
        {
            appSettings.Network.IsAriaHttpProxy = isAriaHttpProxy;
            return SetSettings();
        }

        /// <summary>
        /// 获取Aria的http代理的地址
        /// </summary>
        /// <returns></returns>
        public string GetAriaHttpProxy()
        {
            appSettings = GetSettings();
            if (appSettings.Network.AriaHttpProxy == null)
            {
                // 第一次获取，先设置默认值
                SetAriaHttpProxy(ariaHttpProxy);
                return ariaHttpProxy;
            }
            return appSettings.Network.AriaHttpProxy;
        }

        /// <summary>
        /// 设置Aria的http代理的地址
        /// </summary>
        /// <param name="ariaHttpProxy"></param>
        /// <returns></returns>
        public bool SetAriaHttpProxy(string ariaHttpProxy)
        {
            appSettings.Network.AriaHttpProxy = ariaHttpProxy;
            return SetSettings();
        }

        /// <summary>
        /// 获取Aria的http代理的端口
        /// </summary>
        /// <returns></returns>
        public int GetAriaHttpProxyListenPort()
        {
            appSettings = GetSettings();
            if (appSettings.Network.AriaHttpProxyListenPort == -1)
            {
                // 第一次获取，先设置默认值
                SetAriaHttpProxyListenPort(ariaHttpProxyListenPort);
                return ariaHttpProxyListenPort;
            }
            return appSettings.Network.AriaHttpProxyListenPort;
        }

        /// <summary>
        /// 设置Aria的http代理的端口
        /// </summary>
        /// <param name="ariaHttpProxyListenPort"></param>
        /// <returns></returns>
        public bool SetAriaHttpProxyListenPort(int ariaHttpProxyListenPort)
        {
            appSettings.Network.AriaHttpProxyListenPort = ariaHttpProxyListenPort;
            return SetSettings();
        }

    }
}
