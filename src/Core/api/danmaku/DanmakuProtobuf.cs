using Core.api.danmaku.entity;
using System;
using System.Collections.Generic;
using System.IO;

namespace Core.api.danmaku
{
    /// <summary>
    /// protobuf弹幕
    /// </summary>
    public class DanmakuProtobuf
    {
        private static DanmakuProtobuf instance;

        /// <summary>
        /// 获取DanmakuProtobuf实例
        /// </summary>
        /// <returns></returns>
        public static DanmakuProtobuf GetInstance()
        {
            if (instance == null)
            {
                instance = new DanmakuProtobuf();
            }
            return instance;
        }

        /// <summary>
        /// 隐藏DanmakuProtobuf()方法，必须使用单例模式
        /// </summary>
        private DanmakuProtobuf() { }

        /// <summary>
        /// 下载6分钟内的弹幕，返回弹幕列表
        /// </summary>
        /// <param name="avid">稿件avID</param>
        /// <param name="cid">视频CID</param>
        /// <param name="segmentIndex">分包，每6分钟一包</param>
        /// <returns></returns>
        public List<BiliDanmaku> GetDanmakuProto(long avid, long cid, int segmentIndex)
        {
            string url = $"https://api.bilibili.com/x/v2/dm/web/seg.so?type=1&oid={cid}&pid={avid}&segment_index={segmentIndex}";
            string referer = "https://www.bilibili.com";

            FileDownloadUtil fileDownload = new FileDownloadUtil();
            fileDownload.Init(url, referer, Path.GetTempPath() + "downkyi/danmaku", $"{cid}-{segmentIndex}.proto", "DanmakuProtobuf");
            fileDownload.Download();

            var danmakuList = new List<BiliDanmaku>();

            DmSegMobileReply danmakus;
            try
            {
                using (var input = File.OpenRead(Path.GetTempPath() + $"downkyi/danmaku/{cid}-{segmentIndex}.proto"))
                {
                    danmakus = DmSegMobileReply.Parser.ParseFrom(input);
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
#if DEBUG
                Console.WriteLine("发生异常: {0}", e);
#endif
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
        public List<BiliDanmaku> GetAllDanmakuProto(long avid, long cid)
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
