using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/relation/tags
    [JsonObject]
    public class FollowingGroupOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public List<FollowingGroup> Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class FollowingGroup : BaseEntity
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("tagid")]
        public int TagId { get; set; }
        [JsonProperty("tip")]
        public string Tip { get; set; }
    }

}
