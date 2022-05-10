using DownKyi.Core.Storage.Database.Download;
using DownKyi.Models;
using DownKyi.ViewModels.DownloadManager;
using System.Collections.Generic;

namespace DownKyi.Services.Download
{
    public class DownloadStorageService
    {
        ~DownloadStorageService()
        {
            DownloadingDb downloadingDb = new DownloadingDb();
            downloadingDb.Close();
            DownloadedDb downloadedDb = new DownloadedDb();
            downloadedDb.Close();
            DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
            downloadBaseDb.Close();
        }

        #region 下载中数据

        /// <summary>
        /// 添加下载中数据
        /// </summary>
        /// <param name="downloadingItem"></param>
        public void AddDownloading(DownloadingItem downloadingItem)
        {
            if (downloadingItem == null || downloadingItem.DownloadBase == null) { return; }

            AddDownloadBase(downloadingItem.DownloadBase);

            DownloadingDb downloadingDb = new DownloadingDb();
            object obj = downloadingDb.QueryById(downloadingItem.DownloadBase.Uuid);
            if (obj == null)
            {
                downloadingDb.Insert(downloadingItem.DownloadBase.Uuid, downloadingItem.Downloading);
            }
            //downloadingDb.Close();
        }

        /// <summary>
        /// 删除下载中数据
        /// </summary>
        /// <param name="downloadingItem"></param>
        public void RemoveDownloading(DownloadingItem downloadingItem)
        {
            if (downloadingItem == null || downloadingItem.DownloadBase == null) { return; }

            RemoveDownloadBase(downloadingItem.DownloadBase.Uuid);

            DownloadingDb downloadingDb = new DownloadingDb();
            downloadingDb.Delete(downloadingItem.DownloadBase.Uuid);
            //downloadingDb.Close();
        }

        /// <summary>
        /// 获取所有的下载中数据
        /// </summary>
        /// <returns></returns>
        public List<DownloadingItem> GetDownloading()
        {
            // 从数据库获取数据
            DownloadingDb downloadingDb = new DownloadingDb();
            Dictionary<string, object> dic = downloadingDb.QueryAll();
            //downloadingDb.Close();

            // 遍历
            List<DownloadingItem> list = new List<DownloadingItem>();
            foreach (KeyValuePair<string, object> item in dic)
            {
                if (item.Value is Downloading downloading)
                {
                    DownloadingItem downloadingItem = new DownloadingItem
                    {
                        DownloadBase = GetDownloadBase(item.Key),
                        Downloading = downloading
                    };

                    if (downloadingItem.DownloadBase == null) { continue; }
                    list.Add(downloadingItem);
                }
            }

            return list;
        }

        /// <summary>
        /// 修改下载中数据
        /// </summary>
        /// <param name="downloadingItem"></param>
        public void UpdateDownloading(DownloadingItem downloadingItem)
        {
            if (downloadingItem == null || downloadingItem.DownloadBase == null) { return; }

            UpdateDownloadBase(downloadingItem.DownloadBase);

            DownloadingDb downloadingDb = new DownloadingDb();
            downloadingDb.Update(downloadingItem.DownloadBase.Uuid, downloadingItem.Downloading);
            //downloadingDb.Close();
        }

        #endregion

        #region 下载完成数据

        /// <summary>
        /// 添加下载完成数据
        /// </summary>
        /// <param name="downloadedItem"></param>
        public void AddDownloaded(DownloadedItem downloadedItem)
        {
            if (downloadedItem == null || downloadedItem.DownloadBase == null) { return; }

            AddDownloadBase(downloadedItem.DownloadBase);

            DownloadedDb downloadedDb = new DownloadedDb();
            object obj = downloadedDb.QueryById(downloadedItem.DownloadBase.Uuid);
            if (obj == null)
            {
                downloadedDb.Insert(downloadedItem.DownloadBase.Uuid, downloadedItem.Downloaded);
            }
            //downloadedDb.Close();
        }

        /// <summary>
        /// 删除下载完成数据
        /// </summary>
        /// <param name="downloadedItem"></param>
        public void RemoveDownloaded(DownloadedItem downloadedItem)
        {
            if (downloadedItem == null || downloadedItem.DownloadBase == null) { return; }

            RemoveDownloadBase(downloadedItem.DownloadBase.Uuid);

            DownloadedDb downloadedDb = new DownloadedDb();
            downloadedDb.Delete(downloadedItem.DownloadBase.Uuid);
            //downloadedDb.Close();
        }

        /// <summary>
        /// 获取所有的下载完成数据
        /// </summary>
        /// <returns></returns>
        public List<DownloadedItem> GetDownloaded()
        {
            // 从数据库获取数据
            DownloadedDb downloadedDb = new DownloadedDb();
            Dictionary<string, object> dic = downloadedDb.QueryAll();
            //downloadedDb.Close();

            // 遍历
            List<DownloadedItem> list = new List<DownloadedItem>();
            foreach (KeyValuePair<string, object> item in dic)
            {
                if (item.Value is Downloaded downloaded)
                {
                    DownloadedItem downloadedItem = new DownloadedItem
                    {
                        DownloadBase = GetDownloadBase(item.Key),
                        Downloaded = downloaded
                    };

                    if (downloadedItem.DownloadBase == null) { continue; }
                    list.Add(downloadedItem);
                }
            }

            return list;
        }

        /// <summary>
        /// 修改下载完成数据
        /// </summary>
        /// <param name="downloadedItem"></param>
        public void UpdateDownloaded(DownloadedItem downloadedItem)
        {
            if (downloadedItem == null || downloadedItem.DownloadBase == null) { return; }

            UpdateDownloadBase(downloadedItem.DownloadBase);

            DownloadedDb downloadedDb = new DownloadedDb();
            downloadedDb.Update(downloadedItem.DownloadBase.Uuid, downloadedItem.Downloaded);
            //downloadedDb.Close();
        }

        #endregion

        #region DownloadBase

        /// <summary>
        /// 向数据库添加DownloadBase
        /// </summary>
        /// <param name="downloadBase"></param>
        private void AddDownloadBase(DownloadBase downloadBase)
        {
            if (downloadBase == null) { return; }

            DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
            object obj = downloadBaseDb.QueryById(downloadBase.Uuid);
            if (obj == null)
            {
                downloadBaseDb.Insert(downloadBase.Uuid, downloadBase);
            }
            //downloadBaseDb.Close();
        }

        /// <summary>
        /// 从数据库删除DownloadBase
        /// </summary>
        /// <param name="downloadBase"></param>
        private void RemoveDownloadBase(string uuid)
        {
            DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
            downloadBaseDb.Delete(uuid);
            //downloadBaseDb.Close();
        }

        /// <summary>
        /// 从数据库获取所有的DownloadBase
        /// </summary>
        /// <returns></returns>
        private DownloadBase GetDownloadBase(string uuid)
        {
            DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
            object obj = downloadBaseDb.QueryById(uuid);
            //downloadBaseDb.Close();

            return obj is DownloadBase downloadBase ? downloadBase : null;
        }

        /// <summary>
        /// 从数据库修改DownloadBase
        /// </summary>
        /// <param name="downloadBase"></param>
        private void UpdateDownloadBase(DownloadBase downloadBase)
        {
            if (downloadBase == null) { return; }

            DownloadBaseDb downloadBaseDb = new DownloadBaseDb();
            downloadBaseDb.Update(downloadBase.Uuid, downloadBase);
            //downloadBaseDb.Close();
        }

        #endregion

    }
}
