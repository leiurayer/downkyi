using Aria2cNet.Server;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.Settings;
using Downkyi.Core.Settings.Enum;
using Downkyi.Core.Utils.Validator;
using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.Settings;

public partial class NetworkViewModel : BaseSettingsViewModel
{
    public const string Key = "Settings_Network";

    #region 页面属性申明

    [ObservableProperty]
    private bool _useSSL;

    [ObservableProperty]
    private string _userAgent = string.Empty;

    [ObservableProperty]
    private bool _builtin;

    [ObservableProperty]
    private bool _aria2c;

    [ObservableProperty]
    private bool _customAria2c;

    [ObservableProperty]
    private List<int> _maxCurrentDownloads = new();

    [ObservableProperty]
    private int _selectedMaxCurrentDownload;

    [ObservableProperty]
    private List<int> _splits = new();

    [ObservableProperty]
    private int _selectedSplit;

    [ObservableProperty]
    private bool _isHttpProxy;

    [ObservableProperty]
    private string _httpProxy = string.Empty;

    [ObservableProperty]
    private int _httpProxyPort;

    [ObservableProperty]
    private string _ariaHost = string.Empty;

    [ObservableProperty]
    private int _ariaListenPort;

    [ObservableProperty]
    private string _ariaToken = string.Empty;

    [ObservableProperty]
    private List<string> _ariaLogLevels = new();

    [ObservableProperty]
    private string _selectedAriaLogLevel = string.Empty;

    [ObservableProperty]
    private List<int> _ariaMaxConcurrentDownloads = new();

    [ObservableProperty]
    private int _selectedAriaMaxConcurrentDownload;

    [ObservableProperty]
    private List<int> _ariaSplits = new();

    [ObservableProperty]
    private int _selectedAriaSplit;

    [ObservableProperty]
    private int _ariaMaxOverallDownloadLimit;

    [ObservableProperty]
    private int _ariaMaxDownloadLimit;

    [ObservableProperty]
    private bool _isAriaHttpProxy;

    [ObservableProperty]
    private string _ariaHttpProxy = string.Empty;

    [ObservableProperty]
    private int _ariaHttpProxyPort;

    [ObservableProperty]
    private List<string> _ariaFileAllocations = new();

    [ObservableProperty]
    private string _selectedAriaFileAllocation = string.Empty;

    #endregion

    public NetworkViewModel(BaseServices baseServices) : base(baseServices)
    {
        TipSettingUpdated = DictionaryResource.GetString("TipSettingUpdated");
        TipSettingFailed = DictionaryResource.GetString("TipSettingFailed");

        #region 属性初始化

        // builtin同时下载数
        for (int i = 1; i <= 10; i++) { MaxCurrentDownloads.Add(i); }

        // builtin最大线程数
        for (int i = 1; i <= 10; i++) { Splits.Add(i); }

        // Aria的日志等级
        AriaLogLevels.Add("DEBUG");
        AriaLogLevels.Add("INFO");
        AriaLogLevels.Add("NOTICE");
        AriaLogLevels.Add("WARN");
        AriaLogLevels.Add("ERROR");

        // Aria同时下载数
        for (int i = 1; i <= 10; i++) { AriaMaxConcurrentDownloads.Add(i); }

        // Aria最大线程数
        for (int i = 1; i <= 10; i++) { AriaSplits.Add(i); }

        // Aria文件预分配
        AriaFileAllocations.Add("NONE");
        AriaFileAllocations.Add("PREALLOC");
        AriaFileAllocations.Add("FALLOC");

        #endregion
    }

    #region 命令申明

    /// <summary>
    /// 加载页面时执行
    /// </summary>
    [RelayCommand]
    private void OnLoaded()
    {
        IsOnNavigatedTo = true;

        // 启用https
        AllowStatus useSSL = SettingsManager.Instance.UseSSL();
        UseSSL = useSSL == AllowStatus.YES;

        // UserAgent
        UserAgent = SettingsManager.Instance.GetUserAgent();

        // 选择下载器
        var downloader = SettingsManager.Instance.GetDownloader();
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
            case Downloader.CUSTOM_ARIA:
                CustomAria2c = true;
                break;
        }

        // builtin同时下载数
        SelectedMaxCurrentDownload = SettingsManager.Instance.GetMaxCurrentDownloads();

