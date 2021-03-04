using Newtonsoft.Json;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/space/acc/info?mid={mid}
    [JsonObject]
    public class SpaceInfoOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public SpaceInfo Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class SpaceInfo : BaseEntity
    {
        // ...
        [JsonProperty("face")]
        public string Face { get; set; }
        [JsonProperty("fans_badge")]
        public bool FansBadge { get; set; }
        [JsonProperty("is_followed")]
        public bool IsFollowed { get; set; }
        // ...
        [JsonProperty("level")]
        public int Level { get; set; }
        // ...
        [JsonProperty("mid")]
        public int Mid { get; set; }
        // ...
        [JsonProperty("name")]
        public string Name { get; set; }
        // ...
        [JsonProperty("sex")]
        public string Sex { get; set; }
        // ...
        [JsonProperty("sign")]
        public string Sign { get; set; }
        // ...
        [JsonProperty("top_photo")]
        public string TopPhoto { get; set; }
        [JsonProperty("vip")]
        public SpaceInfoVip Vip { get; set; }
    }

    public class SpaceInfoVip
    {
        [JsonProperty("avatar_subscript")]
        public int AvatarSubscript { get; set; }
        [JsonProperty("label")]
        public SpaceInfoVipLabel Label { get; set; }
        [JsonProperty("nickname_color")]
        public string NicknameColor { get; set; }
        [JsonProperty("status")]
        public int Status { get; set; }
        [JsonProperty("theme_type")]
        public int ThemeType { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
    }

    public class SpaceInfoVipLabel
    {
        [JsonProperty("label_theme")]
        public string LabelTheme { get; set; }
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }

}
