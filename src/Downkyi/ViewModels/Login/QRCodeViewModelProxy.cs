using Avalonia.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using Downkyi.UI.Mvvm;
using Downkyi.UI.ViewModels.Login;
using QRCoder;
using System.IO;

namespace Downkyi.ViewModels.Login;

public partial class QRCodeViewModelProxy : QRCodeViewModel
{
    #region 页面属性申明

    [ObservableProperty]
    private Bitmap? _QRCode;

    #endregion

    public QRCodeViewModelProxy(BaseServices baseServices) : base(baseServices)
    {
        // 根据请求到的url生成二维码
        _QRCodeAction = new((url) =>
        {
            byte[]? qrcodeByte = GenerateQrCode(url, 12);
            using var ms = new MemoryStream(qrcodeByte);
            Bitmap img = new(ms);
            QRCode = img;
        });
    }

    protected override void InitStatus()
    {
        base.InitStatus();

        QRCode = null;
    }

    /// <summary>
    /// 生成二维码
    /// </summary>
    /// <param name="msg">信息</param>
    /// <param name="version">版本 1 ~ 40</param>
    private static byte[] GenerateQrCode(string msg, int version)
    {
        QRCodeGenerator qrGenerator = new();
        var qrCodeData = qrGenerator.CreateQrCode(msg, QRCodeGenerator.ECCLevel.Q/* 这里设置容错率的一个级别 */, true, false, QRCodeGenerator.EciMode.Utf8, version);
        BitmapByteQRCode qrCode = new(qrCodeData);
        var codeByte = qrCode.GetGraphic(20);
        return codeByte;
    }

}