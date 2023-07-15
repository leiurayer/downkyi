using System.Net;

namespace BiliSharp.UnitTest;

public static class Cookies
{
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

    public static void GetMyCookies()
    {
        string cookiesStr = "DedeUserID=42018135; " +
            "DedeUserID__ckMd5=44e22fa30fe34ac4; " +
            "SESSDATA=32c16297%2C1700815953%2Cb11cd%2A51; " +
            "bili_jct=98dbd091dc07d8f9b69ba3845974e7c8; " +
            "sid=6vomjg3u";
        var cookies = ParseCookieByString(cookiesStr);
        BiliManager.Instance().SetCookies(cookies);
    }

}
