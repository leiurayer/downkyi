using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.Log;
using Downkyi.Core.Settings;
using Downkyi.Core.Settings.Models;
using Downkyi.UI.Models;
using Downkyi.UI.Mvvm;
using Downkyi.UI.ViewModels.DownloadManager;
using Downkyi.UI.ViewModels.User;

namespace Downkyi.UI.ViewModels.Video;

public partial class VideoDetailViewModel : ViewModelBase
{
    public const string Key = "VideoDetail";

    // 保存输入字符串，避免被用户修改
    private string? _input = null;

    #region 页面属性申明

    [ObservableProperty]
    private string _inputText = string.Empty;

    [ObservableProperty]
    private bool _loadingVisibility;

    [ObservableProperty]
    private bool _contentVisibility;

    [ObservableProperty]
    private VideoInfoView _videoInfoView = new();

    #endregion

    public VideoDetailViewModel(BaseServices baseServices) : base(baseServices)
    {
        #region 属性初始化

        ContentVisibility = true;

        VideoInfoView.Title = "video name";

        VideoInfoView.CoinNumber = "10";
        VideoInfoView.DanmakuNumber = "1";
        VideoInfoView.FavoriteNumber = "1";
        VideoInfoView.LikeNumber = "10";
        VideoInfoView.PlayNumber = "0";
        VideoInfoView.ReplyNumber = "1";
        VideoInfoView.ShareNumber = "1";

        #endregion
    }

    #region 命令申明

    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task BackwardAsync()
    {
        Dictionary<string, object> parameter = new()
        {
            { "key", Key },
        };

        await NavigationService.BackwardAsync(parameter);
    }

    [RelayCommand]
    private void Input() { }

    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task DownloadManager()
    {
        await NavigationService.ForwardAsync(DownloadManagerViewModel.Key);
    }

    [RelayCommand]
    private void CopyCover() { }

    [RelayCommand]
    private async Task CopyCoverUrl()
    {
        // 复制封面url到剪贴板
        await ClipboardService.SetTextAsync(VideoInfoView.CoverUrl);
        Log.Logger.Info("复制封面url到剪贴板");
    }

    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task Upper()
    {
        await NavigateToViewUserSpace(VideoInfoView.UpperMid);
    }

    #endregion

    /// <summary>
    /// 导航到用户空间，
    /// 如果传入的mid与本地登录的mid一致，
    /// 则进入我的用户空间。
    /// </summary>
    /// <param name="mid"></param>
    private async Task NavigateToViewUserSpace(long mid)
    {
        Dictionary<string, object> parameter = new()
        {
            { "key", Key },
            { "value", mid },
        };

        UserInfoSettings userInfo = SettingsManager.GetInstance().GetUserInfo();
        if (userInfo != null && userInfo.Mid == mid)
        {
            await NavigationService.ForwardAsync(MySpaceViewModel.Key, parameter);
        }
        else
        {
            await NavigationService.ForwardAsync(UserSpaceViewModel.Key, parameter);
        }
    }

    public override void OnNavigatedTo(Dictionary<string, object>? parameter)
    {
        base.OnNavigatedTo(parameter);

        if (parameter!.TryGetValue("value", out object? value))
        {
            _input = (string)value;
            InputText = _input;
        }

    }

}