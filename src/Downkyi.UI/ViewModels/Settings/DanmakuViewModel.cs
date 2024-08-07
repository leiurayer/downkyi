using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.Settings;
using Downkyi.Core.Settings.Enum;
using Downkyi.Core.Utils.Validator;
using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.Settings;

public partial class DanmakuViewModel : BaseSettingsViewModel
{
    public const string Key = "Settings_Danmaku";

    #region 页面属性申明

    [ObservableProperty]
    private bool _topFilter;

    [ObservableProperty]
    private bool _bottomFilter;

    [ObservableProperty]
    private bool _scrollFilter;

    [ObservableProperty]
    private int _screenWidth;

    [ObservableProperty]
    private int _screenHeight;

    [ObservableProperty]
    private List<string> _fonts = new();

    [ObservableProperty]
    private string _selectedFont = string.Empty;

    [ObservableProperty]
    private int _fontSize;

    [ObservableProperty]
    private int _lineCount;

    [ObservableProperty]
    private bool _sync;

    [ObservableProperty]
    private bool _async;

    #endregion

    public DanmakuViewModel(BaseServices baseServices) : base(baseServices)
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

        // 屏蔽顶部弹幕
        AllowStatus danmakuTopFilter = SettingsManager.Instance.GetDanmakuTopFilter();
        TopFilter = danmakuTopFilter == AllowStatus.YES;

        // 屏蔽底部弹幕
        AllowStatus danmakuBottomFilter = SettingsManager.Instance.GetDanmakuBottomFilter();
        BottomFilter = danmakuBottomFilter == AllowStatus.YES;

        // 屏蔽滚动弹幕
        AllowStatus danmakuScrollFilter = SettingsManager.Instance.GetDanmakuScrollFilter();
        ScrollFilter = danmakuScrollFilter == AllowStatus.YES;

        // 分辨率-宽
        ScreenWidth = SettingsManager.Instance.GetDanmakuScreenWidth();

        // 分辨率-高
        ScreenHeight = SettingsManager.Instance.GetDanmakuScreenHeight();

        // 弹幕字体
        string danmakuFont = SettingsManager.Instance.GetDanmakuFontName();
        if (danmakuFont != null && Fonts.Contains(danmakuFont))
        {
            // 只有系统中存在当前设置的字体，才能显示
            SelectedFont = danmakuFont;
        }

        // 弹幕字体大小
        FontSize = SettingsManager.Instance.GetDanmakuFontSize();

        // 弹幕限制行数
        LineCount = SettingsManager.Instance.GetDanmakuLineCount();

        // 弹幕布局算法
        DanmakuLayoutAlgorithm layoutAlgorithm = SettingsManager.Instance.GetDanmakuLayoutAlgorithm();
        SetLayoutAlgorithm(layoutAlgorithm);

        IsOnNavigatedTo = false;
    }

    /// <summary>
    /// 屏蔽顶部弹幕事件
    /// </summary>
    [RelayCommand]
    private void SetTopFilter()
    {
        AllowStatus isTopFilter = TopFilter ? AllowStatus.YES : AllowStatus.NO;

        bool isSucceed = SettingsManager.Instance.SetDanmakuTopFilter(isTopFilter);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 屏蔽底部弹幕事件
    /// </summary>
    [RelayCommand]
    private void SetBottomFilter()
    {
        AllowStatus isBottomFilter = BottomFilter ? AllowStatus.YES : AllowStatus.NO;

        bool isSucceed = SettingsManager.Instance.SetDanmakuBottomFilter(isBottomFilter);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 屏蔽滚动弹幕事件
    /// </summary>
    [RelayCommand]
    private void SetScrollFilter()
    {
        AllowStatus isScrollFilter = ScrollFilter ? AllowStatus.YES : AllowStatus.NO;

        bool isSucceed = SettingsManager.Instance.SetDanmakuScrollFilter(isScrollFilter);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 设置分辨率-宽事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetScreenWidth(string parameter)
    {
        int width = (int)Number.GetInt(parameter);
        ScreenWidth = width;

        bool isSucceed = SettingsManager.Instance.SetDanmakuScreenWidth(ScreenWidth);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 设置分辨率-高事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetScreenHeight(string parameter)
    {
        int height = (int)Number.GetInt(parameter);
        ScreenHeight = height;

        bool isSucceed = SettingsManager.Instance.SetDanmakuScreenHeight(ScreenHeight);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 弹幕字体选择事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void FontSelected(string parameter)
    {
        bool isSucceed = SettingsManager.Instance.SetDanmakuFontName(parameter);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 弹幕字体大小事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetFontSize(string parameter)
    {
        int fontSize = (int)Number.GetInt(parameter);
        FontSize = fontSize;

        bool isSucceed = SettingsManager.Instance.SetDanmakuFontSize(FontSize);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 弹幕限制行数事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetLineCount(string parameter)
    {
        int lineCount = (int)Number.GetInt(parameter);
        LineCount = lineCount;

        bool isSucceed = SettingsManager.Instance.SetDanmakuLineCount(LineCount);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 弹幕布局算法事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetLayoutAlgorithm(string parameter)
    {
        var layoutAlgorithm = parameter switch
        {
            "Sync" => DanmakuLayoutAlgorithm.SYNC,
            "Async" => DanmakuLayoutAlgorithm.ASYNC,
            _ => DanmakuLayoutAlgorithm.SYNC,
        };
        bool isSucceed = SettingsManager.Instance.SetDanmakuLayoutAlgorithm(layoutAlgorithm);
        PublishTip(Key, isSucceed);

        if (isSucceed)
        {
            SetLayoutAlgorithm(layoutAlgorithm);
        }
    }

    #endregion

    /// <summary>
    /// 设置弹幕同步算法
    /// </summary>
    /// <param name="layoutAlgorithm"></param>
    private void SetLayoutAlgorithm(DanmakuLayoutAlgorithm layoutAlgorithm)
    {
        switch (layoutAlgorithm)
        {
            case DanmakuLayoutAlgorithm.SYNC:
                Sync = true;
                Async = false;
                break;
            case DanmakuLayoutAlgorithm.ASYNC:
                Sync = false;
                Async = true;
                break;
            case DanmakuLayoutAlgorithm.NONE:
                Sync = false;
                Async = false;
                break;
            default:
                break;
        }
    }
}