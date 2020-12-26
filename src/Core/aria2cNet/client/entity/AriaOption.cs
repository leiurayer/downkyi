using Newtonsoft.Json;

namespace Core.aria2cNet.client.entity
{
    [JsonObject]
    public class AriaOption
    {
        [JsonProperty("all-proxy")]
        public string AllProxy { get; set; }

        [JsonProperty("allow-overwrite")]
        public string AllowOverwrite { get; set; }

        [JsonProperty("allow-piece-length-change")]
        public string AllowPieceLengthChange { get; set; }

        [JsonProperty("always-resume")]
        public string AlwaysResume { get; set; }

        [JsonProperty("async-dns")]
        public string AsyncDns { get; set; }

        [JsonProperty("auto-file-renaming")]
        public string AutoFileRenaming { get; set; }

        [JsonProperty("bt-enable-hook-after-hash-check")]
        public string BtEnableHookAfterHashCheck { get; set; }

        [JsonProperty("bt-enable-lpd")]
        public string BtEnableLpd { get; set; }

        [JsonProperty("bt-force-encryption")]
        public string BtForceEncryption { get; set; }

        [JsonProperty("bt-hash-check-seed")]
        public string BtHashCheckSeed { get; set; }

        [JsonProperty("bt-load-saved-metadata")]
        public string BtLoadSavedMetadata { get; set; }

        [JsonProperty("bt-max-peers")]
        public string BtMaxPeers { get; set; }

        [JsonProperty("bt-metadata-only")]
        public string BtMetadataOnly { get; set; }

        [JsonProperty("bt-min-crypto-level")]
        public string BtMinCryptoLevel { get; set; }

        [JsonProperty("bt-remove-unselected-file")]
        public string BtRemoveUnselectedFile { get; set; }

        [JsonProperty("bt-request-peer-speed-limit")]
        public string BtRequestPeerSpeedLimit { get; set; }

        [JsonProperty("bt-require-crypto")]
        public string BtRequireCrypto { get; set; }

        [JsonProperty("bt-save-metadata")]
        public string BtSaveMetadata { get; set; }

        [JsonProperty("bt-seed-unverified")]
        public string BtSeedUnverified { get; set; }

        [JsonProperty("bt-stop-timeout")]
        public string BtStopTimeout { get; set; }

        [JsonProperty("bt-tracker-connect-timeout")]
        public string BtTrackerConnectTimeout { get; set; }

        [JsonProperty("bt-tracker-interval")]
        public string BtTrackerInterval { get; set; }

        [JsonProperty("bt-tracker-timeout")]
        public string BtTrackerTimeout { get; set; }

        [JsonProperty("check-integrity")]
        public string CheckIntegrity { get; set; }

        [JsonProperty("conditional-get")]
        public string ConditionalGet { get; set; }

        [JsonProperty("connect-timeout")]
        public string ConnectTimeout { get; set; }

        [JsonProperty("content-disposition-default-utf8")]
        public string ContentDispositionDefaultUtf8 { get; set; }

        [JsonProperty("continue")]
        public string Continue { get; set; }

        [JsonProperty("dir")]
        public string Dir { get; set; }

        [JsonProperty("dry-run")]
        public string DryRun { get; set; }

        [JsonProperty("enable-http-keep-alive")]
        public string EnableHttpKeepAlive { get; set; }

        [JsonProperty("enable-http-pipelining")]
        public string EnableHttpPipelining { get; set; }

        [JsonProperty("enable-mmap")]
        public string EnableMmap { get; set; }

        [JsonProperty("enable-peer-exchange")]
        public string EnablePeerExchange { get; set; }

        [JsonProperty("file-allocation")]
        public string FileAllocation { get; set; }

        [JsonProperty("follow-metalink")]
        public string FollowMetalink { get; set; }

        [JsonProperty("follow-torrent")]
        public string FollowTorrent { get; set; }

        [JsonProperty("force-save")]
        public string ForceSave { get; set; }

        [JsonProperty("ftp-pasv")]
        public string FtpPasv { get; set; }

        [JsonProperty("ftp-reuse-connection")]
        public string FtpReuseConnection { get; set; }

        [JsonProperty("ftp-type")]
        public string FtpType { get; set; }

        [JsonProperty("hash-check-only")]
        public string HashCheckOnly { get; set; }

        [JsonProperty("http-accept-gzip")]
        public string HttpAcceptGzip { get; set; }

        [JsonProperty("http-auth-challenge")]
        public string HttpAuthChallenge { get; set; }

        [JsonProperty("http-no-cache")]
        public string HttpNoCache { get; set; }

        [JsonProperty("lowest-speed-limit")]
        public string LowestSpeedLimit { get; set; }

        [JsonProperty("max-connection-per-server")]
        public string MaxConnectionPerServer { get; set; }

        [JsonProperty("max-download-limit")]
        public string MaxDownloadLimit { get; set; }

        [JsonProperty("max-file-not-found")]
        public string MaxFileNotFound { get; set; }

        [JsonProperty("max-mmap-limit")]
        public string MaxMmapLimit { get; set; }

        [JsonProperty("max-resume-failure-tries")]
        public string MaxResumeFailureTries { get; set; }

        [JsonProperty("max-tries")]
        public string MaxTries { get; set; }

        [JsonProperty("max-upload-limit")]
        public string MaxUploadLimit { get; set; }

        [JsonProperty("metalink-enable-unique-protocol")]
        public string MetalinkEnableUniqueProtocol { get; set; }

        [JsonProperty("metalink-preferred-protocol")]
        public string MetalinkPreferredProtocol { get; set; }

        [JsonProperty("min-split-size")]
        public string MinSplitSize { get; set; }

        [JsonProperty("no-file-allocation-limit")]
        public string NoFileAllocationLimit { get; set; }

        [JsonProperty("no-netrc")]
        public string NoNetrc { get; set; }

        [JsonProperty("out")]
        public string Out { get; set; }

        [JsonProperty("parameterized-uri")]
        public string ParameterizedUri { get; set; }

        [JsonProperty("pause-metadata")]
        public string PauseMetadata { get; set; }

        [JsonProperty("piece-length")]
        public string PieceLength { get; set; }

        [JsonProperty("proxy-method")]
        public string ProxyMethod { get; set; }

        [JsonProperty("realtime-chunk-checksum")]
        public string RealtimeChunkChecksum { get; set; }

        [JsonProperty("remote-time")]
        public string RemoteTime { get; set; }

        [JsonProperty("remove-control-file")]
        public string RemoveControlFile { get; set; }

        [JsonProperty("retry-wait")]
        public string RetryWait { get; set; }

        [JsonProperty("reuse-uri")]
        public string ReuseUri { get; set; }

        [JsonProperty("rpc-save-upload-metadata")]
        public string RpcSaveUploadMetadata { get; set; }

        [JsonProperty("save-not-found")]
        public string SaveNotFound { get; set; }

        [JsonProperty("seed-ratio")]
        public string SeedRatio { get; set; }

        [JsonProperty("split")]
        public string Split { get; set; }

        [JsonProperty("stream-piece-selector")]
        public string StreamPieceSelector { get; set; }

        [JsonProperty("timeout")]
        public string Timeout { get; set; }

        [JsonProperty("uri-selector")]
        public string UriSelector { get; set; }

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
