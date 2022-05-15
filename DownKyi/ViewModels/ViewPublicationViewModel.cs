using DownKyi.Core.BiliApi.VideoStream;
using DownKyi.Core.Storage;
using DownKyi.Core.Utils;
using DownKyi.CustomControl;
using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Services;
using DownKyi.Services.Download;
using DownKyi.Utils;
using DownKyi.ViewModels.PageViewModels;
using DownKyi.ViewModels.UserSpace;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels
{
    public class ViewPublicationViewModel : BaseViewModel
    {
        public const string Tag = "PagePublication";

        private readonly IDialogService dialogService;

        private CancellationTokenSource tokenSource;

        private long mid = -1;

        // 每页视频数量，暂时在此写死，以后在设置中增加选项
        private readonly int VideoNumberInPage = 30;

        #region 页面属性申明

        private string pageName = Tag;
        public string PageName
        {
            get => pageName;
            set => SetProperty(ref pageName, value);
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

        private Visibility noDataVisibility;
        public Visibility NoDataVisibility
        {
            get => noDataVisibility;
            set => SetProperty(ref noDataVisibility, value);
        }

        private VectorImage arrowBack;
        public VectorImage ArrowBack
        {
            get => arrowBack;
            set => SetProperty(ref arrowBack, value);
        }

        private ObservableCollection<TabHeader> tabHeaders;
        public ObservableCollection<TabHeader> TabHeaders
        {
            get => tabHeaders;
            set => SetProperty(ref tabHeaders, value);
        }

        private int selectTabId;
        public int SelectTabId
        {
            get => selectTabId;
            set => SetProperty(ref selectTabId, value);
        }

        private bool isEnabled = true;
        public bool IsEnabled
        {
            get => isEnabled;
            set => SetProperty(ref isEnabled, value);
        }

        private CustomPagerViewModel pager;
        public CustomPagerViewModel Pager
        {
            get => pager;
            set => SetProperty(ref pager, value);
        }

        private ObservableCollection<PublicationMedia> medias;
        public ObservableCollection<PublicationMedia> Medias
        {
            get => medias;
            set => SetProperty(ref medias, value);
        }

        private bool isSelectAll;
        public bool IsSelectAll
        {
            get => isSelectAll;
            set => SetProperty(ref isSelectAll, value);
        }

        #endregion

        public ViewPublicationViewModel(IEventAggregator eventAggregator, IDialogService dialogService) : base(eventAggregator)
        {
            this.dialogService = dialogService;

            #region 属性初始化

            // 初始化loading gif
            Loading = new GifImage(Properties.Resources.loading);
            Loading.StartAnimate();
            LoadingVisibility = Visibility.Collapsed;
            NoDataVisibility = Visibility.Collapsed;

            ArrowBack = NavigationIcon.Instance().ArrowBack;
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            TabHeaders = new ObservableCollection<TabHeader>();
            Medias = new ObservableCollection<PublicationMedia>();

            #endregion
        }

        #region 命令申明

        // 返回事件
        private DelegateCommand backSpaceCommand;
        public DelegateCommand BackSpaceCommand => backSpaceCommand ?? (backSpaceCommand = new DelegateCommand(ExecuteBackSpace));

        /// <summary>
        /// 返回事件
        /// </summary>
        private void ExecuteBackSpace()
        {
            ArrowBack.Fill = DictionaryResource.GetColor("ColorText");

            // 结束任务
            tokenSource?.Cancel();

            NavigationParam parameter = new NavigationParam
            {
                ViewName = ParentView,
                ParentViewName = null,
                Parameter = null
            };
            eventAggregator.GetEvent<NavigationEvent>().Publish(parameter);
        }

        // 左侧tab点击事件
        private DelegateCommand<object> leftTabHeadersCommand;
        public DelegateCommand<object> LeftTabHeadersCommand => leftTabHeadersCommand ?? (leftTabHeadersCommand = new DelegateCommand<object>(ExecuteLeftTabHeadersCommand, CanExecuteLeftTabHeadersCommand));

        /// <summary>
        /// 左侧tab点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteLeftTabHeadersCommand(object parameter)
        {
            if (!(parameter is TabHeader tabHeader)) { return; }

            // 页面选择
            Pager = new CustomPagerViewModel(1, (int)Math.Ceiling(double.Parse(tabHeader.SubTitle) / VideoNumberInPage));
            Pager.CurrentChanged += OnCurrentChanged_Pager;
            Pager.CountChanged += OnCountChanged_Pager;
            Pager.Current = 1;
        }

        /// <summary>
        /// 左侧tab点击事件是否允许执行
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteLeftTabHeadersCommand(object parameter)
        {
            return IsEnabled;
        }

        // 全选按钮点击事件
        private DelegateCommand<object> selectAllCommand;
        public DelegateCommand<object> SelectAllCommand => selectAllCommand ?? (selectAllCommand = new DelegateCommand<object>(ExecuteSelectAllCommand));

        /// <summary>
        /// 全选按钮点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteSelectAllCommand(object parameter)
        {
            if (IsSelectAll)
            {
                foreach (var item in Medias)
                {
                    item.IsSelected = true;
                }
            }
            else
            {
                foreach (var item in Medias)
                {
                    item.IsSelected = false;
                }
            }
        }

        // 列表选择事件
        private DelegateCommand<object> mediasCommand;
        public DelegateCommand<object> MediasCommand => mediasCommand ?? (mediasCommand = new DelegateCommand<object>(ExecuteMediasCommand));

        /// <summary>
        /// 列表选择事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteMediasCommand(object parameter)
        {
            if (!(parameter is IList selectedMedia)) { return; }

            if (selectedMedia.Count == Medias.Count)
            {
                IsSelectAll = true;
            }
            else
            {
                IsSelectAll = false;
            }
        }

        // 添加选中项到下载列表事件
        private DelegateCommand addToDownloadCommand;
        public DelegateCommand AddToDownloadCommand => addToDownloadCommand ?? (addToDownloadCommand = new DelegateCommand(ExecuteAddToDownloadCommand));

        /// <summary>
        /// 添加选中项到下载列表事件
        /// </summary>
        private void ExecuteAddToDownloadCommand()
        {
            AddToDownload(true);
        }

        // 添加所有视频到下载列表事件
        private DelegateCommand addAllToDownloadCommand;
        public DelegateCommand AddAllToDownloadCommand => addAllToDownloadCommand ?? (addAllToDownloadCommand = new DelegateCommand(ExecuteAddAllToDownloadCommand));

        /// <summary>
        /// 添加所有视频到下载列表事件
        /// </summary>
        private void ExecuteAddAllToDownloadCommand()
        {
            AddToDownload(false);
        }

        #endregion

        /// <summary>
        /// 添加到下载
        /// </summary>
        /// <param name="isOnlySelected"></param>
        private async void AddToDownload(bool isOnlySelected)
        {
            // 收藏夹里只有视频
            AddToDownloadService addToDownloadService = new AddToDownloadService(PlayStreamType.VIDEO);

            // 选择文件夹
            string directory = addToDownloadService.SetDirectory(dialogService);

            // 视频计数
            int i = 0;
            await Task.Run(() =>
            {
                // 为了避免执行其他操作时，
                // Medias变化导致的异常
                var list = Medias.ToList();

                // 添加到下载
                foreach (var media in list)
                {
                    // 只下载选中项，跳过未选中项
                    if (isOnlySelected && !media.IsSelected) { continue; }

                    /// 有分P的就下载全部

                    // 开启服务
                    VideoInfoService videoInfoService = new VideoInfoService(media.Bvid);

                    addToDownloadService.SetVideoInfoService(videoInfoService);
                    addToDownloadService.GetVideo();
                    addToDownloadService.ParseVideo(videoInfoService);
                    // 下载
                    i += addToDownloadService.AddToDownload(eventAggregator, directory);
                }
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

        private void OnCountChanged_Pager(int count) { }

        private bool OnCurrentChanged_Pager(int old, int current)
        {
            if (!IsEnabled)
            {
                //Pager.Current = old;
                return false;
            }

            Medias.Clear();
            IsSelectAll = false;
            LoadingVisibility = Visibility.Visible;
            NoDataVisibility = Visibility.Collapsed;

            UpdatePublication(current);

            return true;
        }

        private async void UpdatePublication(int current)
        {
            // 是否正在获取数据
            // 在所有的退出分支中都需要设为true
            IsEnabled = false;

            var tab = TabHeaders[SelectTabId];

            await Task.Run(() =>
            {
                CancellationToken cancellationToken = tokenSource.Token;

                var publications = Core.BiliApi.Users.UserSpace.GetPublication(mid, current, VideoNumberInPage, tab.Id);
                if (publications == null)
                {
                    // 没有数据，UI提示
                    LoadingVisibility = Visibility.Collapsed;
                    NoDataVisibility = Visibility.Visible;
                    return;
                }

                var videos = publications.Vlist;
                if (videos == null)
                {
                    // 没有数据，UI提示
                    LoadingVisibility = Visibility.Collapsed;
                    NoDataVisibility = Visibility.Visible;
                    return;
                }

                foreach (var video in videos)
                {
                    // 查询、保存封面
                    string coverUrl = video.Pic;
                    BitmapImage cover;
                    if (coverUrl == null || coverUrl == "")
                    {
                        cover = null; // new BitmapImage(new Uri($"pack://application:,,,/Resources/video-placeholder.png"));
                    }
                    else
                    {
                        if (!coverUrl.ToLower().StartsWith("http"))
                        {
                            coverUrl = $"https:{video.Pic}";
                        }

                        StorageCover storageCover = new StorageCover();
                        cover = storageCover.GetCoverThumbnail(video.Aid, video.Bvid, -1, coverUrl, 200, 125);
                    }

                    // 播放数
                    string play = string.Empty;
                    if (video.Play > 0)
                    {
                        play = Format.FormatNumber(video.Play);
                    }
                    else
                    {
                        play = "--";
                    }

                    DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
                    DateTime dateCTime = startTime.AddSeconds(video.Created);
                    string ctime = dateCTime.ToString("yyyy-MM-dd");

                    App.PropertyChangeAsync(new Action(() =>
                    {
                        PublicationMedia media = new PublicationMedia(eventAggregator)
                        {
                            Avid = video.Aid,
                            Bvid = video.Bvid,
                            Cover = cover ?? new BitmapImage(new Uri($"pack://application:,,,/Resources/video-placeholder.png")),
                            Duration = video.Length,
                            Title = video.Title,
                            PlayNumber = play,
                            CreateTime = ctime
                        };
                        medias.Add(media);

                        LoadingVisibility = Visibility.Collapsed;
                        NoDataVisibility = Visibility.Collapsed;
                    }));

                    // 判断是否该结束线程，若为true，跳出循环
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                }

            }, (tokenSource = new CancellationTokenSource()).Token);
            IsEnabled = true;
        }

        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitView()
        {
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            TabHeaders.Clear();
            Medias.Clear();
            SelectTabId = -1;
            IsSelectAll = false;
        }

        /// <summary>
        /// 导航到页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            // 根据传入参数不同执行不同任务
            var parameter = navigationContext.Parameters.GetValue<Dictionary<string, object>>("Parameter");
            if (parameter == null)
            {
                return;
            }

            InitView();

            mid = (long)parameter["mid"];
            int tid = (int)parameter["tid"];
            List<PublicationZone> zones = (List<PublicationZone>)parameter["list"];

            foreach (var item in zones)
            {
                TabHeaders.Add(new TabHeader
                {
                    Id = item.Tid,
                    Title = item.Name,
                    SubTitle = item.Count.ToString()
                });
            }

            // 初始选中项
            var selectTab = TabHeaders.FirstOrDefault(item => item.Id == tid);
            SelectTabId = TabHeaders.IndexOf(selectTab);

            // 页面选择
            Pager = new CustomPagerViewModel(1, (int)Math.Ceiling(double.Parse(selectTab.SubTitle) / VideoNumberInPage));
            Pager.CurrentChanged += OnCurrentChanged_Pager;
            Pager.CountChanged += OnCountChanged_Pager;
            Pager.Current = 1;
        }

    }
}
