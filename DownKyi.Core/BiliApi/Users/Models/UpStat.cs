using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/space/upstat?mid={mid}
    public class UpStatOrigin : BaseModel
    {
        [JsonProperty("data")]
        public UpStat Data { get; set; }
    }

    public class UpStat : BaseModel
    {
        [JsonProperty("archive")]
        public UpStatArchive Archive { get; set; }
        [JsonProperty("article")]
        public UpStatArchive Article { get; set; }
        [JsonProperty("likes")]
        public long Likes { get; set; }
    }

    public class UpStatArchive : BaseModel
    {
        [JsonProperty("view")]
        public long View { get; set; } // 视频/文章播放量
    }
}
