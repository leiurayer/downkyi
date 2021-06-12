using Core.aria2cNet.server;

namespace Core.settings
{
    public partial class Settings
    {
        // 是否开启解除地区限制
        private readonly ALLOW_STATUS isLiftingOfRegion = ALLOW_STATUS.YES;

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
        private readonly AriaConfigFileAllocation ariaFileAllocation = AriaConfigFileAllocation.PREALLOC;

        // Aria HttpProxy代理
        private readonly ALLOW_STATUS isAriaHttpProxy = ALLOW_STATUS.NO;
        private readonly string ariaHttpProxy = "";
        private readonly int ariaHttpProxyListenPort = 0;


        /// <summary>
        /// 获取是否解除地区限制
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS IsLiftingOfRegion()
        {
            if (settingsEntity.IsLiftingOfRegion == 0)
            {
                // 第一次获取，先设置默认值
                IsLiftingOfRegion(isLiftingOfRegion);
                return isLiftingOfRegion;
            }
            return settingsEntity.IsLiftingOfRegion;
        }

        /// <summary>
        /// 设置是否解除地区限制
        /// </summary>
        /// <param name="isLiftingOfRegion"></param>
        /// <returns></returns>
        public bool IsLiftingOfRegion(ALLOW_STATUS isLiftingOfRegion)
        {
            settingsEntity.IsLiftingOfRegion = isLiftingOfRegion;
            return SetEntity();
        }

        /// <summary>
        /// 获取Aria服务器的端口号
        /// </summary>
        /// <returns></returns>
        public int GetAriaListenPort()
        {
            if (settingsEntity.AriaListenPort == 0)
            {
                // 第一次获取，先设置默认值
                SetAriaListenPort(ariaListenPort);
                return ariaListenPort;
            }
            return settingsEntity.AriaListenPort;
        }

        /// <summary>
        /// 设置Aria服务器的端口号
        /// </summary>
        /// <param name="ariaListenPort"></param>
        /// <returns></returns>
        public bool SetAriaListenPort(int ariaListenPort)
        {
            settingsEntity.AriaListenPort = ariaListenPort;
            return SetEntity();
        }

        /// <summary>
        /// 获取Aria日志等级
        /// </summary>
        /// <returns></returns>
        public AriaConfigLogLevel GetAriaLogLevel()
        {
            if (settingsEntity.AriaLogLevel == 0)
            {
                // 第一次获取，先设置默认值
                SetAriaLogLevel(ariaLogLevel);
                return ariaLogLevel;
            }
            return settingsEntity.AriaLogLevel;
        }

        /// <summary>
        /// 设置Aria日志等级
        /// </summary>
        /// <param name="ariaLogLevel"></param>
        /// <returns></returns>
        public bool SetAriaLogLevel(AriaConfigLogLevel ariaLogLevel)
        {
            settingsEntity.AriaLogLevel = ariaLogLevel;
            return SetEntity();
        }

        /// <summary>
        /// 获取Aria最大同时下载数(任务数)
        /// </summary>
        /// <returns></returns>
        public int GetAriaMaxConcurrentDownloads()
        {
            if (settingsEntity.AriaMaxConcurrentDownloads == 0)
            {
                // 第一次获取，先设置默认值
                SetAriaMaxConcurrentDownloads(ariaMaxConcurrentDownloads);
                return ariaMaxConcurrentDownloads;
            }
            return settingsEntity.AriaMaxConcurrentDownloads;
        }

        /// <summary>
        /// 设置Aria最大同时下载数(任务数)
        /// </summary>
        /// <param name="ariaMaxConcurrentDownloads"></param>
        /// <returns></returns>
        public bool SetAriaMaxConcurrentDownloads(int ariaMaxConcurrentDownloads)
        {
            settingsEntity.AriaMaxConcurrentDownloads = ariaMaxConcurrentDownloads;
            return SetEntity();
        }

        /// <summary>
        /// 获取Aria单文件最大线程数
        /// </summary>
        /// <returns></returns>
        public int GetAriaSplit()
        {
            if (settingsEntity.AriaSplit == 0)
            {
                // 第一次获取，先设置默认值
                SetAriaSplit(ariaSplit);
                return ariaSplit;
            }
            return settingsEntity.AriaSplit;
        }

        /// <summary>
        /// 设置Aria单文件最大线程数
        /// </summary>
        /// <param name="ariaSplit"></param>
        /// <returns></returns>
        public bool SetAriaSplit(int ariaSplit)
        {
            settingsEntity.AriaSplit = ariaSplit;
            return SetEntity();
        }