        // builtin最大线程数
        SelectedSplit = SettingsManager.Instance.GetSplit();

        // 是否开启builtin http代理
        AllowStatus isHttpProxy = SettingsManager.Instance.IsHttpProxy();
        IsHttpProxy = isHttpProxy == AllowStatus.YES;

        // builtin的http代理的地址
        HttpProxy = SettingsManager.Instance.GetHttpProxy();

        // builtin的http代理的端口
        HttpProxyPort = SettingsManager.Instance.GetHttpProxyListenPort();

        // Aria服务器host
        AriaHost = SettingsManager.Instance.GetAriaHost();

        // Aria服务器端口
        AriaListenPort = SettingsManager.Instance.GetAriaListenPort();

        // Aria服务器Token
        AriaToken = SettingsManager.Instance.GetAriaToken();

        // Aria的日志等级
        AriaConfigLogLevel ariaLogLevel = SettingsManager.Instance.GetAriaLogLevel();
        SelectedAriaLogLevel = ariaLogLevel.ToString("G");

        // Aria同时下载数
        SelectedAriaMaxConcurrentDownload = SettingsManager.Instance.GetMaxCurrentDownloads();

        // Aria最大线程数
        SelectedAriaSplit = SettingsManager.Instance.GetAriaSplit();

        // Aria下载速度限制
        AriaMaxOverallDownloadLimit = SettingsManager.Instance.GetAriaMaxOverallDownloadLimit();

        // Aria下载单文件速度限制
        AriaMaxDownloadLimit = SettingsManager.Instance.GetAriaMaxDownloadLimit();

        // 是否开启Aria http代理
        AllowStatus isAriaHttpProxy = SettingsManager.Instance.IsAriaHttpProxy();
        IsAriaHttpProxy = isAriaHttpProxy == AllowStatus.YES;

        // Aria的http代理的地址
        AriaHttpProxy = SettingsManager.Instance.GetAriaHttpProxy();

        // Aria的http代理的端口
        AriaHttpProxyPort = SettingsManager.Instance.GetAriaHttpProxyListenPort();

        // Aria文件预分配
        AriaConfigFileAllocation ariaFileAllocation = SettingsManager.Instance.GetAriaFileAllocation();
        SelectedAriaFileAllocation = ariaFileAllocation.ToString("G");

