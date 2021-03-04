using Core.entity2.history;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Core.api.history
{
    /// <summary>
    /// 获取稍后再看列表
    /// </summary>
    public class ToView
    {
        private static ToView instance;

        /// <summary>
        /// 获取ToView实例
        /// </summary>
        /// <returns></returns>
        public static ToView GetInstance()
        {
            if (instance == null)
            {
                instance = new ToView();
            }
            return instance;
        }

        /// <summary>
        /// 隐藏ToView()方法，必须使用单例模式
        /// </summary>
        private ToView() { }

        /// <summary>
        /// 获取稍后再看视频列表
        /// </summary>
        /// <returns></returns>
        public List<ToViewDataList> GetHistoryToView()
        {
            string url = "https://api.bilibili.com/x/v2/history/toview/web";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var toView = JsonConvert.DeserializeObject<ToViewOrigin>(response);
                if (toView == null || toView.Data == null) { return null; }
                return toView.Data.List;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetHistoryToView()发生异常: {0}", e);
                return null;
            }
        }

    }

}
