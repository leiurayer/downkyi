using CommunityToolkit.Mvvm.Input;
using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.Login;

public partial class LoginViewModel : ViewModelBase
{
    public const string Key = "Login";

    private bool _isBackward = false;

    private bool _result = false;

    public LoginViewModel(BaseServices baseServices) : base(baseServices)
    {
        BroadcastEvent.Receive("qrcodeLogin",
            new Action<object>((obj) =>
            {
                if (_isBackward) { return; }

                string? value = obj as string;
                if (value == "loginSuccessful")
                {
                    _result = true;
                    _ = BackwardAsync();
                }
                if (value == "getLoginUrlFailed")
                {
                    _result = false;
                    _ = BackwardAsync();
                }
            })
            );

        BroadcastEvent.Receive("cookiesLogin",
            new Action<object>((obj) =>
            {
                if (_isBackward) { return; }

                string? value = obj as string;
                if (value == "loginSuccessful")
                {
                    _result = true;
                    _ = BackwardAsync();
                }
            })
            );
    }

    #region 命令申明

    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task BackwardAsync()
    {
        Dictionary<string, object> parameter = new()
        {
            { "key", Key },
            { "result", _result },
        };

        await NavigationService.BackwardAsync(parameter);

        BroadcastEvent.Send("login", "backward");

        _isBackward = true;
    }

    #endregion

}