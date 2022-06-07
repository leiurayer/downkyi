using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpaceSeasonsSeriesPage : BaseModel
    {
        [JsonProperty("page_num")]
        public int PageNum;
        [JsonProperty("page_size")]
        public int PageSize;
        [JsonProperty("total")]
        public int Total;
    }
}
