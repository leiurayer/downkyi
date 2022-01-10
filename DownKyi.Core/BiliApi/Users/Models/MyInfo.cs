using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/space/myinfo
    public class MyInfoOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public MyInfo Data { get; set; }
    }

    public class MyInfo : BaseModel
    {
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sex")]
        public string Sex { get; set; }
        [JsonProperty("face")]
        public string Face { get; set; }
        [JsonProperty("sign")]
        public string Sign { get; set; }
        // rank
        [JsonProperty("level")]
        public int Level { get; set; }
        // jointime
        [JsonProperty("moral")]
        public int Moral { get; set; }
        [JsonProperty("silence")]
        public int Silence { get; set; }
        [JsonProperty("email_status")]
        public int EmailStatus { get; set; }
        [JsonProperty("tel_status")]
        public int TelStatus { get; set; }
        [JsonProperty("identification")]
        public int Identification { get; set; }
        [JsonProperty("vip")]
        public UserInfoVip Vip { get; set; }
        // pendant
        // nameplate
        // official
        [JsonProperty("birthday")]
        public long Birthday { get; set; }
        [JsonProperty("is_tourist")]
        public int IsTourist { get; set; }
        [JsonProperty("is_fake_account")]
        public int IsFakeAccount { get; set; }
        [JsonProperty("pin_prompting")]
        public int PinPrompting { get; set; }
        [JsonProperty("is_deleted")]
        public int IsDeleted { get; set; }
        // in_reg_audit
        // is_rip_user
        // profession
        // face_nft
        // face_nft_new
        // is_senior_member
        [JsonProperty("level_exp")]
        public UserInfoLevelExp LevelExp { get; set; }
        [JsonProperty("coins")]
        public float Coins { get; set; }
        [JsonProperty("following")]
        public int Following { get; set; }
        [JsonProperty("follower")]
        public int Follower { get; set; }
    }
}
