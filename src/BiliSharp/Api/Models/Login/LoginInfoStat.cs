using System.Text.Json.Serialization;

namespace BiliSharp.Api.Models.Login
{
    /// <summary>
    /// https://api.bilibili.com/x/web-interface/nav/stat
    /// </summary>
    public class LoginInfoStat
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
        public LoginInfoStatData Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LoginInfoStatData
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("following")]
        public int Following { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("follower")]
        public int Follower { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dynamic_count")]
        public int DynamicCount { get; set; }
    }
}