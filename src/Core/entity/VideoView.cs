using System.Collections.Generic;

// 注释掉未使用的属性
namespace Core.entity
{
    public class VideoView
    {
        public int code { get; set; }
        public VideoViewData data { get; set; }
        public string message { get; set; }
        public int ttl { get; set; }
    }

    public class VideoViewData
    {
        public long aid { get; set; }
        //public long attribute { get; set; }
        public string bvid { get; set; }
        //public long cid { get; set; }
        //public int copyright { get; set; }
        public long ctime { get; set; }
        public string desc { get; set; }
        //public VideoViewDataDimension dimension { get; set; }
        //public long duration { get; set; }
        //public string dynamic { get; set; }
        //public VideoViewDataLabel label { get; set; }
        //public long mission_id { get; set; }
        //public bool no_cache { get; set; }
        public VideoViewDataOwner owner { get; set; }
        public List<VideoViewDataPages> pages { get; set; }
        public string pic { get; set; }
        public long pubdate { get; set; }
        //public VideoViewDataRights rights { get; set; }
        //public long season_id { get; set; }
        public VideoViewDataStat stat { get; set; }
        //public int state { get; set; }
        //public VideoViewDataSubtitle subtitle { get; set; }
        public long tid { get; set; }
        public string title { get; set; }
        public string tname { get; set; }
        //public VideoViewDataUgcSeason ugc_season { get; set; }
        //public int videos { get; set; }
    }

    public class VideoViewDataDimension
    {
        public int width { get; set; }
        public int height { get; set; }
        //public int rotate { get; set; }
    }

    //public class VideoViewDataLabel
    //{
    //    public int type { get; set; }
    //}

    public class VideoViewDataOwner
    {
        public string face { get; set; }
        public long mid { get; set; }
        public string name { get; set; }
    }

    public class VideoViewDataPages
    {
        public long cid { get; set; }
        public VideoViewDataDimension dimension { get; set; }
        public long duration { get; set; }
        //public string from { get; set; }
        public int page { get; set; }
        public string part { get; set; }
        //public string vid { get; set; }
        //public string weblink { get; set; }
    }

    //public class VideoViewDataRights
    //{
    //    public int autoplay { get; set; }
    //    public int bp { get; set; }
    //    public int download { get; set; }
    //    public int elec { get; set; }
    //    public int hd5 { get; set; }
    //    public int is_cooperation { get; set; }
    //    public int movie { get; set; }
    //    public int no_background { get; set; }
    //    public int no_reprint { get; set; }
    //    public int pay { get; set; }
    //    public int ugc_pay { get; set; }
    //    public int ugc_pay_preview { get; set; }
    //}

    public class VideoViewDataStat
    {
        public long aid { get; set; }
        public long coin { get; set; }
        public long danmaku { get; set; }
        public long dislike { get; set; }
        public string evaluation { get; set; }
        public long favorite { get; set; }
        public long his_rank { get; set; }
        public long like { get; set; }
        public long now_rank { get; set; }
        public long reply { get; set; }
        public long share { get; set; }
        public long view { get; set; }
    }

    //public class VideoViewDataSubtitle
    //{
    //    public bool allow_submit { get; set; }
    //    //public List<string> list { get; set; }
    //}

    //public class VideoViewDataUgcSeason
    //{
    //    public int attribute { get; set; }
    //    public string cover { get; set; }
    //    public int ep_count { get; set; }
    //    public long id { get; set; }
    //    public string intro { get; set; }
    //    public long mid { get; set; }
    //    public List<VideoViewDataUgcSeasonSections> sections { get; set; }
    //    public int sign_state { get; set; }
    //    public VideoViewDataStat stat { get; set; }
    //    public string title { get; set; }
    //}

    //public class VideoViewDataUgcSeasonSections
    //{
    //    public List<VideoViewDataUgcSeasonSectionsEpisodes> episodes { get; set; }
    //    public long id { get; set; }
    //    public long season_id { get; set; }
    //    public string title { get; set; }
    //    public int type { get; set; }
    //}

    //public class VideoViewDataUgcSeasonStat
    //{
    //    public long coin { get; set; }
    //    public long danmaku { get; set; }
    //    public long favorite { get; set; }
    //    public long his_rank { get; set; }
    //    public long like { get; set; }
    //    public long now_rank { get; set; }
    //    public long reply { get; set; }
    //    public long season_id { get; set; }
    //    public long share { get; set; }
    //    public long view { get; set; }
    //}

    //public class VideoViewDataUgcSeasonSectionsEpisodes
    //{
    //    public long aid { get; set; }
    //    public VideoViewDataUgcSeasonSectionsEpisodesArc arc { get; set; }
    //    public int attribute { get; set; }
    //    public long cid { get; set; }
    //    public long id { get; set; }
    //    public VideoViewDataUgcSeasonSectionsEpisodesPage page { get; set; }
    //    public long season_id { get; set; }
    //    public long section_id { get; set; }
    //    public string title { get; set; }
    //}

    //public class VideoViewDataUgcSeasonSectionsEpisodesArc
    //{
    //    public long aid { get; set; }
    //    public int copyright { get; set; }
    //    public long ctime { get; set; }
    //    public string desc { get; set; }
    //    public VideoViewDataDimension dimension { get; set; }
    //    public long duration { get; set; }
    //    public string dynamic { get; set; }
    //    public VideoViewDataOwner owner { get; set; }
    //    public string pic { get; set; }
    //    public long pubdate { get; set; }
    //    public VideoViewDataRights rights { get; set; }
    //    public VideoViewDataStat stat { get; set; }
    //    public int state { get; set; }
    //    public long tid { get; set; }
    //    public string title { get; set; }
    //    public string tname { get; set; }
    //    public int videos { get; set; }
    //}

    //public class VideoViewDataUgcSeasonSectionsEpisodesPage
    //{
    //    public long cid { get; set; }
    //    public VideoViewDataDimension dimension { get; set; }
    //    public long duration { get; set; }
    //    public string from { get; set; }
    //    public int page { get; set; }
    //    public string part { get; set; }
    //    public string vid { get; set; }
    //    public string weblink { get; set; }
    //}


    // https://api.bilibili.com/x/player/pagelist?bvid={bvid}&jsonp=jsonp
    public class Pagelist
    {
        public int code { get; set; }
        public List<VideoViewDataPages> data { get; set; }
        public string message { get; set; }
        public int ttl { get; set; }
    }

}
