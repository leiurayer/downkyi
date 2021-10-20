using DownKyi.Core.BiliApi.Bangumi.Models;
using DownKyi.Core.Logging;
using Newtonsoft.Json;
using System;

namespace DownKyi.Core.BiliApi.Bangumi
{
    public static class BangumiInfo
    {

        /// <summary>
        /// 剧集基本信息（mediaId方式）
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static BangumiMedia BangumiMediaInfo(long mediaId)
        {
            string url = $"https://api.bilibili.com/pgc/review/user?media_id={mediaId}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                var media = JsonConvert.DeserializeObject<BangumiMediaOrigin>(response);
                if (media != null && media.Result != null) { return media.Result.Media; }
                else { return null; }
            }
            catch (Exception e)
            {
                Utils.Debug.Console.PrintLine("BangumiMediaInfo()发生异常: {0}", e);
                LogManager.Error("BangumiInfo", e);
                return null;
            }
        }

        /// <summary>
        /// 获取剧集明细（web端）（seasonId/episodeId方式）
        /// </summary>
        /// <param name="seasonId"></param>
        /// <param name="episodeId"></param>
        /// <returns></returns>
        public static BangumiSeason BangumiSeasonInfo(long seasonId = -1, long episodeId = -1)
        {
            string baseUrl = "https://api.bilibili.com/pgc/view/web/season";
            string referer = "https://www.bilibili.com";
            string url;
            if (seasonId > -1) { url = $"{baseUrl}?season_id={seasonId}"; }
            else if (episodeId > -1) { url = $"{baseUrl}?ep_id={episodeId}"; }
            else { return null; }

            string response = WebClient.RequestWeb(url, referer);

            try
            {
                var bangumiSeason = JsonConvert.DeserializeObject<BangumiSeasonOrigin>(response);
                if (bangumiSeason != null) { return bangumiSeason.Result; }
                else { return null; }
            }
            catch (Exception e)
            {
                Utils.Debug.Console.PrintLine("BangumiSeasonInfo()发生异常: {0}", e);
                LogManager.Error("BangumiInfo", e);
                return null;
            }
        }

    }
}
