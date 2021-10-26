using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Favorites.Models
{
    public class FavStatus : BaseModel
    {
        [JsonProperty("collect")]
        public long Collect { get; set; }
        [JsonProperty("play")]
        public long Play { get; set; }
        [JsonProperty("thumb_up")]
        public long ThumbUp { get; set; }
        [JsonProperty("share")]
        public long Share { get; set; }
    }
}
