using System.Collections.Generic;

// 注释掉未使用的属性
namespace Core.entity
{
    public class PlayUrl
    {
        //public int code { get; set; }
        public PlayUrlData data { get; set; }
        public PlayUrlData result { get; set; }
        //public string message { get; set; }
        //public int ttl { get; set; }
    }

    public class PlayUrlData
    {
        public List<string> accept_description { get; set; }
        //public string accept_format { get; set; }
        public List<int> accept_quality { get; set; }
        public PlayUrlDataDash dash { get; set; }
        //public string format { get; set; }
        //public string from { get; set; }
        //public string message { get; set; }
        public int quality { get; set; }
        //public string result { get; set; }
        //public string seek_param { get; set; }
        //public string seek_type { get; set; }
        //public string timelength { get; set; }
        //public int video_codecid { get; set; }

        public List<PlayUrlDUrl> durl { get; set; }
    }

    public class PlayUrlDataDash
    {
        public List<PlayUrlDataDashVideo> audio { get; set; }
        public long duration { get; set; }
        //public float minBufferTime { get; set; }
        //public float min_buffer_time { get; set; }
        public List<PlayUrlDataDashVideo> video { get; set; }
    }

    public class PlayUrlDataDashVideo
    {
        //public PlayUrlDataDashSegmentBaseCls SegmentBase { get; set; }
        public List<string> backupUrl { get; set; }
        public List<string> backup_url { get; set; }
        //public long bandwidth { get; set; }
        public string baseUrl { get; set; }
        public string base_url { get; set; }
        //public int codecid { get; set; }
        public string codecs { get; set; }
        public string frameRate { get; set; }
        //public string frame_rate { get; set; }
        public int height { get; set; }
        public int id { get; set; }
        public string mimeType { get; set; }
        //public string mime_type { get; set; }
        //public string sar { get; set; }
        //public PlayUrlDataDashSegmentBaseCls2 segment_base { get; set; }
        //public int startWithSap { get; set; }
        //public int start_with_sap { get; set; }
        public int width { get; set; }
    }

    //public class PlayUrlDataDashSegmentBaseCls
    //{
    //    public string Initialization { get; set; }
    //    public string indexRange { get; set; }
    //}

    //public class PlayUrlDataDashSegmentBaseCls2
    //{
    //    public string initialization { get; set; }
    //    public string index_range { get; set; }
    //}

    public class PlayUrlDUrl
    {
        //public int order { get; set; }
        public long length { get; set; }
        public long size { get; set; }
        //public string ahead { get; set; }
        //public string vhead { get; set; }
        public string url { get; set; }
        public List<string> backup_url { get; set; }
    }

}
