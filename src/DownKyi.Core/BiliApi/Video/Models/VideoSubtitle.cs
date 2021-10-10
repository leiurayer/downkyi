using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Video.Models
{
    public class VideoSubtitle : BaseModel
    {
        [JsonProperty("allow_submit")]
        public bool AllowSubmit { get; set; }
        [JsonProperty("list")]
        public List<Subtitle> List { get; set; }
    }

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
        [JsonProperty("author")]
        public SubtitleAuthor Author { get; set; }
    }

    public class SubtitleAuthor : BaseModel
    {
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sex")]
        public string Sex { get; set; }
        [JsonProperty("face")]
        public string Face { get; set; }
        [JsonProperty("sign")]
        public string Sign { get; set; }
        //[JsonProperty("rank")]
        //public int Rank { get; set; }
        //[JsonProperty("birthday")]
        //public int Birthday { get; set; }
        //[JsonProperty("is_fake_account")]
        //public int IsFakeAccount { get; set; }
        //[JsonProperty("is_deleted")]
        //public int IsDeleted { get; set; }
    }

}
