using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class UserInfoLevelExp : BaseModel
    {
        [JsonProperty("current_level")]
        public int CurrentLevel { get; set; }
        [JsonProperty("current_min")]
        public int CurrentMin { get; set; }
        [JsonProperty("current_exp")]
        public int CurrentExp { get; set; }
        [JsonProperty("next_exp")]
        public int NextExp { get; set; }
    }
}
