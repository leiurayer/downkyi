using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.Aria2cNet.Client.Entity
{
    [JsonObject]
    public class AriaSendData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public List<object> Params { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject]
    public class AriaSendOption
    {
        [JsonProperty("all-proxy")]
        public string HttpProxy { get; set; }

        [JsonProperty("out")]
        public string Out { get; set; }

        [JsonProperty("dir")]
        public string Dir { get; set; }

        [JsonProperty("header")]
        public string Header { get; set; }

        [JsonProperty("use-head")]
        public string UseHead { get; set; }

        [JsonProperty("user-agent")]
        public string UserAgent { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
