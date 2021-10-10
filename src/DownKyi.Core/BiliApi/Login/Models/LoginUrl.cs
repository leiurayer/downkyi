using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Login.Models
{
    // https://passport.bilibili.com/qrcode/getLoginUrl
    [JsonObject]
    public class LoginUrlOrigin : BaseModel
    {
        //public int code { get; set; }
        [JsonProperty("data")]
        public LoginUrl Data { get; set; }
        [JsonProperty("status")]
        public bool Status { get; set; }
        //public long ts { get; set; }
    }

    [JsonObject]
    public class LoginUrl : BaseModel
    {
        [JsonProperty("oauthKey")]
        public string OauthKey { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
