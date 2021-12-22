using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Models.Json
{
    public class SubRipText : BaseModel
    {
        [JsonProperty("lan")]
        public string Lan { get; set; }
        [JsonProperty("lan_doc")]
        public string LanDoc { get; set; }
        [JsonProperty("srtString")]
        public string SrtString { get; set; }
    }
}
