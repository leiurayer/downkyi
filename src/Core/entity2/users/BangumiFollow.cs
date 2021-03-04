using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/space/bangumi/follow/list?vmid={mid}&type={type}&pn={pn}&ps={ps}
    [JsonObject]
    public class BangumiFollowOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public BangumiFollowData Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class BangumiFollowData : BaseEntity
    {
        [JsonProperty("list")]
        public List<BangumiFollow> List { get; set; }
        [JsonProperty("pn")]
        public int Pn { get; set; }
        [JsonProperty("ps")]
        public int Ps { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }

    [JsonObject]
    public class BangumiFollow : BaseEntity
    {
        [JsonProperty("areas")]
        public List<BangumiFollowAreas> Areas { get; set; }
        [JsonProperty("badge")]
        public string Badge { get; set; }
        [JsonProperty("badge_ep")]
        public string BadgeEp { get; set; }
        // ...
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("evaluate")]
        public string Evaluate { get; set; }
        // ...
        [JsonProperty("media_id")] 
        public long MediaId { get; set; }
        // ...
        [JsonProperty("new_ep")] 
        public BangumiFollowNewEp NewEp { get; set; }
        [JsonProperty("progress")] 
        public string Progress { get; set; }
        // ...
        [JsonProperty("season_id")] 
        public long SeasonId { get; set; }
        [JsonProperty("season_status")]
        public int SeasonStatus { get; set; }
        [JsonProperty("season_title")]
        public string SeasonTitle { get; set; }
        [JsonProperty("season_type")]
        public int SeasonType { get; set; }
        [JsonProperty("season_type_name")]
        public string SeasonTypeName { get; set; }
        // ...
        [JsonProperty("title")] 
        public string Title { get; set; }
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    [JsonObject]
    public class BangumiFollowAreas : BaseEntity
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    [JsonObject]
    public class BangumiFollowNewEp : BaseEntity
    {
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("duration")]
        public long Duration { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("index_show")]
        public string IndexShow { get; set; }
        [JsonProperty("long_title")]
        public string LongTitle { get; set; }
        [JsonProperty("pub_time")]
        public string PubTime { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }

}
