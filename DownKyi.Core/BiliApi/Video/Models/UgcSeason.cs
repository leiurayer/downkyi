using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Video.Models
{
    public class UgcSeason : BaseModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("intro")]
        public string Intro { get; set; }
        [JsonProperty("sign_state")]
        public int SignState { get; set; }
        [JsonProperty("attribute")]
        public int Attribute { get; set; }
        [JsonProperty("sections")]
        public List<UgcSection> Sections { get; set; }
        [JsonProperty("stat")]
        public UgcStat Stat { get; set; }
        [JsonProperty("ep_count")]
        public int EpCount { get; set; }
        [JsonProperty("season_type")]
        public int SeasonType { get; set; }
    }
}
