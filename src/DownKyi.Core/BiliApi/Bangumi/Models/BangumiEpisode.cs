using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Bangumi.Models
{
    public class BangumiEpisode : BaseModel
    {
        [JsonProperty("aid")]
        public long Aid { get; set; }
        [JsonProperty("badge")]
        public string Badge { get; set; }
        // badge_info
        // badge_type
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
        [JsonProperty("cid")]
        public long Cid { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("dimension")]
        public Dimension Dimension { get; set; }
        [JsonProperty("duration")]
        public long Duration { get; set; }
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("long_title")]
        public string LongTitle { get; set; }
        [JsonProperty("pub_time")]
        public long PubTime { get; set; }
        // pv
        // release_date
        // rights
        [JsonProperty("share_copy")]
        public string ShareCopy { get; set; }
        [JsonProperty("share_url")]
        public string ShareUrl { get; set; }
        [JsonProperty("short_link")]
        public string ShortLink { get; set; }
        // stat
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("vid")]
        public string Vid { get; set; }
    }
}
