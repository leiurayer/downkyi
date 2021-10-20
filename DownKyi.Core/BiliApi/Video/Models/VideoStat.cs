using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Video.Models
{
    public class VideoStat : BaseModel
    {
        [JsonProperty("aid")]
        public long Aid { get; set; }
        [JsonProperty("view")]
        public long View { get; set; }
        [JsonProperty("danmaku")]
        public long Danmaku { get; set; }
        [JsonProperty("reply")]
        public long Reply { get; set; }
        [JsonProperty("favorite")]
        public long Favorite { get; set; }
        [JsonProperty("coin")]
        public long Coin { get; set; }
        [JsonProperty("share")]
        public long Share { get; set; }
        [JsonProperty("now_rank")]
        public long NowRank { get; set; }
        [JsonProperty("his_rank")]
        public long HisRank { get; set; }
        [JsonProperty("like")]
        public long Like { get; set; }
        [JsonProperty("dislike")]
        public long Dislike { get; set; }
        [JsonProperty("evaluation")]
        public string Evaluation { get; set; }
        [JsonProperty("argue_msg")]
        public string ArgueMsg { get; set; }
    }
}
