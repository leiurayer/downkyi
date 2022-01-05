namespace DownKyi.Core.Storage.Database.Download
{
    public class DownloadBaseDb : DownloadDb
    {
        public DownloadBaseDb()
        {
            tableName = "download_base";
            CreateTable();
        }
    }
}
