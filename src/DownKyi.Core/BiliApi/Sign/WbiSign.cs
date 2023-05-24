using DownKyi.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DownKyi.Core.BiliApi.Sign
{
    public static class WbiSign
    {

        /// <summary>
        /// 打乱重排实时口令
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        private static string GetMixinKey(string origin)
        {
            int[] mixinKeyEncTab = new int[]
            {
                46, 47, 18, 2, 53, 8, 23, 32, 15, 50, 10, 31, 58, 3, 45, 35, 27, 43, 5, 49,
                33, 9, 42, 19, 29, 28, 14, 39,12, 38, 41, 13, 37, 48, 7, 16, 24, 55, 40,
                61, 26, 17, 0, 1, 60, 51, 30, 4, 22, 25, 54, 21, 56, 59, 6, 63, 57, 62, 11,
                36, 20, 34, 44, 52
            };

            var temp = new StringBuilder();
            foreach (var i in mixinKeyEncTab)
            {
                temp.Append(origin[i]);
            }
            return temp.ToString().Substring(0, 32);
        }

        /// <summary>
        /// 将字典参数转为字符串
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string ParametersToQuery(Dictionary<string, object> parameters)
        {
            var keys = parameters.Keys.ToList();
            var queryList = new List<string>();
            foreach (var item in keys)
            {
                var value = parameters[item];
                queryList.Add($"{item}={value}");
            }
            return string.Join("&", queryList);
        }

        /// <summary>
        /// Wbi签名，返回所有参数字典
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Dictionary<string, object> EncodeWbi(Dictionary<string, object> parameters)
        {
            return EncodeWbi(parameters, GetKey().Item1, GetKey().Item2);
        }

        /// <summary>
        /// Wbi签名，返回所有参数字典
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="imgKey"></param>
        /// <param name="subKey"></param>
        /// <returns></returns>
        public static Dictionary<string, object> EncodeWbi(Dictionary<string, object> parameters, string imgKey, string subKey)
        {
            var mixinKey = GetMixinKey(imgKey + subKey);

            var chrFilter = new Regex("[!'()*]");

            var newParameters = new Dictionary<string, object>
            {
                { "wts", (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds }
            };

            foreach (var para in parameters)
            {
                var key = para.Key;
                var value = para.Value.ToString();

                var encodedValue = chrFilter.Replace(value, "");

                newParameters.Add(Uri.EscapeDataString(key), Uri.EscapeDataString(encodedValue));
            }

            var keys = newParameters.Keys.ToList();
            keys.Sort();

            var queryList = new List<string>();
            foreach (var item in keys)
            {
                var value = newParameters[item];
                queryList.Add($"{item}={value}");
            }

            var queryString = string.Join("&", queryList);
            var md5Hasher = MD5.Create();
            var hashStr = queryString + mixinKey;
            var hashedQueryString = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(hashStr));
            var wbiSign = BitConverter.ToString(hashedQueryString).Replace("-", "").ToLower();

            newParameters.Add("w_rid", wbiSign);
            return newParameters;
        }

        public static Tuple<string, string> GetKey()
        {
            var user = SettingsManager.GetInstance().GetUserInfo();

            return new Tuple<string, string>(user.ImgKey, user.SubKey);
        }

    }
}