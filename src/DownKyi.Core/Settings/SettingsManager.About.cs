namespace DownKyi.Core.Settings
{
    public partial class SettingsManager
    {
        // 是否接收测试版更新
        private readonly AllowStatus isReceiveBetaVersion = AllowStatus.NO;

        // 是否在启动时自动检查更新
        private readonly AllowStatus autoUpdateWhenLaunch = AllowStatus.YES;

        /// <summary>
        /// 获取是否接收测试版更新
        /// </summary>
        /// <returns></returns>
        public AllowStatus IsReceiveBetaVersion()
        {
            appSettings = GetSettings();
            if (appSettings.About.IsReceiveBetaVersion == 0)
            {
                // 第一次获取，先设置默认值
                IsReceiveBetaVersion(isReceiveBetaVersion);
                return isReceiveBetaVersion;
            }
            return appSettings.About.IsReceiveBetaVersion;
        }

        /// <summary>
        /// 设置是否接收测试版更新
        /// </summary>
        /// <param name="isReceiveBetaVersion"></param>
        /// <returns></returns>
        public bool IsReceiveBetaVersion(AllowStatus isReceiveBetaVersion)
        {
            appSettings.About.IsReceiveBetaVersion = isReceiveBetaVersion;
            return SetSettings();
        }

        /// <summary>
        /// 获取是否允许启动时检查更新
        /// </summary>
        /// <returns></returns>
        public AllowStatus GetAutoUpdateWhenLaunch()
        {
            appSettings = GetSettings();
            if (appSettings.About.AutoUpdateWhenLaunch == 0)
            {
                // 第一次获取，先设置默认值
                SetAutoUpdateWhenLaunch(autoUpdateWhenLaunch);
                return autoUpdateWhenLaunch;
            }
            return appSettings.About.AutoUpdateWhenLaunch;
        }

        /// <summary>
        /// 设置是否允许启动时检查更新
        /// </summary>
        /// <param name="autoUpdateWhenLaunch"></param>
        /// <returns></returns>
        public bool SetAutoUpdateWhenLaunch(AllowStatus autoUpdateWhenLaunch)
        {
            appSettings.About.AutoUpdateWhenLaunch = autoUpdateWhenLaunch;
            return SetSettings();
        }

    }
}
