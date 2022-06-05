using DownKyi.Core.Aria2cNet.Server;
using DownKyi.Core.Settings;
using DownKyi.Core.Utils.Validator;
using DownKyi.Events;
using DownKyi.Services;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.Generic;

namespace DownKyi.ViewModels.Settings
{
    public class ViewNetworkViewModel : BaseViewModel
    {
        public const string Tag = "PageSettingsNetwork";

        private bool isOnNavigatedTo;

        #region 页面属性申明

        private bool useSSL;
        public bool UseSSL
        {
            get => useSSL;
            set => SetProperty(ref useSSL, value);
        }

        private bool builtin;
        public bool Builtin
        {
            get => builtin;
            set => SetProperty(ref builtin, value);
        }

        private bool aria2c;
        public bool Aria2c
        {
            get => aria2c;
            set => SetProperty(ref aria2c, value);
        }

        private List<int> maxCurrentDownloads;
        public List<int> MaxCurrentDownloads
        {
            get => maxCurrentDownloads;
            set => SetProperty(ref maxCurrentDownloads, value);
        }

        private int selectedMaxCurrentDownload;
        public int SelectedMaxCurrentDownload
        {
            get => selectedMaxCurrentDownload;
            set => SetProperty(ref selectedMaxCurrentDownload, value);
        }

        private List<int> splits;
        public List<int> Splits
        {
            get => splits;
            set => SetProperty(ref splits, value);
        }

        private int selectedSplit;
        public int SelectedSplit
        {
            get => selectedSplit;
            set => SetProperty(ref selectedSplit, value);
        }

        private bool isHttpProxy;
        public bool IsHttpProxy
        {
            get => isHttpProxy;
            set => SetProperty(ref isHttpProxy, value);
        }

        private string httpProxy;
        public string HttpProxy
        {
            get => httpProxy;
            set => SetProperty(ref httpProxy, value);
        }

        private int httpProxyPort;
        public int HttpProxyPort
        {
            get => httpProxyPort;
            set => SetProperty(ref httpProxyPort, value);
        }

        private int ariaListenPort;
        public int AriaListenPort
        {
            get => ariaListenPort;
            set => SetProperty(ref ariaListenPort, value);
        }

        private List<string> ariaLogLevels;
        public List<string> AriaLogLevels
        {
            get => ariaLogLevels;
            set => SetProperty(ref ariaLogLevels, value);
        }

        private string selectedAriaLogLevel;
        public string SelectedAriaLogLevel
        {
            get => selectedAriaLogLevel;
            set => SetProperty(ref selectedAriaLogLevel, value);
        }

        private List<int> ariaMaxConcurrentDownloads;
        public List<int> AriaMaxConcurrentDownloads
        {
            get => ariaMaxConcurrentDownloads;
            set => SetProperty(ref ariaMaxConcurrentDownloads, value);
        }

        private int selectedAriaMaxConcurrentDownload;
        public int SelectedAriaMaxConcurrentDownload
        {
            get => selectedAriaMaxConcurrentDownload;
            set => SetProperty(ref selectedAriaMaxConcurrentDownload, value);
        }

        private List<int> ariaSplits;
        public List<int> AriaSplits
        {
            get => ariaSplits;
            set => SetProperty(ref ariaSplits, value);
        }

        private int selectedAriaSplit;
        public int SelectedAriaSplit
        {
            get => selectedAriaSplit;
            set => SetProperty(ref selectedAriaSplit, value);
        }

        private int ariaMaxOverallDownloadLimit;
        public int AriaMaxOverallDownloadLimit
        {
            get => ariaMaxOverallDownloadLimit;
            set => SetProperty(ref ariaMaxOverallDownloadLimit, value);
        }

        private int ariaMaxDownloadLimit;
        public int AriaMaxDownloadLimit
        {
            get => ariaMaxDownloadLimit;
            set => SetProperty(ref ariaMaxDownloadLimit, value);
        }

        private bool isAriaHttpProxy;
        public bool IsAriaHttpProxy
        {
            get => isAriaHttpProxy;
            set => SetProperty(ref isAriaHttpProxy, value);
        }

        private string ariaHttpProxy;
        public string AriaHttpProxy
        {
            get => ariaHttpProxy;
            set => SetProperty(ref ariaHttpProxy, value);
        }

        private int ariaHttpProxyPort;
        public int AriaHttpProxyPort
        {
            get => ariaHttpProxyPort;
            set => SetProperty(ref ariaHttpProxyPort, value);
        }

