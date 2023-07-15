using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BiliSharp.Api.Models.User
{
    /// <summary>
    /// https://api.bilibili.com/x/web-interface/card?mid=314521322&photo=true
    /// </summary>
    public class UserCard
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
        public UserCardData Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserCardData
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("card")]
        public UserCardDataCard Card { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("space")]
        public UserCardDataSpace Space { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("following")]
        public bool Following { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("archive_count")]
        public int ArchiveCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("article_count")]
        public int ArticleCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("follower")]
        public long Follower { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("like_num")]
        public long LikeNum { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserCardDataCard
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mid")]
        public string Mid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("approve")]
        public bool Approve { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("sex")]
        public string Sex { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("rank")]
        public string Rank { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("face")]
        public string Face { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("face_nft")]
        public int FaceNft { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("face_nft_type")]
        public int FaceNftType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("DisplayRank")]
        public string Displayrank { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("regtime")]
        public int Regtime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("spacesta")]
        public int Spacesta { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("birthday")]
        public string Birthday { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("place")]
        public string Place { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("description")]
        public string Description { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("article")]
        public int Article { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("attentions")]
        public List<object> Attentions { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("fans")]
        public long Fans { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("friend")]
        public int Friend { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("attention")]
        public int Attention { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("sign")]
        public string Sign { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("level_info")]
        public UserCardDataCardLevelInfo LevelInfo { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pendant")]
        public UserCardDataCardPendant Pendant { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("nameplate")]
        public UserCardDataCardNameplate Nameplate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("Official")]
        public UserCardDataCardOfficial Official { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("official_verify")]
        public UserCardDataCardOfficialVerify OfficialVerify { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vip")]
        public UserCardDataCardVip Vip { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_senior_member")]
        public int IsSeniorMember { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserCardDataCardLevelInfo
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
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserCardDataCardPendant
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
    public class UserCardDataCardNameplate
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
    public class UserCardDataCardOfficial
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
    public class UserCardDataCardOfficialVerify
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("desc")]
        public string Desc { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserCardDataCardVip
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
        public UserCardDataCardVipLabel Label { get; set; }

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

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vipType")]
        public int Viptype { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vipStatus")]
        public int Vipstatus { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserCardDataCardVipLabel
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
    public class UserCardDataSpace
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("s_img")]
        public string SImg { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("l_img")]
        public string LImg { get; set; }
    }
}