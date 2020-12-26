using Newtonsoft.Json;

namespace Core.aria2cNet.client.entity
{
    [JsonObject]
    public class AriaGetSessionInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("result")]
        public AriaGetSessionInfoResult Result { get; set; }

        [JsonProperty("error")]
        public AriaError Error { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject]
    public class AriaGetSessionInfoResult
    {
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
