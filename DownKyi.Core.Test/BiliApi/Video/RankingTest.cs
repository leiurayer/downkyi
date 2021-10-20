using DownKyi.Core.BiliApi.Video;
using DownKyi.Core.BiliApi.Video.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace DownKyi.Core.Test.BiliApi.Video
{
    [TestClass]
    public class RankingTest
    {
        // 主分区tid
        private readonly int[] typeIdList = { 1, 13, 167, 3, 129, 4, 36, 234, 223, 160, 211, 217, 119, 155, 5, 181, 177, 23, 11 };

        [TestMethod]
        public void TestRegionRankingList()
        {

            foreach (var tid in typeIdList)
            {
                var regionRankingList = Ranking.RegionRankingList(tid, 3);

                Assert.IsInstanceOfType(regionRankingList, typeof(List<RankingVideoView>));
                Assert.IsInstanceOfType(regionRankingList[0], typeof(RankingVideoView));
                Assert.IsNotNull(regionRankingList[0]);

                regionRankingList = Ranking.RegionRankingList(tid, 7);

                Assert.IsInstanceOfType(regionRankingList, typeof(List<RankingVideoView>));
                Assert.IsInstanceOfType(regionRankingList[0], typeof(RankingVideoView));
                Assert.IsNotNull(regionRankingList[0]);
            }
        }
    }
}
