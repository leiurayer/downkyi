using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/relation/whispers?pn={pn}&ps={ps}
    [JsonObject]
    public class RelationWhisperOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public List<RelationWhisper> Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    public class RelationWhisper : BaseEntity
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
