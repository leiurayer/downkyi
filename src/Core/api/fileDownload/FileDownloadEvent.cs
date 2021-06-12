namespace Core.api.fileDownload
{
    public class FileDownloadEvent
    {

        //private long _downloadSize;
        //private long _totalSize;
        public FileDownloadEvent() { }

        // 每秒下载的速度 B/s 
        public float Speed { get; set; }

        public float Percent
        {
            get { return DownloadSize * 100.0f / TotalSize; }
        }

        public long DownloadSize { get; set; }

        public long TotalSize { get; set; }


    }
}
