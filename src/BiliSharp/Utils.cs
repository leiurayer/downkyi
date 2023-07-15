using System;
using System.Net;
using System.Text.Json;

namespace BiliSharp
{
    internal static class Utils
    {
        internal static T GetData<T>(string url)
        {
            string referer = "https://www.bilibili.com";
            string userAgent = BiliManager.Instance().GetUserAgent();
            CookieContainer cookies = BiliManager.Instance().GetCookies();

            string response = WebClient.RequestWeb(url, referer, userAgent, cookies);

            try
            {
                var obj = JsonSerializer.Deserialize<T>(response);
                return obj;
            }
            catch (Exception)
            {
                return default;
            }
        }

    }
}