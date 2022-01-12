using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpaceCheesePage : BaseModel
    {
        [JsonProperty("next")]
        public bool Next { get; set; }
        [JsonProperty("num")]
        public int Num { get; set; }
        [JsonProperty("size")]
        public int Size { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
