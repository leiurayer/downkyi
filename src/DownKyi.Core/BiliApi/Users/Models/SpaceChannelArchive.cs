using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpaceChannelArchive : BaseModel
    {
        [JsonProperty("aid")]
        public long Aid { get; set; }
        // videos
        [JsonProperty("tid")]
        public int Tid { get; set; }
        [JsonProperty("tname")]
        public string Tname { get; set; }
        // copyright
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
        // state
        [JsonProperty("duration")]
        public long Duration { get; set; }
        // mission_id
        // rights
        [JsonProperty("owner")]
        public VideoOwner Owner { get; set; }
        [JsonProperty("stat")]
        public SpaceChannelArchiveStat Stat { get; set; }
        // dynamic
        [JsonProperty("cid")]
        public long Cid { get; set; }
        [JsonProperty("dimension")]
        public Dimension Dimension { get; set; }
        // season_id
        // short_link_v2
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
        // inter_video
        // is_live_playback
    }
}
