using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.VideoStream.Models
{
    public class PlayUrlDash : BaseModel
    {
        [JsonProperty("duration")]
        public long Duration { get; set; }
        //[JsonProperty("minBufferTime")]
        //public float minBufferTime { get; set; }
        //[JsonProperty("min_buffer_time")]
        //public float min_buffer_time { get; set; }
        [JsonProperty("video")]
        public List<PlayUrlDashVideo> Video { get; set; }
        [JsonProperty("audio")]
        public List<PlayUrlDashVideo> Audio { get; set; }
        [JsonProperty("dolby")]
        public PlayUrlDashDolby Dolby { get; set; }
    }
}
