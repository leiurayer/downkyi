using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Favorites.Models
{
    // https://api.bilibili.com/x/v3/fav/resource/list
    public class FavoritesMediaResourceOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public FavoritesMediaResource Data { get; set; }
    }

    public class FavoritesMediaResource : BaseModel
    {
        [JsonProperty("info")]
        public FavoritesMetaInfo Info { get; set; }
        [JsonProperty("medias")]
        public List<FavoritesMedia> Medias { get; set; }
        [JsonProperty("has_more")]
        public int HasMore { get; set; }
    }

}
