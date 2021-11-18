using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.VideoStream.Models
{
    public class Subtitle : BaseModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("lan")]
        public string Lan { get; set; }
        [JsonProperty("lan_doc")]
        public string LanDoc { get; set; }
        [JsonProperty("is_lock")]
        public bool IsLock { get; set; }
        [JsonProperty("author_mid")]
        public long AuthorMid { get; set; }
        [JsonProperty("subtitle_url")]
        public string SubtitleUrl { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("id_str")]
        public string IdStr { get; set; }
    }
}
