using DownKyi.Core.BiliApi.Danmaku;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace DownKyi.Core.Test.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string url = "https://api.bilibili.com/x/v2/dm/web/seg.so?type=1&oid=421769791&pid=420986799&segment_index=1";
            string localFile = @"C:\Users\FlySelf\Desktop\test.proto";
            WebClient mywebclient = new WebClient();
            mywebclient.DownloadFile(url, localFile);

        }

        [TestMethod]
        public void TestMethod2()
        {
            var danmakus = DanmakuProtobuf.GetAllDanmakuProto(420986799, 421769791);

            Console.WriteLine(danmakus.Count);

            foreach (var dan in danmakus)
            {
                Console.WriteLine(dan.ToString());
            }
        }

    }
}
