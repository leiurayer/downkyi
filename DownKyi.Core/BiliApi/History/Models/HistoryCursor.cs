using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.History.Models
{
    public class HistoryCursor : BaseModel
    {
        [JsonProperty("max")]
        public long Max { get; set; }
        [JsonProperty("view_at")]
        public long ViewAt { get; set; }
        [JsonProperty("business")]
        public string Business { get; set; }
        [JsonProperty("ps")]
        public int Ps { get; set; }
    }
}
