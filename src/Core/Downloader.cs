using Core.entity;
using Core.settings;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace Core
{
    public static class Downloader
    {
        /// <summary>
        /// 获得远程文件的大小
        /// </summary>
        /// <param name="url"></param>
        /// <param name="referer"></param>
        /// <returns></returns>
        public static long GetRemoteFileSize(string url, string referer)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = 30 * 1000;
                request.UserAgent = Utils.GetUserAgent();
                //request.ContentType = "text/html;charset=UTF-8";
                request.Headers["accept-language"] = "zh-CN,zh;q=0.9,en;q=0.8";
                request.Referer = referer;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return response.ContentLength;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetRemoteFileSize()发生异常: {0}", e);
                return 0;
            }
        }

        /// <summary>
        /// 获得视频详情及播放列表
        /// </summary>
        /// <param name="bvid"></param>
        /// <returns></returns>
        public static VideoViewData GetVideoInfo(string bvid, long aid, string referer, bool isBackup = false)
        {
            string url;
            if (bvid != null)
            {
                url = $"https://api.bilibili.com/x/web-interface/view?bvid={bvid}";
            }
            else if (aid >= 0)
            {
                url = $"https://api.bilibili.com/x/web-interface/view?aid={aid}";
            }
            else
            { return null; }

            // 采用备用的api，只能获取cid
            if (isBackup)
            {
                string backupUrl = $"https://api.bilibili.com/x/player/pagelist?bvid={bvid}&jsonp=jsonp";
                url = backupUrl;
            }

            string response = Utils.RequestWeb(url, referer);

            try
            {
                VideoView videoView;
                if (isBackup)
                {
                    Pagelist pagelist = JsonConvert.DeserializeObject<Pagelist>(response);

                    videoView = new VideoView
                    {
                        code = pagelist.code,
                        message = pagelist.message,
                        ttl = pagelist.ttl
                    };
                    videoView.data.pages = pagelist.data;
                }
                else
                {
                    videoView = JsonConvert.DeserializeObject<VideoView>(response);
                }

                if (videoView != null)
                {
                    if (videoView.data != null)
                    {
                        return videoView.data;
                    }
                    else
                    {
                        return null;

                        // 进入备选的url中
                        //return GetVideoInfo(bvid, referer, true);
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine("GetVideoInfo()发生JsonReader异常: {0}", e);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetVideoInfo()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 通过seasonId获得番剧的剧集详情
        /// </summary>
        /// <param name="seasonId"></param>
        /// <returns></returns>
        public static BangumiSeasonResult GetBangumiSeason(long seasonId, string referer)
        {
            string url = $"https://api.bilibili.com/pgc/view/web/season?season_id={seasonId}";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                BangumiSeason bangumiSeason = JsonConvert.DeserializeObject<BangumiSeason>(response);
                if (bangumiSeason != null) { return bangumiSeason.result; }
                else { return null; }
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine("GetBangumiSeason()发生JsonReader异常: {0}", e);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetBangumiSeason()发生异常: {0}", e);
                return null;
            }
        }

        public static long GetBangumiSeasonIdByMedia(long mediaId, string referer)
        {
            string url = $"https://api.bilibili.com/pgc/review/user?media_id={mediaId}";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                BangumiMedia bangumiMedia = JsonConvert.DeserializeObject<BangumiMedia>(response);
                if (bangumiMedia.result.media != null) { return bangumiMedia.result.media.season_id; }
                else { return 0; }
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine("GetBangumiSeasonIdByMedia()发生JsonReader异常: {0}", e);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetBangumiSeasonIdByMedia()发生异常: {0}", e);
                return 0;
            }
        }

        public static long GetBangumiSeasonIdByEpisode(long episode, string referer)
        {
            string url = $"https://www.bilibili.com/bangumi/play/ep{episode}";
            string response = Utils.RequestWeb(url, referer);

            // "ssId": 28324,
            string pattern = "\"ssId\":\\s?\\d+,";
            Regex regex = new Regex(pattern);
            Match match = regex.Match(response);

            // 删除多余的字符
            string ssId = match.Value.Replace("ssId", "");
            ssId = ssId.Replace("\"", "");
            ssId = ssId.Replace(":", "");
            ssId = ssId.Replace(" ", "");
            ssId = ssId.Replace(",", "");

            long seasonId;
            try
            {
                seasonId = long.Parse(ssId);
            }
            catch (FormatException e)
            {
                Console.WriteLine("GetBangumiSeasonIdByEpisode()发生异常: {0}", e);
                return 0;
            }
            return seasonId;
        }

        /// <summary>
        /// 通过ep_id获得课程的信息
        /// </summary>
        /// <param name="episode"></param>
        /// <param name="referer"></param>
        /// <returns></returns>
        public static CheeseSeasonData GetCheeseSeason(long seasonId, long episode, string referer)
        {
            string url = $"https://api.bilibili.com/pugv/view/web/season?";
            if (seasonId != 0)
            {
                url += $"season_id={seasonId}";
            }
            else if (episode != 0)
            {
                url += $"ep_id={episode}";
            }

            string response = Utils.RequestWeb(url, referer);

            try
            {
                CheeseSeason cheeseSeason = JsonConvert.DeserializeObject<CheeseSeason>(response);
                if (cheeseSeason != null) { return cheeseSeason.data; }
                else { return null; }
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine("GetCheeseSeason()发生JsonReader异常: {0}", e);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetCheeseSeason()发生异常: {0}", e);
                return null;
            }
        }

        //public static long GetCheeseEpisodeIdBySeasonId(long seasonId, string referer)
        //{
        //    string url = $"https://api.bilibili.com/pugv/view/web/ep/list?season_id={seasonId}&pn=1";
        //    string response = Utils.RequestWeb(url, referer);

        //    try
        //    {
        //        CheeseList cheeseList = JsonConvert.DeserializeObject<CheeseList>(response);
        //        if (cheeseList.data.items != null && cheeseList.data.items.Count > 0)
        //        {
        //            return cheeseList.data.items[0].id;
        //        }
        //        else { return 0; }
        //    }
        //    catch (JsonReaderException e)
        //    {
        //        Console.WriteLine("发生异常: {0}", e);
        //        return 0;
        //    }
        //}

        /// <summary>
        /// 获得音视频流链接
        /// </summary>
        /// <param name="bvid">视频的bvid</param>
        /// <param name="cid">视频的cid</param>
        public static PlayUrlData GetStreamInfo(string bvid, long avid, long cid, long episodeId, int quality, string referer, bool isProxy = false, int proxy = 0)
        {
            string baseUrlVideo = "https://api.bilibili.com/x/player/playurl";
            string baseUrlSeason = "https://api.bilibili.com/pgc/player/web/playurl";
            string baseUrlCheese = "https://api.bilibili.com/pugv/player/web/playurl";
            string baseUrl;
            VideoType videoType = Common.GetVideoType(referer);
            switch (videoType)
            {
                case VideoType.VIDEO:
                    baseUrl = baseUrlVideo;
                    break;
                case VideoType.VIDEO_AV:
                    baseUrl = baseUrlVideo;
                    break;
                case VideoType.BANGUMI_SEASON:
                    baseUrl = baseUrlSeason;
                    break;
                case VideoType.BANGUMI_EPISODE:
                    baseUrl = baseUrlSeason;
                    break;
                case VideoType.BANGUMI_MEDIA:
                    baseUrl = baseUrlSeason;
                    break;
                case VideoType.CHEESE_SEASON:
                    baseUrl = baseUrlCheese;
                    break;
                case VideoType.CHEESE_EPISODE:
                    baseUrl = baseUrlCheese;
                    break;
                default:
                    baseUrl = baseUrlVideo;
                    break;
            }
            // TODO 没有的参数不加入url
            //string url = $"{baseUrl}?cid={cid}&bvid={bvid}&avid={avid}&ep_id={episodeId}&qn={quality}&otype=json&fourk=1&fnver=0&fnval=16";
            string url = $"{baseUrl}?cid={cid}&qn={quality}&otype=json&fourk=1&fnver=0&fnval=16";
            if (bvid != null)
            {
                url += $"&bvid={bvid}";
            }
            if (avid != 0)
            {
                url += $"&avid={avid}";
            }
            if (episodeId != 0)
            {
                url += $"&ep_id={episodeId}";
            }

            // 代理网址
            //https://www.biliplus.com/BPplayurl.php?cid=180873425&qn=116&type=&otype=json&fourk=1&bvid=BV1pV411o7yD&ep_id=317925&fnver=0&fnval=16&module=pgc
            if (isProxy && proxy == 1)
            {
                string proxyUrl1 = "https://www.biliplus.com/BPplayurl.php";
                url = $"{proxyUrl1}?cid={cid}&bvid={bvid}&qn={quality}&otype=json&fourk=1&fnver=0&fnval=16&module=pgc";
            }
            else if (isProxy && proxy == 2)
            {
                string proxyUrl2 = "https://biliplus.ipcjs.top/BPplayurl.php";
                url = $"{proxyUrl2}?cid={cid}&bvid={bvid}&qn={quality}&otype=json&fourk=1&fnver=0&fnval=16&module=pgc";
            }

            string response = Utils.RequestWeb(url, referer);
            //Console.WriteLine(response);

            try
            {
                PlayUrl playUrl;
                if (isProxy)
                {
                    PlayUrlData playUrlData = JsonConvert.DeserializeObject<PlayUrlData>(response);

                    playUrl = new PlayUrl
                    {
                        result = playUrlData
                    };
                }
                else
                {
                    playUrl = JsonConvert.DeserializeObject<PlayUrl>(response);
                }

                if (playUrl != null)
                {
                    if (playUrl.data != null) { return playUrl.data; }
                    if (playUrl.result != null) { return playUrl.result; }

                    // 无法从B站获取数据，进入代理网站
                    if (Settings.GetInstance().IsLiftingOfRegion() == ALLOW_STATUS.YES)
                    {
                        switch (proxy)
                        {
                            case 0:
                                return GetStreamInfo(bvid, avid, cid, episodeId, quality, referer, true, 1);
                            case 1:
                                return GetStreamInfo(bvid, avid, cid, episodeId, quality, referer, true, 2);
                            case 2:
                                return null;
                        }
                    }

                    return null;
                }
                else { return null; }
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine("GetStreamInfo()发生JsonReader异常: {0}", e);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetStreamInfo()发生异常: {0}", e);
                return null;
            }
        }

        //public static List<Danmaku> GetAllDanmaku(long cid, long publishTime, string referer)
        //{
        //    List<Danmaku> danmakus = new List<Danmaku>();

        //    // 设置视频发布日期
        //    DateTime publishDate = new DateTime(1970, 1, 1);
        //    publishDate = publishDate.AddSeconds(publishTime);

        //    // 获得有弹幕的日期date
        //    List<string> danmakuDateList = new List<string>();
        //    while (true)
        //    {
        //        string month = publishDate.ToString("yyyy-MM");
        //        string url = $"https://api.bilibili.com/x/v2/dm/history/index?type=1&oid={cid}&month={month}";
        //        string response = Utils.RequestWeb(url, referer);

        //        DanmuDate danmakuDate;
        //        try
        //        {
        //            danmakuDate = JsonConvert.DeserializeObject<DanmuDate>(response);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine("GetAllDanmaku()发生异常: {0}", e);
        //            continue;
        //        }
        //        if (danmakuDate != null || danmakuDate.data != null) { danmakuDateList.AddRange(danmakuDate.data); }

        //        if (publishDate.CompareTo(DateTime.Now) > 0) { break; }
        //        publishDate = publishDate.AddMonths(1);
        //        Thread.Sleep(100);
        //    }

        //    // 获取弹幕
        //    foreach (var date in danmakuDateList)
        //    {
        //        Console.WriteLine(date);

        //        List<Danmaku> danmakusOfOneDay = GetDanmaku(cid, date);

        //        foreach (Danmaku danmaku in danmakusOfOneDay)
        //        {
        //            if (danmakus.Find(it => it.DanmuId == danmaku.DanmuId) == null)
        //            {
        //                danmakus.Add(danmaku);
        //            }
        //        }
        //    }

        //    // 按弹幕发布时间排序
        //    danmakus = danmakus.OrderBy(it => it.Timestamp).ToList();

        //    return danmakus;
        //}

        //public static List<Danmaku> GetDanmaku(long cid, string date, string referer)
        //{
        //    string url = $"https://api.bilibili.com/x/v2/dm/history?type=1&oid={cid}&date={date}";
        //    string response = Utils.RequestWeb(url, referer);

        //    // <?xml version="1.0" encoding="UTF-8"?>
        //    // {"code":-101,"message":"账号未登录","ttl":1}
        //    if (response.Contains("<?xml version=\"1.0\" encoding=\"UTF-8\"?>"))
        //    {
        //        List<Danmaku> danmakus = new List<Danmaku>();

        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(response);
        //        // 取得节点名为d的XmlNode集合
        //        XmlNodeList danmuList = doc.GetElementsByTagName("d");
        //        foreach (XmlNode node in danmuList)
        //        {
        //            // 返回的是文字内容
        //            string nodeText = node.InnerText;
        //            // 节点p属性值
        //            string childValue = node.Attributes["p"].Value;
        //            // 拆分属性
        //            string[] attrs = childValue.Split(',');

        //            Danmaku danmaku = new Danmaku
        //            {
        //                Text = nodeText,
        //                Time = float.Parse(attrs[0]),
        //                Type = int.Parse(attrs[1]),
        //                Fontsize = int.Parse(attrs[2]),
        //                Color = long.Parse(attrs[3]),
        //                Timestamp = long.Parse(attrs[4]),
        //                Pool = int.Parse(attrs[5]),
        //                UserId = attrs[6],
        //                DanmuId = attrs[7]
        //            };
        //            danmakus.Add(danmaku);
        //        }

        //        return danmakus;
        //    }
        //    else
        //    {
        //        DanmuFromWeb danmu = JsonConvert.DeserializeObject<DanmuFromWeb>(response);
        //        if (danmu != null) { Console.WriteLine(danmu.message); }
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// 获取弹幕，不需要登录信息，只能获取3000条弹幕
        ///// </summary>
        ///// <param name="cid"></param>
        ///// <returns></returns>
        //public static List<Danmaku> GetDanmaku(long cid, string referer)
        //{
        //    string url = $"https://api.bilibili.com/x/v1/dm/list.so?oid={cid}";
        //    string response = Utils.RequestWeb(url, referer);

        //    if (response.Contains("<?xml version=\"1.0\" encoding=\"UTF-8\"?>"))
        //    {
        //        List<Danmaku> danmakus = new List<Danmaku>();

        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(response);
        //        // 取得节点名为d的XmlNode集合
        //        XmlNodeList danmuList = doc.GetElementsByTagName("d");
        //        foreach (XmlNode node in danmuList)
        //        {
        //            // 返回的是文字内容
        //            string nodeText = node.InnerText;
        //            // 节点p属性值
        //            string childValue = node.Attributes["p"].Value;
        //            // 拆分属性
        //            string[] attrs = childValue.Split(',');

        //            Danmaku danmaku = new Danmaku
        //            {
        //                Text = nodeText,
        //                Time = float.Parse(attrs[0]),
        //                Type = int.Parse(attrs[1]),
        //                Fontsize = int.Parse(attrs[2]),
        //                Color = long.Parse(attrs[3]),
        //                Timestamp = long.Parse(attrs[4]),
        //                Pool = int.Parse(attrs[5]),
        //                UserId = attrs[6],
        //                DanmuId = attrs[7]
        //            };
        //            danmakus.Add(danmaku);
        //        }

        //        return danmakus;
        //    }
        //    return null;
        //}

    }
}
