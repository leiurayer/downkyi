using BiliSharp.Api.Login;

namespace BiliSharp.UnitTest.Api.Login;

public class TestLoginQR
{
    [Fact]
    public void TestGenerateQRCode_Default()
    {
        var qrcode = LoginQR.GenerateQRCode();
        Assert.NotNull(qrcode.Data);
    }

    [Fact]
    public void TestPollQRCode_Default()
    {
        var qrcode = LoginQR.GenerateQRCode();
        Assert.NotNull(qrcode.Data);

        var poll = LoginQR.PollQRCode(qrcode.Data.QrcodeKey);
        Assert.NotNull(poll.Data);
    }
}