using Newtonsoft.Json;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/space/upstat?mid={mid}
    [JsonObject]
    public class UpStatOrigin : BaseEntity
    {
        [JsonProperty("data")]
        public UpStat Data { get; set; }
    }

    [JsonObject]
    public class UpStat : BaseEntity
    {
        [JsonProperty("archive")]
        public UpStatArchive Archive { get; set; }
        [JsonProperty("article")]
        public UpStatArchive Article { get; set; }
        [JsonProperty("likes")]
        public long Likes { get; set; }
    }

    [JsonObject]
    public class UpStatArchive : BaseEntity
    {
        [JsonProperty("view")]
        public long View { get; set; } // 视频播放量
    }

}
