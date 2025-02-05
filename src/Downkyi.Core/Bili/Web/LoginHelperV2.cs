using System.Net;
using Downkyi.BiliSharp;
using Downkyi.BiliSharp.Api.Login;
using Downkyi.Core.Database.Login;
using Downkyi.Core.Settings;
using Downkyi.Core.Settings.Models;

namespace Downkyi.Core.Bili.Web;

public static class LoginHelperV2
{
    /// <summary>
    /// 保存登录的cookies到数据库
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static Task<bool> SaveLoginInfoCookies(string url)
    {
        CookieContainer cookieContainer = Cookies.ParseCookieByUrl(url);

        return SaveLoginInfoCookies(cookieContainer);
    }

    /// <summary>
    /// 保存cookies到数据库
    /// </summary>
    /// <param name="cookieContainer"></param>
    /// <returns></returns>
    public static async Task<bool> SaveLoginInfoCookies(CookieContainer cookieContainer)
    {
        string userAgent = SettingsManager.Instance.GetUserAgent();
        BiliManager.Instance().SetUserAgent(userAgent);
        BiliManager.Instance().SetCookies(cookieContainer);
        await Task.Run(() =>
        {
            var origin = LoginInfo.GetNavigationInfo();
            if (origin == null || origin.Data == null)
            {
                return;
            }

            SettingsManager.Instance.SetUserInfo(new UserInfoSettings
            {
                Mid = origin.Data.Mid,
                Name = origin.Data.Uname,
                IsLogin = origin.Data.Islogin,
                IsVip = origin.Data.Vipstatus == 1
            });
        });

        var userInfo = SettingsManager.Instance.GetUserInfo();
        foreach (Cookie cookie in cookieContainer.GetAllCookies())
        {
            await LoginDatabase.Instance.AddCookiesAsync(new Database.Login.Cookies
            {
                Uid = userInfo.Mid,
                Key = cookie.Name,
                Value = cookie.Value
            });
        }

        return true;
    }

    /// <summary>
    /// 从本地获得登录的cookies
    /// </summary>
    /// <returns></returns>
    public static async Task<CookieContainer?> GetLoginInfoCookies()
    {
        var userInfo = SettingsManager.Instance.GetUserInfo();

        var cookies = await LoginDatabase.Instance.GetCookiesAsync(userInfo.Mid);
        if (cookies.Count == 0) { return null; }

        var cookieContainer = new CookieContainer();
        foreach (var cookie in cookies)
        {
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddMonths(12);

            // 添加cookie
            cookieContainer.Add(new Cookie(cookie.Key, cookie.Value, "/", ".bilibili.com") { Expires = dateTime });
        }
        return cookieContainer;
    }

    /// <summary>
    /// 返回登录信息的cookies的字符串
    /// </summary>
    /// <returns></returns>
    public static async Task<string> GetLoginInfoCookiesString()
    {
        var cookieContainer = await GetLoginInfoCookies();
        if (cookieContainer == null)
        {
            return "";
        }

        return Cookies.GetCookiesString(cookieContainer);
    }

    /// <summary>
    /// 注销登录
    /// </summary>
    /// <returns></returns>
    public static async Task<bool> Logout()
    {
        var userInfo = SettingsManager.Instance.GetUserInfo();
        var result = await LoginDatabase.Instance.DeleteCookiesByUidAsync(userInfo.Mid);
        if (result == 0)
        {
            Log.Log.Logger.Error("LoginDatabase删除失败！（DeleteCookiesByUidAsync）");
            return false;
        }

        SettingsManager.Instance.SetUserInfo(new UserInfoSettings
        {
            Mid = -1,
            Name = "",
            IsLogin = false,
            IsVip = false
        });

        return true;
    }

}