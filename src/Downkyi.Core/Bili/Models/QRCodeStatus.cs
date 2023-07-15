namespace Downkyi.Core.Bili.Models;

public class QRCodeStatus
{
    public string Url { get; set; }
    public string Token { get; set; }
    public long Timestamp { get; set; }

    /// <summary>
    /// 0：扫码登录成功
    /// 86038：二维码已失效
    /// 86090：二维码已扫码未确认
    /// 86101：未扫码
    /// </summary>
    public long Code { get; set; }
    public string Message { get; set; }
}