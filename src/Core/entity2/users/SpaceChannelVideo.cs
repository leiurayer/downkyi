using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/space/channel/video?mid={mid}&cid={cid}&pn={pn}&ps={ps}
    [JsonObject]
    public class SpaceChannelVideoOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public SpaceChannelVideo Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class SpaceChannelVideo : BaseEntity
    {
        [JsonProperty("list")]
        public SpaceChannelVideoList List { get; set; }
        [JsonProperty("page")]
        public SpaceChannelVideoPage Page { get; set; }
    }

    [JsonObject]
    public class SpaceChannelVideoList : BaseEntity
    {
        [JsonProperty("archives")]
        public List<SpaceChannelVideoArchive> Archives { get; set; }
        [JsonProperty("cid")]
        public long Cid { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("intro")]
        public string Intro { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("mtime")]
        public long Mtime { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    [JsonObject]
    public class SpaceChannelVideoPage : BaseEntity
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("num")]
        public int Num { get; set; }
        [JsonProperty("size")]
        public int Size { get; set; }
    }

    [JsonObject]
    public class SpaceChannelVideoArchive : BaseEntity
    {
        [JsonProperty("aid")]
        public long Aid { get; set; }
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
        [JsonProperty("cid")]
        public long Cid { get; set; }
        // ...
        [JsonProperty("ctime")]
        public long Ctime { get; set; }
        [JsonProperty("desc")]
        public string Desc { get; set; }
        // ...
        [JsonProperty("duration")]
        public long Duration { get; set; }
        // ...
        [JsonProperty("pic")]
        public string Pic { get; set; }
        [JsonProperty("pubdate")]
        public long Pubdate { get; set; }
        // ...
        [JsonProperty("stat")]
        public SpaceChannelVideoArchiveStat Stat { get; set; }
        // ...
        [JsonProperty("tid")]
        public int Tid { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("tname")]
        public string Tname { get; set; }
    }

    [JsonObject]
    public class SpaceChannelVideoArchiveStat : BaseEntity
    {
        // ...
        [JsonProperty("view")]
        public long View { get; set; }
    }

}
