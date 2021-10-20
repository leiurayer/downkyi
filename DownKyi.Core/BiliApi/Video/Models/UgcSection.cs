using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Video.Models
{
    public class UgcSection : BaseModel
    {
        [JsonProperty("season_id")]
        public long SeasonId { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("episodes")]
        public List<UgcEpisode> Episodes { get; set; }
    }
}
