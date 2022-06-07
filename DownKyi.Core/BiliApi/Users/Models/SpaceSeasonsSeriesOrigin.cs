using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/space/channel/video?mid={mid}&page_num={pageNum}&page_size={pageSize}
    public class SpaceSeasonsSeriesOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public SpaceSeasonsSeriesData Data { get; set; }
    }

    public class SpaceSeasonsSeriesData : BaseModel
    {
        [JsonProperty("items_lists")]
        public SpaceSeasonsSeries ItemsLists { get; set; }
    }

    public class SpaceSeasonsSeries : BaseModel
    {
        [JsonProperty("page")]
        public SpaceSeasonsSeriesPage Page { get; set; }
        [JsonProperty("seasons_list")]
        public List<SpaceSeasons> SeasonsList { get; set; }
        [JsonProperty("series_list")]
        public List<SpaceSeries> SeriesList { get; set; }
    }

}
