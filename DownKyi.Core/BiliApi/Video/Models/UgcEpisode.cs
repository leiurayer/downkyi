using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Video.Models
{
    public class UgcEpisode : BaseModel
    {
        [JsonProperty("season_id")]
        public long SeasonId { get; set; }
        [JsonProperty("section_id")]
        public long SectionId { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("aid")]
        public long Aid { get; set; }
        [JsonProperty("cid")]
        public long Cid { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("attribute")]
        public int Attribute { get; set; }
        [JsonProperty("arc")]
        public UgcArc Arc { get; set; }
        [JsonProperty("page")]
        public VideoPage Page { get; set; }
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
    }
}
