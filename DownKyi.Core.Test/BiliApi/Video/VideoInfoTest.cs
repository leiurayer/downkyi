using DownKyi.Core.BiliApi.Video;
using DownKyi.Core.BiliApi.Video.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DownKyi.Core.Test.BiliApi.Video
{
    [TestClass]
    public class VideoInfoTest
    {
        // 主分区tid
        private readonly int[] typeIdList = { 1, 13, 167, 3, 129, 4, 36, 234, 223, 160, 211, 217, 119, 155, 5, 181, 177, 23, 11 };

        [TestMethod]
        public void TestVideoViewInfo()
        {
            foreach (var tid in typeIdList)
            {
                var regionDynamicList = Dynamic.RegionDynamicList(tid, 1, 12);
                Assert.IsNotNull(regionDynamicList);

                foreach (var videoView in regionDynamicList)
                {
                    long aid = videoView.Aid;
                    string bvid = videoView.Bvid;

                    var bv = VideoInfo.VideoViewInfo(bvid, -1);
                    Assert.IsNotNull(bv);
                    Assert.IsInstanceOfType(bv, typeof(VideoView));
                    Assert.IsNotNull(bv.Pages);

                    var av = VideoInfo.VideoViewInfo(null, aid);
                    Assert.IsNotNull(av);
                    Assert.IsInstanceOfType(av, typeof(VideoView));
                    Assert.IsNotNull(av.Pages);

                }

            }

        }
    }
}
