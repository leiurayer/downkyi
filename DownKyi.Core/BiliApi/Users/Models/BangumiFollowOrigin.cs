using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/space/bangumi/follow/list?vmid={mid}&type={type:D}&pn={pn}&ps={ps}
    public class BangumiFollowOrigin : BaseModel
    {
        [JsonProperty("data")]
        public BangumiFollowData Data { get; set; }
    }

    public class BangumiFollowData : BaseModel
    {
        [JsonProperty("list")]
        public List<BangumiFollow> List { get; set; }
        [JsonProperty("pn")]
        public int Pn { get; set; }
        [JsonProperty("ps")]
        public int Ps { get; set; }
        [JsonProperty("total")]
        public int Total { get; set; }
    }

}
