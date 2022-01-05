using DownKyi.Core.Aria2cNet;
using DownKyi.Core.Aria2cNet.Client;
using DownKyi.Core.Aria2cNet.Client.Entity;
using DownKyi.Core.Aria2cNet.Server;
using DownKyi.Core.BiliApi.Login;
using DownKyi.Core.BiliApi.VideoStream;
using DownKyi.Core.BiliApi.VideoStream.Models;
using DownKyi.Core.Danmaku2Ass;
using DownKyi.Core.FFmpeg;
using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using DownKyi.Core.Storage;
using DownKyi.Core.Utils;
using DownKyi.Models;
using DownKyi.Utils;
using DownKyi.ViewModels.DownloadManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

        #region 音视频

        /// <summary>
        /// 下载音频，返回下载文件路径
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        public string DownloadAudio(DownloadingItem downloading)
        {
            // 更新状态显示
            downloading.DownloadStatusTitle = DictionaryResource.GetString("WhileDownloading");
            downloading.DownloadContent = DictionaryResource.GetString("DownloadingAudio");

            // 如果没有Dash，返回null
            if (downloading.PlayUrl == null || downloading.PlayUrl.Dash == null) { return null; }

            // 如果audio列表没有内容，则返回null
            if (downloading.PlayUrl.Dash.Audio == null) { return null; }
            else if (downloading.PlayUrl.Dash.Audio.Count == 0) { return null; }

            // 根据音频id匹配
            PlayUrlDashVideo downloadAudio = null;
            foreach (PlayUrlDashVideo audio in downloading.PlayUrl.Dash.Audio)
            {
                if (audio.Id == downloading.AudioCodec.Id)
                {
                    downloadAudio = audio;
                    break;
                }
            }

            return DownloadVideo(downloading, downloadAudio);
        }

        /// <summary>
        /// 下载视频，返回下载文件路径
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        public string DownloadVideo(DownloadingItem downloading)
        {
            // 更新状态显示
            downloading.DownloadStatusTitle = DictionaryResource.GetString("WhileDownloading");
            downloading.DownloadContent = DictionaryResource.GetString("DownloadingVideo");

            // 如果没有Dash，返回null
            if (downloading.PlayUrl == null || downloading.PlayUrl.Dash == null) { return null; }

            // 如果Video列表没有内容，则返回null
            if (downloading.PlayUrl.Dash.Video == null) { return null; }
            else if (downloading.PlayUrl.Dash.Video.Count == 0) { return null; }

            // 根据视频编码匹配
            PlayUrlDashVideo downloadVideo = null;
            foreach (PlayUrlDashVideo video in downloading.PlayUrl.Dash.Video)
            {
                if (video.Id == downloading.Resolution.Id && Utils.GetVideoCodecName(video.Codecs) == downloading.VideoCodecName)
                {
                    downloadVideo = video;
                    break;
                }
            }

            return DownloadVideo(downloading, downloadVideo);
        }

        /// <summary>
        /// 将下载音频和视频的函数中相同代码抽象出来
        /// </summary>
        /// <param name="downloading"></param>
        /// <param name="downloadVideo"></param>
        /// <returns></returns>
        private string DownloadVideo(DownloadingItem downloading, PlayUrlDashVideo downloadVideo)
        {
            // 如果为空，说明没有匹配到可下载的音频视频
            if (downloadVideo == null) { return null; }

            // 下载链接
            List<string> urls = new List<string>();
            if (downloadVideo.BaseUrl != null) { urls.Add(downloadVideo.BaseUrl); }
            if (downloadVideo.BackupUrl != null) { urls.AddRange(downloadVideo.BackupUrl); }

            // 路径
            string[] temp = downloading.DownloadBase.FilePath.Split('/');
            string path = downloading.DownloadBase.FilePath.Replace(temp[temp.Length - 1], "");

            // 下载文件名
            string fileName = Guid.NewGuid().ToString("N");

            // 记录本次下载的文件
            downloading.Downloading.DownloadFiles.Add(fileName);

            // 开始下载
            DownloadResult downloadStatus = DownloadByAria(downloading, urls, path, fileName);
            switch (downloadStatus)
            {
                case DownloadResult.SUCCESS:
                    return Path.Combine(path, fileName);
                case DownloadResult.FAILED:
                    return null;
                case DownloadResult.ABORT:
                    return null;
                default:
                    return null;
            }
        }

        #endregion

        /// <summary>
        /// 下载封面
        /// </summary>
        /// <param name="downloading"></param>
        public string DownloadCover(DownloadingItem downloading, string coverUrl, string fileName)
        {
            // 更新状态显示
            downloading.DownloadStatusTitle = DictionaryResource.GetString("WhileDownloading");
            downloading.DownloadContent = DictionaryResource.GetString("DownloadingCover");
            // 下载大小
            downloading.DownloadingFileSize = string.Empty;
            // 下载速度
            downloading.SpeedDisplay = string.Empty;

            // 查询、保存封面
            StorageCover storageCover = new StorageCover();
            string cover = storageCover.GetCover(downloading.DownloadBase.Avid, downloading.DownloadBase.Bvid, downloading.DownloadBase.Cid, coverUrl);
            if (cover == null)
            {
                return null;
            }

            // 复制图片到指定位置
            try
            {
                File.Copy(cover, fileName, true);

                // 记录本次下载的文件
                downloading.Downloading.DownloadFiles.Add(fileName);

                return fileName;
            }
            catch (Exception e)
            {
                Core.Utils.Debugging.Console.PrintLine(e);
                LogManager.Error(Tag, e);
            }

            return null;
        }

        /// <summary>
        /// 下载弹幕
        /// </summary>
        /// <param name="downloading"></param>
        public string DownloadDanmaku(DownloadingItem downloading)
        {
            // 更新状态显示
            downloading.DownloadStatusTitle = DictionaryResource.GetString("WhileDownloading");
            downloading.DownloadContent = DictionaryResource.GetString("DownloadingDanmaku");
            // 下载大小
            downloading.DownloadingFileSize = string.Empty;
            // 下载速度
            downloading.SpeedDisplay = string.Empty;

            string title = $"{downloading.Name}";
            string assFile = $"{downloading.DownloadBase.FilePath}.ass";

            // 记录本次下载的文件
            downloading.Downloading.DownloadFiles.Add(assFile);

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
            Config subtitleConfig = new Config
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
                .Create(downloading.DownloadBase.Avid, downloading.DownloadBase.Cid, subtitleConfig, assFile);

            return assFile;
        }

        /// <summary>
        /// 下载字幕
        /// </summary>
        /// <param name="downloading"></param>
        public List<string> DownloadSubtitle(DownloadingItem downloading)
        {
            // 更新状态显示
            downloading.DownloadStatusTitle = DictionaryResource.GetString("WhileDownloading");
            downloading.DownloadContent = DictionaryResource.GetString("DownloadingSubtitle");
            // 下载大小
            downloading.DownloadingFileSize = string.Empty;
            // 下载速度
            downloading.SpeedDisplay = string.Empty;

            List<string> srtFiles = new List<string>();

            var subRipTexts = VideoStream.GetSubtitle(downloading.DownloadBase.Avid, downloading.DownloadBase.Bvid, downloading.DownloadBase.Cid);
            if (subRipTexts == null)
            {
                return null;
            }

            foreach (var subRip in subRipTexts)
            {
                string srtFile = $"{downloading.DownloadBase.FilePath}_{subRip.LanDoc}.srt";
                try
                {
                    File.WriteAllText(srtFile, subRip.SrtString);

                    // 记录本次下载的文件
                    downloading.Downloading.DownloadFiles.Add(srtFile);

                    srtFiles.Add(srtFile);
                }
                catch (Exception e)
                {
                    Core.Utils.Debugging.Console.PrintLine("DownloadSubtitle()发生异常: {0}", e);
                    LogManager.Error("DownloadSubtitle()", e);
                }
            }

            return srtFiles;
        }

        /// <summary>
        /// 混流音频和视频
        /// </summary>
        /// <param name="downloading"></param>
        /// <param name="audioUid"></param>
        /// <param name="videoUid"></param>
        /// <returns></returns>
        public string MixedFlow(DownloadingItem downloading, string audioUid, string videoUid)
        {
            // 更新状态显示
            downloading.DownloadStatusTitle = DictionaryResource.GetString("MixedFlow");
            downloading.DownloadContent = DictionaryResource.GetString("DownloadingVideo");
            // 下载大小
            downloading.DownloadingFileSize = string.Empty;
            // 下载速度
            downloading.SpeedDisplay = string.Empty;

            string finalFile = $"{downloading.DownloadBase.FilePath}.mp4";
            if (videoUid == null)
            {
                finalFile = $"{downloading.DownloadBase.FilePath}.aac";
            }

            // 合并音视频
            FFmpegHelper.MergeVideo(audioUid, videoUid, finalFile);

            // 获取文件大小
            if (File.Exists(finalFile))
            {
                FileInfo info = new FileInfo(finalFile);
                downloading.FileSize = Format.FormatFileSize(info.Length);
            }
            else
            {
                downloading.FileSize = Format.FormatFileSize(0);
            }

            return finalFile;
        }

        /// <summary>
        /// 解析视频流的下载链接
        /// </summary>
        /// <param name="downloading"></param>
        public void Parse(DownloadingItem downloading)
        {
            // 更新状态显示
            downloading.DownloadStatusTitle = DictionaryResource.GetString("Parsing");
            downloading.DownloadContent = string.Empty;
            // 下载大小
            downloading.DownloadingFileSize = string.Empty;
            // 下载速度
            downloading.SpeedDisplay = string.Empty;

            if (downloading.PlayUrl != null && downloading.Downloading.DownloadStatus == DownloadStatus.NOT_STARTED)
            {
                return;
            }

            // 解析
            switch (downloading.Downloading.PlayStreamType)
            {
                case PlayStreamType.VIDEO:
                    downloading.PlayUrl = VideoStream.GetVideoPlayUrl(downloading.DownloadBase.Avid, downloading.DownloadBase.Bvid, downloading.DownloadBase.Cid);
                    break;
                case PlayStreamType.BANGUMI:
                    downloading.PlayUrl = VideoStream.GetBangumiPlayUrl(downloading.DownloadBase.Avid, downloading.DownloadBase.Bvid, downloading.DownloadBase.Cid);
                    break;
                case PlayStreamType.CHEESE:
                    downloading.PlayUrl = VideoStream.GetCheesePlayUrl(downloading.DownloadBase.Avid, downloading.DownloadBase.Bvid, downloading.DownloadBase.Cid, downloading.DownloadBase.EpisodeId);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 停止下载服务
        /// </summary>
        public void End()
        {
            // TODO
            // 保存数据

            // 关闭Aria服务器
            CloseAriaServer();

            // 结束任务
            tokenSource.Cancel();
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
        private void DoWork()
        {
            CancellationToken cancellationToken = tokenSource.Token;
            while (true)
            {
                int maxDownloading = SettingsManager.GetInstance().GetAriaMaxConcurrentDownloads();
                int downloadingCount = 0;
                foreach (DownloadingItem downloading in downloadingList)
                {
                    if (downloading.Downloading.DownloadStatus == DownloadStatus.DOWNLOADING)
                    {
                        downloadingCount++;
                    }
                }

                foreach (DownloadingItem downloading in downloadingList)
                {
                    if (downloadingCount >= maxDownloading)
                    {
                        break;
                    }

                    // 开始下载
                    if (downloading.Downloading.DownloadStatus == DownloadStatus.NOT_STARTED || downloading.Downloading.DownloadStatus == DownloadStatus.WAIT_FOR_DOWNLOAD)
                    {
                        SingleDownload(downloading);
                        downloadingCount++;
                    }
                }

                // 判断是否该结束线程，若为true，跳出while循环
                if (cancellationToken.IsCancellationRequested)
                {
                    Core.Utils.Debugging.Console.PrintLine("AriaDownloadService: 下载服务结束，跳出while循环");
                    LogManager.Debug(Tag, "下载服务结束");
                    break;
                }

                // 降低CPU占用
                Thread.Sleep(100);
            }
        }

        /// <summary>
        /// 下载一个视频
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        private async void SingleDownload(DownloadingItem downloading)
        {
            // 路径
            string[] temp = downloading.DownloadBase.FilePath.Split('/');
            string path = downloading.DownloadBase.FilePath.Replace(temp[temp.Length - 1], "");
            // 路径不存在则创建
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            await Task.Run(new Action(() =>
            {
                downloading.Downloading.DownloadStatus = DownloadStatus.DOWNLOADING;

                // 初始化
                downloading.DownloadStatusTitle = string.Empty;
                downloading.DownloadContent = string.Empty;
                downloading.Downloading.DownloadFiles.Clear();

                // 解析并依次下载音频、视频、弹幕、字幕、封面等内容
                Parse(downloading);

                // 暂停
                Pause(downloading);

                string audioUid = null;
                // 如果需要下载音频
                if (downloading.DownloadBase.NeedDownloadContent["downloadAudio"])
                {
                    audioUid = DownloadAudio(downloading);
                }

                // 暂停
                Pause(downloading);

                string videoUid = null;
                // 如果需要下载视频
                if (downloading.DownloadBase.NeedDownloadContent["downloadVideo"])
                {
                    videoUid = DownloadVideo(downloading);
                }

                // 暂停
                Pause(downloading);

                string outputDanmaku = null;
                // 如果需要下载弹幕
                if (downloading.DownloadBase.NeedDownloadContent["downloadDanmaku"])
                {
                    outputDanmaku = DownloadDanmaku(downloading);
                }

                // 暂停
                Pause(downloading);

                List<string> outputSubtitles = null;
                // 如果需要下载字幕
                if (downloading.DownloadBase.NeedDownloadContent["downloadSubtitle"])
                {
                    outputSubtitles = DownloadSubtitle(downloading);
                }

                // 暂停
                Pause(downloading);

                string outputCover = null;
                // 如果需要下载封面
                if (downloading.DownloadBase.NeedDownloadContent["downloadCover"])
                {

                    string fileName = $"{downloading.DownloadBase.FilePath}.{GetImageExtension(downloading.DownloadBase.PageCoverUrl)}";

                    // page的封面
                    outputCover = DownloadCover(downloading, downloading.DownloadBase.PageCoverUrl, fileName);
                    // 封面
                    DownloadCover(downloading, downloading.DownloadBase.CoverUrl, $"{path}/Cover.{GetImageExtension(downloading.DownloadBase.CoverUrl)}");
                }

                // 暂停
                Pause(downloading);

                // 混流
                string outputMedia = string.Empty;
                if (downloading.DownloadBase.NeedDownloadContent["downloadAudio"] || downloading.DownloadBase.NeedDownloadContent["downloadVideo"])
                {
                    outputMedia = MixedFlow(downloading, audioUid, videoUid);
                }

                // 暂停
                Pause(downloading);

                // 检测音频、视频是否下载成功
                if (downloading.DownloadBase.NeedDownloadContent["downloadAudio"] || downloading.DownloadBase.NeedDownloadContent["downloadVideo"])
                {
                    // 只有下载音频不下载视频时才输出aac
                    // 只要下载视频就输出mp4
                    if (File.Exists(outputMedia))
                    {
                        // 成功
                    }
                }

                // 检测弹幕是否下载成功
                if (downloading.DownloadBase.NeedDownloadContent["downloadDanmaku"] && File.Exists(outputDanmaku))
                {
                    // 成功
                }

                // 检测字幕是否下载成功
                if (downloading.DownloadBase.NeedDownloadContent["downloadSubtitle"])
                {
                    if (outputSubtitles == null)
                    {
                        // 为null时表示不存在字幕
                    }
                    else
                    {
                        foreach (string subtitle in outputSubtitles)
                        {
                            if (File.Exists(subtitle))
                            {
                                // 成功
                            }
                        }
                    }
                }

                // 检测封面是否下载成功
                if (downloading.DownloadBase.NeedDownloadContent["downloadCover"] && File.Exists(outputCover))
                {
                    // 成功
                }

                // TODO
                // 将下载结果写入数据库
                // 包括下载请求的DownloadingItem对象，
                // 下载结果是否成功等
                // 对是否成功的判断：只要outputMedia存在则成功，否则失败

                Downloaded downloaded = new Downloaded
                {
                    MaxSpeedDisplay = Format.FormatSpeed(downloading.Downloading.MaxSpeed),
                };
                // 设置完成时间
                downloaded.SetFinishedTimestamp(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());

                DownloadedItem downloadedItem = new DownloadedItem
                {
                    DownloadBase = downloading.DownloadBase,
                    Downloaded = downloaded
                };

                App.PropertyChangeAsync(new Action(() =>
                {
                    // 加入到下载完成list中，并从下载中list去除
                    App.DownloadedList.Add(downloadedItem);
                    App.DownloadingList.Remove(downloading);

                    // 下载完成列表排序
                    DownloadFinishedSort finishedSort = SettingsManager.GetInstance().GetDownloadFinishedSort();
                    App.SortDownloadedList(finishedSort);
                }));
            }));
        }

        /// <summary>
        /// 获取图片的扩展名
        /// </summary>
        /// <param name="coverUrl"></param>
        /// <returns></returns>
        private string GetImageExtension(string coverUrl)
        {
            if (coverUrl == null)
            {
                return string.Empty;
            }

            // 图片的扩展名
            string[] temp = coverUrl.Split('.');
            string fileExtension = temp[temp.Length - 1];
            return fileExtension;
        }

        /// <summary>
        /// 强制暂停
        /// </summary>
        /// <param name="downloading"></param>
        private void Pause(DownloadingItem downloading)
        {
            string oldStatus = downloading.DownloadStatusTitle;
            downloading.DownloadStatusTitle = DictionaryResource.GetString("Pausing");
            while (downloading.Downloading.DownloadStatus == DownloadStatus.PAUSE)
            {
                // 降低CPU占用
                Thread.Sleep(100);
            }
            downloading.DownloadStatusTitle = DictionaryResource.GetString("Waiting");

            int maxDownloading = SettingsManager.GetInstance().GetAriaMaxConcurrentDownloads();
            int downloadingCount;
            do
            {
                downloadingCount = 0;
                foreach (DownloadingItem item in downloadingList)
                {
                    if (item.Downloading.DownloadStatus == DownloadStatus.DOWNLOADING)
                    {
                        downloadingCount++;
                    }
                }

                // 降低CPU占用
                Thread.Sleep(100);
            } while (downloadingCount > maxDownloading);

            downloading.DownloadStatusTitle = oldStatus;
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
        private DownloadResult DownloadByAria(DownloadingItem downloading, List<string> urls, string path, string localFileName)
        {
            // path已斜杠结尾，去掉斜杠
            path = path.TrimEnd('/').TrimEnd('\\');

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
            Task<AriaAddUri> ariaAddUri = AriaClient.AddUriAsync(urls, option);
            if (ariaAddUri == null || ariaAddUri.Result == null || ariaAddUri.Result.Result == null)
            {
                return DownloadResult.FAILED;
            }

            // 保存gid
            string gid = ariaAddUri.Result.Result;
            downloading.Downloading.Gid = gid;

            // 管理下载
            AriaManager ariaManager = new AriaManager();
            ariaManager.TellStatus += AriaTellStatus;
            ariaManager.DownloadFinish += AriaDownloadFinish;
            return ariaManager.GetDownloadStatus(gid, new Action(() =>
            {
                switch (downloading.Downloading.DownloadStatus)
                {
                    case DownloadStatus.PAUSE:
                        Task<AriaPause> ariaPause = AriaClient.PauseAsync(downloading.Downloading.Gid);
                        // 通知UI，并阻塞当前线程
                        Pause(downloading);
                        break;
                    case DownloadStatus.DOWNLOADING:
                        Task<AriaPause> ariaUnpause = AriaClient.UnpauseAsync(downloading.Downloading.Gid);
                        break;
                }
            }));
        }

        private void AriaTellStatus(long totalLength, long completedLength, long speed, string gid)
        {
            // 当前的下载视频
            DownloadingItem video = downloadingList.FirstOrDefault(it => it.Downloading.Gid == gid);
            if (video == null) { return; }

            float percent = 0;
            if (totalLength != 0)
            {
                percent = (float)completedLength / totalLength * 100;
            }

            // 根据进度判断本次是否需要更新UI
            if (Math.Abs(percent - video.Progress) < 0.01) { return; }

            // 下载进度
            video.Progress = percent;

            // 下载大小
            video.DownloadingFileSize = Format.FormatFileSize(completedLength) + "/" + Format.FormatFileSize(totalLength);

            // 下载速度
            video.SpeedDisplay = Format.FormatSpeed(speed);

            // 最大下载速度
            if (video.Downloading.MaxSpeed < speed)
            {
                video.Downloading.MaxSpeed = speed;
            }
        }

        private void AriaDownloadFinish(bool isSuccess, string downloadPath, string gid, string msg)
        {
            //throw new NotImplementedException();
        }
    }
}
