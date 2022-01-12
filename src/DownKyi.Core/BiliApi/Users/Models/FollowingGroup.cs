using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/relation/tags
    public class FollowingGroupOrigin : BaseModel
    {
        [JsonProperty("data")]
        public List<FollowingGroup> Data { get; set; }
    }

    public class FollowingGroup : BaseModel
    {
        [JsonProperty("tagid")]
        public int TagId { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("tip")]
        public string Tip { get; set; }
    }

}
