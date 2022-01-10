using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/relation/tag?tagid={tagId}&pn={pn}&ps={ps}&order_type={orderType}
    public class FollowingGroupContent : BaseModel
    {
        [JsonProperty("data")]
        public List<RelationFollowInfo> Data { get; set; }
    }

}
