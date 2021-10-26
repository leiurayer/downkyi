using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Favorites.Models
{
    public class MediaStatus : BaseModel
    {
        [JsonProperty("collect")]
        public long Collect { get; set; }
        [JsonProperty("play")]
        public long Play { get; set; }
        [JsonProperty("danmaku")]
        public long Danmaku { get; set; }
    }
}
