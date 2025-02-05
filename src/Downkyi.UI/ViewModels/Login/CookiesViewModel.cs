using System.Collections.ObjectModel;
using System.Net;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.Bili.Web;
using Downkyi.Core.Log;
using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.Login;

public partial class CookiesViewModel : ViewModelBase
{
    public const string Key = "Login_Cookies";

    private CookieContainer? _cookieContainer;

    private bool _isParse;

    protected string LoginSuccessful = string.Empty;

    protected string LoginFailed = string.Empty;

    #region 页面属性申明

    [ObservableProperty]
    private string _cookiesStr = string.Empty;

    [ObservableProperty]
    private ObservableCollection<Models.Cookie> _cookies = new();

    #endregion

    public CookiesViewModel(BaseServices baseServices) : base(baseServices)
    {
        LoginSuccessful = DictionaryResource.GetString("CookiesLoginSuccessful");
        LoginFailed = DictionaryResource.GetString("CookiesLoginFailed");
    }

    #region 命令申明

    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private Task TextChangedAsync()
    {
        if (_isParse) { return Task.CompletedTask; }

        _isParse = true;

        try
        {
            _cookieContainer = Core.Bili.Cookies.ParseCookieByString(CookiesStr);
            var cookies = Core.Bili.Cookies.GetAllCookies(_cookieContainer);

            Cookies.Clear();
            foreach (var cookie in cookies)
            {
                Cookies.Add(new Models.Cookie { Key = cookie.Name, Value = cookie.Value });
            }
        }
        catch (Exception) { }

        _isParse = false;
        return Task.CompletedTask;
    }

    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task SaveCookiesAsync()
    {
        if (_cookieContainer == null)
        {
            return;
        }

        // 保存登录信息
        try
        {
            bool isSucceed = await LoginHelperV2.SaveLoginInfoCookies(_cookieContainer);
            if (!isSucceed)
            {
                Log.Logger.Error($"{Key}: {LoginFailed}");
                NotificationEvent.Publish(LoginFailed);

                return;
            }
        }
        catch (Exception e)
        {
            Log.Logger.Error(e);
            NotificationEvent.Publish(LoginFailed);

            return;
        }

        // 发送通知
        NotificationEvent.Publish(LoginSuccessful);
        Log.Logger.Info($"{Key}: {LoginSuccessful}");

        BroadcastEvent.Send("cookiesLogin", "loginSuccessful");
        return;
    }

    #endregion

}