        IsOnNavigatedTo = false;
    }

    /// <summary>
    /// 是否启用https事件
    /// </summary>
    [RelayCommand]
    private void SetUseSSL()
    {
        AllowStatus useSSL = UseSSL ? AllowStatus.YES : AllowStatus.NO;

        bool isSucceed = SettingsManager.Instance.UseSSL(useSSL);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 设置UserAgent事件
    /// </summary>
    [RelayCommand]
    private void SetUserAgent()
    {
        bool isSucceed = SettingsManager.Instance.SetUserAgent(UserAgent);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 下载器选择事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SelectDownloader(string parameter)
    {
        var downloader = parameter switch
        {
            "Builtin" => Downloader.BUILT_IN,
            "Aria2c" => Downloader.ARIA,
            "CustomAria2c" => Downloader.CUSTOM_ARIA,
            _ => SettingsManager.Instance.GetDownloader(),
        };
        bool isSucceed = SettingsManager.Instance.SetDownloader(downloader);
        PublishTip(Key, isSucceed);

        // 弹窗提示是否重启程序
        // TODO

        //AlertService alertService = new AlertService(dialogService);
        //ButtonResult result = alertService.ShowInfo(DictionaryResource.GetString("ConfirmReboot"));
        //if (result == ButtonResult.OK)
        //{
        //    System.Windows.Application.Current.Shutdown();
        //    System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        //}
    }

    /// <summary>
    /// builtin同时下载数事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetMaxCurrentDownloads()
    {
        bool isSucceed = SettingsManager.Instance.SetMaxCurrentDownloads(SelectedMaxCurrentDownload);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// builtin最大线程数事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetSplits()
    {
        bool isSucceed = SettingsManager.Instance.SetSplit(SelectedSplit);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 是否开启builtin http代理事件
    /// </summary>
    [RelayCommand]
    private void SetIsHttpProxy()
    {
        AllowStatus isHttpProxy = IsHttpProxy ? AllowStatus.YES : AllowStatus.NO;

        bool isSucceed = SettingsManager.Instance.IsHttpProxy(isHttpProxy);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// builtin的http代理的地址事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetHttpProxy(string parameter)
    {
        bool isSucceed = SettingsManager.Instance.SetHttpProxy(parameter);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// builtin的http代理的端口事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetHttpProxyPort(string parameter)
    {
        int httpProxyPort = (int)Number.GetInt(parameter);
        HttpProxyPort = httpProxyPort;

        bool isSucceed = SettingsManager.Instance.SetHttpProxyListenPort(HttpProxyPort);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// Aria服务器host事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetAriaHost(string parameter)
    {
        AriaHost = parameter;
        bool isSucceed = SettingsManager.Instance.SetAriaHost(AriaHost);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// Aria服务器端口事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetAriaListenPort(string parameter)
    {
        int listenPort = (int)Number.GetInt(parameter);
        AriaListenPort = listenPort;

        bool isSucceed = SettingsManager.Instance.SetAriaListenPort(AriaListenPort);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// Aria服务器token事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetAriaToken(string parameter)
    {
        AriaToken = parameter;
        bool isSucceed = SettingsManager.Instance.SetAriaToken(AriaToken);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// Aria的日志等级事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetAriaLogLevels()
    {
        var ariaLogLevel = SelectedAriaLogLevel switch
        {
            "DEBUG" => AriaConfigLogLevel.DEBUG,
            "INFO" => AriaConfigLogLevel.INFO,
            "NOTICE" => AriaConfigLogLevel.NOTICE,
            "WARN" => AriaConfigLogLevel.WARN,
            "ERROR" => AriaConfigLogLevel.ERROR,
            _ => AriaConfigLogLevel.INFO,
        };
        bool isSucceed = SettingsManager.Instance.SetAriaLogLevel(ariaLogLevel);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// Aria同时下载数事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetAriaMaxConcurrentDownloads()
    {
        bool isSucceed = SettingsManager.Instance.SetMaxCurrentDownloads(SelectedAriaMaxConcurrentDownload);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// Aria最大线程数事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetAriaSplits()
    {
        bool isSucceed = SettingsManager.Instance.SetAriaSplit(SelectedAriaSplit);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// Aria下载速度限制事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetAriaMaxOverallDownloadLimit(string parameter)
    {
        int downloadLimit = (int)Number.GetInt(parameter);
        AriaMaxOverallDownloadLimit = downloadLimit;

        bool isSucceed = SettingsManager.Instance.SetAriaMaxOverallDownloadLimit(AriaMaxOverallDownloadLimit);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// Aria下载单文件速度限制事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetAriaMaxDownloadLimit(string parameter)
    {
        int downloadLimit = (int)Number.GetInt(parameter);
        AriaMaxDownloadLimit = downloadLimit;

        bool isSucceed = SettingsManager.Instance.SetAriaMaxDownloadLimit(AriaMaxDownloadLimit);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 是否开启Aria http代理事件
    /// </summary>
    [RelayCommand]
    private void SetIsAriaHttpProxy()
    {
        AllowStatus isAriaHttpProxy = IsAriaHttpProxy ? AllowStatus.YES : AllowStatus.NO;

        bool isSucceed = SettingsManager.Instance.IsAriaHttpProxy(isAriaHttpProxy);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// Aria的http代理的地址事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetAriaHttpProxy(string parameter)
    {
        bool isSucceed = SettingsManager.Instance.SetAriaHttpProxy(parameter);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// Aria的http代理的端口事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetAriaHttpProxyPort(string parameter)
    {
        int httpProxyPort = (int)Number.GetInt(parameter);
        AriaHttpProxyPort = httpProxyPort;

        bool isSucceed = SettingsManager.Instance.SetAriaHttpProxyListenPort(AriaHttpProxyPort);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// Aria文件预分配事件
    /// </summary>
    [RelayCommand]
    private void SetAriaFileAllocations()
    {
        var ariaFileAllocation = SelectedAriaFileAllocation switch
        {
            "NONE" => AriaConfigFileAllocation.NONE,
            "PREALLOC" => AriaConfigFileAllocation.PREALLOC,
            "FALLOC" => AriaConfigFileAllocation.FALLOC,
            _ => AriaConfigFileAllocation.PREALLOC,
        };
        bool isSucceed = SettingsManager.Instance.SetAriaFileAllocation(ariaFileAllocation);
        PublishTip(Key, isSucceed);
    }

    #endregion

}