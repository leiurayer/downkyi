using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.History.Models
{
    // https://api.bilibili.com/x/v2/history/toview
    public class ToViewOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public ToViewData Data { get; set; }
    }

    public class ToViewData : BaseModel
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("list")]
        public List<ToViewList> List { get; set; }
    }
}
