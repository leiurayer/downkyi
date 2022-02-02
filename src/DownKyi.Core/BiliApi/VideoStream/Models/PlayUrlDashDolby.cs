using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.VideoStream.Models
{
    public class PlayUrlDashDolby : BaseModel
    {
        // type
        [JsonProperty("audio")]
        public List<PlayUrlDashVideo> Audio { get; set; }
    }
}
