using BiliSharp.Api.Models.Login;

namespace BiliSharp.Api.Login
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
        public static NavigationLoginInfo GetNavigationInfo()
        {
            string url = "https://api.bilibili.com/x/web-interface/nav";
            return Utils.GetData<NavigationLoginInfo>(url);
        }

        /// <summary>
        /// 登录用户状态数（双端）
        /// </summary>
        /// <returns></returns>
        public static LoginInfoStat GetLoginInfoStat()
        {
            string url = "https://api.bilibili.com/x/web-interface/nav/stat";
            return Utils.GetData<LoginInfoStat>(url);
        }

        /// <summary>
        /// 获取硬币数
        /// </summary>
        /// <returns></returns>
        public static MyCoin GetMyCoin()
        {
            string url = "https://account.bilibili.com/site/getCoin";
            return Utils.GetData<MyCoin>(url);
        }
    }
}