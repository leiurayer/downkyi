using Downkyi.Core.Settings;
using Downkyi.Core.Settings.Models;
using Downkyi.Core.Utils.Encryptor;
using System.Net;

namespace Downkyi.Core.Bili.Web;

public static class LoginHelper
{
    // 本地位置
    private static readonly string LOCAL_LOGIN_INFO = Storage.StorageManager.GetLogin();

    private static readonly EncryptorFile encryptor = new();

    /// <summary>
    /// 保存登录的cookies到文件
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static bool SaveLoginInfoCookies(string url)
    {
        CookieContainer cookieContainer = Cookies.ParseCookieByUrl(url);

        return SaveLoginInfoCookies(cookieContainer);
    }

    /// <summary>
    /// 保存cookies到文件
    /// </summary>
    /// <param name="cookieContainer"></param>
    /// <returns></returns>
    public static bool SaveLoginInfoCookies(CookieContainer cookieContainer)
    {
        string tempFile = LOCAL_LOGIN_INFO + "-" + Guid.NewGuid().ToString("N");

        bool isSucceed = Cookies.WriteCookiesToDisk(tempFile, cookieContainer);
        if (isSucceed)
        {
            try
            {
                encryptor.EncryptFile(tempFile, LOCAL_LOGIN_INFO);
            }
            catch (Exception e)
            {
                Log.Log.Logger.Error(e);
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
    public static CookieContainer? GetLoginInfoCookies()
    {
        string tempFile = LOCAL_LOGIN_INFO + "-" + Guid.NewGuid().ToString("N");

        if (File.Exists(LOCAL_LOGIN_INFO))
        {
            try
            {
                encryptor.DecryptFile(LOCAL_LOGIN_INFO, tempFile);
            }
            catch (Exception e)
            {
                Log.Log.Logger.Error(e);
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);
                }
                return null;
            }
        }
        else { return null; }

        var cookies = Cookies.ReadCookiesFromDisk(tempFile);

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
    public static string GetLoginInfoCookiesString()
    {
        var cookieContainer = GetLoginInfoCookies();
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
    public static bool Logout()
    {
        if (File.Exists(LOCAL_LOGIN_INFO))
        {
            try
            {
                File.Delete(LOCAL_LOGIN_INFO);

                SettingsManager.GetInstance().SetUserInfo(new UserInfoSettings
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
                Log.Log.Logger.Error(e);
                return false;
            }
        }
        return false;
    }

}