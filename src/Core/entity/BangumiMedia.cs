// 注释掉未使用的属性
namespace Core.entity
{
    // https://api.bilibili.com/pgc/review/user?media_id=28228367
    class BangumiMedia
    {
        //public int code { get; set; }
        //public string message { get; set; }
        public BangumiMediaData result { get; set; }
    }

    public class BangumiMediaData
    {
        public BangumiMediaDataMedia media { get; set; }
        //public BangumiMediaDataReview review { get; set; }
    }

    public class BangumiMediaDataMedia
    {
        // TODO 暂时只实现部分字段
        //public long media_id { get; set; }

        public long season_id { get; set; }
        //public string share_url { get; set; }
        //public string title { get; set; }
        //public string type_name { get; set; }
    }

    //public class BangumiMediaDataReview
    //{
    //    public int is_coin { get; set; }
    //    public int is_open { get; set; }
    //}

}
