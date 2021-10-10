using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Cheese.Models
{
    public class CheeseStat : BaseModel
    {
        [JsonProperty("play")]
        public long Play { get; set; }
        [JsonProperty("play_desc")]
        public string PlayDesc { get; set; }
    }
}
