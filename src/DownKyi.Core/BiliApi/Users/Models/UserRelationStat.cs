using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/relation/stat?vmid={mid}
    public class UserRelationStatOrigin : BaseModel
    {
        [JsonProperty("data")]
        public UserRelationStat Data { get; set; }
    }

    public class UserRelationStat : BaseModel
    {
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("following")]
        public long Following { get; set; } // 关注数
        [JsonProperty("whisper")]
        public long Whisper { get; set; }
        [JsonProperty("black")]
        public long Black { get; set; }
        [JsonProperty("follower")]
        public long Follower { get; set; } // 粉丝数
    }
}
