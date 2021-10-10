namespace DownKyi.Core.Settings.Models
{
    /// <summary>
    /// 基本
    /// </summary>
    public class BasicSettings
    {
        public AfterDownloadOperation AfterDownload { get; set; }
        public AllowStatus IsListenClipboard { get; set; }
        public AllowStatus IsAutoParseVideo { get; set; }
        public ParseScope ParseScope { get; set; }
        public DownloadFinishedSort DownloadFinishedSort { get; set; }
    }
}
