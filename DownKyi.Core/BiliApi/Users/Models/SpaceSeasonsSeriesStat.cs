using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpaceSeasonsSeriesStat : BaseModel
    {
        [JsonProperty("view")]
        public long View { get; set; }
    }
}
