using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.Aria2cNet.Client.Entity
{
    [JsonObject]
    public class AriaGetPeers
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("result")]
        public List<AriaPeer> Result { get; set; }

        [JsonProperty("error")]
        public AriaError Error { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject]
    public class AriaPeer
    {
        [JsonProperty("amChoking")]
        public string AmChoking { get; set; }

        [JsonProperty("bitfield")]
        public string Bitfield { get; set; }

        [JsonProperty("downloadSpeed")]
        public string DownloadSpeed { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }

        [JsonProperty("peerChoking")]
        public string PeerChoking { get; set; }

        [JsonProperty("peerId")]
        public string PeerId { get; set; }

        [JsonProperty("port")]
        public string Port { get; set; }

        [JsonProperty("seeder")]
        public string Seeder { get; set; }

        [JsonProperty("uploadSpeed")]
        public string UploadSpeed { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
