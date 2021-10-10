using DownKyi.Core.Settings.Models;

namespace DownKyi.Core.Settings
{
    public partial class SettingsManager
    {
        // 登录用户的mid
        private readonly UserInfoSettings userInfo = new UserInfoSettings
        {
            Mid = -1,
            Name = "",
            IsLogin = false,
            IsVip = false
        };

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        public UserInfoSettings GetUserInfo()
        {
            appSettings = GetSettings();
            if (appSettings.UserInfo == null)
            {
                // 第一次获取，先设置默认值
                SetUserInfo(userInfo);
                return userInfo;
            }
            return appSettings.UserInfo;
        }

        /// <summary>
        /// 设置中保存登录用户的信息，在index刷新用户状态时使用
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public bool SetUserInfo(UserInfoSettings userInfo)
        {
            appSettings.UserInfo = userInfo;
            return SetSettings();
        }

    }
}
