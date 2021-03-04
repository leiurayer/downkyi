using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.entity2.history
{
    // https://api.bilibili.com/x/v2/history/toview/web
    [JsonObject]
    public class ToViewOrigin : BaseEntity
    {
        //public int code { get; set; }
        [JsonProperty("data")]
        public ToViewData Data { get; set; }
        //public string message { get; set; }
        //public int ttl { get; set; }
    }

    [JsonObject]
    public class ToViewData : BaseEntity
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("list")]
        public List<ToViewDataList> List { get; set; }
    }

    [JsonObject]
    public class ToViewDataList : BaseEntity
    {
        [JsonProperty("add_at")]
        public long AddAt { get; set; }
        [JsonProperty("aid")]
        public long Aid { get; set; }
        //public long attribute { get; set; }
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
        [JsonProperty("cid")]
        public long Cid { get; set; }
        // ...
        [JsonProperty("owner")]
        public ToViewDataListOwner Owner { get; set; }
        // ...
        [JsonProperty("pic")]
        public string Cover { get; set; }
        // ...
        [JsonProperty("title")]
        public string Title { get; set; }
    }

    [JsonObject]
    public class ToViewDataListOwner : BaseEntity
    {
        [JsonProperty("face")]
        public string Face { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

}
