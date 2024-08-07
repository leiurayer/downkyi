using Downkyi.BiliSharp;
using Downkyi.BiliSharp.Api.Login;
using Downkyi.Core.Bili.Models;
using Downkyi.Core.Settings;

namespace Downkyi.Core.Bili.Web;

internal class Login : ILogin
{
    /// <summary>
    /// 申请二维码(web端)
    /// </summary>
    /// <returns>(url, key)</returns>
    public Tuple<string, string>? GetQRCodeUrl()
    {
        string userAgent = SettingsManager.Instance.GetUserAgent();
        BiliManager.Instance().SetUserAgent(userAgent);

        var qrcode = LoginQR.GenerateQRCode();
        if (qrcode == null || qrcode.Data == null) { return null; }

        return Tuple.Create(qrcode.Data.Url, qrcode.Data.QrcodeKey);
    }

    /// <summary>
    /// 扫码登录(web端)
    /// </summary>
    /// <returns></returns>
    public QRCodeStatus? PollQRCode(string qrcodeKey)
    {
        string userAgent = SettingsManager.Instance.GetUserAgent();
        BiliManager.Instance().SetUserAgent(userAgent);

        var qrcode = LoginQR.PollQRCode(qrcodeKey);
        if (qrcode == null || qrcode.Data == null) { return null; }

        return new QRCodeStatus()
        {
            Url = qrcode.Data.Url,
            Token = qrcode.Data.RefreshToken,
            Timestamp = qrcode.Data.Timestamp,
            Code = qrcode.Data.Code,
            Message = qrcode.Data.Message
        };
    }

    /// <summary>
    /// 导航栏用户信息
    /// </summary>
    /// <returns></returns>
    public NavigationInfo? GetNavigationInfo()
    {
        string userAgent = SettingsManager.Instance.GetUserAgent();
        BiliManager.Instance().SetUserAgent(userAgent);
        BiliManager.Instance().SetCookies(LoginHelper.GetLoginInfoCookies());

        var origin = LoginInfo.GetNavigationInfo();
        if (origin == null || origin.Data == null)
        {
            return null;
        }

        return new NavigationInfo()
        {
            Mid = origin.Data.Mid,
            Name = origin.Data.Uname,
            Header = origin.Data.Face,
            VipStatus = origin.Data.Vipstatus,
            IsLogin = origin.Data.Islogin
        };
    }
}