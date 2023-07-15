namespace BiliSharp.Api.User
{
    /// <summary>
    /// 检查昵称是否可注册
    /// </summary>
    public static class Nickname
    {
        /// <summary>
        /// 检查昵称
        /// </summary>
        /// <param name="nickname"></param>
        /// <returns></returns>
        public static Models.User.Nickname CheckNickname(string nickname)
        {
            string url = $"https://passport.bilibili.com/web/generic/check/nickname?nickName={nickname}";
            return Utils.GetData<Models.User.Nickname>(url);
        }
    }
}