        private List<string> ariaFileAllocations;
        public List<string> AriaFileAllocations
        {
            get => ariaFileAllocations;
            set => SetProperty(ref ariaFileAllocations, value);
        }

        private string selectedAriaFileAllocation;
        public string SelectedAriaFileAllocation
        {
            get => selectedAriaFileAllocation;
            set => SetProperty(ref selectedAriaFileAllocation, value);
        }

        #endregion

        public ViewNetworkViewModel(IEventAggregator eventAggregator, IDialogService dialogService) : base(eventAggregator, dialogService)
        {

            #region 属性初始化

            // builtin同时下载数
            MaxCurrentDownloads = new List<int>();
            for (int i = 1; i <= 10; i++) { MaxCurrentDownloads.Add(i); }

            // builtin最大线程数
            Splits = new List<int>();
            for (int i = 1; i <= 10; i++) { Splits.Add(i); }

            // Aria的日志等级
            AriaLogLevels = new List<string>
            {
                "DEBUG",
                "INFO",
                "NOTICE",
                "WARN",
                "ERROR"
            };

            // Aria同时下载数
            AriaMaxConcurrentDownloads = new List<int>();
            for (int i = 1; i <= 10; i++) { AriaMaxConcurrentDownloads.Add(i); }

            // Aria最大线程数
            AriaSplits = new List<int>();
            for (int i = 1; i <= 10; i++) { AriaSplits.Add(i); }

            // Aria文件预分配
            AriaFileAllocations = new List<string>
            {
                "NONE",
                "PREALLOC",
                "FALLOC"
            };

            #endregion

        }

        /// <summary>
        /// 导航到页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            isOnNavigatedTo = true;

            // 启用https
            AllowStatus useSSL = SettingsManager.GetInstance().UseSSL();
            UseSSL = useSSL == AllowStatus.YES;

            // 选择下载器
            var downloader = SettingsManager.GetInstance().GetDownloader();
            switch (downloader)
            {
                case Downloader.NOT_SET:
                    break;
                case Downloader.BUILT_IN:
                    Builtin = true;
                    break;
                case Downloader.ARIA:
                    Aria2c = true;
                    break;
            }

            // builtin同时下载数
            SelectedMaxCurrentDownload = SettingsManager.GetInstance().GetMaxCurrentDownloads();

            // builtin最大线程数
            SelectedSplit = SettingsManager.GetInstance().GetSplit();

            // 是否开启builtin http代理
            AllowStatus isHttpProxy = SettingsManager.GetInstance().IsHttpProxy();
            IsHttpProxy = isHttpProxy == AllowStatus.YES;

            // builtin的http代理的地址
            HttpProxy = SettingsManager.GetInstance().GetHttpProxy();

            // builtin的http代理的端口
            HttpProxyPort = SettingsManager.GetInstance().GetHttpProxyListenPort();

            // Aria服务器端口
            AriaListenPort = SettingsManager.GetInstance().GetAriaListenPort();

            // Aria的日志等级
            AriaConfigLogLevel ariaLogLevel = SettingsManager.GetInstance().GetAriaLogLevel();
            SelectedAriaLogLevel = ariaLogLevel.ToString("G");

            // Aria同时下载数
            SelectedAriaMaxConcurrentDownload = SettingsManager.GetInstance().GetMaxCurrentDownloads();

            // Aria最大线程数
            SelectedAriaSplit = SettingsManager.GetInstance().GetAriaSplit();

            // Aria下载速度限制
            AriaMaxOverallDownloadLimit = SettingsManager.GetInstance().GetAriaMaxOverallDownloadLimit();

            // Aria下载单文件速度限制
            AriaMaxDownloadLimit = SettingsManager.GetInstance().GetAriaMaxDownloadLimit();

            // 是否开启Aria http代理
            AllowStatus isAriaHttpProxy = SettingsManager.GetInstance().IsAriaHttpProxy();
            IsAriaHttpProxy = isAriaHttpProxy == AllowStatus.YES;

            // Aria的http代理的地址
            AriaHttpProxy = SettingsManager.GetInstance().GetAriaHttpProxy();

            // Aria的http代理的端口
            AriaHttpProxyPort = SettingsManager.GetInstance().GetAriaHttpProxyListenPort();

