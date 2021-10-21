using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Favorites.Models
{
    // https://api.bilibili.com/x/v3/fav/folder/info
    public class FavoritesMetaInfoOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public FavoritesMetaInfo Data { get; set; }
    }

    public class FavoritesMetaInfo : BaseModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("fid")]
        public long Fid { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        // attr
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("upper")]
        public FavUpper Upper { get; set; }
        // cover_type
        [JsonProperty("cnt_info")]
        public FavStatus CntInfo { get; set; }
        // type
        [JsonProperty("intro")]
        public string Intro { get; set; }
        [JsonProperty("ctime")]
        public long Ctime { get; set; }
        [JsonProperty("mtime")]
        public long Mtime { get; set; }
        // state
        [JsonProperty("fav_state")]
        public int FavState { get; set; }
        [JsonProperty("like_state")]
        public int LikeState { get; set; }
        [JsonProperty("media_count")]
        public int MediaCount { get; set; }
    }

    public class FavUpper : BaseModel
    {
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("face")]
        public string Face { get; set; }
        [JsonProperty("followed")]
        public bool Followed { get; set; }
        // vip_type
        // vip_statue
    }

    public class FavStatus : BaseModel
    {
        [JsonProperty("collect")]
        public long Collect { get; set; }
        [JsonProperty("play")]
        public long Play { get; set; }
        [JsonProperty("thumb_up")]
        public long ThumbUp { get; set; }
        [JsonProperty("share")]
        public long Share { get; set; }
    }

}
