using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpaceCheese : BaseModel
    {
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("ep_count")]
        public int EpCount { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("play")]
        public int Play { get; set; }
        [JsonProperty("season_id")]
        public long SeasonId { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("subtitle")]
        public string SubTitle { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
