using BiliSharp.Api.Login;
using BiliSharp.Api.Sign;
using BiliSharp.Api.Video;

namespace BiliSharp.UnitTest.Api.Video
{
    public class TestVideoInfo
    {
        [Fact]
        public void TestGetVideoViewInfo_Default()
        {
            // 设置wbi keys
            var info = LoginInfo.GetNavigationInfo();
            var imgKey = info.Data.WbiImg.ImgUrl.Split('/').ToList().Last().Split('.')[0];
            var subKey = info.Data.WbiImg.SubUrl.Split('/').ToList().Last().Split('.')[0];
            var keys = new Tuple<string, string>(imgKey, subKey);
            WbiSign.SetKey(keys);

            string bvid = "BV1Pu4y1y7FA";
            long aid = 915570400;
            var videoInfo = VideoInfo.GetVideoViewInfo(bvid, aid);

            Assert.Equal(bvid, videoInfo.Data.View.Bvid);
            Assert.Equal(aid, videoInfo.Data.View.Aid);
        }

    }
}