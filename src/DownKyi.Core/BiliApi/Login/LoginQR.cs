using DownKyi.Core.BiliApi.Login.Models;
using DownKyi.Core.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace DownKyi.Core.BiliApi.Login
{
    /// <summary>
    /// 二维码登录
    /// </summary>
    public static class LoginQR
    {
        /// <summary>
        /// 申请二维码URL及扫码密钥（web端）
        /// </summary>
        /// <returns></returns>
        public static LoginUrlOrigin GetLoginUrl()
        {
            string getLoginUrl = "https://passport.bilibili.com/qrcode/getLoginUrl";
            string referer = "https://passport.bilibili.com/login";
            string response = WebClient.RequestWeb(getLoginUrl, referer);

            try
            {
                var loginUrl = JsonConvert.DeserializeObject<LoginUrlOrigin>(response);
                return loginUrl;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetLoginUrl()发生异常: {0}", e);
                LogManager.Error("LoginQR", e);
                return null;
            }
        }

        /// <summary>
        /// 使用扫码登录（web端）
        /// </summary>
        /// <param name="oauthKey"></param>
        /// <param name="goUrl"></param>
        /// <returns></returns>
        public static LoginStatus GetLoginStatus(string oauthKey, string goUrl = "https://www.bilibili.com")
        {
            string url = "https://passport.bilibili.com/qrcode/getLoginInfo";
            string referer = "https://passport.bilibili.com/login";

            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "oauthKey", oauthKey },
                { "gourl", goUrl }
            };

            string response = WebClient.RequestWeb(url, referer, "POST", parameters);
            var loginInfo = new LoginStatus();

            try
            {
                if (response.Contains("\"code\":0") || response.Contains("\"code\": 0"))
                {
                    var ready = JsonConvert.DeserializeObject<LoginStatusReady>(response);
                    if (ready == null)
                    { return null; }

                    loginInfo.Code = ready.Code;
                    loginInfo.Status = ready.Status;
                    loginInfo.Message = "登录成功";
                    loginInfo.Url = ready.Data.Url;
                }
                else
                {
                    var scanning = JsonConvert.DeserializeObject<LoginStatusScanning>(response);
                    if (scanning == null)
                    { return null; }

                    loginInfo.Code = scanning.Data;
                    loginInfo.Status = scanning.Status;
                    loginInfo.Message = scanning.Message;
                    loginInfo.Url = "";
                }

                return loginInfo;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetLoginInfo()发生异常: {0}", e);
                LogManager.Error("LoginQR", e);
                return null;
            }
        }


        /// <summary>
        /// 获得登录二维码
        /// </summary>
        /// <returns></returns>
        public static BitmapImage GetLoginQRCode()
        {
            try
            {
                string loginUrl = GetLoginUrl().Data.Url;
                return GetLoginQRCode(loginUrl);
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetLoginQRCode()发生异常: {0}", e);
                LogManager.Error("LoginQR", e);
                return null;
            }
        }

        /// <summary>
        /// 根据输入url生成二维码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static BitmapImage GetLoginQRCode(string url)
        {
            // 设置的参数影响app能否成功扫码
            Bitmap qrCode = Utils.QRCode.EncodeQRCode(url, 12, 10, null, 0, 0, false);

            MemoryStream ms = new MemoryStream();
            qrCode.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] bytes = ms.GetBuffer();
            ms.Close();

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(bytes);
            image.EndInit();
            return image;
        }

    }
}
