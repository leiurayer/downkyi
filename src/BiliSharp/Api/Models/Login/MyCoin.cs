using System.Text.Json.Serialization;

namespace BiliSharp.Api.Models.Login
{
    /// <summary>
    /// https://account.bilibili.com/site/getCoin
    /// </summary>
    public class MyCoin
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("status")]
        public bool Status { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("data")]
        public MyCoinData Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyCoinData
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("money")]
        public int Money { get; set; }
    }
}