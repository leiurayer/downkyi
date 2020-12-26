using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.aria2cNet.client.entity
{
    [JsonObject]
    public class AriaGetServers
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("result")]
        public List<AriaGetServersResult> Result { get; set; }

        [JsonProperty("error")]
        public AriaError Error { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject]
    public class AriaGetServersResult
    {
        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("servers")]
        public List<AriaResultServer> Servers { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject]
    public class AriaResultServer
    {
        [JsonProperty("currentUri")]
        public string CurrentUri { get; set; }

        [JsonProperty("downloadSpeed")]
        public string DownloadSpeed { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
