using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Login.Models
{
    // https://passport.bilibili.com/qrcode/getLoginInfo
    [JsonObject]
    public class LoginStatus : BaseModel
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
    public class LoginStatusScanning : BaseModel
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("data")]
        public int Data { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

    [JsonObject]
    public class LoginStatusReady : BaseModel
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
    public class LoginStatusData : BaseModel
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
