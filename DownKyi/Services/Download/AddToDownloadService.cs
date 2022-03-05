using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.VideoStream;
using DownKyi.Core.BiliApi.Zone;
using DownKyi.Core.FileName;
using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using DownKyi.Core.Utils;
using DownKyi.Events;
using DownKyi.Models;
using DownKyi.Utils;
using DownKyi.ViewModels.Dialogs;
using DownKyi.ViewModels.DownloadManager;
using DownKyi.ViewModels.PageViewModels;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace DownKyi.Services.Download
{
    /// <summary>
    /// 添加到下载列表服务
    /// </summary>
    public class AddToDownloadService
    {
        private readonly string Tag = "AddToDownloadService";
        private IInfoService videoInfoService;
        private VideoInfoView videoInfoView;
        private List<VideoSection> videoSections;

        // 下载内容
        private bool downloadAudio = true;
        private bool downloadVideo = true;
        private bool downloadDanmaku = true;
        private bool downloadSubtitle = true;
        private bool downloadCover = true;

        /// <summary>
        /// 添加下载
        /// </summary>
        /// <param name="streamType"></param>
        public AddToDownloadService(PlayStreamType streamType)
        {
            switch (streamType)
            {
                case PlayStreamType.VIDEO:
                    videoInfoService = new VideoInfoService(null);
                    break;
                case PlayStreamType.BANGUMI:
                    videoInfoService = new BangumiInfoService(null);
                    break;
                case PlayStreamType.CHEESE:
                    videoInfoService = new CheeseInfoService(null);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 添加下载
        /// </summary>
        /// <param name="id"></param>
        /// <param name="streamType"></param>
        public AddToDownloadService(string id, PlayStreamType streamType)
        {
            switch (streamType)
            {
                case PlayStreamType.VIDEO:
                    videoInfoService = new VideoInfoService(id);
                    break;
                case PlayStreamType.BANGUMI:
                    videoInfoService = new BangumiInfoService(id);
                    break;
                case PlayStreamType.CHEESE:
                    videoInfoService = new CheeseInfoService(id);
                    break;
                default:
                    break;
            }
        }

        public void SetVideoInfoService(IInfoService videoInfoService)
        {
            this.videoInfoService = videoInfoService;
        }

        public void GetVideo(VideoInfoView videoInfoView, List<VideoSection> videoSections)
        {
            this.videoInfoView = videoInfoView;
            this.videoSections = videoSections;
        }

        public void GetVideo()
        {
            videoInfoView = videoInfoService.GetVideoView();
            if (videoInfoView == null)
            {
                LogManager.Debug(Tag, "VideoInfoView is null.");
                return;
            }

            videoSections = videoInfoService.GetVideoSections();
            if (videoSections == null)
            {
                LogManager.Debug(Tag, "videoSections is not exist.");

                videoSections = new List<VideoSection>
                {
                    new VideoSection
                    {
                        Id = 0,
                        Title = "default",
                        IsSelected = true,
                        VideoPages = videoInfoService.GetVideoPages()
                    }
                };
            }

            // 将所有视频设置为选中
            foreach (VideoSection section in videoSections)
            {
                foreach (var item in section.VideoPages)
                {
                    item.IsSelected = true;
                }
            }
        }

        /// <summary>
        /// 解析视频流
        /// </summary>
        /// <param name="videoInfoService"></param>
        public void ParseVideo(IInfoService videoInfoService)
        {
            if (videoSections == null) { return; }

            foreach (VideoSection section in videoSections)
            {
                foreach (VideoPage page in section.VideoPages)
                {
                    // 执行解析任务
                    videoInfoService.GetVideoStream(page);
                }
            }
        }

        /// <summary>
        /// 选择文件夹和下载项
        /// </summary>
        /// <param name="dialogService"></param>
        public string SetDirectory(IDialogService dialogService)
        {
            // 选择的下载文件夹
            string directory = string.Empty;

            // 是否使用默认下载目录
            if (SettingsManager.GetInstance().IsUseSaveVideoRootPath() == AllowStatus.YES)
            {
                directory = SettingsManager.GetInstance().GetSaveVideoRootPath();
            }
            else
            {
                // 打开文件夹选择器
                dialogService.ShowDialog(ViewDownloadSetterViewModel.Tag, null, result =>
                {
                    if (result.Result == ButtonResult.OK)
                    {
                        // 选择的下载文件夹
                        directory = result.Parameters.GetValue<string>("directory");

                        // 下载内容
                        downloadAudio = result.Parameters.GetValue<bool>("downloadAudio");
                        downloadVideo = result.Parameters.GetValue<bool>("downloadVideo");
                        downloadDanmaku = result.Parameters.GetValue<bool>("downloadDanmaku");
                        downloadSubtitle = result.Parameters.GetValue<bool>("downloadSubtitle");
                        downloadCover = result.Parameters.GetValue<bool>("downloadCover");
                    }
                });
            }

            // 下载设置dialog中如果点击取消或者关闭窗口，
            // 会返回空字符串，
            // 这时直接退出
            if (directory == string.Empty) { return null; }

            // 文件夹不存在则创建
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }

        public int AddToDownload(IEventAggregator eventAggregator, string directory)
        {
            if (directory == null || directory == string.Empty) { return -1; }
            if (videoSections == null) { return -1; }

            // 视频计数
            int i = 0;

            // 添加到下载
            foreach (VideoSection section in videoSections)
            {
                foreach (VideoPage page in section.VideoPages)
                {
                    // 只下载选中项，跳过未选中项
                    if (!page.IsSelected) { continue; }

                    // 没有解析的也跳过
                    if (page.PlayUrl == null) { continue; }

                    // 判断VideoQuality
                    int retry = 0;
                    while (page.VideoQuality == null && retry < 5)
                    {
                        // 执行解析任务
                        videoInfoService.GetVideoStream(page);
                        retry++;
                    }
                    if (page.VideoQuality == null) { continue; }

                    // 判断是否同一个视频，需要cid、画质、音质、视频编码都相同

                    // 如果存在正在下载列表，则跳过，并提示
                    bool isDownloading = false;
                    foreach (DownloadingItem item in App.DownloadingList)
                    {
                        if (item.DownloadBase == null) { continue; }

                        if (item.DownloadBase.Cid == page.Cid && item.Resolution.Id == page.VideoQuality.Quality && item.AudioCodec.Name == page.AudioQualityFormat && item.VideoCodecName == page.VideoQuality.SelectedVideoCodec)
                        {
                            eventAggregator.GetEvent<MessageEvent>().Publish($"{page.Name}{DictionaryResource.GetString("TipAlreadyToAddDownloading")}");
                            isDownloading = true;
                            break;
                        }
                    }
                    if (isDownloading) { continue; }

                    // 如果存在下载完成列表，弹出选择框是否再次下载
                    bool isDownloaded = false;
                    foreach (DownloadedItem item in App.DownloadedList)
                    {
                        if (item.DownloadBase == null) { continue; }

                        if (item.DownloadBase.Cid == page.Cid && item.Resolution.Id == page.VideoQuality.Quality && item.AudioCodec.Name == page.AudioQualityFormat && item.VideoCodecName == page.VideoQuality.SelectedVideoCodec)
                        {
                            eventAggregator.GetEvent<MessageEvent>().Publish($"{page.Name}{DictionaryResource.GetString("TipAlreadyToAddDownloaded")}");
                            isDownloaded = true;
                            break;
                        }
                    }
                    if (isDownloaded) { continue; }

                    // 视频分区
                    int zoneId = -1;
                    List<ZoneAttr> zoneList = VideoZone.Instance().GetZones();
                    ZoneAttr zone = zoneList.Find(it => it.Id == videoInfoView.TypeId);
                    if (zone != null)
                    {
                        if (zone.ParentId == 0)
                        {
                            zoneId = zone.Id;
                        }
                        else
                        {
                            ZoneAttr zoneParent = zoneList.Find(it => it.Id == zone.ParentId);
                            if (zoneParent != null)
                            {
                                zoneId = zoneParent.Id;
                            }
                        }
                    }

                    // 如果只有一个视频章节，则不在命名中出现
                    string sectionName = string.Empty;
                    if (videoSections.Count > 1)
                    {
                        sectionName = section.Title;
                    }

                    // 文件路径
                    List<FileNamePart> fileNameParts = SettingsManager.GetInstance().GetFileNameParts();
                    FileName fileName = FileName.Builder(fileNameParts)
                        .SetOrder(page.Order)
                        .SetSection(Format.FormatFileName(sectionName))
                        .SetMainTitle(Format.FormatFileName(videoInfoView.Title))
                        .SetPageTitle(Format.FormatFileName(page.Name))
                        .SetVideoZone(videoInfoView.VideoZone.Split('>')[0])
                        .SetAudioQuality(page.AudioQualityFormat)
                        .SetVideoQuality(page.VideoQuality == null ? "" : page.VideoQuality.QualityFormat)
                        .SetVideoCodec(page.VideoQuality == null ? "" : page.VideoQuality.SelectedVideoCodec.Contains("AVC") ? "AVC" : page.VideoQuality.SelectedVideoCodec.Contains("HEVC") ? "HEVC" : page.VideoQuality.SelectedVideoCodec.Contains("Dolby") ? "Dolby Vision" : "")
                        .SetAvid(page.Avid)
                        .SetBvid(page.Bvid)
                        .SetCid(page.Cid)
                        .SetUpMid(page.Owner.Mid)
                        .SetUpName(page.Owner.Name);
                    string filePath = Path.Combine(directory, fileName.RelativePath());

                    // 视频类别
                    PlayStreamType playStreamType;
                    switch (videoInfoView.TypeId)
                    {
                        case -10:
                            playStreamType = PlayStreamType.CHEESE;
                            break;
                        case 13:
                        case 23:
                        case 177:
                        case 167:
                        case 11:
                            playStreamType = PlayStreamType.BANGUMI;
                            break;
                        case 1:
                        case 3:
                        case 129:
                        case 4:
                        case 36:
                        case 188:
                        case 234:
                        case 223:
                        case 160:
                        case 211:
                        case 217:
                        case 119:
                        case 155:
                        case 202:
                        case 5:
                        case 181:
                        default:
                            playStreamType = PlayStreamType.VIDEO;
                            break;
                    }

                    // 如果不存在，直接添加到下载列表
                    DownloadBase downloadBase = new DownloadBase
                    {
                        Bvid = page.Bvid,
                        Avid = page.Avid,
                        Cid = page.Cid,
                        EpisodeId = page.EpisodeId,
                        CoverUrl = videoInfoView.CoverUrl,
                        PageCoverUrl = page.FirstFrame,
                        ZoneId = zoneId,
                        FilePath = filePath,

                        Order = page.Order,
                        MainTitle = videoInfoView.Title,
                        Name = page.Name,
                        Duration = page.Duration,
                        VideoCodecName = page.VideoQuality.SelectedVideoCodec,
                        Resolution = new Quality { Name = page.VideoQuality.QualityFormat, Id = page.VideoQuality.Quality },
                        AudioCodec = Constant.GetAudioQualities().FirstOrDefault(t => { return t.Name == page.AudioQualityFormat; }),
                    };
                    Downloading downloading = new Downloading
                    {
                        PlayStreamType = playStreamType,
                        DownloadStatus = DownloadStatus.NOT_STARTED,
                    };

                    // 需要下载的内容
                    downloadBase.NeedDownloadContent["downloadAudio"] = downloadAudio;
                    downloadBase.NeedDownloadContent["downloadVideo"] = downloadVideo;
                    downloadBase.NeedDownloadContent["downloadDanmaku"] = downloadDanmaku;
                    downloadBase.NeedDownloadContent["downloadSubtitle"] = downloadSubtitle;
                    downloadBase.NeedDownloadContent["downloadCover"] = downloadCover;

                    DownloadingItem downloadingItem = new DownloadingItem
                    {
                        DownloadBase = downloadBase,
                        Downloading = downloading,
                        PlayUrl = page.PlayUrl,
                    };

                    // 添加到下载列表
                    App.PropertyChangeAsync(new Action(() =>
                    {
                        App.DownloadingList.Add(downloadingItem);
                        Thread.Sleep(10);
                    }));
                    i++;
                }
            }

            return i;
        }

    }
}