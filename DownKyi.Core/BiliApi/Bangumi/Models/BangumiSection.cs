using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Bangumi.Models
{
    public class BangumiSection : BaseModel
    {
        [JsonProperty("episode_id")]
        public long EpisodeId { get; set; }
        [JsonProperty("episodes")]
        public List<BangumiEpisode> Episodes { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
    }
}
