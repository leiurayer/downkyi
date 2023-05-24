using DownKyi.Core.BiliApi.Sign;
using DownKyi.Core.BiliApi.Users.Models;
using DownKyi.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.Users
{
    /// <summary>
    /// 用户空间信息
    /// </summary>
    public static class UserSpace
    {
        /// <summary>
        /// 查询空间设置
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public static SpaceSettings GetSpaceSettings(long mid)
        {
            string url = $"https://space.bilibili.com/ajax/settings/getSettings?mid={mid}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                SpaceSettingsOrigin settings = JsonConvert.DeserializeObject<SpaceSettingsOrigin>(response);
                if (settings == null || settings.Data == null || !settings.Status) { return null; }
                return settings.Data;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetSpaceSettings()发生异常: {0}", e);
                LogManager.Error("UserSpace", e);
                return null;
            }
        }

        #region 投稿

        /// <summary>
        /// 获取用户投稿视频的所有分区
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <returns></returns>
        public static List<SpacePublicationListTypeVideoZone> GetPublicationType(long mid)
        {
            int pn = 1;
            int ps = 1;
            SpacePublicationList publication = GetPublication(mid, pn, ps);
            return GetPublicationType(publication);
        }

        /// <summary>
        /// 获取用户投稿视频的所有分区
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <returns></returns>
        public static List<SpacePublicationListTypeVideoZone> GetPublicationType(SpacePublicationList publication)
        {
            if (publication == null || publication.Tlist == null)
            {
                return null;
            }

            List<SpacePublicationListTypeVideoZone> result = new List<SpacePublicationListTypeVideoZone>();
            JObject typeList = JObject.Parse(publication.Tlist.ToString("N"));
            foreach (KeyValuePair<string, JToken> item in typeList)
            {
                SpacePublicationListTypeVideoZone value = JsonConvert.DeserializeObject<SpacePublicationListTypeVideoZone>(item.Value.ToString());
                result.Add(value);
            }
            return result;
        }

        /// <summary>
        /// 查询用户所有的投稿视频明细
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <param name="order">排序</param>
        /// <param name="tid">视频分区</param>
        /// <param name="keyword">搜索关键词</param>
        /// <returns></returns>
        public static List<SpacePublicationListVideo> GetAllPublication(long mid, int tid = 0, PublicationOrder order = PublicationOrder.PUBDATE, string keyword = "")
        {
            List<SpacePublicationListVideo> result = new List<SpacePublicationListVideo>();

            int i = 0;
            while (true)
            {
                i++;
                int ps = 100;

                SpacePublicationList data = GetPublication(mid, i, ps, tid, order, keyword);
                if (data == null || data.Vlist == null || data.Vlist.Count == 0)
                { break; }

                result.AddRange(data.Vlist);
            }

            return result;
        }

        /// <summary>
        /// 查询用户投稿视频明细
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <param name="pn">页码</param>
        /// <param name="ps">每页的视频数</param>
        /// <param name="order">排序</param>
        /// <param name="tid">视频分区</param>
        /// <param name="keyword">搜索关键词</param>
        /// <returns></returns>
        public static SpacePublicationList GetPublication(long mid, int pn, int ps, long tid = 0, PublicationOrder order = PublicationOrder.PUBDATE, string keyword = "")
        {
            var parameters = new Dictionary<string, object>
            {
                { "mid", mid },
                { "pn", pn },
                { "ps", ps },
                { "order", order.ToString("G").ToLower() },
                { "tid", tid },
                { "keyword", keyword },
            };
            string query = WbiSign.ParametersToQuery(WbiSign.EncodeWbi(parameters));
            string url = $"https://api.bilibili.com/x/space/wbi/arc/search?{query}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                // 忽略play的值为“--”时的类型错误
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    Error = (sender, args) =>
                    {
                        if (Equals(args.ErrorContext.Member, "play") &&
                            args.ErrorContext.OriginalObject.GetType() == typeof(SpacePublicationListVideo))
                        {
                            args.ErrorContext.Handled = true;
                        }
                    }
                };

                SpacePublicationOrigin spacePublication = JsonConvert.DeserializeObject<SpacePublicationOrigin>(response, settings);
                if (spacePublication == null || spacePublication.Data == null) { return null; }
                return spacePublication.Data.List;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetPublication()发生异常: {0}", e);
                LogManager.Error("UserSpace", e);
                return null;
            }
        }

        #endregion

        #region 频道

        /// <summary>
        /// 查询用户频道列表
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <returns></returns>
        public static List<SpaceChannelList> GetChannelList(long mid)
        {
            string url = $"https://api.bilibili.com/x/space/channel/list?mid={mid}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                SpaceChannelOrigin spaceChannel = JsonConvert.DeserializeObject<SpaceChannelOrigin>(response);
                if (spaceChannel == null || spaceChannel.Data == null) { return null; }
                return spaceChannel.Data.List;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetChannelList()发生异常: {0}", e);
                LogManager.Error("UserSpace", e);
                return null;
            }
        }

        /// <summary>
        /// 查询用户频道中的所有视频
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static List<SpaceChannelArchive> GetAllChannelVideoList(long mid, long cid)
        {
            List<SpaceChannelArchive> result = new List<SpaceChannelArchive>();

            int i = 0;
            while (true)
            {
                i++;
                int ps = 100;

                List<SpaceChannelArchive> data = GetChannelVideoList(mid, cid, i, ps);
                if (data == null || data.Count == 0)
                { break; }

                result.AddRange(data);
            }
            return result;
        }

        /// <summary>
        /// 查询用户频道中的视频
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="cid"></param>
        /// <param name="pn"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static List<SpaceChannelArchive> GetChannelVideoList(long mid, long cid, int pn, int ps)
        {
            string url = $"https://api.bilibili.com/x/space/channel/video?mid={mid}&cid={cid}&pn={pn}&ps={ps}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                SpaceChannelVideoOrigin spaceChannelVideo = JsonConvert.DeserializeObject<SpaceChannelVideoOrigin>(response);
                if (spaceChannelVideo == null || spaceChannelVideo.Data == null || spaceChannelVideo.Data.List == null)
                { return null; }
                return spaceChannelVideo.Data.List.Archives;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetChannelVideoList()发生异常: {0}", e);
                LogManager.Error("UserSpace", e);
                return null;
            }
        }

        #endregion

        #region 合集和列表

        /// <summary>
        /// 查询用户的合集和列表
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="pageNum">第几页</param>
        /// <param name="pageSize">每页的数量；最大值为20</param>
        /// <returns></returns>
        public static SpaceSeasonsSeries GetSeasonsSeries(long mid, int pageNum, int pageSize)
        {
            // https://api.bilibili.com/x/polymer/space/seasons_series_list?mid=49246269&page_num=1&page_size=18
            string url = $"https://api.bilibili.com/x/polymer/space/seasons_series_list?mid={mid}&page_num={pageNum}&page_size={pageSize}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                SpaceSeasonsSeriesOrigin origin = JsonConvert.DeserializeObject<SpaceSeasonsSeriesOrigin>(response);
                if (origin == null || origin.Data == null || origin.Data.ItemsLists == null)
                { return null; }
                return origin.Data.ItemsLists;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetSeasonsSeries()发生异常: {0}", e);
                LogManager.Error("UserSpace", e);
                return null;
            }
        }

        /// <summary>
        /// 查询用户的合集的视频详情
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="seasonId"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        public static SpaceSeasonsDetail GetSeasonsDetail(long mid, long seasonId, int pageNum, int pageSize)
        {
            // https://api.bilibili.com/x/polymer/space/seasons_archives_list?mid=23947287&season_id=665&sort_reverse=false&page_num=1&page_size=30
            string url = $"https://api.bilibili.com/x/polymer/space/seasons_archives_list?mid={mid}&season_id={seasonId}&page_num={pageNum}&page_size={pageSize}&sort_reverse=false";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                SpaceSeasonsDetailOrigin origin = JsonConvert.DeserializeObject<SpaceSeasonsDetailOrigin>(response);
                if (origin == null || origin.Data == null)
                { return null; }
                return origin.Data;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetSeasonsDetail()发生异常: {0}", e);
                LogManager.Error("UserSpace", e);
                return null;
            }
        }

        /// <summary>
        /// 查询用户的列表元数据
        /// </summary>
        /// <param name="seriesId"></param>
        /// <returns></returns>
        public static SpaceSeriesMetaData GetSeriesMeta(long seriesId)
        {
            // https://api.bilibili.com/x/series/series?series_id=1253087
            string url = $"https://api.bilibili.com/x/series/series?series_id={seriesId}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                SpaceSeriesMetaOrigin origin = JsonConvert.DeserializeObject<SpaceSeriesMetaOrigin>(response);
                if (origin == null || origin.Data == null)
                { return null; }
                return origin.Data;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetSeriesMeta()发生异常: {0}", e);
                LogManager.Error("UserSpace", e);
                return null;
            }
        }

        /// <summary>
        /// 查询用户的列表的视频详情
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="seriesId"></param>
        /// <param name="pn"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static SpaceSeriesDetail GetSeriesDetail(long mid, long seriesId, int pn, int ps)
        {
            // https://api.bilibili.com/x/series/archives?mid=27899754&series_id=1253087&only_normal=true&sort=desc&pn=1&ps=30

            string url = $"https://api.bilibili.com/x/series/archives?mid={mid}&series_id={seriesId}&only_normal=true&sort=desc&pn={pn}&ps={ps}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                SpaceSeriesDetailOrigin origin = JsonConvert.DeserializeObject<SpaceSeriesDetailOrigin>(response);
                if (origin == null || origin.Data == null)
                { return null; }
                return origin.Data;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetSeriesDetail()发生异常: {0}", e);
                LogManager.Error("UserSpace", e);
                return null;
            }
        }

        #endregion

        #region 课程
        /// <summary>
        /// 查询用户发布的课程列表
        /// </summary>
        /// <param name="mid">目标用户UID</param>
        /// <param name="pn">页码</param>
        /// <param name="ps">每页项数</param>
        /// <returns></returns>
        public static List<SpaceCheese> GetCheese(long mid, int pn, int ps)
        {
            string url = $"https://api.bilibili.com/pugv/app/web/season/page?mid={mid}&pn={pn}&ps={ps}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                SpaceCheeseOrigin cheese = JsonConvert.DeserializeObject<SpaceCheeseOrigin>(response);
                if (cheese == null || cheese.Data == null || cheese.Data.Items == null)
                { return null; }
                return cheese.Data.Items;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetCheese()发生异常: {0}", e);
                LogManager.Error("UserSpace", e);
                return null;
            }
        }

        /// <summary>
        /// 查询用户发布的所有课程列表
        /// </summary>
        /// <param name="mid">目标用户UID</param>
        /// <returns></returns>
        public static List<SpaceCheese> GetAllCheese(long mid)
        {
            List<SpaceCheese> result = new List<SpaceCheese>();

            int i = 0;
            while (true)
            {
                i++;
                int ps = 50;

                List<SpaceCheese> data = GetCheese(mid, i, ps);
                if (data == null || data.Count == 0)
                { break; }

                result.AddRange(data);
            }
            return result;
        }

        #endregion

        #region 订阅

        /// <summary>
        /// 查询用户追番（追剧）明细
        /// </summary>
        /// <param name="mid">目标用户UID</param>
        /// <param name="type">查询类型</param>
        /// <param name="pn">页码</param>
        /// <param name="ps">每页项数</param>
        /// <returns></returns>
        public static BangumiFollowData GetBangumiFollow(long mid, BangumiType type, int pn, int ps)
        {
            string url = $"https://api.bilibili.com/x/space/bangumi/follow/list?vmid={mid}&type={type:D}&pn={pn}&ps={ps}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                BangumiFollowOrigin bangumiFollow = JsonConvert.DeserializeObject<BangumiFollowOrigin>(response);
                if (bangumiFollow == null || bangumiFollow.Data == null)
                { return null; }
                return bangumiFollow.Data;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetBangumiFollow()发生异常: {0}", e);
                LogManager.Error("UserSpace", e);
                return null;
            }
        }

        /// <summary>
        /// 查询用户所有的追番（追剧）明细
        /// </summary>
        /// <param name="mid">目标用户UID</param>
        /// <param name="type">查询类型</param>
        /// <returns></returns>
        public static List<BangumiFollow> GetAllBangumiFollow(long mid, BangumiType type)
        {
            List<BangumiFollow> result = new List<BangumiFollow>();

            int i = 0;
            while (true)
            {
                i++;
                int ps = 30;

                BangumiFollowData data = GetBangumiFollow(mid, type, i, ps);
                if (data == null || data.List == null || data.List.Count == 0)
                { break; }

                result.AddRange(data.List);
            }
            return result;
        }

        #endregion

    }
}
