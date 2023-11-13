using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.LoginNew.Models
{
    [JsonObject]
    public class LoginStatus : BaseModel
    {
        [JsonProperty("code")] public int Code { get; set; }
        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("data")] public LoginStatusData Data { get; set; }
    }

    [JsonObject]
    public class LoginStatusData : BaseModel
    {
        [JsonProperty("url")] public string Url { get; set; }
        [JsonProperty("refresh_token")] public string RefreshToken { get; set; }
        [JsonProperty("code")] public int Code { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
    }
}