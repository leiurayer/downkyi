using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/space/channel/list?mid=
    [JsonObject]
    public  class SpaceChannelOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public SpaceChannel Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class SpaceChannel : BaseEntity
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("list")]
        public List<SpaceChannelList> List { get; set; }
    }

    [JsonObject]
    public class SpaceChannelList : BaseEntity
    {
        [JsonProperty("cid")]
        public long Cid { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("intro")]
        public string Intro { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("mtime")]
        public long Mtime { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

}
