using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/pugv/app/web/season/page?mid={mid}&pn={pn}&ps={ps}
    public class SpaceCheeseOrigin : BaseModel
    {
        [JsonProperty("data")]
        public SpaceCheeseData Data { get; set; }
    }

    public class SpaceCheeseData : BaseModel
    {
        [JsonProperty("items")]
        public List<SpaceCheese> Items { get; set; }
        [JsonProperty("page")]
        public SpaceCheesePage Page { get; set; }
    }
}
