using DownKyi.Core.Settings.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace DownKyi.Core.Settings
{
    public partial class SettingsManager
    {
        private static SettingsManager instance;

        /// <summary>
        /// 获取SettingsManager实例
        /// </summary>
        /// <returns></returns>
        public static SettingsManager GetInstance()
        {
            if (instance == null)
            {
                instance = new SettingsManager();
            }
            return instance;
        }

        /// <summary>
        /// 隐藏Settings()方法，必须使用单例模式
        /// </summary>
        private SettingsManager()
        {
            appSettings = GetSettings();
        }

        // 内存中保存一份配置
        private AppSettings appSettings;

        // 设置的配置文件
        private readonly string settingsName = Storage.StorageManager.GetSettings();

        /// <summary>
        /// 获取AppSettingsModel
        /// </summary>
        /// <returns></returns>
        private AppSettings GetSettings()
        {
            try
            {
                StreamReader streamReader = File.OpenText(settingsName);
                string jsonWordTemplate = streamReader.ReadToEnd();
                streamReader.Close();

                return JsonConvert.DeserializeObject<AppSettings>(jsonWordTemplate);
            }
            catch (Exception e)
            {
                Logging.LogManager.Error(e);
                return new AppSettings();
            }
        }

        /// <summary>
        /// 设置AppSettingsModel
        /// </summary>
        /// <returns></returns>
        private bool SetSettings()
        {
            string json = JsonConvert.SerializeObject(appSettings);

            try
            {
                File.WriteAllText(settingsName, json);
                return true;
            }
            catch (Exception e)
            {
                Logging.LogManager.Error(e);
                return false;
            }
        }

    }
}
