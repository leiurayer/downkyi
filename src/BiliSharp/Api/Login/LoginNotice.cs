namespace BiliSharp.Api.Login
{
    /// <summary>
    /// 登录记录
    /// </summary>
    public static class LoginNotice
    {
        /// <summary>
        /// 查询登录记录
        /// </summary>
        /// <returns></returns>
        public static Models.Login.LoginNotice GetLoginNotice(long mid)
        {
            string url = $"https://api.bilibili.com/x/safecenter/login_notice?mid={mid}";
            return Utils.GetData<Models.Login.LoginNotice>(url);
        }
    }
}