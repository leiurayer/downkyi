using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Favorites.Models
{
    // https://api.bilibili.com/x/v3/fav/folder/collected/list
    public class FavoritesListOrigin : BaseModel
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
        [JsonProperty("data")]
        public FavoritesList Data { get; set; }
    }

    public class FavoritesList : BaseModel
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("list")]
        public List<FavoritesMetaInfo> List { get; set; }
        [JsonProperty("has_more")]
        public int HasMore { get; set; }
    }

}
