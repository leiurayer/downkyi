using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Models
{
    public class Dimension : BaseModel
    {
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("rotate")]
        public int Rotate { get; set; }
    }
}
