using Core.entity2.users;
using Newtonsoft.Json;
using System;

namespace Core.api.users
{
    /// <summary>
    /// 用户状态数
    /// </summary>
    public class UserStatus
    {
        private static UserStatus instance;

        /// <summary>
        /// 获取UserStatus实例
        /// </summary>
        /// <returns></returns>
        public static UserStatus GetInstance()
        {
            if (instance == null)
            {
                instance = new UserStatus();
            }
            return instance;
        }

        /// <summary>
        /// 隐藏UserStatus()方法，必须使用单例模式
        /// </summary>
        private UserStatus() { }

        /// <summary>
        /// 关系状态数
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public UserRelationStat GetUserRelationStat(long mid)
        {
            string url = $"https://api.bilibili.com/x/relation/stat?vmid={mid}";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var userRelationStat = JsonConvert.DeserializeObject<UserRelationStatOrigin>(response);
                if (userRelationStat == null || userRelationStat.Data == null) { return null; }
                return userRelationStat.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetUserRelationStat()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// UP主状态数
        /// 
        /// 注：该接口需要任意用户登录，否则不会返回任何数据
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public UpStat GetUpStat(long mid)
        {
            string url = $"https://api.bilibili.com/x/space/upstat?mid={mid}";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var upStat = JsonConvert.DeserializeObject<UpStatOrigin>(response);
                if (upStat == null || upStat.Data == null) { return null; }
                return upStat.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetUpStat()发生异常: {0}", e);
                return null;
            }
        }

    }
}
