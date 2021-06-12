namespace Core.entity
{
    // https://api.bilibili.com/x/web-interface/view/detail?bvid=BV1TJ411h7E6
    public class VideoDetail
    {
        //public int code { get; set; }
        public VideoDetailData data { get; set; }
        //public string message { get; set; }
        //public int ttl { get; set; }
    }

    public class VideoDetailData
    {
        //public VideoDetailDataCard Card { get; set; }
        //public VideoDetailDataRelated Related { get; set; }
        //public VideoDetailDataReply Reply { get; set; }
        //public VideoDetailDataTags Tags { get; set; }
        public VideoDetailDataView View { get; set; }
    }

    public class VideoDetailDataView
    {
        // ...
        public string redirect_url { get; set; }
        // ...
    }

}
