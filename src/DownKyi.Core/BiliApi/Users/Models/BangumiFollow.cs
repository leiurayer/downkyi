using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class BangumiFollow : BaseModel
    {
        [JsonProperty("season_id")]
        public long SeasonId { get; set; }
        [JsonProperty("media_id")]
        public long MediaId { get; set; }
        [JsonProperty("season_type")]
        public int SeasonType { get; set; }
        [JsonProperty("season_type_name")]
        public string SeasonTypeName { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        // is_finish
        // is_started
        // is_play
        [JsonProperty("badge")]
        public string Badge { get; set; }
        [JsonProperty("badge_type")]
        public int BadgeType { get; set; }
        // rights
        // stat
        [JsonProperty("new_ep")]
        public BangumiFollowNewEp NewEp { get; set; }
        // rating
        // square_cover
        [JsonProperty("season_status")]
        public int SeasonStatus { get; set; }
        [JsonProperty("season_title")]
        public string SeasonTitle { get; set; }
        [JsonProperty("badge_ep")]
        public string BadgeEp { get; set; }
        // media_attr
        // season_attr
        [JsonProperty("evaluate")]
        public string Evaluate { get; set; }
        [JsonProperty("areas")]
        public List<BangumiFollowAreas> Areas { get; set; }
        [JsonProperty("subtitle")]
        public string Subtitle { get; set; }
        [JsonProperty("first_ep")]
        public long FirstEp { get; set; }
        // can_watch
        // series
        // publish
        // mode
        // section
        [JsonProperty("url")]
        public string Url { get; set; }
        // badge_info
        // first_ep_info
        // formal_ep_count
        // short_url
        // badge_infos
        // season_version
        // horizontal_cover_16_9
        // horizontal_cover_16_10
        // subtitle_14
        // viewable_crowd_type
        // producers
        // follow_status
        // is_new
        [JsonProperty("progress")]
        public string Progress { get; set; }
        // both_follow
    }
}
