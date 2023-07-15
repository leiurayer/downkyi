using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.Bili;
using Downkyi.Core.Bili.Web;
using Downkyi.Core.Log;
using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.Login;

public partial class QRCodeViewModel : ViewModelBase
{
    public const string Key = "Login_QRCode";

    private CancellationTokenSource? tokenSource;

    protected Action<string>? _QRCodeAction;

    protected Action<string>? _loginAction;

    protected string GetLoginUrlFailed = string.Empty;

    protected string LoginKeyError = string.Empty;

    protected string LoginTimeOut = string.Empty;

    protected string LoginSuccessful = string.Empty;

    protected string LoginFailed = string.Empty;

    #region 页面属性申明

    [ObservableProperty]
    private double _QRCodeOpacity;

    [ObservableProperty]
    private bool _QRCodeStatus;

    #endregion

    public QRCodeViewModel(BaseServices baseServices) : base(baseServices)
    {
        GetLoginUrlFailed = DictionaryResource.GetString("GetLoginUrlFailed");
        LoginKeyError = DictionaryResource.GetString("LoginKeyError");
        LoginTimeOut = DictionaryResource.GetString("LoginTimeOut");
        LoginSuccessful = DictionaryResource.GetString("LoginSuccessful");
        LoginFailed = DictionaryResource.GetString("LoginFailed");
    }

    #region 命令申明

    [RelayCommand]
    private async Task OnLoaded()
    {
        // 初始化状态
        InitStatus();

        await Task.Run(Login, (tokenSource = new CancellationTokenSource()).Token);
    }

    [RelayCommand]
    private void OnUnloaded()
    {
        // 初始化状态
        InitStatus();

        // 结束任务
        tokenSource?.Cancel();
    }

    #endregion

    #region 业务逻辑

    /// <summary>
    /// 登录
    /// </summary>
    private async Task Login()
    {
        try
        {
            var qrcode = BiliLocator.Login.GetQRCodeUrl();
            if (qrcode == null)
            {
                OnUnloaded();
                BroadcastEvent.Send("qrcodeLogin", "getLoginUrlFailed");
                NotificationEvent.Publish(GetLoginUrlFailed);
                return;
            }

            // 传递给主项目生成二维码
            _QRCodeAction?.Invoke(qrcode.Item1);

            Log.Logger.Debug($"{Key}: {qrcode.Item1}");

            // poll
            await GetLoginStatus(qrcode.Item2);
        }
        catch (Exception e)
        {
            Log.Logger.Error(e);
        }
    }

    /// <summary>
    /// 循环查询登录状态
    /// </summary>
    /// <param name="oauthKey"></param>
    private async Task GetLoginStatus(string oauthKey)
    {
        CancellationToken cancellationToken = tokenSource!.Token;
        while (true)
        {
            await Task.Delay(1000);
            var loginStatus = BiliLocator.Login.PollQRCode(oauthKey);
            if (loginStatus == null) { continue; }

            switch (loginStatus.Code)
            {
                case 0: // 扫码登录成功

                    // 发送通知
                    NotificationEvent.Publish(LoginSuccessful);
                    Log.Logger.Info($"{Key}: {LoginSuccessful}");

                    // 保存登录信息
                    try
                    {
                        bool isSucceed = LoginHelper.SaveLoginInfoCookies(loginStatus.Url);
                        if (!isSucceed)
                        {
                            NotificationEvent.Publish(LoginFailed);
                            Log.Logger.Error($"{Key}: {LoginFailed}");
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Logger.Error(e);
                        NotificationEvent.Publish(LoginFailed);
                    }

                    // 取消任务
                    await Task.Delay(3000);

                    OnUnloaded();
                    BroadcastEvent.Send("qrcodeLogin", "loginSuccessful");

                    break;
                case 86038: // 二维码已失效
                    // 发送通知
                    NotificationEvent.Publish(LoginTimeOut);
                    Log.Logger.Info($"{Key}: {LoginTimeOut}");
                    // 取消任务
                    tokenSource.Cancel();
                    // 创建新任务
                    await Task.Run(Login, (tokenSource = new CancellationTokenSource()).Token);
                    break;
                case 86090: // 二维码已扫码未确认
                    QRCodeOpacity = 0.1;
                    QRCodeStatus = true;
                    break;
                case 86101: // 未扫码
                    break;
            }

            BroadcastEvent.Receive("login",
                new Action<object>((obj) =>
                {
                    string? v = obj as string;
                    if (v == "backward")
                    {
                        // 取消任务
                        tokenSource.Cancel();
                    }
                })
            );

            // 判断是否该结束线程，若为true，跳出while循环
            if (cancellationToken.IsCancellationRequested)
            {
                Log.Logger.Debug($"{Key}: 登录操作结束");
                break;
            }
        }
    }

    /// <summary>
    /// 初始化状态
    /// </summary>
    protected virtual void InitStatus()
    {
        QRCodeOpacity = 1;
        QRCodeStatus = false;
    }

    #endregion

}