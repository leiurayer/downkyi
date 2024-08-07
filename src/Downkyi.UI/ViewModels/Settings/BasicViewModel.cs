using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.Settings;
using Downkyi.Core.Settings.Enum;
using Downkyi.UI.Models;
using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.Settings;

public partial class BasicViewModel : BaseSettingsViewModel
{
    public const string Key = "Settings_Basic";

    #region 页面属性申明

    [ObservableProperty]
    private bool _none;

    [ObservableProperty]
    private bool _closeApp;

    [ObservableProperty]
    private bool _closeSystem;

    [ObservableProperty]
    private bool _isListenClipboard;

    [ObservableProperty]
    private bool _isAutoParseVideo;

    [ObservableProperty]
    private List<ParseScopeDisplay> _parseScopes = new();

    [ObservableProperty]
    private ParseScopeDisplay? _selectedParseScope;

    [ObservableProperty]
    private bool _isAutoDownloadAll;

    #endregion

    public BasicViewModel(BaseServices baseServices) : base(baseServices)
    {
        TipSettingUpdated = DictionaryResource.GetString("TipSettingUpdated");
        TipSettingFailed = DictionaryResource.GetString("TipSettingFailed");

        #region 属性初始化

        // 解析范围
        ParseScopes.Add(new ParseScopeDisplay { Name = DictionaryResource.GetString("ParseNone"), ParseScope = ParseScope.NONE });
        ParseScopes.Add(new ParseScopeDisplay { Name = DictionaryResource.GetString("ParseSelectedItem"), ParseScope = ParseScope.SELECTED_ITEM });
        ParseScopes.Add(new ParseScopeDisplay { Name = DictionaryResource.GetString("ParseCurrentSection"), ParseScope = ParseScope.CURRENT_SECTION });
        ParseScopes.Add(new ParseScopeDisplay { Name = DictionaryResource.GetString("ParseAll"), ParseScope = ParseScope.ALL });

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

        // 下载完成后的操作
        AfterDownloadOperation afterDownload = SettingsManager.Instance.GetAfterDownloadOperation();
        SetAfterDownloadOperation(afterDownload);

        // 是否监听剪贴板
        AllowStatus isListenClipboard = SettingsManager.Instance.IsListenClipboard();
        IsListenClipboard = isListenClipboard == AllowStatus.YES;

        // 是否自动解析视频
        AllowStatus isAutoParseVideo = SettingsManager.Instance.IsAutoParseVideo();
        IsAutoParseVideo = isAutoParseVideo == AllowStatus.YES;

        // 解析范围
        ParseScope parseScope = SettingsManager.Instance.GetParseScope();
        SelectedParseScope = ParseScopes.FirstOrDefault(t => { return t.ParseScope == parseScope; });

        // 解析后是否自动下载解析视频
        AllowStatus isAutoDownloadAll = SettingsManager.Instance.IsAutoDownloadAll();
        IsAutoDownloadAll = isAutoDownloadAll == AllowStatus.YES;

        IsOnNavigatedTo = false;
    }

    /// <summary>
    /// 下载完成后的操作事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void AfterDownload(object parameter)
    {
        if (parameter is not string) { return; }

        var afterDownload = parameter switch
        {
            "None" => AfterDownloadOperation.NONE,
            "CloseApp" => AfterDownloadOperation.CLOSE_APP,
            "CloseSystem" => AfterDownloadOperation.CLOSE_SYSTEM,
            _ => AfterDownloadOperation.NONE,
        };
        bool isSucceed = SettingsManager.Instance.SetAfterDownloadOperation(afterDownload);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 是否监听剪贴板事件
    /// </summary>
    [RelayCommand]
    private void ListenClipboard()
    {
        AllowStatus isListenClipboard = IsListenClipboard ? AllowStatus.YES : AllowStatus.NO;

        bool isSucceed = SettingsManager.Instance.IsListenClipboard(isListenClipboard);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 是否自动解析视频
    /// </summary>
    [RelayCommand]
    private void AutoParseVideo()
    {
        AllowStatus isAutoParseVideo = IsAutoParseVideo ? AllowStatus.YES : AllowStatus.NO;

        bool isSucceed = SettingsManager.Instance.IsAutoParseVideo(isAutoParseVideo);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 解析范围事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetParseScopes()
    {
        //if (parameter is not ParseScopeDisplay parseScope) { return; }

        bool isSucceed = SettingsManager.Instance.SetParseScope(SelectedParseScope!.ParseScope);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 解析后是否自动下载解析视频
    /// </summary>
    [RelayCommand]
    private void AutoDownloadAll()
    {
        AllowStatus isAutoDownloadAll = IsAutoDownloadAll ? AllowStatus.YES : AllowStatus.NO;

        bool isSucceed = SettingsManager.Instance.IsAutoDownloadAll(isAutoDownloadAll);
        PublishTip(Key, isSucceed);
    }

    #endregion


    #region UI逻辑

    /// <summary>
    /// 设置下载完成后的操作
    /// </summary>
    /// <param name="afterDownload"></param>
    private void SetAfterDownloadOperation(AfterDownloadOperation afterDownload)
    {
        switch (afterDownload)
        {
            case AfterDownloadOperation.NONE:
                None = true;
                break;
            case AfterDownloadOperation.OPEN_FOLDER:
                break;
            case AfterDownloadOperation.CLOSE_APP:
                CloseApp = true;
                break;
            case AfterDownloadOperation.CLOSE_SYSTEM:
                CloseSystem = true;
                break;
        }
    }

    #endregion

}