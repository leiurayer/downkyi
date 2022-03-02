using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.History.Models
{
    public class HistoryListHistory : BaseModel
    {
        [JsonProperty("oid")]
        public long Oid { get; set; }
        [JsonProperty("epid")]
        public long Epid { get; set; }
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("cid")]
        public long Cid { get; set; }
        [JsonProperty("part")]
        public string Part { get; set; }
        [JsonProperty("business")]
        public string Business { get; set; }
        [JsonProperty("dt")]
        public int Dt { get; set; }
    }
}
