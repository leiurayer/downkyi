using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/relation/followers?vmid={mid}&pn={pn}&ps={ps}
    // https://api.bilibili.com/x/relation/followings?vmid={mid}&pn={pn}&ps={ps}&order_type={orderType}
    public class RelationFollowOrigin : BaseModel
    {
        [JsonProperty("data")]
        public RelationFollow Data { get; set; }
    }

    public class RelationFollow : BaseModel
    {
        [JsonProperty("list")]
        public List<RelationFollowInfo> List { get; set; }
        //[JsonProperty("re_version")]
        //public long reVersion { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }

}
