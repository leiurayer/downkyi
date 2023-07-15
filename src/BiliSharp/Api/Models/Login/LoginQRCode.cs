using System.Text.Json.Serialization;

namespace BiliSharp.Api.Models.Login
{
    /// <summary>
    /// https://passport.bilibili.com/x/passport-login/web/qrcode/generate
    /// </summary>
    public class LoginQRCode
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ttl")]
        public int Ttl { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("data")]
        public LoginQRCodeData Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LoginQRCodeData
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("qrcode_key")]
        public string QrcodeKey { get; set; }
    }
}