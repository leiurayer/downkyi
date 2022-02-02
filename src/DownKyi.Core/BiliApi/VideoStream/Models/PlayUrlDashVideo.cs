using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.VideoStream.Models
{
    public class PlayUrlDashVideo : BaseModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("base_url")]
        public string BaseUrl { get; set; }
        [JsonProperty("backup_url")]
        public List<string> BackupUrl { get; set; }
        // bandwidth
        [JsonProperty("mimeType")]
        public string MimeType { get; set; }
        // mime_type
        [JsonProperty("codecs")]
        public string Codecs { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("frameRate")]
        public string FrameRate { get; set; }
        // frame_rate
        // sar
        // startWithSap
        // start_with_sap
        // SegmentBase
        // segment_base
        // codecid
    }
}
