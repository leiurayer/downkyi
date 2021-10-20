using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Bangumi.Models
{
    public class BangumiStat : BaseModel
    {
        [JsonProperty("coins")]
        public long Coins { get; set; }
        [JsonProperty("danmakus")]
        public long Danmakus { get; set; }
        [JsonProperty("favorite")]
        public long Favorite { get; set; }
        [JsonProperty("favorites")]
        public long Favorites { get; set; }
        [JsonProperty("likes")]
        public long Likes { get; set; }
        [JsonProperty("reply")]
        public long Reply { get; set; }
        [JsonProperty("share")]
        public long Share { get; set; }
        [JsonProperty("views")]
        public long Views { get; set; }
    }
}
