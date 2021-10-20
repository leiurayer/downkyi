using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.VideoStream.Models
{
    public class PlayUrlDurl : BaseModel
    {
        [JsonProperty("order")]
        public int Order { get; set; }
        [JsonProperty("length")]
        public long Length { get; set; }
        [JsonProperty("size")]
        public long Size { get; set; }
        // ahead
        // vhead
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("backup_url")]
        public List<string> BackupUrl { get; set; }
    }
}
