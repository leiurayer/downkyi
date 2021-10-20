using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Bangumi.Models
{
    public class BangumiSeasonInfo : BaseModel
    {
        [JsonProperty("badge")]
        public string Badge { get; set; }
        // badge_info
        // badge_type
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("media_id")]
        public long MediaId { get; set; }
        // new_ep
        [JsonProperty("season_id")]
        public long SeasonId { get; set; }
        [JsonProperty("season_title")]
        public string SeasonTitle { get; set; }
        [JsonProperty("season_type")]
        public int SeasonType { get; set; }
        // stat
    }
}
