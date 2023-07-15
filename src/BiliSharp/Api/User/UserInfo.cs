using BiliSharp.Api.Models.User;
using BiliSharp.Api.Sign;
using System.Collections.Generic;

namespace BiliSharp.Api.User
{
    /// <summary>
    /// 用户基本信息
    /// </summary>
    public static class UserInfo
    {
        /// <summary>
        /// 用户空间详细信息
        /// </summary>
        /// <returns></returns>
        public static UserSpaceInfo GetUserInfo(long mid)
        {
            var parameters = new Dictionary<string, object>
            {
                { "mid", mid }
            };
            string query = WbiSign.ParametersToQuery(WbiSign.EncodeWbi(parameters));
            string url = $"https://api.bilibili.com/x/space/wbi/acc/info?{query}";
            return Utils.GetData<UserSpaceInfo>(url);
        }

        /// <summary>
        /// 用户名片信息
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="isPhoto"></param>
        /// <returns></returns>
        public static UserCard GetUserCard(long mid, bool isPhoto = false)
        {
            string url = $"https://api.bilibili.com/x/web-interface/card?mid={mid}&photo={isPhoto}";
            return Utils.GetData<UserCard>(url);
        }

        /// <summary>
        /// 登录用户空间详细信息
        /// </summary>
        /// <returns></returns>
        public static MyInfo GetMyInfo()
        {
            string url = "https://api.bilibili.com/x/space/myinfo";
            return Utils.GetData<MyInfo>(url);
        }

        /// <summary>
        /// 多用户详细信息
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static UserCards GetUserCards(List<long> ids)
        {
            string url = "https://api.vc.bilibili.com/account/v1/user/cards?uids=";
            foreach (long id in ids)
            {
                url += $"{id},";
            }
            url = url.TrimEnd(',');

            return Utils.GetData<UserCards>(url);
        }
    }
}