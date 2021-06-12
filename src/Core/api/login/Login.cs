using Core.entity2.login;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Core.api.login
{
    public class Login
    {
        private static Login instance;

        /// <summary>
        /// 获取Login实例
        /// </summary>
        /// <returns></returns>
        public static Login GetInstance()
        {
            if (instance == null)
            {
                instance = new Login();
            }
            return instance;
        }

        /// <summary>
        /// 隐藏Login()方法，必须使用单例模式
        /// </summary>
        private Login() { }

        /// <summary>
        /// 申请二维码URL及扫码密钥（web端）
        /// </summary>
        /// <returns></returns>
        public LoginUrlOrigin GetLoginUrl()
        {
            string getLoginUrl = "https://passport.bilibili.com/qrcode/getLoginUrl";
            string referer = "https://passport.bilibili.com/login";
            string response = Utils.RequestWeb(getLoginUrl, referer);

            try
            {
                var loginUrl = JsonConvert.DeserializeObject<LoginUrlOrigin>(response);
                return loginUrl;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetLoginUrl()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 使用扫码登录（web端）
        /// </summary>
        /// <param name="oauthKey"></param>
        /// <param name="goUrl"></param>
        /// <returns></returns>
        public LoginStatus GetLoginStatus(string oauthKey, string goUrl = "https://www.bilibili.com")
        {
            string url = "https://passport.bilibili.com/qrcode/getLoginInfo";
            string referer = "https://passport.bilibili.com/login";

            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "oauthKey", oauthKey },
                { "gourl", goUrl }
            };

            string response = Utils.RequestWeb(url, referer, "POST", parameters);
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
                Console.WriteLine("GetLoginInfo()发生异常: {0}", e);
                return null;
            }
        }

    }
}
