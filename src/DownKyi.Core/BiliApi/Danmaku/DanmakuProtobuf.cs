using Bilibili.Community.Service.Dm.V1;
using DownKyi.Core.BiliApi.Danmaku.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace DownKyi.Core.BiliApi.Danmaku
{
    public static class DanmakuProtobuf
    {

        /// <summary>
        /// 下载6分钟内的弹幕，返回弹幕列表
        /// </summary>
        /// <param name="avid">稿件avID</param>
        /// <param name="cid">视频CID</param>
        /// <param name="segmentIndex">分包，每6分钟一包</param>
        /// <returns></returns>
        public static List<BiliDanmaku> GetDanmakuProto(long avid, long cid, int segmentIndex)
        {
            string url = $"https://api.bilibili.com/x/v2/dm/web/seg.so?type=1&oid={cid}&pid={avid}&segment_index={segmentIndex}";
            //string referer = "https://www.bilibili.com";

            string directory = Path.Combine(Storage.StorageManager.GetDanmaku(), $"{cid}");
            string filePath = Path.Combine(directory, $"{segmentIndex}.proto");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            try
            {
                System.Net.WebClient mywebclient = new System.Net.WebClient();
                mywebclient.DownloadFile(url, filePath);
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetDanmakuProto()发生异常: {0}", e);
                //Logging.LogManager.Error(e);
            }

            var danmakuList = new List<BiliDanmaku>();
            try
            {
                using (var input = File.OpenRead(filePath))
                {
                    DmSegMobileReply danmakus = DmSegMobileReply.Parser.ParseFrom(input);
                    if (danmakus == null || danmakus.Elems == null)
                    {
                        return danmakuList;
                    }

                    foreach (var dm in danmakus.Elems)
                    {
                        var danmaku = new BiliDanmaku
                        {
                            Id = dm.Id,
                            Progress = dm.Progress,
                            Mode = dm.Mode,
                            Fontsize = dm.Fontsize,
                            Color = dm.Color,
                            MidHash = dm.MidHash,
                            Content = dm.Content,
                            Ctime = dm.Ctime,
                            Weight = dm.Weight,
                            //Action = dm.Action,
                            Pool = dm.Pool
                        };
                        danmakuList.Add(danmaku);
                    }
                }
            }
            catch (Exception e)
            {
                Utils.Debugging.Console.PrintLine("GetDanmakuProto()发生异常: {0}", e);
                //Logging.LogManager.Error(e);
                return null;
            }

            return danmakuList;
        }

        /// <summary>
        /// 下载所有弹幕，返回弹幕列表
        /// </summary>
        /// <param name="avid">稿件avID</param>
        /// <param name="cid">视频CID</param>
        /// <returns></returns>
        public static List<BiliDanmaku> GetAllDanmakuProto(long avid, long cid)
        {
            var danmakuList = new List<BiliDanmaku>();

            int segmentIndex = 0;
            while (true)
            {
                segmentIndex += 1;
                var danmakus = GetDanmakuProto(avid, cid, segmentIndex);
                if (danmakus == null) { break; }
                danmakuList.AddRange(danmakus);
            }
            return danmakuList;
        }
    }
}
