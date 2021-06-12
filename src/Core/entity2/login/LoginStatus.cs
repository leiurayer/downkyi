using Newtonsoft.Json;

namespace Core.entity2.login
{
    // https://passport.bilibili.com/qrcode/getLoginInfo
    [JsonObject]
    public class LoginStatus : BaseEntity
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    [JsonObject]
    public class LoginStatusScanning : BaseEntity
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("data")]
        public int Data { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    [JsonObject]
    public class LoginStatusReady : BaseEntity
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("status")]
        public bool Status { get; set; }
        //public long ts { get; set; }
        [JsonProperty("data")]
        public LoginStatusData Data { get; set; }
    }

    [JsonObject]
    public class LoginStatusData : BaseEntity
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }

}
