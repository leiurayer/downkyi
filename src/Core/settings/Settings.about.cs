namespace Core.settings
{
    public partial class Settings
    {
        // 是否接收测试版更新
        private readonly ALLOW_STATUS isReceiveBetaVersion = ALLOW_STATUS.NO;

        // 是否在启动时自动检查更新
        private readonly ALLOW_STATUS autoUpdateWhenLaunch = ALLOW_STATUS.YES;


        /// <summary>
        /// 获取是否接收测试版更新
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS IsReceiveBetaVersion()
        {
            if (settingsEntity.IsReceiveBetaVersion == 0)
            {
                // 第一次获取，先设置默认值
                IsReceiveBetaVersion(isReceiveBetaVersion);
                return isReceiveBetaVersion;
            }
            return settingsEntity.IsReceiveBetaVersion;
        }

        /// <summary>
        /// 设置是否接收测试版更新
        /// </summary>
        /// <param name="isReceiveBetaVersion"></param>
        /// <returns></returns>
        public bool IsReceiveBetaVersion(ALLOW_STATUS isReceiveBetaVersion)
        {
            settingsEntity.IsReceiveBetaVersion = isReceiveBetaVersion;
            return SetEntity();
        }

        /// <summary>
        /// 获取是否允许启动时检查更新
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS GetAutoUpdateWhenLaunch()
        {
            if (settingsEntity.AutoUpdateWhenLaunch == 0)
            {
                // 第一次获取，先设置默认值
                SetAutoUpdateWhenLaunch(autoUpdateWhenLaunch);
                return autoUpdateWhenLaunch;
            }
            return settingsEntity.AutoUpdateWhenLaunch;
        }

        /// <summary>
        /// 设置是否允许启动时检查更新
        /// </summary>
        /// <param name="autoUpdateWhenLaunch"></param>
        /// <returns></returns>
        public bool SetAutoUpdateWhenLaunch(ALLOW_STATUS autoUpdateWhenLaunch)
        {
            settingsEntity.AutoUpdateWhenLaunch = autoUpdateWhenLaunch;
            return SetEntity();
        }

    }
}
