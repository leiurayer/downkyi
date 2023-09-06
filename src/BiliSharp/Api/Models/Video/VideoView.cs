using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BiliSharp.Api.Models.Video
{
    /// <summary>
    /// https://api.bilibili.com/x/web-interface/wbi/view/detail?platform=web&bvid=BV1Pu4y1y7FA&aid=872233698&need_operation_card=1&web_rm_repeat=1&need_elec=1&out_referer=&page_no=1&p=1&web_location=1446382&w_rid=b9d75e9f42896cc2093f022e5e9b1fd2&wts=1694017091
    /// </summary>
    public class VideoView
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
        public VideoViewData Data { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewData
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("View")]
        public VideoViewDataView View { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("Card")]
        public VideoViewDataCard Card { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("Tags")]
        public List<VideoViewDataTags> Tags { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("Reply")]
        public VideoViewDataReply Reply { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("Related")]
        public List<VideoViewDataRelated> Related { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("Spec")]
        public object Spec { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("hot_share")]
        public VideoViewDataHotShare HotShare { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("elec")]
        public VideoViewDataElec Elec { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("recommend")]
        public object Recommend { get; set; }

        /// <summary>
        ///
        /// </summary>
        //[JsonPropertyName("view_addit")]
        //public VideoViewDataViewAddit ViewAddit { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("guide")]
        public object Guide { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("query_tags")]
        public object QueryTags { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_old_user")]
        public bool IsOldUser { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataView
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("bvid")]
        public string Bvid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("aid")]
        public long Aid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("videos")]
        public int Videos { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("tid")]
        public int Tid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("tname")]
        public string Tname { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("copyright")]
        public int Copyright { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pic")]
        public string Pic { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pubdate")]
        public long Pubdate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ctime")]
        public long Ctime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("desc_v2")]
        public List<VideoViewDataViewDescV2> DescV2 { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("state")]
        public int State { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mission_id")]
        public long MissionId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("rights")]
        public VideoViewDataViewRights Rights { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("owner")]
        public VideoViewDataViewOwner Owner { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("stat")]
        public VideoViewDataViewStat Stat { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dynamic")]
        public string Dynamic { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("cid")]
        public long Cid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dimension")]
        public VideoViewDataViewDimension Dimension { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("season_id")]
        public long SeasonId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("premiere")]
        public object Premiere { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("teenage_mode")]
        public int TeenageMode { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_chargeable_season")]
        public bool IsChargeableSeason { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_story")]
        public bool IsStory { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_upower_exclusive")]
        public bool IsUpowerExclusive { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_upower_play")]
        public bool IsUpowerPlay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("enable_vt")]
        public int EnableVt { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vt_display")]
        public string VtDisplay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("no_cache")]
        public bool NoCache { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pages")]
        public List<VideoViewDataViewPages> Pages { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("subtitle")]
        public VideoViewDataViewSubtitle Subtitle { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("label")]
        public VideoViewDataViewLabel Label { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("staff")]
        public List<VideoViewDataViewStaff> Staff { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ugc_season")]
        public VideoViewDataViewUgcSeason UgcSeason { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_season_display")]
        public bool IsSeasonDisplay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("user_garb")]
        public VideoViewDataViewUserGarb UserGarb { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("honor_reply")]
        public VideoViewDataViewHonorReply HonorReply { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("like_icon")]
        public string LikeIcon { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("need_jump_bv")]
        public bool NeedJumpBv { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("disable_show_up_info")]
        public bool DisableShowUpInfo { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewDescV2
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("raw_text")]
        public string RawText { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("biz_id")]
        public int BizId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewRights
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("bp")]
        public int Bp { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("elec")]
        public int Elec { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("download")]
        public int Download { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("movie")]
        public int Movie { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pay")]
        public int Pay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("hd5")]
        public int Hd5 { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("no_reprint")]
        public int NoReprint { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("autoplay")]
        public int Autoplay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ugc_pay")]
        public int UgcPay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_cooperation")]
        public int IsCooperation { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ugc_pay_preview")]
        public int UgcPayPreview { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("no_background")]
        public int NoBackground { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("clean_mode")]
        public int CleanMode { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_stein_gate")]
        public int IsSteinGate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_360")]
        public int Is360 { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("no_share")]
        public int NoShare { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("arc_pay")]
        public int ArcPay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("free_watch")]
        public int FreeWatch { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewOwner
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
        [JsonPropertyName("face")]
        public string Face { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewStat
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("aid")]
        public long Aid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("view")]
        public long View { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("danmaku")]
        public int Danmaku { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("reply")]
        public int Reply { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("favorite")]
        public int Favorite { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("coin")]
        public int Coin { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("share")]
        public int Share { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("now_rank")]
        public int NowRank { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("his_rank")]
        public int HisRank { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("like")]
        public int Like { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dislike")]
        public int Dislike { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("evaluation")]
        public string Evaluation { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("argue_msg")]
        public string ArgueMsg { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vt")]
        public int Vt { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewDimension
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("rotate")]
        public int Rotate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewPages
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("cid")]
        public long Cid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("from")]
        public string From { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("part")]
        public string Part { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vid")]
        public string Vid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("weblink")]
        public string Weblink { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dimension")]
        public VideoViewDataViewPagesDimension Dimension { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("first_frame")]
        public string FirstFrame { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewPagesDimension
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("rotate")]
        public int Rotate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewSubtitle
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("allow_submit")]
        public bool AllowSubmit { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("list")]
        public List<object> List { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewLabel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewStaff
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mid")]
        public long Mid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("face")]
        public string Face { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vip")]
        public VideoViewDataViewStaffVip Vip { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("official")]
        public VideoViewDataViewStaffOfficial Official { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("follower")]
        public long Follower { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("label_style")]
        public int LabelStyle { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewStaffVip
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
        public VideoViewDataViewStaffVipLabel Label { get; set; }

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
        [JsonPropertyName("tv_due_date")]
        public int TvDueDate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewStaffVipLabel
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
    public class VideoViewDataViewStaffOfficial
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
    public class VideoViewDataViewUgcSeason
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

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
        [JsonPropertyName("mid")]
        public long Mid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("intro")]
        public string Intro { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("sign_state")]
        public int SignState { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("attribute")]
        public int Attribute { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("sections")]
        public List<VideoViewDataViewUgcSeasonSections> Sections { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("stat")]
        public VideoViewDataViewUgcSeasonStat Stat { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ep_count")]
        public int EpCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("season_type")]
        public int SeasonType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_pay_season")]
        public bool IsPaySeason { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("enable_vt")]
        public int EnableVt { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewUgcSeasonSections
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("season_id")]
        public long SeasonId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("episodes")]
        public List<VideoViewDataViewUgcSeasonSectionsEpisodes> Episodes { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewUgcSeasonSectionsEpisodes
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("season_id")]
        public long SeasonId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("section_id")]
        public long SectionId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("aid")]
        public long Aid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("cid")]
        public long Cid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("attribute")]
        public int Attribute { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("arc")]
        public VideoViewDataViewUgcSeasonSectionsEpisodesArc Arc { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("page")]
        public VideoViewDataViewUgcSeasonSectionsEpisodesPage Page { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("bvid")]
        public string Bvid { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewUgcSeasonSectionsEpisodesArc
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("aid")]
        public long Aid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("videos")]
        public int Videos { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("type_id")]
        public int TypeId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("type_name")]
        public string TypeName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("copyright")]
        public int Copyright { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pic")]
        public string Pic { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pubdate")]
        public long Pubdate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ctime")]
        public long Ctime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("state")]
        public int State { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("rights")]
        public VideoViewDataViewUgcSeasonSectionsEpisodesArcRights Rights { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("author")]
        public VideoViewDataViewUgcSeasonSectionsEpisodesArcAuthor Author { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("stat")]
        public VideoViewDataViewUgcSeasonSectionsEpisodesArcStat Stat { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dynamic")]
        public string Dynamic { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dimension")]
        public VideoViewDataViewUgcSeasonSectionsEpisodesArcDimension Dimension { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("desc_v2")]
        public object DescV2 { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_chargeable_season")]
        public bool IsChargeableSeason { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_blooper")]
        public bool IsBlooper { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("enable_vt")]
        public int EnableVt { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vt_display")]
        public string VtDisplay { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewUgcSeasonSectionsEpisodesArcRights
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("bp")]
        public int Bp { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("elec")]
        public int Elec { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("download")]
        public int Download { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("movie")]
        public int Movie { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pay")]
        public int Pay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("hd5")]
        public int Hd5 { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("no_reprint")]
        public int NoReprint { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("autoplay")]
        public int Autoplay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ugc_pay")]
        public int UgcPay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_cooperation")]
        public int IsCooperation { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ugc_pay_preview")]
        public int UgcPayPreview { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("arc_pay")]
        public int ArcPay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("free_watch")]
        public int FreeWatch { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewUgcSeasonSectionsEpisodesArcAuthor
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mid")]
        public int Mid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("face")]
        public string Face { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewUgcSeasonSectionsEpisodesArcStat
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("aid")]
        public long Aid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("view")]
        public long View { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("danmaku")]
        public int Danmaku { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("reply")]
        public int Reply { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("fav")]
        public int Fav { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("coin")]
        public int Coin { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("share")]
        public int Share { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("now_rank")]
        public int NowRank { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("his_rank")]
        public int HisRank { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("like")]
        public long Like { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dislike")]
        public int Dislike { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("evaluation")]
        public string Evaluation { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("argue_msg")]
        public string ArgueMsg { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vt")]
        public int Vt { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vv")]
        public long Vv { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewUgcSeasonSectionsEpisodesArcDimension
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("rotate")]
        public int Rotate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewUgcSeasonSectionsEpisodesPage
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("cid")]
        public long Cid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("from")]
        public string From { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("part")]
        public string Part { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vid")]
        public string Vid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("weblink")]
        public string Weblink { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dimension")]
        public VideoViewDataViewUgcSeasonSectionsEpisodesPageDimension Dimension { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewUgcSeasonSectionsEpisodesPageDimension
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("rotate")]
        public int Rotate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewUgcSeasonStat
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("season_id")]
        public long SeasonId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("view")]
        public long View { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("danmaku")]
        public int Danmaku { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("reply")]
        public int Reply { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("fav")]
        public long Fav { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("coin")]
        public long Coin { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("share")]
        public long Share { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("now_rank")]
        public int NowRank { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("his_rank")]
        public int HisRank { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("like")]
        public long Like { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vt")]
        public int Vt { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vv")]
        public int Vv { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewUserGarb
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("url_image_ani_cut")]
        public string UrlImageAniCut { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewHonorReply
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("honor")]
        public List<VideoViewDataViewHonorReplyHonor> Honor { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataViewHonorReplyHonor
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("aid")]
        public long Aid { get; set; }

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

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("weekly_recommend_num")]
        public int WeeklyRecommendNum { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataCard
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("card")]
        public VideoViewDataCardCard Card { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("space")]
        public VideoViewDataCardSpace Space { get; set; }

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
    public class VideoViewDataCardCard
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
        public VideoViewDataCardCardLevelInfo LevelInfo { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pendant")]
        public VideoViewDataCardCardPendant Pendant { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("nameplate")]
        public VideoViewDataCardCardNameplate Nameplate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("Official")]
        public VideoViewDataCardCardOfficial Official { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("official_verify")]
        public VideoViewDataCardCardOfficialVerify OfficialVerify { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vip")]
        public VideoViewDataCardCardVip Vip { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_senior_member")]
        public int IsSeniorMember { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataCardCardLevelInfo
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
    public class VideoViewDataCardCardPendant
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
    public class VideoViewDataCardCardNameplate
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
    public class VideoViewDataCardCardOfficial
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
    public class VideoViewDataCardCardOfficialVerify
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
    public class VideoViewDataCardCardVip
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
        public int DueDate { get; set; }

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
        public VideoViewDataCardCardVipLabel Label { get; set; }

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
        [JsonPropertyName("tv_due_date")]
        public int TvDueDate { get; set; }

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
    public class VideoViewDataCardCardVipLabel
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
    public class VideoViewDataCardSpace
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

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataTags
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("tag_id")]
        public int TagId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("cover")]
        public string Cover { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("head_cover")]
        public string HeadCover { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("short_content")]
        public string ShortContent { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("state")]
        public int State { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ctime")]
        public int Ctime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("count")]
        public VideoViewDataTagsCount Count { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_atten")]
        public int IsAtten { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("likes")]
        public int Likes { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("hates")]
        public int Hates { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("attribute")]
        public int Attribute { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("liked")]
        public int Liked { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("hated")]
        public int Hated { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("extra_attr")]
        public int ExtraAttr { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("music_id")]
        public string MusicId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("tag_type")]
        public string TagType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_activity")]
        public bool IsActivity { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("color")]
        public string Color { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("alpha")]
        public int Alpha { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_season")]
        public bool IsSeason { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("subscribed_count")]
        public int SubscribedCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("archive_count")]
        public string ArchiveCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("featured_count")]
        public int FeaturedCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("jump_url")]
        public string JumpUrl { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataTagsCount
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("view")]
        public int View { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("use")]
        public int Use { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("atten")]
        public int Atten { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataReply
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("page")]
        public object Page { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("replies")]
        public List<VideoViewDataReplyReplies> Replies { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataReplyReplies
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("rpid")]
        public int Rpid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("oid")]
        public int Oid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("type")]
        public int Type { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mid")]
        public int Mid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("root")]
        public int Root { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("parent")]
        public int Parent { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dialog")]
        public int Dialog { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("rcount")]
        public int Rcount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("state")]
        public int State { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("fansgrade")]
        public int Fansgrade { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("attr")]
        public int Attr { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ctime")]
        public int Ctime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("like")]
        public int Like { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("action")]
        public int Action { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("content")]
        public object Content { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("replies")]
        public object Replies { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("assist")]
        public int Assist { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("show_follow")]
        public bool ShowFollow { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataRelated
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("aid")]
        public long Aid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("videos")]
        public int Videos { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("tid")]
        public int Tid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("tname")]
        public string Tname { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("copyright")]
        public int Copyright { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pic")]
        public string Pic { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pubdate")]
        public long Pubdate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ctime")]
        public long Ctime { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("desc")]
        public string Desc { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("state")]
        public int State { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("mission_id")]
        public long MissionId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("rights")]
        public VideoViewDataRelatedRights Rights { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("owner")]
        public VideoViewDataRelatedOwner Owner { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("stat")]
        public VideoViewDataRelatedStat Stat { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dynamic")]
        public string Dynamic { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("cid")]
        public long Cid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dimension")]
        public VideoViewDataRelatedDimension Dimension { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("season_id")]
        public long SeasonId { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("short_link_v2")]
        public string ShortLinkV2 { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("up_from_v2")]
        public int UpFromV2 { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("first_frame")]
        public string FirstFrame { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pub_location")]
        public string PubLocation { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("bvid")]
        public string Bvid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("season_type")]
        public int SeasonType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_ogv")]
        public bool IsOgv { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ogv_info")]
        public object OgvInfo { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("rcmd_reason")]
        public string RcmdReason { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("enable_vt")]
        public int EnableVt { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataRelatedRights
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("bp")]
        public int Bp { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("elec")]
        public int Elec { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("download")]
        public int Download { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("movie")]
        public int Movie { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pay")]
        public int Pay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("hd5")]
        public int Hd5 { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("no_reprint")]
        public int NoReprint { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("autoplay")]
        public int Autoplay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ugc_pay")]
        public int UgcPay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("is_cooperation")]
        public int IsCooperation { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("ugc_pay_preview")]
        public int UgcPayPreview { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("no_background")]
        public int NoBackground { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("arc_pay")]
        public int ArcPay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("pay_free_watch")]
        public int PayFreeWatch { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataRelatedOwner
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
        [JsonPropertyName("face")]
        public string Face { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataRelatedStat
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("aid")]
        public long Aid { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("view")]
        public long View { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("danmaku")]
        public int Danmaku { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("reply")]
        public int Reply { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("favorite")]
        public int Favorite { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("coin")]
        public int Coin { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("share")]
        public int Share { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("now_rank")]
        public int NowRank { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("his_rank")]
        public int HisRank { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("like")]
        public long Like { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("dislike")]
        public int Dislike { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vt")]
        public int Vt { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("vv")]
        public long Vv { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataRelatedDimension
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("rotate")]
        public int Rotate { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataHotShare
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("show")]
        public bool Show { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("list")]
        public List<object> List { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataElec
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("show_info")]
        public VideoViewDataElecShowInfo ShowInfo { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("av_count")]
        public int AvCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("special_day")]
        public int SpecialDay { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("display_num")]
        public int DisplayNum { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataElecShowInfo
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
        [JsonPropertyName("jump_url")]
        public string JumpUrl { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("high_level")]
        public VideoViewDataElecShowInfoHighLevel HighLevel { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class VideoViewDataElecShowInfoHighLevel
    {
        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("privilege_type")]
        public int PrivilegeType { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("level_str")]
        public string LevelStr { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("intro")]
        public string Intro { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonPropertyName("open")]
        public bool Open { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    //public class VideoViewDataViewAddit
    //{
    //    /// <summary>
    //    ///
    //    /// </summary>
    //    [JsonPropertyName("63")]
    //    public bool 63 { get; set; }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    [JsonPropertyName("64")]
    //    public bool 64 { get; set; }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    [JsonPropertyName("69")]
    //    public bool 69 { get; set; }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    [JsonPropertyName("71")]
    //    public bool 71 { get; set; }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    [JsonPropertyName("72")]
    //    public bool 72 { get; set; }
    //}
}