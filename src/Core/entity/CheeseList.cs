using System.Collections.Generic;

namespace Core.entity
{
    // https://api.bilibili.com/pugv/view/web/ep/list?season_id=112&pn=1
    public class CheeseList
    {
        public int code { get; set; }
        public CheeseListData data { get; set; }
        public string message { get; set; }
    }

    public class CheeseListData
    {
        public List<CheeseListDataItem> items { get; set; }
        public CheeseListDataPage page { get; set; }
    }

    public class CheeseListDataItem
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

    public class CheeseListDataPage
    {
        public bool next { get; set; } // 是否还有下一页
        public int num { get; set; } // 当前页
        public int size { get; set; } // list大小
        public int total { get; set; } // 总的视频数量
    }

}
