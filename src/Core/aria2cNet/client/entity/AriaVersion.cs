using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.aria2cNet.client.entity
{
    [JsonObject]
    public class AriaVersion
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("result")]
        public AriaVersionResult Result { get; set; }

        [JsonProperty("error")]
        public AriaError Error { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject]
    public class AriaVersionResult
    {
        [JsonProperty("enabledFeatures")]
        public List<string> EnabledFeatures { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
