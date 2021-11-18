using DownKyi.Core.BiliApi.Models.Json;
using DownKyi.Core.BiliApi.VideoStream.Models;
using DownKyi.Core.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DownKyi.Core.BiliApi.VideoStream
{
    public static class VideoStream
    {

        /// <summary>
        /// 获取播放器信息（web端）
        /// </summary>
        /// <param name="avid"></param>
        /// <param name="bvid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static PlayerV2 PlayerV2(long avid, string bvid, long cid)
        {
            string url = $"https://api.bilibili.com/x/player/v2?cid={cid}&aid={avid}&bvid={bvid}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                var playUrl = JsonConvert.DeserializeObject<PlayerV2Origin>(response);
                return playUrl?.Data;
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("PlayerV2()发生异常: {0}", e);
                LogManager.Error("PlayerV2()", e);
                return null;
            }
        }

        /// <summary>
        /// 获取所有字幕
        /// </summary>
        /// <param name="avid"></param>
        /// <param name="bvid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public static List<SubRipText> GetSubtitle(long avid, string bvid, long cid)
        {
            List<SubRipText> subRipTexts = new List<SubRipText>();

            // 获取播放器信息
            var player = PlayerV2(avid, bvid, cid);

            foreach (var subtitle in player.Subtitle.Subtitles)
            {
                string referer = "https://www.bilibili.com";
                string response = WebClient.RequestWeb($"https:{subtitle.SubtitleUrl}", referer);

                try
                {
                    var subtitleJson = JsonConvert.DeserializeObject<SubtitleJson>(response);
                    if (subtitleJson == null) { continue; }

                    subRipTexts.Add(new SubRipText
                    {
                        Lan = subtitle.Lan,
                        LanDoc = subtitle.LanDoc,
                        SrtString = subtitleJson.ToSubRip()
                    });
                }
                catch (Exception e)
                {
                    Utils.Debugging.Console.PrintLine("GetSubtitle()发生异常: {0}", e);
                    LogManager.Error("GetSubtitle()", e);
                }
            }

            return subRipTexts;
        }

        /// <summary>
        /// 获取普通视频的视频流
        /// </summary>
        /// <param name="avid"></param>
        /// <param name="bvid"></param>
        /// <param name="cid"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static PlayUrl GetVideoPlayUrl(long avid, string bvid, long cid, int quality = 125)
        {
            string baseUrl = $"https://api.bilibili.com/x/player/playurl?cid={cid}&qn={quality}&fourk=1&fnver=0&fnval=80";
            string url;
            if (bvid != null) { url = $"{baseUrl}&bvid={bvid}"; }
            else if (avid > -1) { url = $"{baseUrl}&aid={avid}"; }
            else { return null; }

            return GetPlayUrl(url);
        }

        /// <summary>
        /// 获取番剧的视频流
        /// </summary>
        /// <param name="avid"></param>
        /// <param name="bvid"></param>
        /// <param name="cid"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static PlayUrl GetBangumiPlayUrl(long avid, string bvid, long cid, int quality = 125)
        {
            string baseUrl = $"https://api.bilibili.com/pgc/player/web/playurl?cid={cid}&qn={quality}&fourk=1&fnver=0&fnval=80";
            string url;
            if (bvid != null) { url = $"{baseUrl}&bvid={bvid}"; }
            else if (avid > -1) { url = $"{baseUrl}&aid={avid}"; }
            else { return null; }

            return GetPlayUrl(url);
        }

        /// <summary>
        /// 获取课程的视频流
        /// </summary>
        /// <param name="avid"></param>
        /// <param name="bvid"></param>
        /// <param name="cid"></param>
        /// <param name="quality"></param>
        /// <returns></returns>
        public static PlayUrl GetCheesePlayUrl(long avid, string bvid, long cid, long episodeId, int quality = 125)
        {
            string baseUrl = $"https://api.bilibili.com/pugv/player/web/playurl?cid={cid}&qn={quality}&fourk=1&fnver=0&fnval=80";
            string url;
            if (bvid != null) { url = $"{baseUrl}&bvid={bvid}"; }
            else if (avid > -1) { url = $"{baseUrl}&aid={avid}"; }
            else { return null; }

            // 必须有episodeId，否则会返回请求错误
            if (episodeId != 0)
            {
                url += $"&ep_id={episodeId}";
            }

            return GetPlayUrl(url);
        }

        /// <summary>
        /// 获取视频流
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static PlayUrl GetPlayUrl(string url)
        {
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                var playUrl = JsonConvert.DeserializeObject<PlayUrlOrigin>(response);
                if (playUrl == null) { return null; }
                else if (playUrl.Data != null) { return playUrl.Data; }
                else if (playUrl.Result != null) { return playUrl.Result; }
                else { return null; }
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetPlayUrl()发生异常: {0}", e);
                LogManager.Error("GetPlayUrl()", e);
                return null;
            }
        }

    }
}
