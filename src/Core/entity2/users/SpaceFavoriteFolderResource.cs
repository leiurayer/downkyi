using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/v3/fav/resource/list?media_id={mediaId}&pn={pn}&ps={ps}
    [JsonObject]
    public class SpaceFavoriteFolderResourceOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public SpaceFavoriteFolderResource Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class SpaceFavoriteFolderResource : BaseEntity
    {
        [JsonProperty("has_more")]
        public int HasMore { get; set; }
        // ...
        [JsonProperty("medias")]
        public List<SpaceFavoriteFolderMedia> Medias { get; set; }
    }

    [JsonObject]
    public class SpaceFavoriteFolderMedia : BaseEntity
    {
        [JsonProperty("attr")]
        public int Attr { get; set; }
        [JsonProperty("bv_id")]
        public string BvId { get; set; }
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
        [JsonProperty("cnt_info")]
        public SpaceFavoriteFolderMediaCntInfo CntInfo { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("ctime")]
        public long Ctime { get; set; }
        [JsonProperty("duration")]
        public long Duration { get; set; }
        [JsonProperty("fav_time")]
        public long FavTime { get; set; }
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("intro")]
        public string Intro { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("pubtime")]
        public long Pubtime { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("upper")]
        public SpaceFavoriteFolderMediaUpper Upper { get; set; }
    }

    [JsonObject]
    public class SpaceFavoriteFolderMediaCntInfo : BaseEntity
    {
        [JsonProperty("collect")]
        public long Collect { get; set; }
        [JsonProperty("danmaku")]
        public long Danmaku { get; set; }
        [JsonProperty("play")]
        public long Play { get; set; }
    }

    [JsonObject]
    public class SpaceFavoriteFolderMediaUpper : BaseEntity
    {
        [JsonProperty("face")]
        public string Face { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

}
