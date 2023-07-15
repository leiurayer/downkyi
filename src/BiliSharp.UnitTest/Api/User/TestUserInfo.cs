using BiliSharp.Api.Login;
using BiliSharp.Api.Sign;
using BiliSharp.Api.User;

namespace BiliSharp.UnitTest.Api.User
{
    public class TestUserInfo
    {
        [Fact]
        public void TestGetUserInfo_Default()
        {
            Cookies.GetMyCookies();

            // 设置wbi keys
            var info = LoginInfo.GetNavigationInfo();
            var imgKey = info.Data.WbiImg.ImgUrl.Split('/').ToList().Last().Split('.')[0];
            var subKey = info.Data.WbiImg.SubUrl.Split('/').ToList().Last().Split('.')[0];
            var keys = new Tuple<string, string>(imgKey, subKey);
            WbiSign.SetKey(keys);

            long mid = 42018135;
            var userInfo = UserInfo.GetUserInfo(mid);

            Assert.Equal(mid, userInfo.Data.Mid);
        }

        [Fact]
        public void TestGetUserCard_Default()
        {
            Cookies.GetMyCookies();

            long mid = 42018135;
            var userCard = UserInfo.GetUserCard(mid);

            Assert.Equal(mid.ToString(), userCard.Data.Card.Mid);
        }

        [Fact]
        public void TestGetMyInfo_Default()
        {
            Cookies.GetMyCookies();

            long mid = 42018135;
            var myInfo = UserInfo.GetMyInfo();

            Assert.Equal(mid, myInfo.Data.Mid);
        }

        [Fact]
        public void TestGetUserCards_Default()
        {
            Cookies.GetMyCookies();

            // https://api.vc.bilibili.com/account/v1/user/cards?uids=314521322,206840230,49246269
            List<long> ids = new()
            {
                314521322,
                206840230,
                49246269
            };
            var users = UserInfo.GetUserCards(ids);

            foreach (var user in users.Data)
            {
                Assert.Contains(user.Mid, ids);
            }
        }

    }
}