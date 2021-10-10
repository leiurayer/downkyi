using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Bangumi.Models
{
    // https://api.bilibili.com/pgc/review/user
    public class BangumiMediaOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("result")]
        public BangumiMediaData Result { get; set; }
    }

    public class BangumiMediaData : BaseModel
    {
        [JsonProperty("media")]
        public BangumiMedia Media { get; set; }
    }

    public class BangumiMedia : BaseModel
    {
        [JsonProperty("areas")]
        public List<BangumiArea> Areas { get; set; }
        [JsonProperty("cover")]
        public string Cover { get; set; }
        [JsonProperty("media_id")]
        public long MediaId { get; set; }
        // new_ep
        // rating
        [JsonProperty("season_id")]
        public long SeasonId { get; set; }
        [JsonProperty("share_url")]
        public string ShareUrl { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("type_name")]
        public string TypeName { get; set; }
    }

}
