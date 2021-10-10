using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Bangumi.Models
{
    // https://api.bilibili.com/pgc/view/web/season
    public class BangumiSeasonOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("result")]
        public BangumiSeason Result { get; set; }
    }

    public class BangumiSeason : BaseModel
    {
        // activity
        // alias
        [JsonProperty("areas")]
        public List<BangumiArea> Areas { get; set; }
        [JsonProperty("bkg_cover")]
        public string BkgCover { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("episodes")]
        public List<BangumiEpisode> Episodes { get; set; }
        [JsonProperty("evaluate")]
        public string Evaluate { get; set; }
        // freya
        // jp_title
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("media_id")]
        public long MediaId { get; set; }
        // mode
        // new_ep
        // payment
        [JsonProperty("positive")]
        public BangumiPositive Positive { get; set; }
        // publish
        // rating
        // record
        // rights
        [JsonProperty("season_id")]
        public long SeasonId { get; set; }
        [JsonProperty("season_title")]
        public string SeasonTitle { get; set; }
        [JsonProperty("seasons")]
        public List<BangumiSeasonInfo> Seasons { get; set; }
        [JsonProperty("section")]
        public List<BangumiSection> Section { get; set; }
        // series
        // share_copy
        // share_sub_title
        // share_url
        // show
        [JsonProperty("square_cover")]
        public string SquareCover { get; set; }
        [JsonProperty("stat")]
        public BangumiStat Stat { get; set; }
        // status
        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("up_info")]
        public BangumiUpInfo UpInfo { get; set; }
    }

}
