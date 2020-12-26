using Newtonsoft.Json;

namespace Core.aria2cNet.client.entity
{
    //{
    //"id": "downkyi",
    //"jsonrpc": "2.0",
    //"result": "1aac102a4875c8cd"
    //}
    [JsonObject]
    public class AriaAddUri
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("error")]
        public AriaError Error { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
