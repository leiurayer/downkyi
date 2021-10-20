using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Video.Models
{
    // https://api.bilibili.com/x/web-interface/view
    public class VideoViewOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public VideoView Data { get; set; }
    }

    public class VideoView : BaseModel
    {
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
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
        [JsonProperty("redirect_url")]
        public string RedirectUrl { get; set; }
        [JsonProperty("mission_id")]
        public long MissionId { get; set; }
        //[JsonProperty("rights")]
        //public VideoRights Rights { get; set; }
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
        [JsonProperty("season_id")]
        public long SeasonId { get; set; }
        [JsonProperty("festival_jump_url")]
        public string FestivalJumpUrl { get; set; }
        //[JsonProperty("no_cache")]
        //public bool no_cache { get; set; }
        [JsonProperty("pages")]
        public List<VideoPage> Pages { get; set; }
        [JsonProperty("subtitle")]
        public VideoSubtitle Subtitle { get; set; }
        [JsonProperty("ugc_season")]
        public UgcSeason UgcSeason { get; set; }
        //[JsonProperty("staff")]
        //public List<Staff> staff { get; set; }
        //[JsonProperty("user_garb")]
        //public user_garb user_garb { get; set; }
    }
}
