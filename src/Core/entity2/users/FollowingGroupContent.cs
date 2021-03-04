using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/relation/tag?tagid={tagId}&pn={pn}&ps={ps}&order_type={orderType}
    [JsonObject]
    public   class FollowingGroupContentOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public List<FollowingGroupContent> Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class FollowingGroupContent : BaseEntity
    {
        [JsonProperty("attribute")]
        public int Attribute { get; set; }
        // ...
        [JsonProperty("face")]
        public string Face { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
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
