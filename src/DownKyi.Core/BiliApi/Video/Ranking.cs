using DownKyi.Core.BiliApi.Video.Models;
using DownKyi.Core.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Video
{
    public static class Ranking
    {

        /// <summary>
        /// 获取分区视频排行榜列表
        /// </summary>
        /// <param name="rid">目标分区tid</param>
        /// <param name="day">3日榜或周榜（3/7）</param>
        /// <param name="original"></param>
        /// <returns></returns>
        public static List<RankingVideoView> RegionRankingList(int rid, int day = 3, int original = 0)
        {
            string url = $"https://api.bilibili.com/x/web-interface/ranking/region?rid={rid}&day={day}&ps={original}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                var ranking = JsonConvert.DeserializeObject<RegionRanking>(response);
                if (ranking != null) { return ranking.Data; }
                else { return null; }
            }
            catch (Exception e)
            {
                Utils.Debug.Console.PrintLine("RegionRankingList()发生异常: {0}", e);
                LogManager.Error("Ranking", e);
                return null;
            }
        }

    }
}
