using DownKyi.Core.BiliApi.Video;
using DownKyi.Core.BiliApi.Video.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace DownKyi.Core.Test.BiliApi.Video
{
    [TestClass]
    public class DynamicTest
    {
        // 主分区tid
        private readonly int[] typeIdList = { 1, 13, 167, 3, 129, 4, 36, 234, 223, 160, 211, 217, 119, 155, 5, 181, 177, 23, 11 };

        [TestMethod]
        public void TestRegionDynamicList()
        {
            foreach (var tid in typeIdList)
            {
                var time = DateTime.Now;
                Random random = new Random(time.GetHashCode());
                int ps = random.Next(1, 51);

                var regionDynamicList = Dynamic.RegionDynamicList(tid, 1, ps);

                Assert.IsInstanceOfType(regionDynamicList, typeof(List<DynamicVideoView>));
                Assert.IsInstanceOfType(regionDynamicList[0], typeof(DynamicVideoView));
                Assert.IsNotNull(regionDynamicList[0]);

                Assert.AreEqual(regionDynamicList.Count, ps);

            }
        }
    }
}
