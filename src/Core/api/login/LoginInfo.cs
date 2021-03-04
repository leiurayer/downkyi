using Core.entity2.login;
using Newtonsoft.Json;
using System;

namespace Core.api.login
{
    /// <summary>
    /// 登录基本信息
    /// </summary>
    public class LoginInfo
    {
        private static LoginInfo instance;

        /// <summary>
        /// 获取UserInfo实例
        /// </summary>
        /// <returns></returns>
        public static LoginInfo GetInstance()
        {
            if (instance == null)
            {
                instance = new LoginInfo();
            }
            return instance;
        }

        /// <summary>
        /// 隐藏LoginInfo()方法，必须使用单例模式
        /// </summary>
        private LoginInfo() { }

        /// <summary>
        /// 导航栏用户信息
        /// </summary>
        /// <returns></returns>
        public UserInfoForNavigation GetUserInfoForNavigation()
        {
            string url = "https://api.bilibili.com/x/web-interface/nav";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var userInfo = JsonConvert.DeserializeObject<UserInfoForNavigationOrigin>(response);
                if (userInfo == null || userInfo.Data == null) { return null; }

                if (userInfo.Data.IsLogin) { return userInfo.Data; }
                else { return null; }
            }
            catch (Exception e)
            {
                Console.WriteLine("GetUserInfoForNavigation()发生异常: {0}", e);
                return null;
            }
        }

    }
}
