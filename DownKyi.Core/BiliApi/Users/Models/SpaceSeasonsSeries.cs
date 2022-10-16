using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpaceSeasons : BaseModel
    {
        [JsonProperty("archives")]
        public List<SpaceSeasonsSeriesArchives> Archives { get; set; }
        [JsonProperty("meta")]
        public SpaceSeasonsMeta Meta { get; set; }
        [JsonProperty("recent_aids")]
        public List<long> RecentAids { get; set; }
    }

    public class SpaceSeries : BaseModel
    {
        [JsonProperty("archives")]
        public List<SpaceSeasonsSeriesArchives> Archives { get; set; }
        [JsonProperty("meta")]
        public SpaceSeriesMeta Meta { get; set; }
        [JsonProperty("recent_aids")]
        public List<long> RecentAids { get; set; }
    }
}
