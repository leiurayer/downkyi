namespace DownKyi.Core.Storage.Database.Download
{
    public class DownloadingDb : DownloadDb
    {
        public DownloadingDb()
        {
            tableName = "downloading";
            CreateTable();
        }
    }
}
