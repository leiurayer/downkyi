using System;
using System.Collections.Generic;

namespace Core.api.fileDownload
{
    [Serializable]
    internal class FileDownloadConfig
    {
        public long DownloadSize { get; set; }
        public long TotalSize { get; set; }
        public Queue<ThreadDownloadInfo> DownloadQueue { get; set; }

        /// <summary>
        /// 保存文件下载的配置信息，包括总字节数、已下载字节数、下载队列信息
        /// </summary>
        /// <param name="configFile"></param>
        /// <param name="downloadSize"></param>
        /// <param name="totalSize"></param>
        /// <param name="downloadQueue"></param>
        public static void SaveConfig(string configFile, long downloadSize, long totalSize, Queue<ThreadDownloadInfo> downloadQueue)
        {
            var config = new FileDownloadConfig()
            {
                DownloadSize = downloadSize,
                TotalSize = totalSize,
                DownloadQueue = downloadQueue
            };
            Utils.WriteObjectToDisk(configFile, config);
        }

        /// <summary>
        /// 读取文件下载的配置信息，包括总字节数、已下载字节数、下载队列信息
        /// </summary>
        /// <param name="configFile"></param>
        /// <returns></returns>
        public static FileDownloadConfig ReadConfig(string configFile)
        {
            return (FileDownloadConfig)Utils.ReadObjectFromDisk(configFile);
        }

    }
}
