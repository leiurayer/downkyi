using CommunityToolkit.Mvvm.Input;
using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.User;

public partial class UserSpaceViewModel : ViewModelBase
{
    public const string Key = "UserSpace";

    public UserSpaceViewModel(BaseServices baseServices) : base(baseServices)
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