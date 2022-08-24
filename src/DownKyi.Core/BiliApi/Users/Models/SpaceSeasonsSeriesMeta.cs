using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpaceSeasonsSeriesMeta : BaseModel
    {
        [JsonProperty("category")]
        public int Category { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class SpaceSeasonsMeta : SpaceSeasonsSeriesMeta
    {
        [JsonProperty("ptime")]
        public long Ptime { get; set; }
        [JsonProperty("season_id")]
        public long SeasonId { get; set; }
    }

    public class SpaceSeriesMeta : SpaceSeasonsSeriesMeta
    {
        [JsonProperty("creator")]
        public string Creator { get; set; }
        [JsonProperty("ctime")]
        public long Ctime { get; set; }
        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; }
        [JsonProperty("last_update_ts")]
        public long LastUpdate { get; set; }
        [JsonProperty("mtime")]
        public long Mtime { get; set; }
        [JsonProperty("raw_keywords")]
        public string RawKeywords { get; set; }
        [JsonProperty("series_id")]
        public long SeriesId { get; set; }
        [JsonProperty("state")]
        public int State { get; set; }
    }

}
