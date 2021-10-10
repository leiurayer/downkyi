using Newtonsoft.Json;
using System.Collections.Generic;

namespace DownKyi.Core.Aria2cNet.Client.Entity
{
    [JsonObject]
    public class AriaTellStatus
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("result")]
        public AriaTellStatusResult Result { get; set; }

        [JsonProperty("error")]
        public AriaError Error { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject]
    public class AriaTellStatusList
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("result")]
        public List<AriaTellStatusResult> Result { get; set; }

        [JsonProperty("error")]
        public AriaError Error { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject]
    public class AriaTellStatusResult
    {
        [JsonProperty("bitfield")]
        public string Bitfield { get; set; }

        [JsonProperty("completedLength")]
        public string CompletedLength { get; set; }

        [JsonProperty("connections")]
        public string Connections { get; set; }

        [JsonProperty("dir")]
        public string Dir { get; set; }

        [JsonProperty("downloadSpeed")]
        public string DownloadSpeed { get; set; }

        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("files")]
        public List<AriaTellStatusResultFile> Files { get; set; }

        [JsonProperty("gid")]
        public string Gid { get; set; }

        [JsonProperty("numPieces")]
        public string NumPieces { get; set; }

        [JsonProperty("pieceLength")]
        public string PieceLength { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("totalLength")]
        public string TotalLength { get; set; }

        [JsonProperty("uploadLength")]
        public string UploadLength { get; set; }

        [JsonProperty("uploadSpeed")]
        public string UploadSpeed { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    [JsonObject]
    public class AriaTellStatusResultFile
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
