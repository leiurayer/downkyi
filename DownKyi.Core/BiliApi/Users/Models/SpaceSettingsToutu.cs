using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpaceSettingsToutu : BaseModel
    {
        [JsonProperty("sid")]
        public int Sid { get; set; }
        [JsonProperty("expire")]
        public long Expire { get; set; }
        [JsonProperty("s_img")]
        public string Simg { get; set; } // 完整url为http://i0.hdslb.com/+相对路径
        [JsonProperty("l_img")]
        public string Limg { get; set; } // 完整url为http://i0.hdslb.com/+相对路径
        [JsonProperty("android_img")]
        public string AndroidImg { get; set; }
        [JsonProperty("iphone_img")]
        public string IphoneImg { get; set; }
        [JsonProperty("ipad_img")]
        public string IpadImg { get; set; }
        [JsonProperty("thumbnail_img")]
        public string ThumbnailImg { get; set; }
        [JsonProperty("platform")]
        public int Platform { get; set; }
    }
}
