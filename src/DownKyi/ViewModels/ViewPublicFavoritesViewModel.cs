using DownKyi.Core.BiliApi.Favorites;
using DownKyi.Core.BiliApi.VideoStream;
using DownKyi.Core.Logging;
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DownKyi.ViewModels
{
    public class ViewPublicFavoritesViewModel : BaseViewModel
    {
        public const string Tag = "PagePublicFavorites";

        private CancellationTokenSource tokenSource;

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

        private VectorImage downloadManage;
        public VectorImage DownloadManage
        {
            get => downloadManage;
            set => SetProperty(ref downloadManage, value);
        }

        private Favorites favorites;
        public Favorites Favorites
        {
            get => favorites;
            set => SetProperty(ref favorites, value);
        }

        private ObservableCollection<FavoritesMedia> favoritesMedias;
        public ObservableCollection<FavoritesMedia> FavoritesMedias
        {
            get => favoritesMedias;
            set => SetProperty(ref favoritesMedias, value);
        }

        private Visibility contentVisibility;
        public Visibility ContentVisibility
        {
            get => contentVisibility;
            set => SetProperty(ref contentVisibility, value);
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

        private GifImage mediaLoading;
        public GifImage MediaLoading
        {
            get => mediaLoading;
            set => SetProperty(ref mediaLoading, value);
        }

        private Visibility mediaLoadingVisibility;
        public Visibility MediaLoadingVisibility
        {
            get => mediaLoadingVisibility;
            set => SetProperty(ref mediaLoadingVisibility, value);
        }

        private Visibility mediaNoDataVisibility;
        public Visibility MediaNoDataVisibility
        {
            get => mediaNoDataVisibility;
            set => SetProperty(ref mediaNoDataVisibility, value);
        }

        #endregion

        public ViewPublicFavoritesViewModel(IEventAggregator eventAggregator, IDialogService dialogService) : base(eventAggregator)
        {
            this.dialogService = dialogService;

            #region 属性初始化

            // 初始化loading gif
            Loading = new GifImage(Properties.Resources.loading);
            Loading.StartAnimate();
            LoadingVisibility = Visibility.Collapsed;
            NoDataVisibility = Visibility.Collapsed;

            MediaLoading = new GifImage(Properties.Resources.loading);
            MediaLoading.StartAnimate();
            MediaLoadingVisibility = Visibility.Collapsed;
            MediaNoDataVisibility = Visibility.Collapsed;

            ArrowBack = NavigationIcon.Instance().ArrowBack;
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            // 下载管理按钮
            DownloadManage = ButtonIcon.Instance().DownloadManage;
            DownloadManage.Height = 24;
            DownloadManage.Width = 24;
            DownloadManage.Fill = DictionaryResource.GetColor("ColorPrimary");

            FavoritesMedias = new ObservableCollection<FavoritesMedia>();

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

        // 复制封面事件
        private DelegateCommand copyCoverCommand;
        public DelegateCommand CopyCoverCommand => copyCoverCommand ?? (copyCoverCommand = new DelegateCommand(ExecuteCopyCoverCommand));

        /// <summary>
        /// 复制封面事件
        /// </summary>
        private void ExecuteCopyCoverCommand()
        {
            // 复制封面图片到剪贴板
            Clipboard.SetImage(Favorites.Cover);
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
            Clipboard.SetText(Favorites.CoverUrl);
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
            NavigateToView.NavigateToViewUserSpace(eventAggregator, Tag, Favorites.UpperMid);
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

        // 列表选择事件
        private DelegateCommand<object> favoritesMediasCommand;
        public DelegateCommand<object> FavoritesMediasCommand => favoritesMediasCommand ?? (favoritesMediasCommand = new DelegateCommand<object>(ExecuteFavoritesMediasCommand));

        /// <summary>
        /// 列表选择事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteFavoritesMediasCommand(object parameter)
        {
        }

        #endregion

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
                var list = FavoritesMedias.ToList();

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
                    i += addToDownloadService.AddToDownload(eventAggregator, dialogService, directory);
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

        /// <summary>
        /// 初始化页面元素
        /// </summary>
        private void InitView()
        {
            LogManager.Debug(Tag, "初始化页面元素");

            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            DownloadManage = ButtonIcon.Instance().DownloadManage;
            DownloadManage.Height = 24;
            DownloadManage.Width = 24;
            DownloadManage.Fill = DictionaryResource.GetColor("ColorPrimary");

            ContentVisibility = Visibility.Collapsed;
            LoadingVisibility = Visibility.Collapsed;
            NoDataVisibility = Visibility.Collapsed;
            MediaLoadingVisibility = Visibility.Collapsed;
            MediaNoDataVisibility = Visibility.Collapsed;

            FavoritesMedias.Clear();
        }

        /// <summary>
        /// 更新页面
        /// </summary>
        private void UpdateView(IFavoritesService favoritesService, long favoritesId, CancellationToken cancellationToken)
        {
            LoadingVisibility = Visibility.Visible;

            Favorites = favoritesService.GetFavorites(favoritesId);
            if (Favorites == null)
            {
                LogManager.Debug(Tag, "Favorites is null.");

                ContentVisibility = Visibility.Collapsed;
                LoadingVisibility = Visibility.Collapsed;
                NoDataVisibility = Visibility.Visible;
                return;
            }
            else
            {
                ContentVisibility = Visibility.Visible;
                LoadingVisibility = Visibility.Collapsed;
                NoDataVisibility = Visibility.Collapsed;
            }

            MediaLoadingVisibility = Visibility.Visible;

            List<Core.BiliApi.Favorites.Models.FavoritesMedia> medias = FavoritesResource.GetAllFavoritesMedia(favoritesId);
            if (medias == null || medias.Count == 0)
            {
                MediaLoadingVisibility = Visibility.Collapsed;
                MediaNoDataVisibility = Visibility.Visible;
                return;
            }
            else
            {
                MediaLoadingVisibility = Visibility.Collapsed;
                MediaNoDataVisibility = Visibility.Collapsed;
            }

            favoritesService.GetFavoritesMediaList(medias, FavoritesMedias, eventAggregator, cancellationToken);
        }

        /// <summary>
        /// 接收收藏夹id参数
        /// </summary>
        /// <param name="navigationContext"></param>
        public override async void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            // 根据传入参数不同执行不同任务
            long parameter = navigationContext.Parameters.GetValue<long>("Parameter");
            if (parameter == 0)
            {
                return;
            }

            InitView();
            await Task.Run(new Action(() =>
            {
                CancellationToken cancellationToken = tokenSource.Token;

                UpdateView(new FavoritesService(), parameter, cancellationToken);
            }), (tokenSource = new CancellationTokenSource()).Token);

        }
    }
}
