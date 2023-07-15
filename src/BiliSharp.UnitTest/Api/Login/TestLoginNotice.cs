using BiliSharp.Api.Login;

namespace BiliSharp.UnitTest.Api.Login;

public class TestLoginNotice
{
    [Fact]
    public void TestGetLoginNotice_Default()
    {
        Cookies.GetMyCookies();

        long mid = 42018135;
        var notice = LoginNotice.GetLoginNotice(mid);
        Assert.Equal(mid, notice.Data.Mid);
    }
}