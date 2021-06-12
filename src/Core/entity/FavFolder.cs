using System.Collections.Generic;

namespace Core.entity
{
    /// <summary>
    /// https://api.bilibili.com/x/v3/fav/folder/created/list?pn=1&ps=10&up_mid=42018135
    /// https://api.bilibili.com/x/v3/fav/folder/collected/list?pn=1&ps=20&up_mid=42018135
    /// 上述两个网址都使用同一个class解析
    /// </summary>
    public class FavFolder
    {
        //public int code { get; set; }
        public FavFolderData data { get; set; }
        //public string message { get; set; }
        //public int ttl { get; set; }
    }

    public class FavFolderData
    {
        public int count { get; set; }
        public List<FavFolderDataList> list { get; set; }
    }

    public class FavFolderDataList
    {
        public int attr { get; set; }
        public string cover { get; set; }
        public int cover_type { get; set; }
        public long ctime { get; set; }
        public int fav_state { get; set; }
        public long fid { get; set; }
        public long id { get; set; }
        public string intro { get; set; }
        public int media_count { get; set; }
        public long mid { get; set; }
        public long mtime { get; set; }
        public int state { get; set; }
        public string title { get; set; }
        public FavFolderDataUpper upper { get; set; }
    }

    public class FavFolderDataUpper
    {
        public string face { get; set; }
        public long mid { get; set; }
        public string name { get; set; }
    }

}
