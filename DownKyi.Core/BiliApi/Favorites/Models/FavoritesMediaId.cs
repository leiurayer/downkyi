using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Favorites.Models
{
    // https://api.bilibili.com/x/v3/fav/resource/ids
    public class FavoritesMediaIdOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public List<FavoritesMediaId> Data { get; set; }
    }

    public class FavoritesMediaId : BaseModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("type")]
        public int Type { get; set; }
        [JsonProperty("bv_id")]
        public string BvId { get; set; }
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
    }

}
