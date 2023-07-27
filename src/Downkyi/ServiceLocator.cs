using CommunityToolkit.Mvvm.DependencyInjection;
using Downkyi.Services;
using Downkyi.UI.Mvvm;
using Downkyi.UI.Services;
using Downkyi.UI.Services.Event;
using Downkyi.UI.ViewModels;
using Downkyi.UI.ViewModels.DownloadManager;
using Downkyi.UI.ViewModels.Login;
using Downkyi.UI.ViewModels.Settings;
using Downkyi.UI.ViewModels.Toolbox;
using Downkyi.UI.ViewModels.User;
using Downkyi.UI.ViewModels.Video;
using Downkyi.ViewModels;
using Downkyi.ViewModels.Login;
using Downkyi.ViewModels.Settings;
using Downkyi.ViewModels.Toolbox;
using Microsoft.Extensions.DependencyInjection;

namespace Downkyi;

public static class ServiceLocator
{
    private static bool _initialized;

    // 主窗体
    public static MainWindowViewModel MainWindowViewModel =>
        Ioc.Default.GetRequiredService<MainWindowViewModel>();

    // 主页
    public static IndexViewModel IndexViewModel =>
        Ioc.Default.GetRequiredService<IndexViewModel>();

    // 登录
    public static LoginViewModel LoginViewModel =>
        Ioc.Default.GetRequiredService<LoginViewModel>();
    public static QRCodeViewModelProxy QRCodeViewModel =>
        Ioc.Default.GetRequiredService<QRCodeViewModelProxy>();
    public static CookiesViewModel CookiesViewModel =>
        Ioc.Default.GetRequiredService<CookiesViewModel>();

    // 我的个人空间
    public static MySpaceViewModel MySpaceViewModel =>
        Ioc.Default.GetRequiredService<MySpaceViewModel>();

    // 个人空间
    public static UserSpaceViewModel UserSpaceViewModel =>
        Ioc.Default.GetRequiredService<UserSpaceViewModel>();

    // 设置
    public static SettingsViewModel SettingsViewModel =>
        Ioc.Default.GetRequiredService<SettingsViewModel>();
    public static BasicViewModel BasicViewModel =>
        Ioc.Default.GetRequiredService<BasicViewModel>();
    public static NetworkViewModel NetworkViewModel =>
        Ioc.Default.GetRequiredService<NetworkViewModel>();
    public static VideoViewModelProxy VideoViewModel =>
        Ioc.Default.GetRequiredService<VideoViewModelProxy>();
    public static DanmakuViewModelProxy DanmakuViewModel =>
        Ioc.Default.GetRequiredService<DanmakuViewModelProxy>();
    public static AboutViewModelProxy AboutViewModel =>
        Ioc.Default.GetRequiredService<AboutViewModelProxy>();

    // 下载管理
    public static DownloadManagerViewModel DownloadManagerViewModel =>
        Ioc.Default.GetRequiredService<DownloadManagerViewModel>();
    public static DownloadingViewModel DownloadingViewModel =>
        Ioc.Default.GetRequiredService<DownloadingViewModel>();
    public static DownloadFinishedViewModel DownloadFinishedViewModel =>
        Ioc.Default.GetRequiredService<DownloadFinishedViewModel>();

    // 工具箱
    public static ToolboxViewModel ToolboxViewModel =>
        Ioc.Default.GetRequiredService<ToolboxViewModel>();
    public static BiliHelperViewModel BiliHelperViewModel =>
        Ioc.Default.GetRequiredService<BiliHelperViewModel>();
    public static DelogoViewModelProxy DelogoViewModel =>
        Ioc.Default.GetRequiredService<DelogoViewModelProxy>();
    public static ExtractMediaViewModelProxy ExtractMediaViewModel =>
        Ioc.Default.GetRequiredService<ExtractMediaViewModelProxy>();

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    public static void ConfigureServices()
    {
        // Register services
        if (!_initialized)
        {
            _initialized = true;
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                //Services
                .AddScoped<BaseServices>()
                .AddSingleton<IBroadcastEvent, BroadcastEvent>()
                .AddSingleton<INotificationEvent, NotificationEvent>()
                .AddSingleton<IClipboardService, ClipboardService>()
                .AddSingleton<IDictionaryResource, DictionaryResource>()
                .AddSingleton<IMainSearchService, MainSearchService>()
                .AddSingleton<INavigationService, NavigationService>()
                .AddSingleton<IStoragePicker, StoragePicker>()
                .AddSingleton<IStorageService, StorageService>()
                //ViewModels
                .AddSingleton<MainWindowViewModel>()
                .AddSingleton<IndexViewModel>()
                //
                .AddSingleton<DownloadManagerViewModel>()
                .AddSingleton<DownloadingViewModel>()
                .AddSingleton<DownloadFinishedViewModel>()
                //
                .AddSingleton<LoginViewModel>()
                .AddSingleton<QRCodeViewModelProxy>()
                .AddSingleton<CookiesViewModel>()
                //
                .AddSingleton<SettingsViewModel>()
                .AddSingleton<BasicViewModel>()
                .AddSingleton<NetworkViewModel>()
                .AddSingleton<VideoViewModelProxy>()
                .AddSingleton<DanmakuViewModelProxy>()
                .AddSingleton<AboutViewModelProxy>()
                //
                .AddSingleton<ToolboxViewModel>()
                .AddSingleton<BiliHelperViewModel>()
                .AddSingleton<DelogoViewModelProxy>()
                .AddSingleton<ExtractMediaViewModelProxy>()
                //
                .AddSingleton<MySpaceViewModel>()
                .AddSingleton<UserSpaceViewModel>()
                //
                .AddSingleton<VideoDetailViewModel>()
                .AddSingleton<PublicFavoritesViewModel>()
                .BuildServiceProvider());
        }
    }
}