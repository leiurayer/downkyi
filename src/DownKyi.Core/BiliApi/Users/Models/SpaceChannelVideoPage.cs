using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpaceChannelVideoPage : BaseModel
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("num")]
        public int Num { get; set; }
        [JsonProperty("size")]
        public int Size { get; set; }
    }
}
