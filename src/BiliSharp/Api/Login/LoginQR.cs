using BiliSharp.Api.Models.Login;

namespace BiliSharp.Api.Login
{
    /// <summary>
    /// 二维码登录
    /// </summary>
    public static class LoginQR
    {
        /// <summary>
        /// 申请二维码(web端)
        /// </summary>
        /// <returns></returns>
        public static LoginQRCode GenerateQRCode()
        {
            string url = "https://passport.bilibili.com/x/passport-login/web/qrcode/generate";
            return Utils.GetData<LoginQRCode>(url);
        }

        /// <summary>
        /// 扫码登录(web端)
        /// </summary>
        /// <returns></returns>
        public static LoginQRCodePoll PollQRCode(string qrcodeKey)
        {
            string url = $"https://passport.bilibili.com/x/passport-login/web/qrcode/poll?qrcode_key={qrcodeKey}";
            return Utils.GetData<LoginQRCodePoll>(url);
        }
    }
}