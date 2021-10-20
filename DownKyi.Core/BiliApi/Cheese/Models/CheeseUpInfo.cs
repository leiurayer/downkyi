using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Cheese.Models
{
    public class CheeseUpInfo : BaseModel
    {
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("brief")]
        public string Brief { get; set; }
        [JsonProperty("follower")]
        public long Follower { get; set; }
        [JsonProperty("is_follow")]
        public int IsFollow { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("uname")]
        public string Name { get; set; }
    }
}
