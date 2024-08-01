using Downkyi.Core.Utils;
using System.Collections;
using System.Net;
using System.Web;

namespace Downkyi.Core.Bili;

public static class Cookies
{
    /// <summary>
    /// 写入cookies到磁盘
    /// </summary>
    /// <param name="file"></param>
    /// <param name="cookieJar"></param>
    /// <returns></returns>
    public static bool WriteCookiesToDisk(string file, CookieContainer cookieJar)
    {
        return ObjectHelper.WriteObjectToDisk(file, cookieJar);
    }

    /// <summary>
    /// 从磁盘读取cookie
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static CookieContainer? ReadCookiesFromDisk(string file)
    {
        return (CookieContainer?)ObjectHelper.ReadObjectFromDisk(file);
    }

    /// <summary>
    /// 将CookieContainer中的所有的Cookie读出来
    /// </summary>
    /// <param name="cc"></param>
    /// <returns></returns>
    public static List<Cookie> GetAllCookies(CookieContainer cc)
    {
        var lstCookies = new List<Cookie>();

        var table = (Hashtable?)cc.GetType().InvokeMember("m_domainTable",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
            System.Reflection.BindingFlags.Instance, null, cc, Array.Empty<object>());

        foreach (object pathList in table!.Values)
        {
            var lstCookieCol = (SortedList?)pathList.GetType().InvokeMember("m_list",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                | System.Reflection.BindingFlags.Instance, null, pathList, Array.Empty<object>());
            foreach (CookieCollection colCookies in lstCookieCol!.Values)
            {
                foreach (Cookie c in colCookies.Cast<Cookie>())
                {
                    lstCookies.Add(c);
                }
            }
        }

        return lstCookies;
    }

    /// <summary>
    /// 返回cookies的字符串
    /// </summary>
    /// <returns></returns>
    public static string GetCookiesString(CookieContainer cc)
    {
        var cookies = GetAllCookies(cc);

        string cookie = string.Empty;
        foreach (var item in cookies)
        {
            cookie += item.ToString() + ";";
        }
        return cookie.TrimEnd(';');
    }

    /// <summary>
    /// 解析二维码登录返回的url，用于设置cookie
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static CookieContainer ParseCookieByUrl(string url)
    {
        var cookieContainer = new CookieContainer();

        if (url == null || url == "") { return cookieContainer; }

        string[] strList = url.Split('?');
        if (strList.Length < 2) { return cookieContainer; }

        string[] strList2 = strList[1].Split('&');
        if (strList2.Length == 0) { return cookieContainer; }

        // 获取expires
        string expires = strList2.FirstOrDefault(it => it.Contains("Expires"))!.Split('=')[1];
        DateTime dateTime = DateTime.Now;
        dateTime = dateTime.AddSeconds(int.Parse(expires));

        foreach (var item in strList2)
        {
            string[] strList3 = item.Split('=');
            if (strList3.Length < 2) { continue; }

            string name = strList3[0];
            string value = strList3[1];
            value = HttpUtility.UrlEncode(value);

            // 不需要
            if (name == "Expires" || name == "gourl") { continue; }

            // 添加cookie
            cookieContainer.Add(new Cookie(name, value, "/", ".bilibili.com") { Expires = dateTime });

        }

        return cookieContainer;
    }

    /// <summary>
    /// 解析从浏览器获取的cookies，用于设置cookie
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static CookieContainer ParseCookieByString(string str)
    {
        var cookieContainer = new CookieContainer();

        var cookies = str.Replace(" ", "").Split(";");
        foreach (var cookie in cookies)
        {
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddMonths(12);

            var temp = cookie.Split("=");
            var name = temp[0];
            var value = temp[1];

            // 添加cookie
            cookieContainer.Add(new Cookie(name, value, "/", ".bilibili.com") { Expires = dateTime });
        }
        return cookieContainer;
    }

}