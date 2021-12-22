using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Models.Json
{
    public class Subtitle : BaseModel
    {
        [JsonProperty("from")]
        public float From { get; set; }
        [JsonProperty("to")]
        public float To { get; set; }
        [JsonProperty("location")]
        public int Location { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