            // Aria文件预分配
            AriaConfigFileAllocation ariaFileAllocation = SettingsManager.GetInstance().GetAriaFileAllocation();
            SelectedAriaFileAllocation = ariaFileAllocation.ToString("G");

            isOnNavigatedTo = false;
        }

        #region 命令申明

        // 是否启用https事件
        private DelegateCommand useSSLCommand;
        public DelegateCommand UseSSLCommand => useSSLCommand ?? (useSSLCommand = new DelegateCommand(ExecuteUseSSLCommand));

        /// <summary>
        /// 是否启用https事件
        /// </summary>
        private void ExecuteUseSSLCommand()
        {
            AllowStatus useSSL = UseSSL ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().UseSSL(useSSL);
            PublishTip(isSucceed);
        }

        // 下载器选择事件
        private DelegateCommand<string> selectDownloaderCommand;
        public DelegateCommand<string> SelectDownloaderCommand => selectDownloaderCommand ?? (selectDownloaderCommand = new DelegateCommand<string>(ExecuteSelectDownloaderCommand));

        /// <summary>
        /// 下载器选择事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteSelectDownloaderCommand(string parameter)
        {
            Downloader downloader;
            switch (parameter)
            {
                case "Builtin":
                    downloader = Downloader.BUILT_IN;
                    break;
                case "Aria2c":
                    downloader = Downloader.ARIA;
                    break;
                default:
                    downloader = SettingsManager.GetInstance().GetDownloader();
                    break;
            }

            bool isSucceed = SettingsManager.GetInstance().SetDownloader(downloader);
            PublishTip(isSucceed);

            AlertService alertService = new AlertService(dialogService);
            ButtonResult result = alertService.ShowInfo(DictionaryResource.GetString("ConfirmReboot"));
            if (result == ButtonResult.OK)
            {
                System.Windows.Application.Current.Shutdown();
                System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        // builtin同时下载数事件
        private DelegateCommand<object> maxCurrentDownloadsCommand;
        public DelegateCommand<object> MaxCurrentDownloadsCommand => maxCurrentDownloadsCommand ?? (maxCurrentDownloadsCommand = new DelegateCommand<object>(ExecuteMaxCurrentDownloadsCommand));

        /// <summary>
        /// builtin同时下载数事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteMaxCurrentDownloadsCommand(object parameter)
        {
            SelectedMaxCurrentDownload = (int)parameter;

            bool isSucceed = SettingsManager.GetInstance().SetMaxCurrentDownloads(SelectedMaxCurrentDownload);
            PublishTip(isSucceed);
        }

        // builtin最大线程数事件
        private DelegateCommand<object> splitsCommand;
        public DelegateCommand<object> SplitsCommand => splitsCommand ?? (splitsCommand = new DelegateCommand<object>(ExecuteSplitsCommand));

        /// <summary>
        /// builtin最大线程数事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteSplitsCommand(object parameter)
        {
            SelectedSplit = (int)parameter;

            bool isSucceed = SettingsManager.GetInstance().SetSplit(SelectedSplit);
            PublishTip(isSucceed);
        }

        // 是否开启builtin http代理事件
        private DelegateCommand isHttpProxyCommand;
        public DelegateCommand IsHttpProxyCommand => isHttpProxyCommand ?? (isHttpProxyCommand = new DelegateCommand(ExecuteIsHttpProxyCommand));

        /// <summary>
        /// 是否开启builtin http代理事件
        /// </summary>
        private void ExecuteIsHttpProxyCommand()
        {
            AllowStatus isHttpProxy = IsHttpProxy ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().IsHttpProxy(isHttpProxy);
            PublishTip(isSucceed);
        }

        // builtin的http代理的地址事件
        private DelegateCommand<string> httpProxyCommand;
        public DelegateCommand<string> HttpProxyCommand => httpProxyCommand ?? (httpProxyCommand = new DelegateCommand<string>(ExecuteHttpProxyCommand));

        /// <summary>
        /// builtin的http代理的地址事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteHttpProxyCommand(string parameter)
        {
            bool isSucceed = SettingsManager.GetInstance().SetHttpProxy(parameter);
            PublishTip(isSucceed);
        }

        // builtin的http代理的端口事件
        private DelegateCommand<string> httpProxyPortCommand;
        public DelegateCommand<string> HttpProxyPortCommand => httpProxyPortCommand ?? (httpProxyPortCommand = new DelegateCommand<string>(ExecuteHttpProxyPortCommand));

        /// <summary>
        /// builtin的http代理的端口事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteHttpProxyPortCommand(string parameter)
        {
            int httpProxyPort = (int)Number.GetInt(parameter);
            HttpProxyPort = httpProxyPort;

            bool isSucceed = SettingsManager.GetInstance().SetHttpProxyListenPort(HttpProxyPort);
            PublishTip(isSucceed);
        }

        // Aria服务器端口事件
        private DelegateCommand<string> ariaListenPortCommand;
        public DelegateCommand<string> AriaListenPortCommand => ariaListenPortCommand ?? (ariaListenPortCommand = new DelegateCommand<string>(ExecuteAriaListenPortCommand));

        /// <summary>
        /// Aria服务器端口事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteAriaListenPortCommand(string parameter)
        {
            int listenPort = (int)Number.GetInt(parameter);
            AriaListenPort = listenPort;

            bool isSucceed = SettingsManager.GetInstance().SetAriaListenPort(AriaListenPort);
            PublishTip(isSucceed);
        }

        // Aria的日志等级事件
        private DelegateCommand<string> ariaLogLevelsCommand;
        public DelegateCommand<string> AriaLogLevelsCommand => ariaLogLevelsCommand ?? (ariaLogLevelsCommand = new DelegateCommand<string>(ExecuteAriaLogLevelsCommand));

        /// <summary>
        /// Aria的日志等级事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteAriaLogLevelsCommand(string parameter)
        {
            AriaConfigLogLevel ariaLogLevel;
            switch (parameter)
            {
                case "DEBUG":
                    ariaLogLevel = AriaConfigLogLevel.DEBUG;
                    break;
                case "INFO":
                    ariaLogLevel = AriaConfigLogLevel.INFO;
                    break;
                case "NOTICE":
                    ariaLogLevel = AriaConfigLogLevel.NOTICE;
                    break;
                case "WARN":
                    ariaLogLevel = AriaConfigLogLevel.WARN;
                    break;
                case "ERROR":
                    ariaLogLevel = AriaConfigLogLevel.ERROR;
                    break;
                default:
                    ariaLogLevel = AriaConfigLogLevel.INFO;
                    break;
            }

            bool isSucceed = SettingsManager.GetInstance().SetAriaLogLevel(ariaLogLevel);
            PublishTip(isSucceed);
        }

        // Aria同时下载数事件
        private DelegateCommand<object> ariaMaxConcurrentDownloadsCommand;
        public DelegateCommand<object> AriaMaxConcurrentDownloadsCommand => ariaMaxConcurrentDownloadsCommand ?? (ariaMaxConcurrentDownloadsCommand = new DelegateCommand<object>(ExecuteAriaMaxConcurrentDownloadsCommand));

        /// <summary>
        /// Aria同时下载数事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteAriaMaxConcurrentDownloadsCommand(object parameter)
        {
            SelectedAriaMaxConcurrentDownload = (int)parameter;

            bool isSucceed = SettingsManager.GetInstance().SetMaxCurrentDownloads(SelectedAriaMaxConcurrentDownload);
            PublishTip(isSucceed);
        }

        // Aria最大线程数事件
        private DelegateCommand<object> ariaSplitsCommand;
        public DelegateCommand<object> AriaSplitsCommand => ariaSplitsCommand ?? (ariaSplitsCommand = new DelegateCommand<object>(ExecuteAriaSplitsCommand));

        /// <summary>
        /// Aria最大线程数事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteAriaSplitsCommand(object parameter)
        {
            SelectedAriaSplit = (int)parameter;

            bool isSucceed = SettingsManager.GetInstance().SetAriaSplit(SelectedAriaSplit);
            PublishTip(isSucceed);
        }

        // Aria下载速度限制事件
        private DelegateCommand<string> ariaMaxOverallDownloadLimitCommand;
        public DelegateCommand<string> AriaMaxOverallDownloadLimitCommand => ariaMaxOverallDownloadLimitCommand ?? (ariaMaxOverallDownloadLimitCommand = new DelegateCommand<string>(ExecuteAriaMaxOverallDownloadLimitCommand));

        /// <summary>
        /// Aria下载速度限制事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteAriaMaxOverallDownloadLimitCommand(string parameter)
        {
            int downloadLimit = (int)Number.GetInt(parameter);
            AriaMaxOverallDownloadLimit = downloadLimit;

            bool isSucceed = SettingsManager.GetInstance().SetAriaMaxOverallDownloadLimit(AriaMaxOverallDownloadLimit);
            PublishTip(isSucceed);
        }

        // Aria下载单文件速度限制事件
        private DelegateCommand<string> ariaMaxDownloadLimitCommand;
        public DelegateCommand<string> AriaMaxDownloadLimitCommand => ariaMaxDownloadLimitCommand ?? (ariaMaxDownloadLimitCommand = new DelegateCommand<string>(ExecuteAriaMaxDownloadLimitCommand));

        /// <summary>
        /// Aria下载单文件速度限制事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteAriaMaxDownloadLimitCommand(string parameter)
        {
            int downloadLimit = (int)Number.GetInt(parameter);
            AriaMaxDownloadLimit = downloadLimit;

            bool isSucceed = SettingsManager.GetInstance().SetAriaMaxDownloadLimit(AriaMaxDownloadLimit);
            PublishTip(isSucceed);
        }

        // 是否开启Aria http代理事件
        private DelegateCommand isAriaHttpProxyCommand;
        public DelegateCommand IsAriaHttpProxyCommand => isAriaHttpProxyCommand ?? (isAriaHttpProxyCommand = new DelegateCommand(ExecuteIsAriaHttpProxyCommand));

        /// <summary>
        /// 是否开启Aria http代理事件
        /// </summary>
        private void ExecuteIsAriaHttpProxyCommand()
        {
            AllowStatus isAriaHttpProxy = IsAriaHttpProxy ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().IsAriaHttpProxy(isAriaHttpProxy);
            PublishTip(isSucceed);
        }

        // Aria的http代理的地址事件
        private DelegateCommand<string> ariaHttpProxyCommand;
        public DelegateCommand<string> AriaHttpProxyCommand => ariaHttpProxyCommand ?? (ariaHttpProxyCommand = new DelegateCommand<string>(ExecuteAriaHttpProxyCommand));

        /// <summary>
        /// Aria的http代理的地址事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteAriaHttpProxyCommand(string parameter)
        {
            bool isSucceed = SettingsManager.GetInstance().SetAriaHttpProxy(parameter);
            PublishTip(isSucceed);
        }

        // Aria的http代理的端口事件
        private DelegateCommand<string> ariaHttpProxyPortCommand;
        public DelegateCommand<string> AriaHttpProxyPortCommand => ariaHttpProxyPortCommand ?? (ariaHttpProxyPortCommand = new DelegateCommand<string>(ExecuteAriaHttpProxyPortCommand));

        /// <summary>
        /// Aria的http代理的端口事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteAriaHttpProxyPortCommand(string parameter)
        {
            int httpProxyPort = (int)Number.GetInt(parameter);
            AriaHttpProxyPort = httpProxyPort;

            bool isSucceed = SettingsManager.GetInstance().SetAriaHttpProxyListenPort(AriaHttpProxyPort);
            PublishTip(isSucceed);
        }

        // Aria文件预分配事件
        private DelegateCommand<string> ariaFileAllocationsCommand;
        public DelegateCommand<string> AriaFileAllocationsCommand => ariaFileAllocationsCommand ?? (ariaFileAllocationsCommand = new DelegateCommand<string>(ExecuteAriaFileAllocationsCommand));

        /// <summary>
        /// Aria文件预分配事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteAriaFileAllocationsCommand(string parameter)
        {
            AriaConfigFileAllocation ariaFileAllocation;
            switch (parameter)
            {
                case "NONE":
                    ariaFileAllocation = AriaConfigFileAllocation.NONE;
                    break;
                case "PREALLOC":
                    ariaFileAllocation = AriaConfigFileAllocation.PREALLOC;
                    break;
                case "FALLOC":
                    ariaFileAllocation = AriaConfigFileAllocation.FALLOC;
                    break;
                default:
                    ariaFileAllocation = AriaConfigFileAllocation.PREALLOC;
                    break;
            }

            bool isSucceed = SettingsManager.GetInstance().SetAriaFileAllocation(ariaFileAllocation);
            PublishTip(isSucceed);
        }

        #endregion

        /// <summary>
        /// 发送需要显示的tip
        /// </summary>
        /// <param name="isSucceed"></param>
        private void PublishTip(bool isSucceed)
        {
            if (isOnNavigatedTo) { return; }

            if (isSucceed)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipSettingUpdated"));
            }
            else
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipSettingFailed"));
            }
        }

    }
}
