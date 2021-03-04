using Newtonsoft.Json;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/space/myinfo
    [JsonObject]
    public class MyInfoOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public MyInfo Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class MyInfo : BaseEntity
    {
        [JsonProperty("birthday")]
        public long Birthday { get; set; }
        [JsonProperty("coins")]
        public float Coins { get; set; }
        [JsonProperty("email_status")]
        public int EmailStatus { get; set; }
        [JsonProperty("face")]
        public string Face { get; set; }
        [JsonProperty("follower")]
        public int Follower { get; set; }
        [JsonProperty("following")]
        public int Following { get; set; }
        [JsonProperty("identification")]
        public int Identification { get; set; }
        [JsonProperty("is_deleted")]
        public int IsDeleted { get; set; }
        [JsonProperty("is_fake_account")]
        public int IsFakeAccount { get; set; }
        [JsonProperty("is_tourist")]
        public int IsTourist { get; set; }
        [JsonProperty("jointime")]
        public int Jointime { get; set; }
        [JsonProperty("level")]
        public int Level { get; set; }
        [JsonProperty("level_exp")]
        public MyInfoLevelExp LevelExp { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("moral")]
        public int Moral { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("nameplate")]
        public MyInfoNamePlate Nameplate { get; set; }
        [JsonProperty("official")]
        public MyInfoOfficial Official { get; set; }
        [JsonProperty("pendant")]
        public MyInfoPendant Pendant { get; set; }
        [JsonProperty("pin_prompting")]
        public int PinPrompting { get; set; }
        [JsonProperty("rank")]
        public int Rank { get; set; }
        [JsonProperty("sex")]
        public string Sex { get; set; }
        [JsonProperty("sign")]
        public string Sign { get; set; }
        [JsonProperty("silence")]
        public int Silence { get; set; }
        [JsonProperty("tel_status")]
        public int TelStatus { get; set; }
        [JsonProperty("vip")]
        public MyInfoVip Vip { get; set; }
    }

    [JsonObject]
    public class MyInfoLevelExp : BaseEntity
    {
        [JsonProperty("current_exp")]
        public int CurrentExp { get; set; }
        [JsonProperty("current_level")]
        public int CurrentLevel { get; set; }
        [JsonProperty("current_min")]
        public int CurrentMin { get; set; }
        [JsonProperty("next_exp")]
        public int NextExp { get; set; }
    }

    [JsonObject]
    public class MyInfoNamePlate : BaseEntity
    {
        [JsonProperty("condition")]
        public string Condition { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("image_small")]
        public string ImageSmall { get; set; }
        [JsonProperty("level")]
        public string Level { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("nid")]
        public int Nid { get; set; }
    }

    [JsonObject]
    public class MyInfoOfficial : BaseEntity
    {
        [JsonProperty("desc")]
        public string Desc { get; set; }
        [JsonProperty("role")]
        public int Role { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
    }

    [JsonObject]
    public class MyInfoPendant : BaseEntity
    {
        [JsonProperty("expire")]
        public int Expire { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
        [JsonProperty("image_enhance")]
        public string ImageEnhance { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("pid")]
        public int Pid { get; set; }
    }

    [JsonObject]
    public class MyInfoVip : BaseEntity
    {
        [JsonProperty("avatar_subscript")]
        public int AvatarSubscript { get; set; }
        [JsonProperty("due_date")]
        public long DueDate { get; set; }
        [JsonProperty("label")]
        public MyInfoDataVipLabel Label { get; set; }
        [JsonProperty("nickname_color")]
        public string NicknameColor { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("theme_type")]
        public int ThemeType { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("vip_pay_type")]
        public int VipPayType { get; set; }
    }

    [JsonObject]
    public class MyInfoDataVipLabel : BaseEntity
    {
        [JsonProperty("label_theme")]
        public string LabelTheme { get; set; }
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }

}
