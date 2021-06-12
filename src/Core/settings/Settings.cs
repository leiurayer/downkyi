using Newtonsoft.Json;
using System;
using System.IO;

namespace Core.settings
{
    public partial class Settings
    {
        private static Settings instance;

        /// <summary>
        /// 获取Settings实例
        /// </summary>
        /// <returns></returns>
        public static Settings GetInstance()
        {
            if (instance == null)
            {
                instance = new Settings();
            }
            return instance;
        }

        /// <summary>
        /// 隐藏Settings()方法，必须使用单例模式
        /// </summary>
        private Settings()
        {
            settingsEntity = GetEntity();
        }


        // 内存中保存一份配置
        private readonly SettingsEntity settingsEntity;

        // 设置的配置文件
        private readonly string settingsName = Common.ConfigPath + "Settings";

        // 密钥
        private readonly string password = "QA!M^gE@";

        // 登录用户的mid
        private readonly UserInfoForSetting userInfo = new UserInfoForSetting
        {
            Mid = -1,
            Name = "",
            IsLogin = false,
            IsVip = false
        };

        private SettingsEntity GetEntity()
        {
            try
            {
                StreamReader streamReader = File.OpenText(settingsName);
                string jsonWordTemplate = streamReader.ReadToEnd();
                streamReader.Close();

                // 解密字符串
                jsonWordTemplate = Encryptor.DecryptString(jsonWordTemplate, password);

                return JsonConvert.DeserializeObject<SettingsEntity>(jsonWordTemplate);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("GetEntity()发生异常: {0}", e);
                return new SettingsEntity();
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("GetEntity()发生异常: {0}", e);
                return new SettingsEntity();
            }
            catch (Exception e)
            {
                Console.WriteLine("GetEntity()发生异常: {0}", e);
                return new SettingsEntity();
            }
        }

        private bool SetEntity()
        {
            if (!Directory.Exists(Common.ConfigPath))
            {
                Directory.CreateDirectory(Common.ConfigPath);
            }

            string json = JsonConvert.SerializeObject(settingsEntity);

            // 加密字符串
            json = Encryptor.EncryptString(json, password);
            try
            {
                File.WriteAllText(settingsName, json);
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine("SetEntity()发生异常: {0}", e);
                return false;
            }
        }

        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        public UserInfoForSetting GetUserInfo()
        {
            if (settingsEntity.UserInfo == null)
            {
                // 第一次获取，先设置默认值
                SetUserInfo(userInfo);
                return userInfo;
            }
            return settingsEntity.UserInfo;
        }

        /// <summary>
        /// 设置中保存登录用户的信息，在index刷新用户状态时使用
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public bool SetUserInfo(UserInfoForSetting userInfo)
        {
            settingsEntity.UserInfo = userInfo;
            return SetEntity();
        }


    }
}
