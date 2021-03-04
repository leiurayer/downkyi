using Core.entity2.history;
using Newtonsoft.Json;
using System;

namespace Core.api.history
{
    /// <summary>
    /// 历史记录
    /// </summary>
    public class History
    {
        private static History instance;

        /// <summary>
        /// 获取UserInfo实例
        /// </summary>
        /// <returns></returns>
        public static History GetInstance()
        {
            if (instance == null)
            {
                instance = new History();
            }
            return instance;
        }

        /// <summary>
        /// History()方法，必须使用单例模式
        /// </summary>
        private History() { }

        /// <summary>
        /// 获取历史记录列表（视频、直播、专栏）
        /// startId和startTime必须同时使用才有效，分别对应结果中的max和view_at，默认为0
        /// </summary>
        /// <param name="startId">历史记录开始目标ID</param>
        /// <param name="startTime">历史记录开始时间</param>
        /// <param name="ps">每页项数</param>
        /// <param name="business">历史记录ID类型</param>
        /// <returns></returns>
        public HistoryData GetHistory(long startId, long startTime, int ps = 30, HistoryBusiness business = HistoryBusiness.ARCHIVE)
        {
            string businessStr = string.Empty;
            switch (business)
            {
                case HistoryBusiness.ARCHIVE:
                    businessStr = "archive";
                    break;
                case HistoryBusiness.PGC:
                    businessStr = "pgc";
                    break;
                case HistoryBusiness.LIVE:
                    businessStr = "live";
                    break;
                case HistoryBusiness.ARTICLE_LIST:
                    businessStr = "article-list";
                    break;
                case HistoryBusiness.ARTICLE:
                    businessStr = "article";
                    break;
            }
            string url = $"https://api.bilibili.com/x/web-interface/history/cursor?max={startId}&view_at={startTime}&ps={ps}&business={businessStr}";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var history = JsonConvert.DeserializeObject<HistoryOrigin>(response);
                if (history == null || history.Data == null) { return null; }
                return history.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetHistory()发生异常: {0}", e);
                return null;
            }
        }

    }

    public enum HistoryBusiness
    {
        ARCHIVE = 1, // 稿件
        PGC, // 番剧（影视）
        LIVE, // 直播
        ARTICLE_LIST, // 文集
        ARTICLE, // 文章
    }

}
