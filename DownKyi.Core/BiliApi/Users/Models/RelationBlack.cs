using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/relation/blacks?pn={pn}&ps={ps}
    public class RelationBlack : BaseModel
    {
        [JsonProperty("data")]
        public List<RelationFollowInfo> Data { get; set; }
    }
}
