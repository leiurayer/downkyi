using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/space/channel/video?mid={mid}&cid={cid}&pn={pn}&ps={ps}
    public class SpaceChannelVideoOrigin : BaseModel
    {
        [JsonProperty("data")]
        public SpaceChannelVideo Data { get; set; }
    }

    public class SpaceChannelVideo : BaseModel
    {
        // episodic_button
        [JsonProperty("list")]
        public SpaceChannelVideoList List { get; set; }
        [JsonProperty("page")]
        public SpaceChannelVideoPage Page { get; set; }
    }
}
