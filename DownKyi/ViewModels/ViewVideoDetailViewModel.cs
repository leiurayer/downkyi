using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.VideoStream;
using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using DownKyi.CustomControl;
using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Services;
using DownKyi.Services.Download;
using DownKyi.Utils;
using DownKyi.ViewModels.Dialogs;
using DownKyi.ViewModels.PageViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DownKyi.ViewModels
{
    public class ViewVideoDetailViewModel : BaseViewModel
    {
        public const string Tag = "PageVideoDetail";

        // 保存输入字符串，避免被用户修改
        private string input = null;

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

        public ViewVideoDetailViewModel(IEventAggregator eventAggregator, IDialogService dialogService) : base(eventAggregator, dialogService)
        {
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

                    input = InputText;

                    // 更新页面
                    UnityUpdateView(UpdateView, input, null);

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

                    UnityUpdateView(ParseVideo, input, videoPage);
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
                                    if (page.IsSelected)
                                    {
                                        // 执行解析任务
                                        UnityUpdateView(ParseVideo, input, page);
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
                                        // 执行解析任务
                                        UnityUpdateView(ParseVideo, input, page);
                                    }
                                }
                            }
                            break;
                        case ParseScope.ALL:
                            foreach (VideoSection section in VideoSections)
                            {
                                foreach (VideoPage page in section.VideoPages)
                                {
                                    // 执行解析任务
                                    UnityUpdateView(ParseVideo, input, page);
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

            // 解析后是否自动下载解析视频
            AllowStatus isAutoDownloadAll = SettingsManager.GetInstance().IsAutoDownloadAll();
            if (parseScope != ParseScope.NONE && isAutoDownloadAll == AllowStatus.YES)
            {
                AddToDownload(true);
            }
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
            AddToDownload(false);
            //AddToDownloadService addToDownloadService = null;
            //// 视频
            //if (ParseEntrance.IsAvUrl(input) || ParseEntrance.IsBvUrl(input))
            //{
            //    addToDownloadService = new AddToDownloadService(PlayStreamType.VIDEO);
            //}
            //// 番剧（电影、电视剧）
            //else if (ParseEntrance.IsBangumiSeasonUrl(input) || ParseEntrance.IsBangumiEpisodeUrl(input) || ParseEntrance.IsBangumiMediaUrl(input))
            //{
            //    addToDownloadService = new AddToDownloadService(PlayStreamType.BANGUMI);
            //}
            //// 课程
            //else if (ParseEntrance.IsCheeseSeasonUrl(input) || ParseEntrance.IsCheeseEpisodeUrl(input))
            //{
            //    addToDownloadService = new AddToDownloadService(PlayStreamType.CHEESE);
            //}
            //else
            //{
            //    return;
            //}

            //// 选择文件夹
            //string directory = addToDownloadService.SetDirectory(dialogService);

            //// 视频计数
            //int i = 0;
            //await Task.Run(() =>
            //{
            //    // 传递video对象
            //    addToDownloadService.GetVideo(VideoInfoView, VideoSections.ToList());
            //    // 下载
            //    i = addToDownloadService.AddToDownload(eventAggregator, directory);
            //});

            //if (directory == null)
            //{
            //    return;
            //}

            //// 通知用户添加到下载列表的结果
            //if (i <= 0)
            //{
            //    eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipAddDownloadingZero"));
            //}
            //else
            //{
            //    eventAggregator.GetEvent<MessageEvent>().Publish($"{DictionaryResource.GetString("TipAddDownloadingFinished1")}{i}{DictionaryResource.GetString("TipAddDownloadingFinished2")}");
            //}
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
            if (ParseEntrance.IsAvUrl(input) || ParseEntrance.IsBvUrl(input))
            {
                action(new VideoInfoService(input), page);
            }

            // 番剧（电影、电视剧）
            if (ParseEntrance.IsBangumiSeasonUrl(input) || ParseEntrance.IsBangumiEpisodeUrl(input) || ParseEntrance.IsBangumiMediaUrl(input))
            {
                action(new BangumiInfoService(input), page);
            }

            // 课程
            if (ParseEntrance.IsCheeseSeasonUrl(input) || ParseEntrance.IsCheeseEpisodeUrl(input))
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
            // 获取视频详情
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

            // 获取视频列表
            List<VideoSection> videoSections = videoInfoService.GetVideoSections(false);

            // 清空以前的数据
            PropertyChangeAsync(new Action(() =>
            {
                VideoSections.Clear();
            }));

            // 添加新数据
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

        /// <summary>
        /// 添加到下载列表事件
        /// </summary>
        /// <param name="isAll">是否下载所有，包括未选中项</param>
        private async void AddToDownload(bool isAll)
        {
            AddToDownloadService addToDownloadService = null;
            // 视频
            if (ParseEntrance.IsAvUrl(input) || ParseEntrance.IsBvUrl(input))
            {
                addToDownloadService = new AddToDownloadService(PlayStreamType.VIDEO);
            }
            // 番剧（电影、电视剧）
            else if (ParseEntrance.IsBangumiSeasonUrl(input) || ParseEntrance.IsBangumiEpisodeUrl(input) || ParseEntrance.IsBangumiMediaUrl(input))
            {
                addToDownloadService = new AddToDownloadService(PlayStreamType.BANGUMI);
            }
            // 课程
            else if (ParseEntrance.IsCheeseSeasonUrl(input) || ParseEntrance.IsCheeseEpisodeUrl(input))
            {
                addToDownloadService = new AddToDownloadService(PlayStreamType.CHEESE);
            }
            else
            {
                return;
            }

            // 选择文件夹
            string directory = addToDownloadService.SetDirectory(dialogService);

            // 视频计数
            int i = 0;
            await Task.Run(() =>
            {
                // 传递video对象
                addToDownloadService.GetVideo(VideoInfoView, VideoSections.ToList());
                // 下载
                i = addToDownloadService.AddToDownload(eventAggregator, directory, isAll);
            });

            if (directory == null)
            {
                return;
            }

            // 通知用户添加到下载列表的结果
            if (i <= 0)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipAddDownloadingZero"));
            }
            else
            {
                eventAggregator.GetEvent<MessageEvent>().Publish($"{DictionaryResource.GetString("TipAddDownloadingFinished1")}{i}{DictionaryResource.GetString("TipAddDownloadingFinished2")}");
            }
        }

        #endregion

        /// <summary>
        /// 导航到VideoDetail页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            DownloadManage = ButtonIcon.Instance().DownloadManage;
            DownloadManage.Height = 24;
            DownloadManage.Width = 24;
            DownloadManage.Fill = DictionaryResource.GetColor("ColorPrimary");

            // Parent参数为null时，表示是从下一个页面返回到本页面，不需要执行任务
            if (navigationContext.Parameters.GetValue<string>("Parent") != null)
            {
                string param = navigationContext.Parameters.GetValue<string>("Parameter");
                // 移除剪贴板id
                string intput = param.Replace(AppConstant.ClipboardId, "");

                // 检测是否从剪贴板传入
                if (InputText == intput && param.EndsWith(AppConstant.ClipboardId))
                {
                    return;
                }

                // 正在执行任务时不开启新任务
                if (LoadingVisibility != Visibility.Visible)
                {
                    InputText = intput;
                    PropertyChangeAsync(ExecuteInputCommand);
                }
            }

            base.OnNavigatedTo(navigationContext);
        }

    }
}
