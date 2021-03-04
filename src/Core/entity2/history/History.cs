using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.entity2.history
{
    // https://api.bilibili.com/x/web-interface/history/cursor?max={startId}&view_at={startTime}&ps={ps}&business={businessStr}
    [JsonObject]
    public class HistoryOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public HistoryData Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class HistoryData : BaseEntity
    {
        [JsonProperty("cursor")]
        public HistoryDataCursor Cursor { get; set; }
        [JsonProperty("list")]
        public List<HistoryDataList> List { get; set; }
        //public List<HistoryDataTab> tab { get; set; }
    }

    [JsonObject]
    public class HistoryDataCursor : BaseEntity
    {
        [JsonProperty("business")]
        public string Business { get; set; }
        [JsonProperty("max")]
        public long Max { get; set; }
        [JsonProperty("ps")]
        public int Ps { get; set; }
        [JsonProperty("view_at")]
        public long ViewAt { get; set; }
    }

    [JsonObject]
    public class HistoryDataList : BaseEntity
    {
        [JsonProperty("author_face")]
        public string AuthorFace { get; set; }
        [JsonProperty("author_mid")]
        public long AuthorMid { get; set; }
        [JsonProperty("author_name")]
        public string AuthorName { get; set; }
        // ...
        [JsonProperty("cover")]
        public string Cover { get; set; }
        // ...
        [JsonProperty("duration")]
        public long Duration { get; set; }
        [JsonProperty("history")]
        public HistoryDataListHistory History { get; set; }
        // ...
        [JsonProperty("new_desc")]
        public string NewDesc { get; set; }
        [JsonProperty("progress")]
        public long Progress { get; set; }
        [JsonProperty("show_title")]
        public string ShowTitle { get; set; }
        [JsonProperty("tag_name")]
        public string TagName { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        // ...
        [JsonProperty("uri")]
        public string Uri { get; set; }
        [JsonProperty("videos")]
        public int Videos { get; set; }
        [JsonProperty("view_at")]
        public long ViewAt { get; set; }
    }

    [JsonObject]
    public class HistoryDataListHistory : BaseEntity
    {
        [JsonProperty("business")]
        public string Business { get; set; }
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
        [JsonProperty("cid")]
        public long Cid { get; set; }
        [JsonProperty("dt")]
        public int Dt { get; set; }
        [JsonProperty("epid")]
        public long Epid { get; set; }
        [JsonProperty("oid")]
        public long Oid { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("part")]
        public string Part { get; set; }
    }

}
