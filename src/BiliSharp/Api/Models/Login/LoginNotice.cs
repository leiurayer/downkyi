using System.Text.Json.Serialization;

namespace BiliSharp.Api.Models.Login
{
    /// <summary>
    /// https://api.bilibili.com/x/safecenter/login_notice?mid=
    /// </summary>
    public class LoginNotice
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
        public LoginNoticeData Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LoginNoticeData
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mid")]
        public long Mid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("device_name")]
        public string DeviceName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("login_type")]
        public string LoginType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("login_time")]
        public string LoginTime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("location")]
        public string Location { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ip")]
        public string Ip { get; set; }
    }
}