using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpacePublicationListTypeVideoZone : BaseModel
    {
        [JsonProperty("tid")]
        public int Tid { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
