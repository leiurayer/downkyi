using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpaceSeasonsSeriesArchives : BaseModel
    {
        [JsonProperty("aid")]
        public long Aid { get; set; }
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
        [JsonProperty("ctime")]
        public long Ctime { get; set; }
        [JsonProperty("duration")]
        public long Duration { get; set; }
        [JsonProperty("interactive_video")]
        public bool InteractiveVideo { get; set; }
        [JsonProperty("pic")]
        public string Pic { get; set; }
        [JsonProperty("pubdate")]
        public long Pubdate { get; set; }
        // stat
        // state
        [JsonProperty("title")]
        public string Title { get; set; }
        // ugc_pay
    }
}