        /// <summary>
        /// 获取Aria下载速度限制
        /// </summary>
        /// <returns></returns>
        public int GetAriaMaxOverallDownloadLimit()
        {
            if (settingsEntity.AriaMaxOverallDownloadLimit == 0)
            {
                // 第一次获取，先设置默认值
                SetAriaMaxOverallDownloadLimit(ariaMaxOverallDownloadLimit);
                return ariaMaxOverallDownloadLimit;
            }
            return settingsEntity.AriaMaxOverallDownloadLimit;
        }

        /// <summary>
        /// 设置Aria下载速度限制
        /// </summary>
        /// <param name="ariaMaxOverallDownloadLimit"></param>
        /// <returns></returns>
        public bool SetAriaMaxOverallDownloadLimit(int ariaMaxOverallDownloadLimit)
        {
            settingsEntity.AriaMaxOverallDownloadLimit = ariaMaxOverallDownloadLimit;
            return SetEntity();
        }

        /// <summary>
        /// 获取Aria下载单文件速度限制
        /// </summary>
        /// <returns></returns>
        public int GetAriaMaxDownloadLimit()
        {
            if (settingsEntity.AriaMaxDownloadLimit == 0)
            {
                // 第一次获取，先设置默认值
                SetAriaMaxDownloadLimit(ariaMaxDownloadLimit);
                return ariaMaxDownloadLimit;
            }
            return settingsEntity.AriaMaxDownloadLimit;
        }

        /// <summary>
        /// 设置Aria下载单文件速度限制
        /// </summary>
        /// <param name="ariaMaxDownloadLimit"></param>
        /// <returns></returns>
        public bool SetAriaMaxDownloadLimit(int ariaMaxDownloadLimit)
        {
            settingsEntity.AriaMaxDownloadLimit = ariaMaxDownloadLimit;
            return SetEntity();
        }

        /// <summary>
        /// 获取Aria文件预分配
        /// </summary>
        /// <returns></returns>
        public AriaConfigFileAllocation GetAriaFileAllocation()
        {
            if (settingsEntity.AriaFileAllocation == 0)
            {
                // 第一次获取，先设置默认值
                SetAriaFileAllocation(ariaFileAllocation);
                return ariaFileAllocation;
            }
            return settingsEntity.AriaFileAllocation;
        }

        /// <summary>
        /// 设置Aria文件预分配
        /// </summary>
        /// <param name="ariaFileAllocation"></param>
        /// <returns></returns>
        public bool SetAriaFileAllocation(AriaConfigFileAllocation ariaFileAllocation)
        {
            settingsEntity.AriaFileAllocation = ariaFileAllocation;
            return SetEntity();
        }

        /// <summary>
        /// 获取是否开启Aria http代理
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS IsAriaHttpProxy()
        {
            if (settingsEntity.IsAriaHttpProxy == 0)
            {
                // 第一次获取，先设置默认值
                IsAriaHttpProxy(isAriaHttpProxy);
                return isAriaHttpProxy;
            }
            return settingsEntity.IsAriaHttpProxy;
        }

        /// <summary>
        /// 设置是否开启Aria http代理
        /// </summary>
        /// <param name="isAriaHttpProxy"></param>
        /// <returns></returns>
        public bool IsAriaHttpProxy(ALLOW_STATUS isAriaHttpProxy)
        {
            settingsEntity.IsAriaHttpProxy = isAriaHttpProxy;
            return SetEntity();
        }

        /// <summary>
        /// 获取Aria的http代理的地址
        /// </summary>
        /// <returns></returns>
        public string GetAriaHttpProxy()
        {
            if (settingsEntity.AriaHttpProxy == null)
            {
                // 第一次获取，先设置默认值
                SetAriaHttpProxy(ariaHttpProxy);
                return ariaHttpProxy;
            }
            return settingsEntity.AriaHttpProxy;
        }

        /// <summary>
        /// 设置Aria的http代理的地址
        /// </summary>
        /// <param name="ariaHttpProxy"></param>
        /// <returns></returns>
        public bool SetAriaHttpProxy(string ariaHttpProxy)
        {
            settingsEntity.AriaHttpProxy = ariaHttpProxy;
            return SetEntity();
        }

        /// <summary>
        /// 获取Aria的http代理的端口
        /// </summary>
        /// <returns></returns>
        public int GetAriaHttpProxyListenPort()
        {
            if (settingsEntity.AriaHttpProxyListenPort == 0)
            {
                // 第一次获取，先设置默认值
                SetAriaHttpProxyListenPort(ariaHttpProxyListenPort);
                return ariaHttpProxyListenPort;
            }
            return settingsEntity.AriaHttpProxyListenPort;
        }

        /// <summary>
        /// 设置Aria的http代理的端口
        /// </summary>
        /// <param name="ariaHttpProxyListenPort"></param>
        /// <returns></returns>
        public bool SetAriaHttpProxyListenPort(int ariaHttpProxyListenPort)
        {
            settingsEntity.AriaHttpProxyListenPort = ariaHttpProxyListenPort;
            return SetEntity();
        }

    }
}
