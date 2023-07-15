using BiliSharp.Api.User;

namespace BiliSharp.UnitTest.Api.User
{
    public class TestNickname
    {
        [Fact]
        public void TestCheckNickname_Default()
        {
            Equal("downkyi", 0);
            Equal("maozedong", 40002);
            Equal("//", 40004);
            Equal("test0000000000000", 40005);
            Equal("0", 40006);
            Equal("test", 40014);
        }

        private static void Equal(string nickname, int code)
        {
            var response = Nickname.CheckNickname(nickname);
            Assert.Equal(code, response.Code);
        }

    }
}