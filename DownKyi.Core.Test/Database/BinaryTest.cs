using DownKyi.Core.BiliApi.VideoStream;
using DownKyi.Core.Storage.Database.Download;
using DownKyi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DownKyi.Core.Test.Database
{
    [TestClass]
    public class BinaryTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Downloading downloading = new Downloading
            {
                Gid = "gid",
                PlayStreamType = PlayStreamType.VIDEO,
                DownloadContent = "视频",
                DownloadStatusTitle = "下载中",
                Progress = 50,
                DownloadingFileSize = "60MB/120MB",
                MaxSpeed = 123456,
                SpeedDisplay = "5MB/s",
            };

            DownloadingDb db = new DownloadingDb();
            db.Insert("testId", downloading);
        }

        [TestMethod]
        public void TestMethod2()
        {
            DownloadingDb db = new DownloadingDb();
            Dictionary<string, object> query = db.QueryAll();

            Downloading downloading = (Downloading)query["testId"];

            Console.WriteLine(downloading.Gid);
            Console.WriteLine(downloading.PlayStreamType);
            Console.WriteLine(downloading.DownloadContent);
            Console.WriteLine(downloading.DownloadStatusTitle);
            Console.WriteLine(downloading.Progress);
            Console.WriteLine(downloading.DownloadingFileSize);
            Console.WriteLine(downloading.MaxSpeed);
            Console.WriteLine(downloading.SpeedDisplay);
        }
    }
}
