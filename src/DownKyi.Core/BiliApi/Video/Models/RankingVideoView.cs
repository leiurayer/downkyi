using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Video.Models
{
    public class RankingVideoView : BaseModel
    {
        [JsonProperty("aid")]
        public long Aid { get; set; }
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
        [JsonProperty("typename")]
        public string TypeName { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }
        [JsonProperty("play")]
        public long Play { get; set; }
        [JsonProperty("review")]
        public long Review { get; set; }
        [JsonProperty("video_review")]
        public long VideoReview { get; set; }
        [JsonProperty("favorites")]
        public long Favorites { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("create")]
        public string Create { get; set; }
        [JsonProperty("pic")]
        public string Pic { get; set; }
        [JsonProperty("coins")]
        public long Coins { get; set; }
        [JsonProperty("duration")]
        public string Duration { get; set; }
        [JsonProperty("badgepay")]
        public bool Badgepay { get; set; }
        [JsonProperty("pts")]
        public long Pts { get; set; }
        [JsonProperty("redirect_url")]
        public string RedirectUrl { get; set; }
    }
}
