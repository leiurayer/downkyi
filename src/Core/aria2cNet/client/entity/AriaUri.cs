using Newtonsoft.Json;

namespace Core.aria2cNet.client.entity
{
    [JsonObject]
    public class AriaUri
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
