using BiliSharp.Api.Login;

namespace BiliSharp.UnitTest.Api.Login;

public class TestLoginInfo
{
    [Fact]
    public void TestGetNavigationInfo_Default()
    {
        Cookies.GetMyCookies();

        long mid = 42018135;
        var info = LoginInfo.GetNavigationInfo();
        Assert.Equal(mid, info.Data.Mid);
    }

    [Fact]
    public void TestGetLoginInfoStat_Default()
    {
        Cookies.GetMyCookies();

        var stat = LoginInfo.GetLoginInfoStat();
        Assert.NotNull(stat);
    }

    [Fact]
    public void TestGetMyCoin_Default()
    {
        Cookies.GetMyCookies();

        var coin = LoginInfo.GetMyCoin();
        Assert.NotNull(coin);
    }

}