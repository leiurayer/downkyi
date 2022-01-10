using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpaceChannelVideoList : BaseModel
    {
        [JsonProperty("cid")]
        public long Cid { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("intro")]
        public string Intro { get; set; }
        [JsonProperty("mtime")]
        public long Mtime { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        // is_live_playback
        [JsonProperty("archives")]
        public List<SpaceChannelArchive> Archives { get; set; }
    }
}
