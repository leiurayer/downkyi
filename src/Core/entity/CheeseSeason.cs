using System.Collections.Generic;

namespace Core.entity
{
    // https://api.bilibili.com/pugv/view/web/season?ep_id=2126
    class CheeseSeason
    {
        //public int code { get; set; }
        public CheeseSeasonData data { get; set; }
        //public string message { get; set; }
    }

    public class CheeseSeasonData
    {

        public string cover { get; set; }

        public List<CheeseSeasonDataEpisode> episodes { get; set; }

        public long season_id { get; set; }

        public CheeseSeasonDataStat stat { get; set; }

        public string subtitle { get; set; }
        public string title { get; set; }

    }

    public class CheeseSeasonDataEpisode
    {
        public long aid { get; set; }
        public long cid { get; set; }
        public long duration { get; set; }
        public string from { get; set; }
        public long id { get; set; } // ep_id
        public int index { get; set; }
        public int page { get; set; }
        public long play { get; set; }
        public long release_date { get; set; }
        public int status { get; set; }
        public string title { get; set; }
        public bool watched { get; set; }
        public int watchedHistory { get; set; }
    }

    public class CheeseSeasonDataStat
    {
        public long play { get; set; }
        public string play_desc { get; set; }
    }

}
