using Newtonsoft.Json;

namespace Core.aria2cNet.client.entity
{
    /*
     {
    "id": "qwer",
    "jsonrpc": "2.0",
    "result": {
        "downloadSpeed": "0",
        "numActive": "0",
        "numStopped": "0",
        "numStoppedTotal": "0",
        "numWaiting": "0",
        "uploadSpeed": "0"
    }
    }
     */
    [JsonObject]
    public class AriaGetGlobalStat
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("result")]
        public AriaGetGlobalStatResult Result { get; set; }

        [JsonProperty("error")]
        public AriaError Error { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject]
    public class AriaGetGlobalStatResult
    {
        [JsonProperty("downloadSpeed")]
        public string DownloadSpeed { get; set; }

        [JsonProperty("numActive")]
        public string NumActive { get; set; }

        [JsonProperty("numStopped")]
        public string NumStopped { get; set; }

        [JsonProperty("numStoppedTotal")]
        public string NumStoppedTotal { get; set; }

        [JsonProperty("numWaiting")]
        public string NumWaiting { get; set; }

        [JsonProperty("uploadSpeed")]
        public string UploadSpeed { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

}
