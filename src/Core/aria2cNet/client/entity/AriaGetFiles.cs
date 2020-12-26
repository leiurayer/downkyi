using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.aria2cNet.client.entity
{
    [JsonObject]
    public class AriaGetFiles
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("result")]
        public List<AriaUri> Result { get; set; }

        [JsonProperty("error")]
        public AriaError Error { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject]
    public class AriaGetFilesResult
    {
        [JsonProperty("completedLength")]
        public string CompletedLength { get; set; }

        [JsonProperty("index")]
        public string Index { get; set; }

        [JsonProperty("length")]
        public string Length { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("selected")]
        public string Selected { get; set; }

        [JsonProperty("uris")]
        public List<AriaUri> Uris { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
