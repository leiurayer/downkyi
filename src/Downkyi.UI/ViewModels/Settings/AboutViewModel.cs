using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.Settings;
using Downkyi.Core.Settings.Enum;
using Downkyi.UI.Models;
using Downkyi.UI.Mvvm;
using System.Diagnostics;

namespace Downkyi.UI.ViewModels.Settings;

public partial class AboutViewModel : BaseSettingsViewModel
{
    public const string Key = "Settings_About";

    #region 页面属性申明

    [ObservableProperty]
    private string _appName = string.Empty;

    [ObservableProperty]
    private string _appVersion = string.Empty;

    [ObservableProperty]
    private bool _isReceiveBetaVersion;

    [ObservableProperty]
    private bool _isAutoUpdateWhenLaunch;

    [ObservableProperty]
    private List<ThirdParty> _thirdParties = new();

    #endregion

    public AboutViewModel(BaseServices baseServices) : base(baseServices)
    {
        TipSettingUpdated = DictionaryResource.GetString("TipSettingUpdated");
        TipSettingFailed = DictionaryResource.GetString("TipSettingFailed");
    }

    #region 命令申明

    /// <summary>
    /// 加载页面时执行
    /// </summary>
    [RelayCommand]
    private void OnLoaded()
    {
        IsOnNavigatedTo = true;

        // 是否接收测试版更新
        var isReceiveBetaVersion = SettingsManager.GetInstance().IsReceiveBetaVersion();
        IsReceiveBetaVersion = isReceiveBetaVersion == AllowStatus.YES;

        // 是否在启动时自动检查更新
        var isAutoUpdateWhenLaunch = SettingsManager.GetInstance().GetAutoUpdateWhenLaunch();
        IsAutoUpdateWhenLaunch = isAutoUpdateWhenLaunch == AllowStatus.YES;

        IsOnNavigatedTo = false;
    }

    /// <summary>
    /// 访问主页事件
    /// </summary>
    [RelayCommand]
    private void VisitHomepage()
    {
        Process.Start(new ProcessStartInfo("https://github.com/leiurayer/downkyi") { UseShellExecute = true });
    }

    /// <summary>
    /// 检查更新事件
    /// </summary>
    [RelayCommand]
    private void CheckUpdate()
    {
        // TODO
        NotificationEvent.Publish("请前往主页下载最新版~");
    }

    /// <summary>
    /// 意见反馈事件
    /// </summary>
    [RelayCommand]
    private void Feedback()
    {
        Process.Start(new ProcessStartInfo("https://github.com/leiurayer/downkyi/issues") { UseShellExecute = true });
    }

    /// <summary>
    /// 是否接收测试版更新事件
    /// </summary>
    [RelayCommand]
    private void ReceiveBetaVersion()
    {
        AllowStatus isReceiveBetaVersion = IsReceiveBetaVersion ? AllowStatus.YES : AllowStatus.NO;

        bool isSucceed = SettingsManager.GetInstance().IsReceiveBetaVersion(isReceiveBetaVersion);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 是否在启动时自动检查更新事件
    /// </summary>
    [RelayCommand]
    private void AutoUpdateWhenLaunch()
    {
        AllowStatus isAutoUpdateWhenLaunch = IsAutoUpdateWhenLaunch ? AllowStatus.YES : AllowStatus.NO;

        bool isSucceed = SettingsManager.GetInstance().SetAutoUpdateWhenLaunch(isAutoUpdateWhenLaunch);
        PublishTip(Key, isSucceed);
    }

    #endregion

}