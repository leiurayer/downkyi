using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.entity2.users
{
    // https://api.bilibili.com/pugv/app/web/season/page?mid={mid}&pn={pn}&ps={ps}
    [JsonObject]
    public class SpaceCheeseOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public SpaceCheeseList Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class SpaceCheeseList : BaseEntity
    {
        [JsonProperty("items")]
        public List<SpaceCheese> Items { get; set; }
        [JsonProperty("page")]
        public SpaceCheesePage Page { get; set; }
    }

    [JsonObject]
    public class SpaceCheese : BaseEntity
    {
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("ep_count")]
        public int EpCount { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("play")]
        public int Play { get; set; }
        [JsonProperty("season_id")]
        public long SeasonId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("subtitle")]
        public string SubTitle { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }

    [JsonObject]
    public class SpaceCheesePage : BaseEntity
    {
        [JsonProperty("next")]
        public bool Next { get; set; }
        [JsonProperty("num")]
        public int Num { get; set; }
        [JsonProperty("size")]
        public int Size { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }

}
