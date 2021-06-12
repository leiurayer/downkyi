using System.Collections.Generic;

namespace Core.entity
{
    // https://api.bilibili.com/x/v3/fav/resource/list?media_id=94341835&pn=1&ps=20
    public class FavResource
    {
        //public int code { get; set; }
        public FavResourceData data { get; set; }
        //public string message { get; set; }
        //public int ttl { get; set; }
    }

    public class FavResourceData
    {
        //public FavResourceDataInfo info{get;set;}
        public List<FavResourceDataMedia> medias { get; set; }
    }

    public class FavResourceDataMedia
    {
        public int attr { get; set; }
        public string bv_id { get; set; }
        public string bvid { get; set; }
        public FavResourceDataMediaCnt_info cnt_info { get; set; }
        public string cover { get; set; }
        public long ctime { get; set; }
        public long duration { get; set; }
        public long fav_time { get; set; }
        public long id { get; set; }
        public string intro { get; set; }
        public string link { get; set; }
        public int page { get; set; }
        public long pubtime { get; set; }
        public string title { get; set; }
        public int type { get; set; }
        public FavResourceDataMediaUpper upper { get; set; }
    }

    public class FavResourceDataMediaCnt_info
    {
        public long collect { get; set; }
        public long danmaku { get; set; }
        public long play { get; set; }
    }

    public class FavResourceDataMediaUpper
    {
        public string face { get; set; }
        public long mid { get; set; }
        public string name { get; set; }
    }

}
