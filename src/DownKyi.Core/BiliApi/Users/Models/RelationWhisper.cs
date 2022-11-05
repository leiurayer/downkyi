using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users.Models
{
    // https://api.bilibili.com/x/relation/whispers?pn={pn}&ps={ps}
    public class RelationWhisper : BaseModel
    {
        [JsonProperty("data")]
        public RelationWhisperData Data { get; set; }
    }

    public class RelationWhisperData : BaseModel
    {
        [JsonProperty("list")]
        public List<RelationFollowInfo> List { get; set; }
        // re_version
    }

}
