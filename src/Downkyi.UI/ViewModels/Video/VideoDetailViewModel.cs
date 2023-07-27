using CommunityToolkit.Mvvm.Input;
using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.Video;

public partial class VideoDetailViewModel : ViewModelBase
{
    public const string Key = "VideoDetail";

    public VideoDetailViewModel(BaseServices baseServices) : base(baseServices)
    {
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

    #endregion

}