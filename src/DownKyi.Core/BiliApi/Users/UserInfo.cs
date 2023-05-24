using DownKyi.Core.BiliApi.Sign;
using DownKyi.Core.BiliApi.Users.Models;
using DownKyi.Core.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users
{
    /// <summary>
    /// 用户基本信息
    /// </summary>
    public static class UserInfo
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
                UserInfoForNavigationOrigin userInfo = JsonConvert.DeserializeObject<UserInfoForNavigationOrigin>(response);
                if (userInfo == null || userInfo.Data == null) { return null; }

                if (userInfo.Data.IsLogin) { return userInfo.Data; }
                else { return null; }
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetUserInfoForNavigation()发生异常: {0}", e);
                LogManager.Error("UserInfo", e);
                return null;
            }
        }

        /// <summary>
        /// 用户空间详细信息
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public static UserInfoForSpace GetUserInfoForSpace(long mid)
        {
            var parameters = new Dictionary<string, object>
            {
                { "mid", mid }
            };
            string query = WbiSign.ParametersToQuery(WbiSign.EncodeWbi(parameters));
            string url = $"https://api.bilibili.com/x/space/wbi/acc/info?{query}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                UserInfoForSpaceOrigin spaceInfo = JsonConvert.DeserializeObject<UserInfoForSpaceOrigin>(response);
                if (spaceInfo == null || spaceInfo.Data == null) { return null; }
                return spaceInfo.Data;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetInfoForSpace()发生异常: {0}", e);
                LogManager.Error("UserInfo", e);
                return null;
            }
        }

        /// <summary>
        /// 本用户详细信息
        /// </summary>
        /// <returns></returns>
        public static MyInfo GetMyInfo()
        {
            string url = "https://api.bilibili.com/x/space/myinfo";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                MyInfoOrigin myInfo = JsonConvert.DeserializeObject<MyInfoOrigin>(response);
                if (myInfo == null || myInfo.Data == null) { return null; }
                return myInfo.Data;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetMyInfo()发生异常: {0}", e);
                LogManager.Error("UserInfo", e);
                return null;
            }
        }

    }
}
