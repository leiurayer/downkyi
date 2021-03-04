using Newtonsoft.Json;

namespace Core.entity2.users
{
    // https://space.bilibili.com/ajax/settings/getSettings?mid={mid}
    [JsonObject]
    public class SpaceSettingsOrigin : BaseEntity
    {
        [JsonProperty("data")]
        public SpaceSettings Data { get; set; }
        [JsonProperty("status")]
        public bool Status { get; set; }
    }

    [JsonObject]
    public class SpaceSettings : BaseEntity
    {
        // ...
        [JsonProperty("toutu")]
        public SpaceSettingsToutu Toutu { get; set; }
    }

    [JsonObject]
    public class SpaceSettingsToutu : BaseEntity
    {
        [JsonProperty("android_img")]
        public string AndroidImg { get; set; }
        [JsonProperty("expire")]
        public long Expire { get; set; }
        [JsonProperty("ipad_img")]
        public string IpadImg { get; set; }
        [JsonProperty("iphone_img")]
        public string IphoneImg { get; set; }
        [JsonProperty("l_img")]
        public string Limg { get; set; } // 完整url为http://i0.hdslb.com/+相对路径
        [JsonProperty("platform")]
        public int Platform { get; set; }
        [JsonProperty("s_img")]
        public string Simg { get; set; } // 完整url为http://i0.hdslb.com/+相对路径
        [JsonProperty("sid")]
        public int Sid { get; set; }
        [JsonProperty("thumbnail_img")]
        public string ThumbnailImg { get; set; }
    }

}
