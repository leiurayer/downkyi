using System.Text.Json.Serialization;

namespace BiliSharp.Api.Models.User
{
    /// <summary>
    /// https://passport.bilibili.com/web/generic/check/nickname?nickName=hahaha
    /// </summary>
    public class Nickname
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
    }
}