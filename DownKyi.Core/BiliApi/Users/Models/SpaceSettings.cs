using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://space.bilibili.com/ajax/settings/getSettings?mid={mid}
    public class SpaceSettingsOrigin : BaseModel
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("data")]
        public SpaceSettings Data { get; set; }
    }

    public class SpaceSettings : BaseModel
    {
        // ...
        [JsonProperty("toutu")]
        public SpaceSettingsToutu Toutu { get; set; }
    }
}
