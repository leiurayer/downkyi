using Newtonsoft.Json;

namespace DownKyi.Core.Aria2cNet.Client.Entity
{
    //"error": {
    //    "code": 1,
    //    "message": "Unauthorized"
    //}
    [JsonObject]
    public class AriaError
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
