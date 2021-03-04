using Core.entity2.users;
using Newtonsoft.Json;
using System;

namespace Core.api.users
{
    /// <summary>
    /// 用户基本信息
    /// </summary>
    public class UserInfo
    {
        private static UserInfo instance;

        /// <summary>
        /// 获取UserInfo实例
        /// </summary>
        /// <returns></returns>
        public static UserInfo GetInstance()
        {
            if (instance == null)
            {
                instance = new UserInfo();
            }
            return instance;
        }

        /// <summary>
        /// 隐藏UserInfo()方法，必须使用单例模式
        /// </summary>
        private UserInfo() { }

        /// <summary>
        /// 用户详细信息1 (用于空间)
        /// </summary>
        /// <param name="mid">目标用户UID</param>
        /// <returns></returns>
        public SpaceInfo GetInfoForSpace(long mid)
        {
            string url = $"https://api.bilibili.com/x/space/acc/info?mid={mid}";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var spaceInfo = JsonConvert.DeserializeObject<SpaceInfoOrigin>(response);
                if (spaceInfo == null || spaceInfo.Data == null) { return null; }
                return spaceInfo.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetInfoForSpace()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 本用户详细信息
        /// </summary>
        /// <returns></returns>
        public MyInfo GetMyInfo()
        {
            string url = "https://api.bilibili.com/x/space/myinfo";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var myInfo = JsonConvert.DeserializeObject<MyInfoOrigin>(response);
                if (myInfo == null || myInfo.Data == null) { return null; }
                return myInfo.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetMyInfo()发生异常: {0}", e);
                return null;
            }
        }

    }

}
