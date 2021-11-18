using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.VideoStream.Models
{
    // https://api.bilibili.com/x/player/v2?cid={cid}&aid={avid}&bvid={bvid}
    public class PlayerV2Origin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public PlayerV2 Data { get; set; }
    }

    public class PlayerV2 : BaseModel
    {
        [JsonProperty("aid")]
        public long Aid { get; set; }
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
        // allow_bp
        // no_share
        [JsonProperty("cid")]
        public long Cid { get; set; }
        // ...
        [JsonProperty("subtitle")]
        public SubtitleInfo Subtitle { get; set; }
    }

}
