using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Bangumi.Models
{
    public class BangumiUpInfo : BaseModel
    {
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        // follower
        // is_follow
        [JsonProperty("mid")]
        public long Mid { get; set; }
        // pendant
        // theme_type
        [JsonProperty("uname")]
        public string Name { get; set; }
        // verify_type
        // vip_status
        // vip_type
    }
}
