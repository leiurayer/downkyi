using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.History.Models
{
    public class HistoryList : BaseModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        // long_title
        [JsonProperty("cover")]
        public string Cover { get; set; }
        // covers
        [JsonProperty("uri")]
        public string Uri { get; set; }
        [JsonProperty("history")]
        public HistoryListHistory History { get; set; }
        [JsonProperty("videos")]
        public int Videos { get; set; }
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }
        [JsonProperty("author_face")]
        public string AuthorFace { get; set; }
        [JsonProperty("author_mid")]
        public long AuthorMid { get; set; }
        [JsonProperty("view_at")]
        public long ViewAt { get; set; }
        [JsonProperty("progress")]
        public long Progress { get; set; }
        // badge
        [JsonProperty("show_title")]
        public string ShowTitle { get; set; }
        [JsonProperty("duration")]
        public long Duration { get; set; }
        // current
        // total
        [JsonProperty("new_desc")]
        public string NewDesc { get; set; }
        // is_finish
        // is_fav
        // kid
        [JsonProperty("tag_name")]
        public string TagName { get; set; }
        // live_status
    }
}
