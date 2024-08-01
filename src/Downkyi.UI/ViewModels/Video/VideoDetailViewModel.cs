using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.UI.Models;
using Downkyi.UI.Mvvm;
using Downkyi.UI.ViewModels.DownloadManager;

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
    private void CopyCoverUrl() { }

    [RelayCommand]
    private void Upper() { }

    #endregion

    public override void OnNavigatedTo(Dictionary<string, object>? parameter)
    {
        base.OnNavigatedTo(parameter);

        if (parameter!.ContainsKey("value"))
        {
            _input = (string)parameter["value"];
            InputText = _input;
        }
       
    }

}