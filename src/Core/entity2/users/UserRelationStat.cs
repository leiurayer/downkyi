using Newtonsoft.Json;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/relation/stat?vmid={mid}
    [JsonObject]
    public class UserRelationStatOrigin : BaseEntity
    {
        [JsonProperty("data")]
        public UserRelationStat Data { get; set; }
    }

    [JsonObject]
    public class UserRelationStat : BaseEntity
    {
        [JsonProperty("black")]
        public long Black { get; set; }
        [JsonProperty("follower")]
        public long Follower { get; set; } // 粉丝数
        [JsonProperty("following")]
        public long Following { get; set; } // 关注数
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("whisper")]
        public long Whisper { get; set; }
    }

}
