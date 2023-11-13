using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.LoginNew.Models
{
    // https://passport.bilibili.com/qrcode/getLoginUrl
    [JsonObject]
    public class LoginUrlOrigin : BaseModel
    {
        //public int code { get; set; }
        [JsonProperty("data")]
        public LoginUrl Data { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        //public long ts { get; set; }
    }

    [JsonObject]
    public class LoginUrl : BaseModel
    {
        [JsonProperty("qrcode_key")]
        public string QrcodeKey { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
