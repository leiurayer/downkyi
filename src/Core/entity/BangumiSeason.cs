using System.Collections.Generic;

// 注释掉未使用的属性
namespace Core.entity
{
    //https://api.bilibili.com/pgc/view/web/season?season_id=28324
    public class BangumiSeason
    {
        //public int code { get; set; }
        //public string message { get; set; }
        public BangumiSeasonResult result { get; set; }
    }

    public class BangumiSeasonResult
    {
        //public BangumiSeasonResultActivity activity { get; set; }
        //public string alias { get; set; }
        //public string bkg_cover { get; set; }
        public string cover { get; set; }
        public List<BangumiSeasonResultEpisodes> episodes { get; set; }
        public string evaluate { get; set; }
        //public string jp_title { get; set; }
        //public string link { get; set; }
        //public long media_id { get; set; }
        //public int mode { get; set; }
        //public BangumiSeasonResultNewEp new_ep { get; set; }
        //public BangumiSeasonResultPositive positive { get; set; }
        //public BangumiSeasonResultPublish publish { get; set; }
        //public BangumiSeasonResultRating rating { get; set; }
        //public string record { get; set; }
        //public BangumiSeasonResultRights rights { get; set; }
        //public long season_id { get; set; }
        //public string season_title { get; set; }
        //public List<BangumiSeasonResultSeasons> seasons { get; set; }
        //public BangumiSeasonResultSeries series { get; set; }
        //public string share_copy { get; set; }
        //public string share_sub_title { get; set; }
        //public string share_url { get; set; }
        //public BangumiSeasonResultShow show { get; set; }
        //public string square_cover { get; set; }
        public BangumiSeasonResultStat stat { get; set; }
        //public int status { get; set; }
        //public string subtitle { get; set; }
        public string title { get; set; }
        //public int total { get; set; }
        public int type { get; set; }
    }

    //public class BangumiSeasonResultActivity
    //{
    //    public string head_bg_url { get; set; }
    //    public long id { get; set; }
    //    public string title { get; set; }
    //}

    public class BangumiSeasonResultEpisodes
    {
        public long aid { get; set; }
        //public string badge { get; set; }
        //public BangumiSeasonResultBadgeInfo badge_info { get; set; }
        //public int badge_type { get; set; }
        public string bvid { get; set; }
        public long cid { get; set; }
        //public string cover { get; set; }
        public BangumiSeasonResultEpisodesDimension dimension { get; set; }
        //public string from { get; set; }
        //public long id { get; set; }
        //public string link { get; set; }
        //public string long_title { get; set; }
        //public long pub_time { get; set; }
        //public string release_date { get; set; }
        //public BangumiSeasonResultEpisodesRights rights { get; set; }
        public string share_copy { get; set; }
        //public string share_url { get; set; }
        //public string short_link { get; set; }
        //public int status { get; set; }
        //public string subtitle { get; set; }
        public string title { get; set; }
        //public string vid { get; set; }
    }

    //public class BangumiSeasonResultBadgeInfo
    //{
    //    public string bg_color { get; set; }
    //    public string bg_color_night { get; set; }
    //    public string text { get; set; }
    //}

    public class BangumiSeasonResultEpisodesDimension
    {
        public int width { get; set; }
        public int height { get; set; }
        //public int rotate { get; set; }
    }

    //public class BangumiSeasonResultEpisodesRights
    //{
    //    public int allow_dm { get; set; }
    //}

    //public class BangumiSeasonResultNewEp
    //{
    //    public string desc { get; set; }
    //    public long id { get; set; }
    //    public int is_new { get; set; }
    //    public string title { get; set; }
    //}

    //public class BangumiSeasonResultPositive
    //{
    //    public long id { get; set; }
    //    public string title { get; set; }
    //}

    //public class BangumiSeasonResultPublish
    //{
    //    public int is_finish { get; set; }
    //    public int is_started { get; set; }
    //    public string pub_time { get; set; }
    //    public string pub_time_show { get; set; }
    //    public int unknow_pub_date { get; set; }
    //    public int weekday { get; set; }
    //}

    //public class BangumiSeasonResultRating
    //{
    //    public long count { get; set; }
    //    public float score { get; set; }
    //}

    //public class BangumiSeasonResultRights
    //{
    //    public int allow_bp { get; set; }
    //    public int allow_bp_rank { get; set; }
    //    public int allow_download { get; set; }
    //    public int allow_review { get; set; }
    //    public int area_limit { get; set; }
    //    public int ban_area_show { get; set; }
    //    public int can_watch { get; set; }
    //    public string copyright { get; set; }
    //    public int forbid_pre { get; set; }
    //    public int is_cover_show { get; set; }
    //    public int is_preview { get; set; }
    //    public int only_vip_download { get; set; }
    //    public string resource { get; set; }
    //    public int watch_platform { get; set; }
    //}

    //public class BangumiSeasonResultSeasons
    //{
    //    public string badge { get; set; }
    //    public BangumiSeasonResultBadgeInfo badge_info { get; set; }
    //    public int badge_type { get; set; }
    //    public string cover { get; set; }
    //    public long media_id { get; set; }
    //    public BangumiSeasonResultSeasonsNewEp new_ep { get; set; }
    //    public long season_id { get; set; }
    //    public string season_title { get; set; }
    //    public int season_type { get; set; }
    //    public BangumiSeasonResultSeasonsStat stat { get; set; }
    //}

    //public class BangumiSeasonResultSeasonsNewEp
    //{
    //    public string cover { get; set; }
    //    public long id { get; set; }
    //    public string index_show { get; set; }
    //}

    //public class BangumiSeasonResultSeasonsStat
    //{
    //    public long favorites { get; set; }
    //    public long series_follow { get; set; }
    //    public long views { get; set; }
    //}

    //public class BangumiSeasonResultSeries
    //{
    //    public long series_id { get; set; }
    //    public string series_title { get; set; }
    //}

    //public class BangumiSeasonResultShow
    //{
    //    public int wide_screen { get; set; }
    //}

    public class BangumiSeasonResultStat
    {
        public long coins { get; set; }
        public long danmakus { get; set; }
        public long favorites { get; set; }
        public long likes { get; set; }
        public long reply { get; set; }
        public long share { get; set; }
        public long views { get; set; }
    }

}
