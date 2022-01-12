using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/space/arc/search
    public class SpacePublicationOrigin : BaseModel
    {
        [JsonProperty("data")]
        public SpacePublication Data { get; set; }
    }

    public class SpacePublication : BaseModel
    {
        [JsonProperty("list")]
        public SpacePublicationList List { get; set; }
        [JsonProperty("page")]
        public SpacePublicationPage Page { get; set; }
    }

}
