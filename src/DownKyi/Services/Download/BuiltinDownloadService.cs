using DownKyi.Core.BiliApi.Login;
using DownKyi.Core.BiliApi.VideoStream.Models;
using DownKyi.Core.Downloader;
using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using DownKyi.Core.Utils;
using DownKyi.Models;
using DownKyi.Utils;
using DownKyi.ViewModels.DownloadManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace DownKyi.Services.Download
{
    public class BuiltinDownloadService : DownloadService, IDownloadService
    {
        public BuiltinDownloadService(ObservableCollection<DownloadingItem> downloadingList, ObservableCollection<DownloadedItem> downloadedList) : base(downloadingList, downloadedList)
        {
            Tag = "BuiltinDownloadService";
        }

        #region 音视频

        /// <summary>
        /// 下载音频，返回下载文件路径
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        public override string DownloadAudio(DownloadingItem downloading)
        {
            PlayUrlDashVideo downloadAudio = BaseDownloadAudio(downloading);

            return DownloadVideo(downloading, downloadAudio);
        }

        /// <summary>
        /// 下载视频，返回下载文件路径
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        public override string DownloadVideo(DownloadingItem downloading)
        {
            PlayUrlDashVideo downloadVideo = BaseDownloadVideo(downloading);

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
            downloading.DownloadBase.FilePath = downloading.DownloadBase.FilePath.Replace("\\", "/");
            string[] temp = downloading.DownloadBase.FilePath.Split('/');
            //string path = downloading.DownloadBase.FilePath.Replace(temp[temp.Length - 1], "");
            string path = downloading.DownloadBase.FilePath.TrimEnd(temp[temp.Length - 1].ToCharArray());

            // 下载文件名
            string fileName = Guid.NewGuid().ToString("N");
            string key = $"{downloadVideo.Id}_{downloadVideo.Codecs}";

            // 老版本数据库没有这一项，会变成null
            if (downloading.Downloading.DownloadedFiles == null)
            {
                downloading.Downloading.DownloadedFiles = new List<string>();
            }

            if (downloading.Downloading.DownloadFiles.ContainsKey(key))
            {
                // 如果存在，表示下载过，
                // 则继续使用上次下载的文件名
                fileName = downloading.Downloading.DownloadFiles[key];

                // 还要检查一下文件有没有被人删掉，删掉的话重新下载
                // 如果下载视频之后音频文件被人删了。此时gid还是视频的，会下错文件
                if (downloading.Downloading.DownloadedFiles.Contains(key) && File.Exists(Path.Combine(path, fileName)))
                {
                    return Path.Combine(path, fileName);
                }
            }
            else
            {
                // 记录本次下载的文件
                try
                {
                    downloading.Downloading.DownloadFiles.Add(key, fileName);
                }
                catch (ArgumentException) { }
                // Gid最好能是每个文件单独存储，现在复用有可能会混
                // 不过好消息是下载是按固定顺序的，而且下载了两个音频会混流不过
                downloading.Downloading.Gid = null;
            }

            // 启用https
            AllowStatus useSSL = SettingsManager.GetInstance().UseSSL();
            if (useSSL == AllowStatus.YES)
            {
                for (int i = 0; i < urls.Count; i++)
                {
                    string url = urls[i];
                    if (url.StartsWith("http://"))
                    {
                        urls[i] = url.Replace("http://", "https://");
                    }
                }
            }
            else
            {
                for (int i = 0; i < urls.Count; i++)
                {
                    string url = urls[i];
                    if (url.StartsWith("https://"))
                    {
                        urls[i] = url.Replace("https://", "http://");
                    }
                }
            }

            // 开始下载
            try
            {
                var downloadStatus = DownloadByBuiltin(downloading, urls, path, fileName);
                if (downloadStatus)
                {
                    downloading.Downloading.DownloadedFiles.Add(key);
                    downloading.Downloading.Gid = null;
                    return Path.Combine(path, fileName);
                }
                else
                {
                    return nullMark;
                }
            }
            catch (FileNotFoundException e)
            {
                Core.Utils.Debugging.Console.PrintLine("BuiltinDownloadService.DownloadVideo()发生异常: {0}", e);
                LogManager.Error("BuiltinDownloadService.DownloadVideo()", e);

                return nullMark;
            }
        }

        #endregion

        /// <summary>
        /// 下载封面
        /// </summary>
        /// <param name="downloading"></param>
        /// <param name="coverUrl"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public override string DownloadCover(DownloadingItem downloading, string coverUrl, string fileName)
        {
            return BaseDownloadCover(downloading, coverUrl, fileName);
        }

        /// <summary>
        /// 下载弹幕
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        public override string DownloadDanmaku(DownloadingItem downloading)
        {
            return BaseDownloadDanmaku(downloading);
        }

        /// <summary>
        /// 下载字幕
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        public override List<string> DownloadSubtitle(DownloadingItem downloading)
        {
            return BaseDownloadSubtitle(downloading);
        }

        /// <summary>
        /// 混流音频和视频
        /// </summary>
        /// <param name="downloading"></param>
        /// <param name="audioUid"></param>
        /// <param name="videoUid"></param>
        /// <returns></returns>
        public override string MixedFlow(DownloadingItem downloading, string audioUid, string videoUid)
        {
            return BaseMixedFlow(downloading, audioUid, videoUid);
        }

        /// <summary>
        /// 解析视频流的下载链接
        /// </summary>
        /// <param name="downloading"></param>
        public override void Parse(DownloadingItem downloading)
        {
            BaseParse(downloading);
        }

        /// <summary>
        /// 停止下载服务(转换await和Task.Wait两种调用形式)
        /// </summary>
        private async Task EndTask()
        {
            // 停止基本任务
            await BaseEndTask();
        }

        /// <summary>
        /// 停止下载服务
        /// </summary>
        public void End()
        {
            Task.Run(EndTask).Wait();
        }

        public void Start()
        {
            // 启动基本服务
            BaseStart();
        }

        /// <summary>
        /// 强制暂停
        /// </summary>
        /// <param name="downloading"></param>
        /// <exception cref="OperationCanceledException"></exception>
        protected override void Pause(DownloadingItem downloading)
        {
            cancellationToken.ThrowIfCancellationRequested();

            downloading.DownloadStatusTitle = DictionaryResource.GetString("Pausing");
            if (downloading.Downloading.DownloadStatus == DownloadStatus.PAUSE)
            {
                throw new OperationCanceledException("Stop thread by pause");
            }
            // 是否存在
            var isExist = IsExist(downloading);
            if (!isExist)
            {
                throw new OperationCanceledException("Task is deleted");
            }
        }

        /// <summary>
        /// 是否存在于下载列表中
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        private bool IsExist(DownloadingItem downloading)
        {
            bool isExist = downloadingList.Contains(downloading);
            if (isExist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 内建下载器

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="downloading"></param>
        /// <param name="urls"></param>
        /// <param name="path"></param>
        /// <param name="localFileName"></param>
        /// <returns></returns>
        private bool DownloadByBuiltin(DownloadingItem downloading, List<string> urls, string path, string localFileName)
        {
            // path已斜杠结尾，去掉斜杠
            path = path.TrimEnd('/').TrimEnd('\\');

            foreach (var url in urls)
            {
                // 创建下载器
                var mtd = new MultiThreadDownloader(url,
                    Environment.GetEnvironmentVariable("temp"),
                    Path.Combine(path, localFileName),
                    SettingsManager.GetInstance().GetSplit());
                // 配置网络请求
                mtd.Configure(req =>
                {
                    req.CookieContainer = LoginHelper.GetLoginInfoCookies();
                    req.UserAgent = SettingsManager.GetInstance().GetUserAgent();
                    req.Referer = "https://www.bilibili.com";
                    req.Headers.Add("Origin", "https://www.bilibili.com");

                    if (SettingsManager.GetInstance().IsHttpProxy() == AllowStatus.YES)
                    {
                        req.Proxy = new WebProxy(SettingsManager.GetInstance().GetHttpProxy(),
                            SettingsManager.GetInstance().GetHttpProxyListenPort());
                    }
                });

                // 下载进度回调
                mtd.TotalProgressChanged += (sender, e) =>
                {
                    try
                    {
                        // 状态更新
                        var downloader = sender as MultiThreadDownloader;

                        // 下载进度百分比
                        float percent = downloader.TotalProgress;

                        // 根据进度判断本次是否需要更新UI
                        if (Math.Abs(percent - downloading.Progress) < 0.01) { return; }
                        if (Math.Abs(percent - downloading.Progress) > 5) { return; }

                        // 下载进度
                        downloading.Progress = percent;

                        // 下载大小
                        downloading.DownloadingFileSize = Format.FormatFileSize(downloader.TotalBytesReceived) + "/" + Format.FormatFileSize(downloader.Size);

                        // 下载速度
                        long speed = (long)downloader.TotalSpeedInBytes;

                        // 下载速度显示
                        downloading.SpeedDisplay = Format.FormatSpeed(speed);

                        // 最大下载速度
                        if (downloading.Downloading.MaxSpeed < speed)
                        {
                            downloading.Downloading.MaxSpeed = speed;
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        Core.Utils.Debugging.Console.PrintLine($"{Tag}.DownloadByBuiltin()发生InvalidOperationException异常: {0}", ex);
                        LogManager.Error($"{Tag}.DownloadByBuiltin()", ex);
                    }
                    catch (Exception ex)
                    {
                        Core.Utils.Debugging.Console.PrintLine($"{Tag}.DownloadByBuiltin()发生异常: {0}", ex);
                        LogManager.Error($"{Tag}.DownloadByBuiltin()", ex);
                    }
                };

                // 文件合并完成回调
                bool isComplete = false;
                mtd.FileMergedComplete += (sender, e) =>
                {
                    // 跳出循环
                    if (File.Exists(Path.Combine(path, localFileName))) { isComplete = true; }
                };

                // 开始下载
                mtd.Start();

                // 阻塞当前任务，监听暂停事件
                while (!isComplete)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    switch (downloading.Downloading.DownloadStatus)
                    {
                        case DownloadStatus.PAUSE:
                            // 暂停下载
                            mtd.Pause();
                            // 通知UI，并阻塞当前线程
                            Pause(downloading);
                            break;
                        case DownloadStatus.DOWNLOADING:
                            break;
                    }

                    Thread.Sleep(100);
                }
                return isComplete;
            }

            return false;
        }

        #endregion

    }
}
