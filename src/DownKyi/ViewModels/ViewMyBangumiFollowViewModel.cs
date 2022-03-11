using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.VideoStream;
using DownKyi.Core.Storage;
using DownKyi.CustomControl;
using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Services;
using DownKyi.Services.Download;
using DownKyi.Utils;
using DownKyi.ViewModels.PageViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels
{
    public class ViewMyBangumiFollowViewModel : BaseViewModel
    {
        public const string Tag = "PageMyBangumiFollow";

        private readonly IDialogService dialogService;

        private CancellationTokenSource tokenSource;

        private long mid = -1;

        // 每页视频数量，暂时在此写死，以后在设置中增加选项
        private readonly int VideoNumberInPage = 15;

        #region 页面属性申明

        private string pageName = Tag;
        public string PageName
        {
            get => pageName;
            set => SetProperty(ref pageName, value);
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

        private Visibility contentVisibility;
        public Visibility ContentVisibility
        {
            get => contentVisibility;
            set => SetProperty(ref contentVisibility, value);
        }

        private CustomPagerViewModel pager;
        public CustomPagerViewModel Pager
        {
            get => pager;
            set => SetProperty(ref pager, value);
        }

        private ObservableCollection<BangumiFollowMedia> medias;
        public ObservableCollection<BangumiFollowMedia> Medias
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

        #endregion

        public ViewMyBangumiFollowViewModel(IEventAggregator eventAggregator, IDialogService dialogService) : base(eventAggregator)
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

            TabHeaders = new ObservableCollection<TabHeader>
            {
                new TabHeader { Id = (int)Core.BiliApi.Users.Models.BangumiType.ANIME, Title = DictionaryResource.GetString("FollowAnime") },
                new TabHeader { Id = (int)Core.BiliApi.Users.Models.BangumiType.EPISODE, Title = DictionaryResource.GetString("FollowMovie") }
            };

            Medias = new ObservableCollection<BangumiFollowMedia>();

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
            InitView();

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

        // 顶部tab点击事件
        private DelegateCommand<object> tabHeadersCommand;
        public DelegateCommand<object> TabHeadersCommand => tabHeadersCommand ?? (tabHeadersCommand = new DelegateCommand<object>(ExecuteTabHeadersCommand, CanExecuteTabHeadersCommand));

        /// <summary>
        /// 左侧tab点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteTabHeadersCommand(object parameter)
        {
            if (!(parameter is TabHeader tabHeader)) { return; }

            // 顶部tab点击后，隐藏Content
            ContentVisibility = Visibility.Collapsed;

            // 页面选择
            Pager = new CustomPagerViewModel(1, 1);
            Pager.CurrentChanged += OnCurrentChanged_Pager;
            Pager.CountChanged += OnCountChanged_Pager;
            Pager.Current = 1;
        }

        /// <summary>
        /// 顶部tab点击事件是否允许执行
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteTabHeadersCommand(object parameter)
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
            // 订阅里只有BANGUMI类型
            AddToDownloadService addToDownloadService = new AddToDownloadService(PlayStreamType.BANGUMI);

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
                    BangumiInfoService service = new BangumiInfoService($"{ParseEntrance.BangumiMediaUrl}md{media.MediaId}");

                    addToDownloadService.SetVideoInfoService(service);
                    addToDownloadService.GetVideo();
                    addToDownloadService.ParseVideo(service);
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

            UpdateBangumiMediaList(current);

            return true;
        }

        private async void UpdateBangumiMediaList(int current)
        {
            Medias.Clear();
            IsSelectAll = false;

            LoadingVisibility = Visibility.Visible;
            NoDataVisibility = Visibility.Collapsed;

            // 是否正在获取数据
            // 在所有的退出分支中都需要设为true
            IsEnabled = false;

            var tab = TabHeaders[SelectTabId];
            Core.BiliApi.Users.Models.BangumiType type = (Core.BiliApi.Users.Models.BangumiType)tab.Id;

            await Task.Run(() =>
            {
                CancellationToken cancellationToken = tokenSource.Token;

                var bangumiFollows = Core.BiliApi.Users.UserSpace.GetBangumiFollow(mid, type, current, VideoNumberInPage);
                if (bangumiFollows == null || bangumiFollows.List == null || bangumiFollows.List.Count == 0)
                {
                    LoadingVisibility = Visibility.Collapsed;
                    NoDataVisibility = Visibility.Visible;
                    return;
                }

                // 更新总页码
                Pager.Count = (int)Math.Ceiling((double)bangumiFollows.Total / VideoNumberInPage);
                // 更新内容
                ContentVisibility = Visibility.Visible;

                foreach (var bangumiFollow in bangumiFollows.List)
                {
                    // 查询、保存封面
                    string coverUrl = bangumiFollow.Cover;
                    BitmapImage cover;
                    if (coverUrl == null || coverUrl == "")
                    {
                        cover = null;
                    }
                    else
                    {
                        if (!coverUrl.ToLower().StartsWith("http"))
                        {
                            coverUrl = $"https:{bangumiFollow.Cover}";
                        }

                        StorageCover storageCover = new StorageCover();
                        cover = storageCover.GetCoverThumbnail(bangumiFollow.MediaId, bangumiFollow.SeasonId.ToString(), -1, coverUrl, 110, 140);
                    }

                    // 地区
                    string area = string.Empty;
                    if (bangumiFollow.Areas != null && bangumiFollow.Areas.Count > 0)
                    {
                        area = bangumiFollow.Areas[0].Name;
                    }

                    // 视频更新进度
                    string indexShow = string.Empty;
                    if (bangumiFollow.NewEp != null)
                    {
                        indexShow = bangumiFollow.NewEp.IndexShow;
                    }

                    // 观看进度
                    string progress;
                    if (bangumiFollow.Progress == null || bangumiFollow.Progress == "")
                    {
                        progress = DictionaryResource.GetString("BangumiNotWatched");
                    }
                    else
                    {
                        progress = bangumiFollow.Progress;
                    }

                    App.PropertyChangeAsync(() =>
                    {
                        BangumiFollowMedia media = new BangumiFollowMedia(eventAggregator)
                        {
                            MediaId = bangumiFollow.MediaId,
                            SeasonId = bangumiFollow.SeasonId,
                            Title = bangumiFollow.Title,
                            SeasonTypeName = bangumiFollow.SeasonTypeName,
                            Area = area,
                            Badge = bangumiFollow.Badge,
                            Cover = cover ?? new BitmapImage(new Uri($"pack://application:,,,/Resources/video-placeholder.png")),
                            Evaluate = bangumiFollow.Evaluate,
                            IndexShow = indexShow,
                            Progress = progress
                        };

                        Medias.Add(media);

                        LoadingVisibility = Visibility.Collapsed;
                        NoDataVisibility = Visibility.Collapsed;
                    });

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

            ContentVisibility = Visibility.Collapsed;
            LoadingVisibility = Visibility.Visible;
            NoDataVisibility = Visibility.Collapsed;

            Medias.Clear();
            IsSelectAll = false;
        }

        /// <summary>
        /// 导航到VideoDetail页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            // 根据传入参数不同执行不同任务
            mid = navigationContext.Parameters.GetValue<long>("Parameter");
            if (mid == 0)
            {
                return;
            }

            InitView();

            // 初始选中项
            SelectTabId = 0;

            // 页面选择
            Pager = new CustomPagerViewModel(1, 1);
            Pager.CurrentChanged += OnCurrentChanged_Pager;
            Pager.CountChanged += OnCountChanged_Pager;
            Pager.Current = 1;
        }

    }
}
