using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Favorites.Models
{
    public class FavoritesMedia : BaseModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("intro")]
        public string Intro { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("duration")]
        public long Duration { get; set; }
        [JsonProperty("upper")]
        public FavUpper Upper { get; set; }
        // attr
        [JsonProperty("cnt_info")]
        public MediaStatus CntInfo { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("ctime")]
        public long Ctime { get; set; }
        [JsonProperty("pubtime")]
        public long Pubtime { get; set; }
        [JsonProperty("fav_time")]
        public long FavTime { get; set; }
        [JsonProperty("bv_id")]
        public string BvId { get; set; }
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
        // season
        // ogv
        // ugc
    }
}
