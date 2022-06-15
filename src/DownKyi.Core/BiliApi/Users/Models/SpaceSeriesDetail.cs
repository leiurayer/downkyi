using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/series/archives?mid={mid}&series_id={seriesId}&only_normal=true&sort=desc&pn={pn}&ps={ps}
    public class SpaceSeriesDetailOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public SpaceSeriesDetail Data { get; set; }
    }

    public class SpaceSeriesDetail : BaseModel
    {
        [JsonProperty("aids")]
        public List<long> Aids { get; set; }
        // page
        [JsonProperty("archives")]
        public List<SpaceSeasonsSeriesArchives> Archives { get; set; }
    }

}
