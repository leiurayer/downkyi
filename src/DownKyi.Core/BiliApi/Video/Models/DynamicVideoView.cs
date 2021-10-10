using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Video.Models
{
    public class DynamicVideoView : BaseModel
    {
        [JsonProperty("aid")]
        public long Aid { get; set; }
        [JsonProperty("videos")]
        public int Videos { get; set; }
        [JsonProperty("tid")]
        public int Tid { get; set; }
        [JsonProperty("tname")]
        public string Tname { get; set; }
        [JsonProperty("copyright")]
        public int Copyright { get; set; }
        [JsonProperty("pic")]
        public string Pic { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("pubdate")]
        public long Pubdate { get; set; }
        [JsonProperty("ctime")]
        public long Ctime { get; set; }
        [JsonProperty("desc")]
        public string Desc { get; set; }
        [JsonProperty("state")]
        public int State { get; set; }
        [JsonProperty("duration")]
        public long Duration { get; set; }
        [JsonProperty("owner")]
        public VideoOwner Owner { get; set; }
        [JsonProperty("stat")]
        public VideoStat Stat { get; set; }
        [JsonProperty("dynamic")]
        public string Dynamic { get; set; }
        [JsonProperty("cid")]
        public long Cid { get; set; }
        [JsonProperty("dimension")]
        public Dimension Dimension { get; set; }
        [JsonProperty("short_link")]
        public string ShortLink { get; set; }
        [JsonProperty("short_link_v2")]
        public string ShortLinkV2 { get; set; }
        [JsonProperty("first_frame")]
        public string FirstFrame { get; set; }
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
        [JsonProperty("season_type")]
        public int SeasonType { get; set; }
        [JsonProperty("is_ogv")]
        public bool IsOgv { get; set; }
    }
}
