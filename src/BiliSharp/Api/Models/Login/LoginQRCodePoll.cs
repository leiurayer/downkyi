using System.Text.Json.Serialization;

namespace BiliSharp.Api.Models.Login
{
    /// <summary>
    /// https://passport.bilibili.com/x/passport-login/web/qrcode/poll?qrcode_key=
    /// </summary>
    public class LoginQRCodePoll
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
        public LoginQRCodePollData Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LoginQRCodePollData
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("code")]
        public long Code { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}