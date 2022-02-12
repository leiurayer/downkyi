using DownKyi.Core.Settings.Models;
using Newtonsoft.Json;
using System;
using System.IO;

#if DEBUG
#else
using DownKyi.Core.Utils.Encryptor;
#endif

namespace DownKyi.Core.Settings
{
    public partial class SettingsManager
    {
        private static SettingsManager instance;

        // 内存中保存一份配置
        private AppSettings appSettings;

#if DEBUG
        // 设置的配置文件
        private readonly string settingsName = Storage.StorageManager.GetSettings() + "_debug.json";
#else
        // 设置的配置文件
        private readonly string settingsName = Storage.StorageManager.GetSettings();

        // 密钥
        private readonly string password = "YO1J$4#p";
#endif

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

#if DEBUG
#else
                // 解密字符串
                jsonWordTemplate = Encryptor.DecryptString(jsonWordTemplate, password);
#endif

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

#if DEBUG
#else
            // 加密字符串
            json = Encryptor.EncryptString(json, password);
#endif

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
