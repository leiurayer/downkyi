using System.Net;

namespace BiliSharp
{
    public class BiliManager
    {
        private static readonly BiliManager _instance = new BiliManager();
        private string _userAgent;
        private CookieContainer _cookies;

        private BiliManager() { }

        public static BiliManager Instance()
        {
            return _instance;
        }

        /// <summary>
        /// 设置cookies
        /// </summary>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public BiliManager SetCookies(CookieContainer cookies)
        {
            _cookies = cookies;
            return this;
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <returns></returns>
        public CookieContainer GetCookies() { return _cookies; }

        /// <summary>
        /// 设置userAgent
        /// </summary>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public BiliManager SetUserAgent(string userAgent)
        {
            _userAgent = userAgent;
            return this;
        }

        /// <summary>
        /// 获取userAgent
        /// </summary>
        /// <returns></returns>
        public string GetUserAgent() { return _userAgent; }

    }
}