using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Cheese.Models
{
    public class CheeseImg : BaseModel
    {
        [JsonProperty("aspect_ratio")]
        public double AspectRatio { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
