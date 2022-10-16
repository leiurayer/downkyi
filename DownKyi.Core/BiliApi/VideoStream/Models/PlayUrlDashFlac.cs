using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.VideoStream.Models
{
    public class PlayUrlDashFlac : BaseModel
    {
        [JsonProperty("audio")]
        public PlayUrlDashVideo Audio { get; set; }
        //bool display { get; set; }
    }
}
