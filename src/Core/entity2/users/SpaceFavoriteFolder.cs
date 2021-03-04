using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/v3/fav/folder/created/list?up_mid={mid}&pn={pn}&ps={ps}
    // https://api.bilibili.com/x/v3/fav/folder/collected/list?up_mid={mid}&pn={pn}&ps={ps}
    [JsonObject]
    public  class SpaceFavoriteFolderOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public SpaceFavoriteFolder Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class SpaceFavoriteFolder : BaseEntity
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("has_more")]
        public int HasMore { get; set; }
        [JsonProperty("list")]
        public List<SpaceFavoriteFolderList> List { get; set; }
    }

    [JsonObject]
    public class SpaceFavoriteFolderList : BaseEntity
    {
        [JsonProperty("attr")]
        public int Attr { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("cover_type")]
        public int CoverType { get; set; }
        [JsonProperty("ctime")]
        public long Ctime { get; set; }
        [JsonProperty("fav_state")]
        public int FavState { get; set; }
        [JsonProperty("fid")]
        public long Fid { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("intro")]
        public string Intro { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("media_count")]
        public int MediaCount { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("mtime")]
        public long Mtime { get; set; }
        [JsonProperty("state")]
        public int State { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("upper")]
        public FavoriteFolderUpper Upper { get; set; }
        // ...
    }

    [JsonObject]
    public class FavoriteFolderUpper : BaseEntity
    {
        [JsonProperty("face")]
        public string Face { get; set; }
        [JsonProperty("mid")] 
        public long Mid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

}
