using DownKyi.Core.BiliApi.History.Models;
using DownKyi.Core.Logging;
using Newtonsoft.Json;
using System;

namespace DownKyi.Core.BiliApi.History
{
    /// <summary>
    /// 历史记录
    /// </summary>
    public static class History
    {
        /// <summary>
        /// 获取历史记录列表（视频、直播、专栏）
        /// startId和startTime必须同时使用才有效，分别对应结果中的max和view_at，默认为0
        /// </summary>
        /// <param name="startId">历史记录开始目标ID</param>
        /// <param name="startTime">历史记录开始时间</param>
        /// <param name="ps">每页项数</param>
        /// <param name="business">历史记录ID类型</param>
        /// <returns></returns>
        public static HistoryData GetHistory(long startId, long startTime, int ps = 30, HistoryBusiness business = HistoryBusiness.ARCHIVE)
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
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                var history = JsonConvert.DeserializeObject<HistoryOrigin>(response);
                if (history == null || history.Data == null) { return null; }
                return history.Data;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetHistory()发生异常: {0}", e);
                LogManager.Error("History", e);
                return null;
            }
        }

    }
}
