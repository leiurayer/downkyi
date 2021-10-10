using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Video.Models
{
    // https://api.bilibili.com/x/player/pagelist
    public class VideoPagelist : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public List<VideoPage> Data { get; set; }
    }
}
