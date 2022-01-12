using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpacePublicationPage : BaseModel
    {
        [JsonProperty("pn")]
        public int Pn { get; set; }
        [JsonProperty("ps")]
        public int Ps { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
