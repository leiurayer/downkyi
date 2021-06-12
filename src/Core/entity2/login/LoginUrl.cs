using Newtonsoft.Json;

namespace Core.entity2.login
{
    // https://passport.bilibili.com/qrcode/getLoginUrl
    [JsonObject]
    public class LoginUrlOrigin : BaseEntity
    {
        //public int code { get; set; }
        [JsonProperty("data")]
        public LoginUrl Data { get; set; }
        [JsonProperty("status")]
        public bool Status { get; set; }
        //public long ts { get; set; }
    }

    [JsonObject]
    public class LoginUrl : BaseEntity
    {
        [JsonProperty("oauthKey")]
        public string OauthKey { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }

}
