using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class BangumiFollowNewEp : BaseModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("index_show")]
        public string IndexShow { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("long_title")]
        public string LongTitle { get; set; }
        [JsonProperty("pub_time")]
        public string PubTime { get; set; }
        [JsonProperty("duration")]
        public long Duration { get; set; }
    }
}
