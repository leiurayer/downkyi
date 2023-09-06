using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BiliSharp.Api.Models.User
{
    /// <summary>
    /// https://api.bilibili.com/x/space/acc/info?mid=
    /// </summary>
    public class UserSpaceInfo
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
        public UserSpaceInfoData Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserSpaceInfoData
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
        [JsonPropertyName("coins")]
        public int Coins { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("fans_badge")]
        public bool FansBadge { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("fans_medal")]
        public UserSpaceInfoDataFansMedal FansMedal { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("official")]
        public UserSpaceInfoDataOfficial Official { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vip")]
        public UserSpaceInfoDataVip Vip { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pendant")]
        public UserSpaceInfoDataPendant Pendant { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("nameplate")]
        public UserSpaceInfoDataNameplate Nameplate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("user_honour_info")]
        public UserSpaceInfoDataUserHonourInfo UserHonourInfo { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_followed")]
        public bool IsFollowed { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("top_photo")]
        public string TopPhoto { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("theme")]
        public UserSpaceInfoDataTheme Theme { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("sys_notice")]
        public UserSpaceInfoDataSysNotice SysNotice { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("live_room")]
        public UserSpaceInfoDataLiveRoom LiveRoom { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("birthday")]
        public string Birthday { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("school")]
        public UserSpaceInfoDataSchool School { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("profession")]
        public UserSpaceInfoDataProfession Profession { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("tags")]
        public object Tags { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("series")]
        public UserSpaceInfoDataSeries Series { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_senior_member")]
        public int IsSeniorMember { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mcn_info")]
        public object McnInfo { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("gaia_res_type")]
        public int GaiaResType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("gaia_data")]
        public object GaiaData { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_risk")]
        public bool IsRisk { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("elec")]
        public UserSpaceInfoDataElec Elec { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("contract")]
        public UserSpaceInfoDataContract Contract { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserSpaceInfoDataFansMedal
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("show")]
        public bool Show { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("wear")]
        public bool Wear { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("medal")]
        public object Medal { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserSpaceInfoDataOfficial
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
    public class UserSpaceInfoDataVip
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
        public UserSpaceInfoDataVipLabel Label { get; set; }

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
    public class UserSpaceInfoDataVipLabel
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
    public class UserSpaceInfoDataPendant
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
    public class UserSpaceInfoDataNameplate
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
    public class UserSpaceInfoDataUserHonourInfo
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mid")]
        public int Mid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("colour")]
        public object Colour { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("tags")]
        public List<object> Tags { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserSpaceInfoDataTheme
    { }

    /// <summary>
    /// 
    /// </summary>
    public class UserSpaceInfoDataSysNotice
    { }

    /// <summary>
    /// 
    /// </summary>
    public class UserSpaceInfoDataLiveRoom
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("roomStatus")]
        public int Roomstatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("liveStatus")]
        public int Livestatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("cover")]
        public string Cover { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("roomid")]
        public long Roomid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("roundStatus")]
        public int Roundstatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("broadcast_type")]
        public int BroadcastType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("watched_show")]
        public UserSpaceInfoDataLiveRoomWatchedShow WatchedShow { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserSpaceInfoDataLiveRoomWatchedShow
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("switch")]
        public bool Switch { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("num")]
        public int Num { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("text_small")]
        public string TextSmall { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("text_large")]
        public string TextLarge { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("icon_location")]
        public string IconLocation { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("icon_web")]
        public string IconWeb { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserSpaceInfoDataSchool
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserSpaceInfoDataProfession
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("department")]
        public string Department { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_show")]
        public int IsShow { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserSpaceInfoDataSeries
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("user_upgrade_status")]
        public int UserUpgradeStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("show_upgrade_window")]
        public bool ShowUpgradeWindow { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserSpaceInfoDataElec
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("show_info")]
        public UserSpaceInfoDataElecShowInfo ShowInfo { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserSpaceInfoDataElecShowInfo
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("show")]
        public bool Show { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("state")]
        public int State { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("jump_url")]
        public string JumpUrl { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class UserSpaceInfoDataContract
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_display")]
        public bool IsDisplay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_follow_display")]
        public bool IsFollowDisplay { get; set; }
    }
}