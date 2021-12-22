using DownKyi.Models;
using System.Collections.ObjectModel;

namespace DownKyi.Services.Download
{
    public class DownloadService
    {
        protected string Tag = "DownloadService";

        protected ObservableCollection<DownloadingItem> downloadingList;
        protected ObservableCollection<DownloadedItem> downloadedList;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        public DownloadService(ObservableCollection<DownloadingItem> downloadingList, ObservableCollection<DownloadedItem> downloadedList)
        {
            this.downloadingList = downloadingList;
            this.downloadedList = downloadedList;
        }

    }
}
