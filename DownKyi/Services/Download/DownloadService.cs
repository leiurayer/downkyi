using DownKyi.Core.BiliApi.VideoStream;
using DownKyi.Core.BiliApi.VideoStream.Models;
using DownKyi.Core.Danmaku2Ass;
using DownKyi.Core.FFmpeg;
using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using DownKyi.Core.Storage;
using DownKyi.Core.Utils;
using DownKyi.Images;
using DownKyi.Models;
using DownKyi.Utils;
using DownKyi.ViewModels.DownloadManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace DownKyi.Services.Download
{
    public abstract class DownloadService
    {
        protected string Tag = "DownloadService";

        protected ObservableCollection<DownloadingItem> downloadingList;
        protected ObservableCollection<DownloadedItem> downloadedList;

        protected Task workTask;
        protected CancellationTokenSource tokenSource;
        protected CancellationToken cancellationToken;
        protected List<Task> downloadingTasks = new List<Task>();

        protected readonly int retry = 5;
        protected readonly string nullMark = "<null>";

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        public DownloadService(ObservableCollection<DownloadingItem> downloadingList, ObservableCollection<DownloadedItem> downloadedList)
        {
            this.downloadingList = downloadingList;
            this.downloadedList = downloadedList;
        }

        protected PlayUrlDashVideo BaseDownloadAudio(DownloadingItem downloading)
        {
            // 更新状态显示
            downloading.DownloadStatusTitle = DictionaryResource.GetString("WhileDownloading");
            downloading.DownloadContent = DictionaryResource.GetString("DownloadingAudio");
            // 下载大小
            downloading.DownloadingFileSize = string.Empty;
            downloading.Progress = 0;
            // 下载速度
            downloading.SpeedDisplay = string.Empty;

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

            // 避免Dolby==null及其它未知情况，直接使用异常捕获
            try
            {
                // Dolby Atmos
                if (downloading.AudioCodec.Id == 30250)
                {
                    downloadAudio = downloading.PlayUrl.Dash.Dolby.Audio[0];
                }
            }
            catch (Exception) { }

            return downloadAudio;
        }

        protected PlayUrlDashVideo BaseDownloadVideo(DownloadingItem downloading)
        {
            // 更新状态显示
            downloading.DownloadStatusTitle = DictionaryResource.GetString("WhileDownloading");
            downloading.DownloadContent = DictionaryResource.GetString("DownloadingVideo");
            // 下载大小
            downloading.DownloadingFileSize = string.Empty;
            downloading.Progress = 0;
            // 下载速度
            downloading.SpeedDisplay = string.Empty;

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

            return downloadVideo;
        }

        protected string BaseDownloadCover(DownloadingItem downloading, string coverUrl, string fileName)
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
                if (!downloading.Downloading.DownloadFiles.ContainsKey(coverUrl))
                {
                    downloading.Downloading.DownloadFiles.Add(coverUrl, fileName);
                }

                return fileName;
            }
            catch (Exception e)
            {
                Core.Utils.Debugging.Console.PrintLine(e);
                LogManager.Error(Tag, e);
            }

            return null;
        }

        protected string BaseDownloadDanmaku(DownloadingItem downloading)
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
            if (!downloading.Downloading.DownloadFiles.ContainsKey("danmaku"))
            {
                downloading.Downloading.DownloadFiles.Add("danmaku", assFile);
            }

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

        protected List<string> BaseDownloadSubtitle(DownloadingItem downloading)
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
                    if (!downloading.Downloading.DownloadFiles.ContainsKey("subtitle"))
                    {
                        downloading.Downloading.DownloadFiles.Add("subtitle", srtFile);
                    }

                    srtFiles.Add(srtFile);
                }
                catch (Exception e)
                {
                    Core.Utils.Debugging.Console.PrintLine($"{Tag}.DownloadSubtitle()发生异常: {0}", e);
                    LogManager.Error($"{Tag}.DownloadSubtitle()", e);
                }
            }

            return srtFiles;
        }

        protected string BaseMixedFlow(DownloadingItem downloading, string audioUid, string videoUid)
        {
            // 更新状态显示
            downloading.DownloadStatusTitle = DictionaryResource.GetString("MixedFlow");
            downloading.DownloadContent = DictionaryResource.GetString("DownloadingVideo");
            // 下载大小
            downloading.DownloadingFileSize = string.Empty;
            // 下载速度
            downloading.SpeedDisplay = string.Empty;

            //if (videoUid == nullMark)
            //{
            //    return null;
            //}

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

        protected void BaseParse(DownloadingItem downloading)
        {
            // 更新状态显示
            downloading.DownloadStatusTitle = DictionaryResource.GetString("Parsing");
            downloading.DownloadContent = string.Empty;
            // 下载大小
            downloading.DownloadingFileSize = string.Empty;
            downloading.Progress = 0;
            // 下载速度
            downloading.SpeedDisplay = string.Empty;

            if (downloading.PlayUrl != null && downloading.Downloading.DownloadStatus == DownloadStatus.NOT_STARTED)
            {
                // 设置下载状态
                downloading.Downloading.DownloadStatus = DownloadStatus.DOWNLOADING;

                return;
            }

            // 设置下载状态
            downloading.Downloading.DownloadStatus = DownloadStatus.DOWNLOADING;

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
        /// 执行任务
        /// </summary>
        protected async Task DoWork()
        {
            // 上次循环时正在下载的数量
            int lastDownloadingCount = 0;

            while (true)
            {
                int maxDownloading = SettingsManager.GetInstance().GetMaxCurrentDownloads();
                int downloadingCount = 0;

                try
                {
                    downloadingTasks.RemoveAll((m) => m.IsCompleted);
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
                            //这里需要立刻设置状态，否则如果SingleDownload没有及时执行，会重复创建任务
                            downloading.Downloading.DownloadStatus = DownloadStatus.DOWNLOADING;
                            downloadingTasks.Add(SingleDownload(downloading));
                            downloadingCount++;
                        }
                    }
                }
                catch (InvalidOperationException e)
                {
                    Core.Utils.Debugging.Console.PrintLine("Start DoWork()发生InvalidOperationException异常: {0}", e);
                    LogManager.Error("Start DoWork() InvalidOperationException", e);
                }
                catch (Exception e)
                {
                    Core.Utils.Debugging.Console.PrintLine("Start DoWork()发生异常: {0}", e);
                    LogManager.Error("Start DoWork()", e);
                }

                // 判断是否该结束线程，若为true，跳出while循环
                if (cancellationToken.IsCancellationRequested)
                {
                    Core.Utils.Debugging.Console.PrintLine("AriaDownloadService: 下载服务结束，跳出while循环");
                    LogManager.Debug(Tag, "下载服务结束");
                    break;
                }

                // 判断下载列表中的视频是否全部下载完成
                if (lastDownloadingCount > 0 && downloadingList.Count == 0 && downloadedList.Count > 0)
                {
                    AfterDownload();
                }
                lastDownloadingCount = downloadingList.Count;

                // 降低CPU占用
                await Task.Delay(500);
            }

            await Task.WhenAny(Task.WhenAll(downloadingTasks), Task.Delay(30000));
            foreach (Task tsk in downloadingTasks.FindAll((m) => !m.IsCompleted))
            {
                Core.Utils.Debugging.Console.PrintLine("AriaDownloadService: 任务结束超时");
                LogManager.Debug(Tag, "任务结束超时");
            }
        }

        /// <summary>
        /// 下载一个视频
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        private async Task SingleDownload(DownloadingItem downloading)
        {
            // 路径
            downloading.DownloadBase.FilePath = downloading.DownloadBase.FilePath.Replace("\\", "/");
            string[] temp = downloading.DownloadBase.FilePath.Split('/');
            //string path = downloading.DownloadBase.FilePath.Replace(temp[temp.Length - 1], "");
            string path = downloading.DownloadBase.FilePath.TrimEnd(temp[temp.Length - 1].ToCharArray());

            // 路径不存在则创建
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            try
            {
                await Task.Run(new Action(() =>
                {
                    // 初始化
                    downloading.DownloadStatusTitle = string.Empty;
                    downloading.DownloadContent = string.Empty;
                    //downloading.Downloading.DownloadFiles.Clear();

                    // 解析并依次下载音频、视频、弹幕、字幕、封面等内容
                    Parse(downloading);

                    // 暂停
                    Pause(downloading);

                    string audioUid = null;
                    // 如果需要下载音频
                    if (downloading.DownloadBase.NeedDownloadContent["downloadAudio"])
                    {
                        //audioUid = DownloadAudio(downloading);
                        for (int i = 0; i < retry; i++)
                        {
                            audioUid = DownloadAudio(downloading);
                            if (audioUid != null && audioUid != nullMark)
                            {
                                break;
                            }
                        }
                    }
                    if (audioUid == nullMark)
                    {
                        DownloadFailed(downloading);
                        return;
                    }

                    // 暂停
                    Pause(downloading);

                    string videoUid = null;
                    // 如果需要下载视频
                    if (downloading.DownloadBase.NeedDownloadContent["downloadVideo"])
                    {
                        //videoUid = DownloadVideo(downloading);
                        for (int i = 0; i < retry; i++)
                        {
                            videoUid = DownloadVideo(downloading);
                            if (videoUid != null && videoUid != nullMark)
                            {
                                break;
                            }
                        }
                    }
                    if (videoUid == nullMark)
                    {
                        DownloadFailed(downloading);
                        return;
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
                    string outputPageCover = null;
                    // 如果需要下载封面
                    if (downloading.DownloadBase.NeedDownloadContent["downloadCover"])
                    {
                        string fileName = $"{downloading.DownloadBase.FilePath}.{GetImageExtension(downloading.DownloadBase.PageCoverUrl)}";

                        // page的封面
                        outputPageCover = DownloadCover(downloading, downloading.DownloadBase.PageCoverUrl, fileName);
                        // 封面
                        outputCover = DownloadCover(downloading, downloading.DownloadBase.CoverUrl, $"{path}/Cover.{GetImageExtension(downloading.DownloadBase.CoverUrl)}");
                    }

                    // 暂停
                    Pause(downloading);

                    // 混流
                    string outputMedia = string.Empty;
                    if (downloading.DownloadBase.NeedDownloadContent["downloadAudio"] || downloading.DownloadBase.NeedDownloadContent["downloadVideo"])
                    {
                        outputMedia = MixedFlow(downloading, audioUid, videoUid);
                    }

                    // 这里本来只有IsExist，没有pause，不知道怎么处理
                    // 是否存在
                    //isExist = IsExist(downloading);
                    //if (!isExist.Result)
                    //{
                    //    return;
                    //}

                    // 检测音频、视频是否下载成功
                    bool isMediaSuccess = true;
                    if (downloading.DownloadBase.NeedDownloadContent["downloadAudio"] || downloading.DownloadBase.NeedDownloadContent["downloadVideo"])
                    {
                        // 只有下载音频不下载视频时才输出aac
                        // 只要下载视频就输出mp4
                        if (File.Exists(outputMedia))
                        {
                            // 成功
                            isMediaSuccess = true;
                        }
                        else
                        {
                            isMediaSuccess = false;
                        }
                    }

                    // 检测弹幕是否下载成功
                    bool isDanmakuSuccess = true;
                    if (downloading.DownloadBase.NeedDownloadContent["downloadDanmaku"])
                    {
                        if (File.Exists(outputDanmaku))
                        {
                            // 成功
                            isDanmakuSuccess = true;
                        }
                        else
                        {
                            isDanmakuSuccess = false;
                        }
                    }

                    // 检测字幕是否下载成功
                    bool isSubtitleSuccess = true;
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
                                if (!File.Exists(subtitle))
                                {
                                    // 如果有一个不存在则失败
                                    isSubtitleSuccess = false;
                                }
                            }
                        }
                    }

                    // 检测封面是否下载成功
                    bool isCover = true;
                    if (downloading.DownloadBase.NeedDownloadContent["downloadCover"])
                    {
                        if (File.Exists(outputCover) || File.Exists(outputPageCover))
                        {
                            // 成功
                            isCover = true;
                        }
                        else
                        {
                            isCover = false;
                        }
                    }

                    if (!isMediaSuccess || !isDanmakuSuccess || !isSubtitleSuccess || !isCover)
                    {
                        DownloadFailed(downloading);
                        return;
                    }

                    // 下载完成后处理
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
                        downloadedList.Add(downloadedItem);
                        downloadingList.Remove(downloading);

                        // 下载完成列表排序
                        DownloadFinishedSort finishedSort = SettingsManager.GetInstance().GetDownloadFinishedSort();
                        App.SortDownloadedList(finishedSort);
                    }));
                }));
            }
            catch (OperationCanceledException e)
            {
                Core.Utils.Debugging.Console.PrintLine(Tag, e.ToString());
                LogManager.Debug(Tag, e.Message);
            }
        }

        /// <summary>
        /// 下载失败后的处理
        /// </summary>
        /// <param name="downloading"></param>
        protected void DownloadFailed(DownloadingItem downloading)
        {
            downloading.DownloadStatusTitle = DictionaryResource.GetString("DownloadFailed");
            downloading.DownloadContent = string.Empty;
            downloading.DownloadingFileSize = string.Empty;
            downloading.SpeedDisplay = string.Empty;
            downloading.Progress = 0;

            downloading.Downloading.DownloadStatus = DownloadStatus.DOWNLOAD_FAILED;
            downloading.StartOrPause = ButtonIcon.Instance().Retry;
            downloading.StartOrPause.Fill = DictionaryResource.GetColor("ColorPrimary");
        }

        /// <summary>
        /// 获取图片的扩展名
        /// </summary>
        /// <param name="coverUrl"></param>
        /// <returns></returns>
        protected string GetImageExtension(string coverUrl)
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
        /// 下载完成后的操作
        /// </summary>
        protected void AfterDownload()
        {
            AfterDownloadOperation operation = SettingsManager.GetInstance().GetAfterDownloadOperation();
            switch (operation)
            {
                case AfterDownloadOperation.NONE:
                    // 没有操作
                    break;
                case AfterDownloadOperation.OPEN_FOLDER:
                    // 打开文件夹
                    break;
                case AfterDownloadOperation.CLOSE_APP:
                    // 关闭程序
                    App.PropertyChangeAsync(() =>
                    {
                        System.Windows.Application.Current.Shutdown();
                    });
                    break;
                case AfterDownloadOperation.CLOSE_SYSTEM:
                    // 关机
                    System.Diagnostics.Process.Start("shutdown.exe", "-s");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 停止基本下载服务(转换await和Task.Wait两种调用形式)
        /// </summary>
        protected async Task BaseEndTask()
        {
            // 结束任务
            tokenSource.Cancel();

            await workTask;

            //先简单等待一下

            // 下载数据存储服务
            DownloadStorageService downloadStorageService = new DownloadStorageService();
            // 保存数据
            foreach (DownloadingItem item in downloadingList)
            {
                switch (item.Downloading.DownloadStatus)
                {
                    case DownloadStatus.NOT_STARTED:
                        break;
                    case DownloadStatus.WAIT_FOR_DOWNLOAD:
                        break;
                    case DownloadStatus.PAUSE_STARTED:
                        break;
                    case DownloadStatus.PAUSE:
                        break;
                    case DownloadStatus.DOWNLOADING:
                        // TODO 添加设置让用户选择重启后是否自动开始下载
                        item.Downloading.DownloadStatus = DownloadStatus.WAIT_FOR_DOWNLOAD;
                        //item.Downloading.DownloadStatus = DownloadStatus.PAUSE;
                        break;
                    case DownloadStatus.DOWNLOAD_SUCCEED:
                    case DownloadStatus.DOWNLOAD_FAILED:
                        break;
                    default:
                        break;
                }

                item.Progress = 0;

                downloadStorageService.UpdateDownloading(item);
            }
            foreach (DownloadedItem item in downloadedList)
            {
                downloadStorageService.UpdateDownloaded(item);
            }
        }

        /// <summary>
        /// 启动基本下载服务
        /// </summary>
        protected void BaseStart()
        {
            tokenSource = new CancellationTokenSource();
            cancellationToken = tokenSource.Token;
            workTask = Task.Run(DoWork);
        }

        #region 抽象接口函数
        public abstract void Parse(DownloadingItem downloading);
        public abstract string DownloadAudio(DownloadingItem downloading);
        public abstract string DownloadVideo(DownloadingItem downloading);
        public abstract string DownloadDanmaku(DownloadingItem downloading);
        public abstract List<string> DownloadSubtitle(DownloadingItem downloading);
        public abstract string DownloadCover(DownloadingItem downloading, string coverUrl, string fileName);
        public abstract string MixedFlow(DownloadingItem downloading, string audioUid, string videoUid);

        protected abstract void Pause(DownloadingItem downloading);
        #endregion
    }
}
