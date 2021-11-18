using DownKyi.Core.Aria2cNet;
using DownKyi.Core.Aria2cNet.Client;
using DownKyi.Core.Aria2cNet.Client.Entity;
using DownKyi.Core.Aria2cNet.Server;
using DownKyi.Core.BiliApi.Login;
using DownKyi.Core.BiliApi.VideoStream;
using DownKyi.Core.Danmaku2Ass;
using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using DownKyi.Core.Storage;
using DownKyi.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DownKyi.Services.Download
{
    /// <summary>
    /// 音视频采用Aria下载，其余采用WebClient下载
    /// </summary>
    public class AriaDownloadService : DownloadService, IDownloadService
    {
        private CancellationTokenSource tokenSource;

        public AriaDownloadService(ObservableCollection<DownloadingItem> downloadingList, ObservableCollection<DownloadedItem> downloadedList) : base(downloadingList, downloadedList)
        {
            Tag = "AriaDownloadService";
        }

        public string DownloadAudio(DownloadingItem downloading)
        {
            // 如果没有Dash，返回null
            if(downloading.PlayUrl.Dash == null) { return null; }

            // 如果audio列表没有内容，则返回null
            if (downloading.PlayUrl.Dash.Audio == null) { return null; }
            else if (downloading.PlayUrl.Dash.Audio.Count == 0) { return null; }


            throw new NotImplementedException();
        }

        /// <summary>
        /// 下载封面
        /// </summary>
        /// <param name="downloading"></param>
        public void DownloadCover(DownloadingItem downloading)
        {
            // 查询、保存封面
            StorageCover storageCover = new StorageCover();
            string cover = storageCover.GetCover(downloading.Avid, downloading.Bvid, downloading.Cid, downloading.CoverUrl);
            if (cover == null)
            {
                return;
            }

            // 图片的扩展名
            string[] temp = downloading.CoverUrl.Split('.');
            string fileExtension = temp[temp.Length - 1];

            // 图片的地址
            string coverPath = $"{StorageManager.GetCover()}/{cover}";

            // 复制图片到指定位置
            try
            {
                File.Copy(coverPath, $"{downloading.FilePath}.{fileExtension}");
            }
            catch (Exception e)
            {
                Core.Utils.Debugging.Console.PrintLine(e);
                LogManager.Error(Tag, e);
            }
        }

        /// <summary>
        /// 下载弹幕
        /// </summary>
        /// <param name="downloading"></param>
        public void DownloadDanmaku(DownloadingItem downloading)
        {
            string title = $"{downloading.Name}";
            string assFile = $"{downloading.FilePath}.ass";

            int screenWidth = SettingsManager.GetInstance().GetDanmakuScreenWidth();
            int screenHeight = SettingsManager.GetInstance().GetDanmakuScreenHeight();
            //if (SettingsManager.GetInstance().IsCustomDanmakuResolution() != AllowStatus.YES)
            //{
            //    if (downloadingEntity.Width > 0 && downloadingEntity.Height > 0)
            //    {
            //        screenWidth = downloadingEntity.Width;
            //        screenHeight = downloadingEntity.Height;
            //    }
            //}

            // 字幕配置
            var subtitleConfig = new Config
            {
                Title = title,
                ScreenWidth = screenWidth,
                ScreenHeight = screenHeight,
                FontName = SettingsManager.GetInstance().GetDanmakuFontName(),
                BaseFontSize = SettingsManager.GetInstance().GetDanmakuFontSize(),
                LineCount = SettingsManager.GetInstance().GetDanmakuLineCount(),
                LayoutAlgorithm = SettingsManager.GetInstance().GetDanmakuLayoutAlgorithm().ToString("G").ToLower(), // async/sync
                TuneDuration = 0,
                DropOffset = 0,
                BottomMargin = 0,
                CustomOffset = 0
            };

            Core.Danmaku2Ass.Bilibili.GetInstance()
                .SetTopFilter(SettingsManager.GetInstance().GetDanmakuTopFilter() == AllowStatus.YES)
                .SetBottomFilter(SettingsManager.GetInstance().GetDanmakuBottomFilter() == AllowStatus.YES)
                .SetScrollFilter(SettingsManager.GetInstance().GetDanmakuScrollFilter() == AllowStatus.YES)
                .Create(downloading.Avid, downloading.Cid, subtitleConfig, assFile);
        }

        /// <summary>
        /// 下载字幕
        /// </summary>
        /// <param name="downloading"></param>
        public void DownloadSubtitle(DownloadingItem downloading)
        {
            var subRipTexts = VideoStream.GetSubtitle(downloading.Avid, downloading.Bvid, downloading.Cid);
            foreach (var subRip in subRipTexts)
            {
                string srtFile = $"{downloading.FilePath}_{subRip.LanDoc}.srt";
                try
                {
                    File.WriteAllText(srtFile, subRip.SrtString);
                }
                catch (Exception e)
                {
                    Core.Utils.Debugging.Console.PrintLine("DownloadSubtitle()发生异常: {0}", e);
                    LogManager.Error("DownloadSubtitle()", e);
                }
            }
        }

        public string DownloadVideo(DownloadingItem downloading)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 停止下载服务
        /// </summary>
        public void End()
        {
            // TODO something

            // 关闭Aria服务器
            CloseAriaServer();

            // 结束任务
            tokenSource.Cancel();
        }

        public void MixedFlow(DownloadingItem downloading, string audioUid, string videoUid)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 解析视频流的下载链接
        /// </summary>
        /// <param name="downloading"></param>
        public void Parse(DownloadingItem downloading)
        {
            if (downloading.PlayUrl != null && downloading.DownloadStatus == DownloadStatus.NOT_STARTED)
            {
                return;
            }

            switch (downloading.PlayStreamType)
            {
                case PlayStreamType.VIDEO:
                    downloading.PlayUrl = VideoStream.GetVideoPlayUrl(downloading.Avid, downloading.Bvid, downloading.Cid);
                    break;
                case PlayStreamType.BANGUMI:
                    downloading.PlayUrl = VideoStream.GetBangumiPlayUrl(downloading.Avid, downloading.Bvid, downloading.Cid);
                    break;
                case PlayStreamType.CHEESE:
                    downloading.PlayUrl = VideoStream.GetCheesePlayUrl(downloading.Avid, downloading.Bvid, downloading.Cid, downloading.EpisodeId);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 启动下载服务
        /// </summary>
        public async void Start()
        {
            // 启动Aria服务器
            StartAriaServer();

            await Task.Run(DoWork, (tokenSource = new CancellationTokenSource()).Token);
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        private async void DoWork()
        {
            CancellationToken cancellationToken = tokenSource.Token;
            while (true)
            {
                int maxDownloading = SettingsManager.GetInstance().GetAriaMaxConcurrentDownloads();
                int countDownloading = 0;
                bool isOutOfMaxDownloading = false;
                foreach (DownloadingItem downloading in downloadingList)
                {
                    // 对正在下载的元素计数
                    if (downloading.DownloadStatus == DownloadStatus.DOWNLOADING)
                    {
                        countDownloading++;
                    }

                    // 正在下载数量等于最大下载数量，退出本次循环
                    if (countDownloading == maxDownloading)
                    {
                        break;
                    }

                    // 正在下载数量大于最大下载数量
                    if (countDownloading > maxDownloading)
                    {
                        isOutOfMaxDownloading = true;
                    }

                    // 将超过下载数量的元素暂停
                    if (isOutOfMaxDownloading)
                    {
                        if (downloading.DownloadStatus == DownloadStatus.DOWNLOADING)
                        {
                            downloading.DownloadStatus = DownloadStatus.PAUSE;
                        }
                    }

                    await Task.Run(new Action(() =>
                    {
                        downloading.DownloadStatus = DownloadStatus.DOWNLOADING;

                        // 依次下载音频、视频、弹幕、字幕、封面等内容
                        Parse(downloading);
                        string audioUid = DownloadAudio(downloading);
                        string videoUid = DownloadVideo(downloading);
                        DownloadDanmaku(downloading);
                        DownloadSubtitle(downloading);
                        DownloadCover(downloading);
                        MixedFlow(downloading, audioUid, videoUid);
                    }),
                    (tokenSource = new CancellationTokenSource()).Token);

                }

                // 判断是否该结束线程，若为true，跳出while循环
                if (cancellationToken.IsCancellationRequested)
                {
                    Core.Utils.Debugging.Console.PrintLine("AriaDownloadService: 下载服务结束，跳出while循环");
                    LogManager.Debug(Tag, "下载服务结束");
                    break;
                }

                // 降低CPU占用
                Thread.Sleep(500);
            }
        }

        /// <summary>
        /// 启动Aria服务器
        /// </summary>
        private async void StartAriaServer()
        {
            List<string> header = new List<string>
            {
                $"Cookie: {LoginHelper.GetLoginInfoCookiesString()}",
                $"Origin: https://www.bilibili.com",
                $"Referer: https://www.bilibili.com",
                $"User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit / 537.36(KHTML, like Gecko) Chrome / 91.0.4472.77 Safari / 537.36"
            };

            AriaConfig config = new AriaConfig()
            {
                ListenPort = SettingsManager.GetInstance().GetAriaListenPort(),
                Token = "downkyi",
                LogLevel = SettingsManager.GetInstance().GetAriaLogLevel(),
                MaxConcurrentDownloads = SettingsManager.GetInstance().GetAriaMaxConcurrentDownloads(),
                MaxConnectionPerServer = 16, // 最大取16
                Split = SettingsManager.GetInstance().GetAriaSplit(),
                //MaxTries = 5,
                MinSplitSize = 10, // 10MB
                MaxOverallDownloadLimit = SettingsManager.GetInstance().GetAriaMaxOverallDownloadLimit() * 1024L, // 输入的单位是KB/s，所以需要乘以1024
                MaxDownloadLimit = SettingsManager.GetInstance().GetAriaMaxDownloadLimit() * 1024L, // 输入的单位是KB/s，所以需要乘以1024
                MaxOverallUploadLimit = 0,
                MaxUploadLimit = 0,
                ContinueDownload = true,
                FileAllocation = SettingsManager.GetInstance().GetAriaFileAllocation(),
                Headers = header
            };
            var task = await AriaServer.StartServerAsync(config);
            if (task) { Console.WriteLine("Start ServerAsync Completed"); }
            Console.WriteLine("Start ServerAsync end");

            // 恢复所有下载
            //var ariaPause = await AriaClient.UnpauseAllAsync();
            //if (ariaPause != null)
            //{
            //    Core.Utils.Debugging.Console.PrintLine(ariaPause.ToString());
            //}
        }

        /// <summary>
        /// 关闭Aria服务器
        /// </summary>
        private void CloseAriaServer()
        {
            new Thread(() =>
            {
                // 暂停所有下载
                var ariaPause = AriaClient.PauseAllAsync();
                Core.Utils.Debugging.Console.PrintLine(ariaPause.ToString());

                // 关闭服务器
                bool close = AriaServer.CloseServer();
                Core.Utils.Debugging.Console.PrintLine(close);
            })
            { IsBackground = false }
            .Start();
        }

        /// <summary>
        /// 采用Aria下载文件
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        private DownloadResult DownloadByAria(DownloadingItem downloading, List<string> urls, string localFileName)
        {
            string[] temp = downloading.FilePath.Split('/');
            string path = downloading.FilePath.Replace(temp[temp.Length - 1], "");

            AriaSendOption option = new AriaSendOption
            {
                //HttpProxy = $"http://{Settings.GetAriaHttpProxy()}:{Settings.GetAriaHttpProxyListenPort()}",
                Dir = path,
                Out = localFileName
                //Header = $"cookie: {Login.GetLoginInfoCookiesString()}\nreferer: https://www.bilibili.com",
                //UseHead = "true",
                //UserAgent = Utils.GetUserAgent()
            };

            // 如果设置了代理，则增加HttpProxy
            if (SettingsManager.GetInstance().IsAriaHttpProxy() == AllowStatus.YES)
            {
                option.HttpProxy = $"http://{SettingsManager.GetInstance().GetAriaHttpProxy()}:{SettingsManager.GetInstance().GetAriaHttpProxyListenPort()}";
            }

            // 添加一个下载
            var ariaAddUri = AriaClient.AddUriAsync(urls, option);
            if (ariaAddUri == null || ariaAddUri.Result == null || ariaAddUri.Result.Result == null)
            {
                return DownloadResult.FAILED;
            }

            // 保存gid
            string gid = ariaAddUri.Result.Result;
            downloading.Gid = gid;

            // 管理下载
            AriaManager ariaManager = new AriaManager();
            ariaManager.TellStatus += AriaTellStatus;
            ariaManager.DownloadFinish += AriaDownloadFinish;
            return ariaManager.GetDownloadStatus(gid, new Action(() =>
            {
                switch (downloading.DownloadStatus)
                {
                    case DownloadStatus.PAUSE:
                        var ariaPause = AriaClient.PauseAsync(downloading.Gid);
                        // TODO
                        break;
                    case DownloadStatus.DOWNLOADING:
                        var ariaUnpause = AriaClient.UnpauseAsync(downloading.Gid);
                        // TODO
                        break;
                }
            }));
        }

        private void AriaTellStatus(long totalLength, long completedLength, long speed, string gid)
        {
            throw new NotImplementedException();
        }

        private void AriaDownloadFinish(bool isSuccess, string downloadPath, string gid, string msg)
        {
            throw new NotImplementedException();
        }
    }
}
