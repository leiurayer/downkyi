using System.Text.Json.Serialization;

namespace BiliSharp.Api.Models.User
{
    /// <summary>
    /// https://api.bilibili.com/x/space/myinfo
    /// </summary>
    public class MyInfo
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ttl")]
        public int Ttl { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("data")]
        public MyInfoData Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyInfoData
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mid")]
        public long Mid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("sex")]
        public string Sex { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("face")]
        public string Face { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("sign")]
        public string Sign { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("rank")]
        public int Rank { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("level")]
        public int Level { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("jointime")]
        public int Jointime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("moral")]
        public int Moral { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("silence")]
        public int Silence { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("email_status")]
        public int EmailStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("tel_status")]
        public int TelStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("identification")]
        public int Identification { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vip")]
        public MyInfoDataVip Vip { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pendant")]
        public MyInfoDataPendant Pendant { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("nameplate")]
        public MyInfoDataNameplate Nameplate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("official")]
        public MyInfoDataOfficial Official { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("birthday")]
        public long Birthday { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_tourist")]
        public int IsTourist { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_fake_account")]
        public int IsFakeAccount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pin_prompting")]
        public int PinPrompting { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_deleted")]
        public int IsDeleted { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("in_reg_audit")]
        public int InRegAudit { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_rip_user")]
        public bool IsRipUser { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("profession")]
        public MyInfoDataProfession Profession { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("face_nft")]
        public int FaceNft { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("face_nft_new")]
        public int FaceNftNew { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_senior_member")]
        public int IsSeniorMember { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("honours")]
        public MyInfoDataHonours Honours { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("digital_id")]
        public string DigitalId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("digital_type")]
        public int DigitalType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("level_exp")]
        public MyInfoDataLevelExp LevelExp { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("coins")]
        public int Coins { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("following")]
        public int Following { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("follower")]
        public int Follower { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyInfoDataVip
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("due_date")]
        public long DueDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vip_pay_type")]
        public int VipPayType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("theme_type")]
        public int ThemeType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("label")]
        public MyInfoDataVipLabel Label { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("avatar_subscript")]
        public int AvatarSubscript { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("nickname_color")]
        public string NicknameColor { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("role")]
        public int Role { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("avatar_subscript_url")]
        public string AvatarSubscriptUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("tv_vip_status")]
        public int TvVipStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("tv_vip_pay_type")]
        public int TvVipPayType { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyInfoDataVipLabel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("path")]
        public string Path { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("text")]
        public string Text { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("label_theme")]
        public string LabelTheme { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("text_color")]
        public string TextColor { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("bg_style")]
        public int BgStyle { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("bg_color")]
        public string BgColor { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("border_color")]
        public string BorderColor { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("use_img_label")]
        public bool UseImgLabel { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("img_label_uri_hans")]
        public string ImgLabelUriHans { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("img_label_uri_hant")]
        public string ImgLabelUriHant { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("img_label_uri_hans_static")]
        public string ImgLabelUriHansStatic { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("img_label_uri_hant_static")]
        public string ImgLabelUriHantStatic { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyInfoDataPendant
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pid")]
        public int Pid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("image")]
        public string Image { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("expire")]
        public int Expire { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("image_enhance")]
        public string ImageEnhance { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("image_enhance_frame")]
        public string ImageEnhanceFrame { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyInfoDataNameplate
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("nid")]
        public int Nid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("image")]
        public string Image { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("image_small")]
        public string ImageSmall { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("level")]
        public string Level { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("condition")]
        public string Condition { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyInfoDataOfficial
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("role")]
        public int Role { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyInfoDataProfession
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("show_name")]
        public string ShowName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_show")]
        public int IsShow { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("category_one")]
        public string CategoryOne { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("realname")]
        public string Realname { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("department")]
        public string Department { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyInfoDataHonours
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mid")]
        public long Mid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("colour")]
        public MyInfoDataHonoursColour Colour { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("tags")]
        public object Tags { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyInfoDataHonoursColour
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dark")]
        public string Dark { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("normal")]
        public string Normal { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MyInfoDataLevelExp
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("current_level")]
        public int CurrentLevel { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("current_min")]
        public int CurrentMin { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("current_exp")]
        public int CurrentExp { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("next_exp")]
        public int NextExp { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("level_up")]
        public long LevelUp { get; set; }
    }
}