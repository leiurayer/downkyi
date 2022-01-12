using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/space/acc/info?mid={mid}
    public class UserInfoForSpaceOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public UserInfoForSpace Data { get; set; }
    }

    public class UserInfoForSpace : BaseModel
    {
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sex")]
        public string Sex { get; set; }
        [JsonProperty("face")]
        public string Face { get; set; }
        // face_nft
        [JsonProperty("sign")]
        public string Sign { get; set; }
        // rank
        [JsonProperty("level")]
        public int Level { get; set; }
        // jointime
        // moral
        // silence
        // coins
        //[JsonProperty("fans_badge")]
        //public bool FansBadge { get; set; }
        // fans_medal
        // official
        [JsonProperty("vip")]
        public UserInfoVip Vip { get; set; }
        // pendant
        // nameplate
        // user_honour_info
        [JsonProperty("is_followed")]
        public bool IsFollowed { get; set; }
        [JsonProperty("top_photo")]
        public string TopPhoto { get; set; }
        // ...
    }

}
