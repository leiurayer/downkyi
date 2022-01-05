namespace DownKyi.Core.Storage.Database.Download
{
    public class DownloadedDb : DownloadDb
    {
        public DownloadedDb()
        {
            tableName = "downloaded";
            CreateTable();
        }
    }
}
