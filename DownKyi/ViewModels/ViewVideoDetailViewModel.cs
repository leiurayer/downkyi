using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.VideoStream;
using DownKyi.Core.BiliApi.Zone;
using DownKyi.Core.FileName;
using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using DownKyi.Core.Utils;
using DownKyi.CustomControl;
using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Models;
using DownKyi.Services;
using DownKyi.Utils;
using DownKyi.ViewModels.Dialogs;
using DownKyi.ViewModels.DownloadManager;
using DownKyi.ViewModels.PageViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DownKyi.ViewModels
{
    public class ViewVideoDetailViewModel : BaseViewModel
    {
        public const string Tag = "PageVideoDetail";

        private readonly IDialogService dialogService;

        #region 页面属性申明

        private VectorImage arrowBack;
        public VectorImage ArrowBack
        {
            get => arrowBack;
            set => SetProperty(ref arrowBack, value);
        }

        private string inputText;
        public string InputText
        {
            get => inputText;
            set => SetProperty(ref inputText, value);
        }

        private GifImage loading;
        public GifImage Loading
        {
            get => loading;
            set => SetProperty(ref loading, value);
        }

        private Visibility loadingVisibility;
        public Visibility LoadingVisibility
        {
            get => loadingVisibility;
            set => SetProperty(ref loadingVisibility, value);
        }

        private VectorImage downloadManage;
        public VectorImage DownloadManage
        {
            get => downloadManage;
            set => SetProperty(ref downloadManage, value);
        }

        private VideoInfoView videoInfoView;
        public VideoInfoView VideoInfoView
        {
            get => videoInfoView;
            set => SetProperty(ref videoInfoView, value);
        }

        private ObservableCollection<VideoSection> videoSections;
        public ObservableCollection<VideoSection> VideoSections
        {
            get => videoSections;
            set => SetProperty(ref videoSections, value);
        }

        private bool isSelectAll;
        public bool IsSelectAll
        {
            get => isSelectAll;
            set => SetProperty(ref isSelectAll, value);
        }

        private Visibility contentVisibility;
        public Visibility ContentVisibility
        {
            get => contentVisibility;
            set => SetProperty(ref contentVisibility, value);
        }

        private Visibility noDataVisibility;
        public Visibility NoDataVisibility
        {
            get => noDataVisibility;
            set => SetProperty(ref noDataVisibility, value);
        }

        #endregion

        public ViewVideoDetailViewModel(IEventAggregator eventAggregator, IDialogService dialogService) : base(eventAggregator)
        {
            this.dialogService = dialogService;

            #region 属性初始化

            // 初始化loading gif
            Loading = new GifImage(Properties.Resources.loading);
            Loading.StartAnimate();
            LoadingVisibility = Visibility.Collapsed;

            ArrowBack = NavigationIcon.Instance().ArrowBack;
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            DownloadManage = ButtonIcon.Instance().DownloadManage;
            DownloadManage.Height = 24;
            DownloadManage.Width = 24;
            DownloadManage.Fill = DictionaryResource.GetColor("ColorPrimary");

            VideoSections = new ObservableCollection<VideoSection>();

            #endregion
        }


        #region 命令申明

        // 返回
        private DelegateCommand backSpaceCommand;
        public DelegateCommand BackSpaceCommand => backSpaceCommand ?? (backSpaceCommand = new DelegateCommand(ExecuteBackSpace));

        /// <summary>
        /// 返回
        /// </summary>
        private void ExecuteBackSpace()
        {
            NavigationParam parameter = new NavigationParam
            {
                ViewName = ParentView,
                ParentViewName = null,
                Parameter = null
            };
            eventAggregator.GetEvent<NavigationEvent>().Publish(parameter);
        }

        // 前往下载管理页面
        private DelegateCommand downloadManagerCommand;
        public DelegateCommand DownloadManagerCommand => downloadManagerCommand ?? (downloadManagerCommand = new DelegateCommand(ExecuteDownloadManagerCommand));

        /// <summary>
        /// 前往下载管理页面
        /// </summary>
        private void ExecuteDownloadManagerCommand()
        {
            NavigationParam parameter = new NavigationParam
            {
                ViewName = ViewDownloadManagerViewModel.Tag,
                ParentViewName = Tag,
                Parameter = null
            };
            eventAggregator.GetEvent<NavigationEvent>().Publish(parameter);
        }

        // 输入确认事件
        private DelegateCommand inputCommand;
        public DelegateCommand InputCommand => inputCommand ?? (inputCommand = new DelegateCommand(ExecuteInputCommand, CanExecuteInputCommand));

        /// <summary>
        /// 处理输入事件
        /// </summary>
        private async void ExecuteInputCommand()
        {
            InitView();
            try
            {
                await Task.Run(() =>
                {
                    if (InputText == null || InputText == string.Empty) { return; }

                    LogManager.Debug(Tag, $"InputText: {InputText}");

                    // 更新页面
                    UnityUpdateView(UpdateView, InputText, null);

                    // 是否自动解析视频
                    if (SettingsManager.GetInstance().IsAutoParseVideo() == AllowStatus.YES)
                    {
                        PropertyChangeAsync(ExecuteParseAllVideoCommand);
                    }
                });
            }
            catch (Exception e)
            {
                Core.Utils.Debugging.Console.PrintLine("InputCommand()发生异常: {0}", e);
                LogManager.Error(Tag, e);

                LoadingVisibility = Visibility.Collapsed;
                ContentVisibility = Visibility.Collapsed;
                NoDataVisibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// 输入事件是否允许执行
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteInputCommand()
        {
            return LoadingVisibility != Visibility.Visible;
        }

        // 复制封面事件
        private DelegateCommand copyCoverCommand;
        public DelegateCommand CopyCoverCommand => copyCoverCommand ?? (copyCoverCommand = new DelegateCommand(ExecuteCopyCoverCommand));

        /// <summary>
        /// 复制封面事件
        /// </summary>
        private void ExecuteCopyCoverCommand()
        {
            // 复制封面图片到剪贴板
            Clipboard.SetImage(VideoInfoView.Cover);
            LogManager.Info(Tag, "复制封面图片到剪贴板");
        }

        // 复制封面URL事件
        private DelegateCommand copyCoverUrlCommand;
        public DelegateCommand CopyCoverUrlCommand => copyCoverUrlCommand ?? (copyCoverUrlCommand = new DelegateCommand(ExecuteCopyCoverUrlCommand));

        /// <summary>
        /// 复制封面URL事件
        /// </summary>
        private void ExecuteCopyCoverUrlCommand()
        {
            // 复制封面url到剪贴板
            Clipboard.SetText(VideoInfoView.CoverUrl);
            LogManager.Info(Tag, "复制封面url到剪贴板");
        }

        // 前往UP主页事件
        private DelegateCommand upperCommand;
        public DelegateCommand UpperCommand => upperCommand ?? (upperCommand = new DelegateCommand(ExecuteUpperCommand));

        /// <summary>
        /// 前往UP主页事件
        /// </summary>
        private void ExecuteUpperCommand()
        {
            NavigateToView.NavigateToViewUserSpace(eventAggregator, Tag, VideoInfoView.UpperMid);
        }

        // 视频章节选择事件
        private DelegateCommand<object> videoSectionsCommand;
        public DelegateCommand<object> VideoSectionsCommand => videoSectionsCommand ?? (videoSectionsCommand = new DelegateCommand<object>(ExecuteVideoSectionsCommand));

        /// <summary>
        /// 视频章节选择事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteVideoSectionsCommand(object parameter)
        {
            if (!(parameter is VideoSection section)) { return; }

            bool isSelectAll = true;
            foreach (VideoPage page in section.VideoPages)
            {
                if (!page.IsSelected)
                {
                    isSelectAll = false;
                    break;
                }
            }

            IsSelectAll = section.VideoPages.Count != 0 && isSelectAll;
        }

        // 视频page选择事件
        private DelegateCommand<object> videoPagesCommand;
        public DelegateCommand<object> VideoPagesCommand => videoPagesCommand ?? (videoPagesCommand = new DelegateCommand<object>(ExecuteVideoPagesCommand));

        /// <summary>
        /// 视频page选择事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteVideoPagesCommand(object parameter)
        {
            if (!(parameter is ObservableCollection<object> videoPages)) { return; }

            VideoSection section = VideoSections.FirstOrDefault(item => item.IsSelected);
            if (section == null) { return; }
            IsSelectAll = section.VideoPages.Count == videoPages.Count && section.VideoPages.Count != 0;
        }

        // Ctrl+A 全选事件
        private DelegateCommand<object> keySelectAllCommand;
        public DelegateCommand<object> KeySelectAllCommand => keySelectAllCommand ?? (keySelectAllCommand = new DelegateCommand<object>(ExecuteKeySelectAllCommand));

        /// <summary>
        /// Ctrl+A 全选事件
        /// </summary>
        private void ExecuteKeySelectAllCommand(object parameter)
        {
            if (!(parameter is VideoSection section)) { return; }
            foreach (VideoPage page in section.VideoPages)
            {
                page.IsSelected = true;
            }
        }

        // 全选事件
        private DelegateCommand<object> selectAllCommand;
        public DelegateCommand<object> SelectAllCommand => selectAllCommand ?? (selectAllCommand = new DelegateCommand<object>(ExecuteSelectAllCommand));

        /// <summary>
        /// 全选事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteSelectAllCommand(object parameter)
        {
            if (!(parameter is VideoSection section)) { return; }
            if (IsSelectAll)
            {
                foreach (VideoPage page in section.VideoPages)
                {
                    page.IsSelected = true;
                }
            }
            else
            {
                foreach (VideoPage page in section.VideoPages)
                {
                    page.IsSelected = false;
                }
            }
        }

        // 解析视频流事件
        private DelegateCommand<object> parseCommand;
        public DelegateCommand<object> ParseCommand => parseCommand ?? (parseCommand = new DelegateCommand<object>(ExecuteParseCommand, CanExecuteParseCommand));

        /// <summary>
        /// 解析视频流事件
        /// </summary>
        /// <param name="parameter"></param>
        private async void ExecuteParseCommand(object parameter)
        {
            if (!(parameter is VideoPage videoPage))
            {
                return;
            }

            LoadingVisibility = Visibility.Visible;

            try
            {
                await Task.Run(() =>
                {
                    LogManager.Debug(Tag, $"Video Page: {videoPage.Cid}");

                    UnityUpdateView(ParseVideo, null, videoPage);
                });
            }
            catch (Exception e)
            {
                Core.Utils.Debugging.Console.PrintLine("ParseCommand()发生异常: {0}", e);
                LogManager.Error(Tag, e);

                LoadingVisibility = Visibility.Collapsed;
            }

            LoadingVisibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 解析视频流事件是否允许执行
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteParseCommand(object parameter)
        {
            return LoadingVisibility != Visibility.Visible;
        }

        // 解析所有视频流事件
        private DelegateCommand parseAllVideoCommand;
        public DelegateCommand ParseAllVideoCommand => parseAllVideoCommand ?? (parseAllVideoCommand = new DelegateCommand(ExecuteParseAllVideoCommand, CanExecuteParseAllVideoCommand));

        /// <summary>
        /// 解析所有视频流事件
        /// </summary>
        private async void ExecuteParseAllVideoCommand()
        {
            LoadingVisibility = Visibility.Visible;

            // 解析范围
            ParseScope parseScope = SettingsManager.GetInstance().GetParseScope();

            // 是否选择了解析范围
            if (parseScope == ParseScope.NONE)
            {
                // 打开解析选择器
                dialogService.ShowDialog(ViewParsingSelectorViewModel.Tag, null, result =>
                {
                    if (result.Result == ButtonResult.OK)
                    {
                        // 选择的解析范围
                        parseScope = result.Parameters.GetValue<ParseScope>("parseScope");
                    }
                });
            }
            LogManager.Debug(Tag, $"ParseScope: {parseScope:G}");

            try
            {
                await Task.Run(() =>
                {
                    LogManager.Debug(Tag, "Parse video");

                    switch (parseScope)
                    {
                        case ParseScope.NONE:
                            break;
                        case ParseScope.SELECTED_ITEM:
                            foreach (VideoSection section in VideoSections)
                            {
                                foreach (VideoPage page in section.VideoPages)
                                {
                                    VideoPage videoPage = section.VideoPages.FirstOrDefault(t => t == page);

                                    if (videoPage.IsSelected)
                                    {
                                        // 执行解析任务
                                        UnityUpdateView(ParseVideo, null, videoPage);
                                    }
                                }
                            }
                            break;
                        case ParseScope.CURRENT_SECTION:
                            foreach (VideoSection section in VideoSections)
                            {
                                if (section.IsSelected)
                                {
                                    foreach (VideoPage page in section.VideoPages)
                                    {
                                        VideoPage videoPage = section.VideoPages.FirstOrDefault(t => t == page);

                                        // 执行解析任务
                                        UnityUpdateView(ParseVideo, null, videoPage);
                                    }
                                }
                            }
                            break;
                        case ParseScope.ALL:
                            foreach (VideoSection section in VideoSections)
                            {
                                foreach (VideoPage page in section.VideoPages)
                                {
                                    VideoPage videoPage = section.VideoPages.FirstOrDefault(t => t == page);

                                    // 执行解析任务
                                    UnityUpdateView(ParseVideo, null, videoPage);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                });
            }
            catch (Exception e)
            {
                Core.Utils.Debugging.Console.PrintLine("ParseCommand()发生异常: {0}", e);
                LogManager.Error(Tag, e);

                LoadingVisibility = Visibility.Collapsed;
            }

            LoadingVisibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 解析所有视频流事件是否允许执行
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteParseAllVideoCommand()
        {
            return LoadingVisibility != Visibility.Visible;
        }

        // 添加到下载列表事件
        private DelegateCommand addToDownloadCommand;
        public DelegateCommand AddToDownloadCommand => addToDownloadCommand ?? (addToDownloadCommand = new DelegateCommand(ExecuteAddToDownloadCommand, CanExecuteAddToDownloadCommand));

        /// <summary>
        /// 添加到下载列表事件
        /// </summary>
        private void ExecuteAddToDownloadCommand()
        {
            // 选择的下载文件夹
            string directory = string.Empty;

            // 下载内容
            bool downloadAudio = true;
            bool downloadVideo = true;
            bool downloadDanmaku = true;
            bool downloadSubtitle = true;
            bool downloadCover = true;

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
            if (directory == string.Empty) { return; }

            // 文件夹不存在则创建
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // 添加视频计数
            int i = 0;

            // 添加到下载
            foreach (VideoSection section in VideoSections)
            {
                foreach (VideoPage page in section.VideoPages)
                {
                    // 只下载选中项，跳过未选中项
                    if (!page.IsSelected) { continue; }

                    // 没有解析的也跳过
                    if (page.PlayUrl == null) { continue; }

                    // 判断是否同一个视频，需要cid、画质、音质、视频编码都相同

                    // 如果存在正在下载列表，则跳过，并提示
                    foreach (DownloadingItem item in App.DownloadingList)
                    {
                        if (item.DownloadBase.Cid == page.Cid && item.Resolution.Id == page.VideoQuality.Quality && item.AudioCodec.Name == page.AudioQualityFormat && item.VideoCodecName == page.VideoQuality.SelectedVideoCodec)
                        {
                            eventAggregator.GetEvent<MessageEvent>().Publish($"{page.Name}{DictionaryResource.GetString("TipAlreadyToAddDownloading")}");
                            continue;
                        }
                    }

                    // 如果存在下载完成列表，弹出选择框是否再次下载
                    foreach (DownloadedItem item in App.DownloadedList)
                    {
                        if (item.DownloadBase.Cid == page.Cid && item.Resolution.Id == page.VideoQuality.Quality && item.AudioCodec.Name == page.AudioQualityFormat && item.VideoCodecName == page.VideoQuality.SelectedVideoCodec)
                        {
                            eventAggregator.GetEvent<MessageEvent>().Publish($"{page.Name}{DictionaryResource.GetString("TipAlreadyToAddDownloaded")}");
                            continue;
                        }
                    }

                    // 视频分区
                    int zoneId = -1;
                    List<ZoneAttr> zoneList = VideoZone.Instance().GetZones();
                    ZoneAttr zone = zoneList.Find(it => it.Id == VideoInfoView.TypeId);
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
                    if (VideoSections.Count > 1)
                    {
                        sectionName = section.Title;
                    }

                    // 文件路径
                    List<FileNamePart> fileNameParts = SettingsManager.GetInstance().GetFileNameParts();
                    FileName fileName = FileName.Builder(fileNameParts)
                        .SetOrder(page.Order)
                        .SetSection(Format.FormatFileName(sectionName))
                        .SetMainTitle(Format.FormatFileName(VideoInfoView.Title))
                        .SetPageTitle(Format.FormatFileName(page.Name))
                        .SetVideoZone(VideoInfoView.VideoZone.Split('>')[0])
                        .SetAudioQuality(page.AudioQualityFormat)
                        .SetVideoQuality(page.VideoQuality.QualityFormat)
                        .SetVideoCodec(page.VideoQuality.SelectedVideoCodec.Contains("AVC") ? "AVC" : page.VideoQuality.SelectedVideoCodec.Contains("HEVC") ? "HEVC" : "");
                    string filePath = Path.Combine(directory, fileName.RelativePath());

                    // 视频类别
                    PlayStreamType playStreamType;
                    switch (VideoInfoView.TypeId)
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
                        CoverUrl = VideoInfoView.CoverUrl,
                        PageCoverUrl = page.FirstFrame,
                        ZoneId = zoneId,
                        FilePath = filePath,

                        Order = page.Order,
                        MainTitle = VideoInfoView.Title,
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
                        //ZoneImage = (DrawingImage)Application.Current.Resources[VideoZoneIcon.Instance().GetZoneImageKey(zoneId)],
                    };

                    // 添加到下载列表
                    App.DownloadingList.Add(downloadingItem);
                    i++;
                }
            }

            // 通知用户添加到下载列表的结果
            if (i == 0)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipAddDownloadingZero"));
            }
            else
            {
                eventAggregator.GetEvent<MessageEvent>().Publish($"{DictionaryResource.GetString("TipAddDownloadingFinished1")}{i}{DictionaryResource.GetString("TipAddDownloadingFinished2")}");
            }
        }

        /// <summary>
        /// 添加到下载列表事件是否允许执行
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteAddToDownloadCommand()
        {
            return LoadingVisibility != Visibility.Visible;
        }

        #endregion


        #region 业务逻辑

        /// <summary>
        /// 初始化页面元素
        /// </summary>
        private void InitView()
        {
            LogManager.Debug(Tag, "初始化页面元素");

            LoadingVisibility = Visibility.Visible;
            ContentVisibility = Visibility.Collapsed;
            NoDataVisibility = Visibility.Collapsed;

            VideoSections.Clear();
        }

        /// <summary>
        /// 更新页面的统一方法
        /// </summary>
        /// <param name="action"></param>
        /// <param name="input"></param>
        /// <param name="page"></param>
        private void UnityUpdateView(Action<IInfoService, VideoPage> action, string input, VideoPage page)
        {
            // 视频
            if (ParseEntrance.IsAvUrl(InputText) || ParseEntrance.IsBvUrl(InputText))
            {
                action(new VideoInfoService(input), page);
            }

            // 番剧（电影、电视剧）
            if (ParseEntrance.IsBangumiSeasonUrl(InputText) || ParseEntrance.IsBangumiEpisodeUrl(InputText) || ParseEntrance.IsBangumiMediaUrl(InputText))
            {
                action(new BangumiInfoService(input), page);
            }

            // 课程
            if (ParseEntrance.IsCheeseSeasonUrl(InputText) || ParseEntrance.IsCheeseEpisodeUrl(InputText))
            {
                action(new CheeseInfoService(input), page);
            }
        }

        /// <summary>
        /// 更新页面
        /// </summary>
        /// <param name="videoInfoService"></param>
        private void UpdateView(IInfoService videoInfoService, VideoPage param)
        {
            VideoInfoView = videoInfoService.GetVideoView();
            if (VideoInfoView == null)
            {
                LogManager.Debug(Tag, "VideoInfoView is null.");

                LoadingVisibility = Visibility.Collapsed;
                ContentVisibility = Visibility.Collapsed;
                NoDataVisibility = Visibility.Visible;
                return;
            }
            else
            {
                LoadingVisibility = Visibility.Collapsed;
                ContentVisibility = Visibility.Visible;
                NoDataVisibility = Visibility.Collapsed;
            }

            List<VideoSection> videoSections = videoInfoService.GetVideoSections();
            if (videoSections == null)
            {
                LogManager.Debug(Tag, "videoSections is not exist.");

                List<VideoPage> pages = videoInfoService.GetVideoPages();

                PropertyChangeAsync(new Action(() =>
                {
                    VideoSections.Add(new VideoSection
                    {
                        Id = 0,
                        Title = "default",
                        IsSelected = true,
                        VideoPages = pages
                    });
                }));
            }
            else
            {
                PropertyChangeAsync(new Action(() =>
                {
                    VideoSections.AddRange(videoSections);
                }));
            }
        }

        /// <summary>
        /// 解析视频流
        /// </summary>
        /// <param name="videoInfoService"></param>
        private void ParseVideo(IInfoService videoInfoService, VideoPage videoPage)
        {
            videoInfoService.GetVideoStream(videoPage);
        }

        #endregion

        /// <summary>
        /// 导航到VideoDetail页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            DownloadManage = ButtonIcon.Instance().DownloadManage;
            DownloadManage.Height = 24;
            DownloadManage.Width = 24;
            DownloadManage.Fill = DictionaryResource.GetColor("ColorPrimary");

            // Parent参数为null时，表示是从下一个页面返回到本页面，不需要执行任务
            if (navigationContext.Parameters.GetValue<string>("Parent") != null)
            {
                // 正在执行任务时不开启新任务
                if (LoadingVisibility != Visibility.Visible)
                {
                    InputText = navigationContext.Parameters.GetValue<string>("Parameter");
                    PropertyChangeAsync(ExecuteInputCommand);
                }
            }
        }

    }
}
