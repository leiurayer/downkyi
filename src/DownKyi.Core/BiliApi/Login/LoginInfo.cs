using DownKyi.Core.BiliApi.Login.Models;
using DownKyi.Core.Logging;
using Newtonsoft.Json;
using System;

namespace DownKyi.Core.BiliApi.Login
{
    /// <summary>
    /// 登录基本信息
    /// </summary>
    public static class LoginInfo
    {

        /// <summary>
        /// 导航栏用户信息
        /// </summary>
        /// <returns></returns>
        public static UserInfoForNavigation GetUserInfoForNavigation()
        {
            string url = "https://api.bilibili.com/x/web-interface/nav";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                var userInfo = JsonConvert.DeserializeObject<UserInfoForNavigationOrigin>(response);
                if (userInfo == null || userInfo.Data == null) { return null; }

                if (userInfo.Data.IsLogin) { return userInfo.Data; }
                else { return null; }
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetUserInfoForNavigation()发生异常: {0}", e);
                LogManager.Error("LoginInfo", e);
                return null;
            }
        }
    }
}
