using Downkyi.Core.Bili.Models;

namespace Downkyi.Core.Bili;

public interface ILogin
{
    /// <summary>
    /// 申请二维码
    /// </summary>
    /// <returns>(url, key)</returns>
    Tuple<string, string>? GetQRCodeUrl();

    /// <summary>
    /// 扫码登录
    /// </summary>
    /// <param name="qrcodeKey"></param>
    /// <returns></returns>
    QRCodeStatus? PollQRCode(string qrcodeKey);

    /// <summary>
    /// 导航栏用户信息
    /// </summary>
    /// <returns></returns>
    NavigationInfo? GetNavigationInfo();
}