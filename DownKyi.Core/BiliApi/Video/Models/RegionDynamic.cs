using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Video.Models
{
    // https://api.bilibili.com/x/web-interface/dynamic/region
    public class RegionDynamicOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public RegionDynamic Data { get; set; }
    }

    public class RegionDynamic : BaseModel
    {
        [JsonProperty("archives")]
        public List<DynamicVideoView> Archives { get; set; }
        // page
    }

}
