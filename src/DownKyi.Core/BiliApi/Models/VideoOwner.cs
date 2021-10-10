using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Models
{
    public class VideoOwner : BaseModel
    {
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("face")]
        public string Face { get; set; }
    }
}
