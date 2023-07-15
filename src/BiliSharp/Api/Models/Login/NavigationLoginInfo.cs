using System.Text.Json.Serialization;

namespace BiliSharp.Api.Models.Login
{
    /// <summary>
    /// https://api.bilibili.com/x/web-interface/nav
    /// </summary>
    public class NavigationLoginInfo
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
        public NavigationLoginInfoData Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NavigationLoginInfoData
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("isLogin")]
        public bool Islogin { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("email_verified")]
        public int EmailVerified { get; set; }

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
        [JsonPropertyName("level_info")]
        public NavigationLoginInfoDataLevelInfo LevelInfo { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mid")]
        public long Mid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mobile_verified")]
        public int MobileVerified { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("money")]
        public int Money { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("moral")]
        public int Moral { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("official")]
        public NavigationLoginInfoDataOfficial Official { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("officialVerify")]
        public NavigationLoginInfoDataOfficialverify Officialverify { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pendant")]
        public NavigationLoginInfoDataPendant Pendant { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("scores")]
        public int Scores { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("uname")]
        public string Uname { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vipDueDate")]
        public long Vipduedate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vipStatus")]
        public int Vipstatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vipType")]
        public int Viptype { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vip_pay_type")]
        public int VipPayType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vip_theme_type")]
        public int VipThemeType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vip_label")]
        public NavigationLoginInfoDataVipLabel VipLabel { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vip_avatar_subscript")]
        public int VipAvatarSubscript { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vip_nickname_color")]
        public string VipNicknameColor { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vip")]
        public NavigationLoginInfoDataVip Vip { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("wallet")]
        public NavigationLoginInfoDataWallet Wallet { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("has_shop")]
        public bool HasShop { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("shop_url")]
        public string ShopUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("allowance_count")]
        public int AllowanceCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("answer_status")]
        public int AnswerStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_senior_member")]
        public int IsSeniorMember { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("wbi_img")]
        public NavigationLoginInfoDataWbiImg WbiImg { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_jury")]
        public bool IsJury { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NavigationLoginInfoDataLevelInfo
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
        public object NextExp { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NavigationLoginInfoDataOfficial
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
    public class NavigationLoginInfoDataOfficialverify
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
    public class NavigationLoginInfoDataPendant
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
    public class NavigationLoginInfoDataVipLabel
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
    public class NavigationLoginInfoDataVip
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
        public NavigationLoginInfoDataVipLabel Label { get; set; }

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
    public class NavigationLoginInfoDataWallet
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mid")]
        public long Mid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("bcoin_balance")]
        public int BcoinBalance { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("coupon_balance")]
        public int CouponBalance { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("coupon_due_time")]
        public int CouponDueTime { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class NavigationLoginInfoDataWbiImg
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("img_url")]
        public string ImgUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("sub_url")]
        public string SubUrl { get; set; }
    }
}