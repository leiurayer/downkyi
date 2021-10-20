using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Video.Models
{
    public class VideoPage : BaseModel
    {
        [JsonProperty("cid")]
        public long Cid { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("from")]
        public string From { get; set; }
        [JsonProperty("part")]
        public string Part { get; set; }
        [JsonProperty("duration")]
        public long Duration { get; set; }
        [JsonProperty("vid")]
        public string Vid { get; set; }
        [JsonProperty("weblink")]
        public string Weblink { get; set; }
        [JsonProperty("dimension")]
        public Dimension Dimension { get; set; }
    }
}
