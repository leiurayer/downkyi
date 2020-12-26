using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.aria2cNet.client.entity
{
    [JsonObject]
    public class SystemMulticallMathod
    {
        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public List<object> Params { get; set; }
    }
}
