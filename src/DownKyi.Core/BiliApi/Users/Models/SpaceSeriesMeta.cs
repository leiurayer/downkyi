using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/series/series?series_id={seriesId}
    public class SpaceSeriesMetaOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public SpaceSeriesMetaData Data { get; set; }
    }

    public class SpaceSeriesMetaData : BaseModel
    {
        [JsonProperty("meta")]
        public SpaceSeriesMeta Meta { get; set; }
        [JsonProperty("recent_aids")]
        public List<long> RecentAids { get; set; }
    }

}
