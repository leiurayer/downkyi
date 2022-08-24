using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/polymer/space/seasons_archives_list?mid={mid}&season_id={seasonId}&page_num={pageNum}&page_size={pageSize}&sort_reverse=false
    public class SpaceSeasonsDetailOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public SpaceSeasonsDetail Data { get; set; }
    }

    public class SpaceSeasonsDetail : BaseModel
    {
        [JsonProperty("aids")]
        public List<long> Aids { get; set; }
        [JsonProperty("archives")]
        public List<SpaceSeasonsSeriesArchives> Archives { get; set; }
        [JsonProperty("meta")]
        public SpaceSeasonsMeta Meta { get; set; }
        [JsonProperty("page")]
        public SpaceSeasonsSeriesPage Page { get; set; }
    }

}
