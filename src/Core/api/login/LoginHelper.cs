using Core.settings;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;

namespace Core.api.login
{
    public class LoginHelper
    {
        private static LoginHelper instance;

        /// <summary>
        /// 获取LoginHelper实例
        /// </summary>
        /// <returns></returns>
        public static LoginHelper GetInstance()
        {
            if (instance == null)
            {
                instance = new LoginHelper();
            }
            return instance;
        }

        /// <summary>
        /// 隐藏LoginHelper()方法，必须使用单例模式
        /// </summary>
        private LoginHelper() { }

        private static readonly string LOCAL_LOGIN_INFO = Common.ConfigPath + "Login";
        private static readonly string SecretKey = "Ps*rB$TGaM#&JvOe"; // 16位密码，ps:密码位数没有限制，可任意设置

        /// <summary>
        /// 获得登录二维码
        /// </summary>
        /// <returns></returns>
        public BitmapImage GetLoginQRCode()
        {
            try
            {
                string loginUrl = Login.GetInstance().GetLoginUrl().Data.Url;
                return GetLoginQRCode(loginUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine("GetLoginQRCode()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 根据输入url生成二维码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public BitmapImage GetLoginQRCode(string url)
        {
            // 设置的参数影响app能否成功扫码
            Bitmap qrCode = Utils.EncodeQRCode(url, 10, 10, null, 0, 0, false);

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

        /// <summary>
        /// 保存登录的cookies到文件
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public bool SaveLoginInfoCookies(string url)
        {
            if (!Directory.Exists(Common.ConfigPath))
            {
                Directory.CreateDirectory(Common.ConfigPath);
            }

            string tempFile = LOCAL_LOGIN_INFO + "-" + Guid.NewGuid().ToString("N");
            CookieContainer cookieContainer = Utils.ParseCookie(url);

            bool isSucceed = Utils.WriteCookiesToDisk(tempFile, cookieContainer);
            if (isSucceed)
            {
                // 加密密钥，增加机器码
                string password = SecretKey + MachineCode.GetMachineCodeString();

                try
                {
                    Encryptor.EncryptFile(tempFile, LOCAL_LOGIN_INFO, password);
                }
                catch (Exception e)
                {
                    Console.WriteLine("SaveLoginInfoCookies()发生异常: {0}", e);
                    return false;
                }
            }

            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
            return isSucceed;
        }

        /// <summary>
        /// 获得登录的cookies
        /// </summary>
        /// <returns></returns>
        public CookieContainer GetLoginInfoCookies()
        {
            string tempFile = LOCAL_LOGIN_INFO + "-" + Guid.NewGuid().ToString("N");

            if (File.Exists(LOCAL_LOGIN_INFO))
            {
                try
                {
                    // 加密密钥，增加机器码
                    string password = SecretKey + MachineCode.GetMachineCodeString();
                    Encryptor.DecryptFile(LOCAL_LOGIN_INFO, tempFile, password);
                }
                catch (CryptoHelpException e)
                {
                    Console.WriteLine("GetLoginInfoCookies()发生异常: {0}", e);
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                    return null;
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine("GetLoginInfoCookies()发生异常: {0}", e);
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                    return null;
                }
                catch (DirectoryNotFoundException e)
                {
                    Console.WriteLine("GetLoginInfoCookies()发生异常: {0}", e);
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                    return null;
                }
                catch (Exception e)
                {
                    Console.WriteLine("GetLoginInfoCookies()发生异常: {0}", e);
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);
                    }
                    return null;
                }
            }
            else { return null; }

            CookieContainer cookies = Utils.ReadCookiesFromDisk(tempFile);

            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
            return cookies;
        }

        /// <summary>
        /// 返回登录信息的cookies的字符串
        /// </summary>
        /// <returns></returns>
        public string GetLoginInfoCookiesString()
        {
            var cookieContainer = GetLoginInfoCookies();
            if (cookieContainer == null)
            {
                return "";
            }

            var cookies = Utils.GetAllCookies(cookieContainer);

            string cookie = string.Empty;
            foreach (var item in cookies)
            {
                cookie += item.ToString() + ";";
            }
            return cookie.TrimEnd(';');
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <returns></returns>
        public bool Logout()
        {
            if (File.Exists(LOCAL_LOGIN_INFO))
            {
                try
                {
                    File.Delete(LOCAL_LOGIN_INFO);

                    Settings.GetInstance().SetUserInfo(new UserInfoForSetting
                    {
                        Mid = -1,
                        Name = "",
                        IsLogin = false,
                        IsVip = false
                    });
                    return true;
                }
                catch (IOException e)
                {
                    Console.WriteLine("Logout()发生异常: {0}", e);
                    return false;
                }
            }
            return false;
        }

    }
}
