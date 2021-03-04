using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/relation/followers?vmid={mid}&pn={pn}&ps={ps}
    // https://api.bilibili.com/x/relation/followings?vmid={mid}&pn={pn}&ps={ps}&order_type={orderType}
    [JsonObject]
    public class RelationFollowOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public RelationFollow Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class RelationFollow : BaseEntity
    {
        [JsonProperty("list")]
        public List<RelationFollowInfo> List { get; set; }
        //[JsonProperty("re_version")]
        //public long reVersion { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }

    [JsonObject]
    public class RelationFollowInfo : BaseEntity
    {
        [JsonProperty("attribute")]
        public int Attribute { get; set; }
        // ...
        [JsonProperty("face")]
        public string Face { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("mtime")]
        public long Mtime { get; set; }
        // ...
        [JsonProperty("sign")]
        public string Sign { get; set; }
        [JsonProperty("special")]
        public int Special { get; set; }
        [JsonProperty("tag")]
        public List<long> Tag { get; set; }
        [JsonProperty("uname")]
        public string Name { get; set; }
        // ...
    }

}
