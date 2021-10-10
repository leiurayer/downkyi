using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.VideoStream.Models
{
    public class PlayUrlOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public PlayUrl Data { get; set; }
        [JsonProperty("result")]
        public PlayUrl Result { get; set; }
    }

    public class PlayUrl : BaseModel
    {
        // from
        // result
        // message
        // quality
        // format
        // timelength
        // accept_format
        [JsonProperty("accept_description")]
        public List<string> AcceptDescription { get; set; }
        [JsonProperty("accept_quality")]
        public List<int> AcceptQuality { get; set; }
        // video_codecid
        // seek_param
        // seek_type
        [JsonProperty("durl")]
        public List<PlayUrlDurl> Durl { get; set; }
        [JsonProperty("dash")]
        public PlayUrlDash Dash { get; set; }
        [JsonProperty("support_formats")]
        public List<PlayUrlSupportFormat> SupportFormats { get; set; }
        // high_format
    }

}
