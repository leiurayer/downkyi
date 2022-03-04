using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.History.Models
{
    // https://api.bilibili.com/x/web-interface/history/cursor?max={startId}&view_at={startTime}&ps={ps}&business={businessStr}
    public class HistoryOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public HistoryData Data { get; set; }
    }

    public class HistoryData : BaseModel
    {
        [JsonProperty("cursor")]
        public HistoryCursor Cursor { get; set; }
        //public List<HistoryDataTab> tab { get; set; }
        [JsonProperty("list")]
        public List<HistoryList> List { get; set; }
    }
}
