using DownKyi.Core.BiliApi.History;
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
    public class ViewMyToViewVideoViewModel : BaseViewModel
    {
        public const string Tag = "PageMyToView";

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

        private Visibility contentVisibility;
        public Visibility ContentVisibility
        {
            get => contentVisibility;
            set => SetProperty(ref contentVisibility, value);
        }

        private ObservableCollection<ToViewMedia> medias;
        public ObservableCollection<ToViewMedia> Medias
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

        public ViewMyToViewVideoViewModel(IEventAggregator eventAggregator, IDialogService dialogService) : base(eventAggregator)
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

            // 下载管理按钮
            DownloadManage = ButtonIcon.Instance().DownloadManage;
            DownloadManage.Height = 24;
            DownloadManage.Width = 24;
            DownloadManage.Fill = DictionaryResource.GetColor("ColorPrimary");

            Medias = new ObservableCollection<ToViewMedia>();

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
            // 稍后再看里只有视频
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

        private async void UpdateToViewMediaList()
        {
            LoadingVisibility = Visibility.Visible;
            NoDataVisibility = Visibility.Collapsed;
            Medias.Clear();

            await Task.Run(() =>
            {
                CancellationToken cancellationToken = tokenSource.Token;

                var toViewList = ToView.GetToView();
                if (toViewList == null || toViewList.Count == 0)
                {
                    LoadingVisibility = Visibility.Collapsed;
                    NoDataVisibility = Visibility.Visible;
                    return;
                }

                foreach (var toView in toViewList)
                {

                    // 查询、保存封面
                    string coverUrl = toView.Pic;
                    BitmapImage cover;
                    if (coverUrl == null || coverUrl == "")
                    {
                        cover = null;
                    }
                    else
                    {
                        if (!coverUrl.ToLower().StartsWith("http"))
                        {
                            coverUrl = $"https:{toView.Pic}";
                        }

                        StorageCover storageCover = new StorageCover();
                        cover = storageCover.GetCoverThumbnail(toView.Aid, toView.Bvid, toView.Cid, coverUrl, 160, 100);
                    }

                    // 获取用户头像
                    long upMid = -1;
                    string upName;
                    BitmapImage upHeader;
                    if (toView.Owner != null && toView.Owner.Face != null)
                    {
                        upMid = toView.Owner.Mid;
                        upName = toView.Owner.Name;
                        StorageHeader storageHeader = new StorageHeader();
                        upHeader = storageHeader.GetHeaderThumbnail(toView.Owner.Mid, upName, toView.Owner.Face, 24, 24);
                    }
                    else
                    {
                        upName = "";
                        upHeader = null;
                    }

                    App.PropertyChangeAsync(() =>
                    {
                        ToViewMedia media = new ToViewMedia(eventAggregator)
                        {
                            Aid = toView.Aid,
                            Bvid = toView.Bvid,
                            UpMid = upMid,
                            Cover = cover ?? new BitmapImage(new Uri($"pack://application:,,,/Resources/video-placeholder.png")),
                            Title = toView.Title,
                            UpName = upName,
                            UpHeader = upHeader
                        };

                        Medias.Add(media);

                        ContentVisibility = Visibility.Visible;
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
        }

        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitView()
        {
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            ContentVisibility = Visibility.Collapsed;
            LoadingVisibility = Visibility.Collapsed;
            NoDataVisibility = Visibility.Collapsed;

            Medias.Clear();
            IsSelectAll = false;
        }

        /// <summary>
        /// 导航到页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            DownloadManage = ButtonIcon.Instance().DownloadManage;
            DownloadManage.Height = 24;
            DownloadManage.Width = 24;
            DownloadManage.Fill = DictionaryResource.GetColor("ColorPrimary");

            // 根据传入参数不同执行不同任务
            long mid = navigationContext.Parameters.GetValue<long>("Parameter");
            if (mid == 0)
            {
                IsSelectAll = false;
                foreach (var media in Medias)
                {
                    media.IsSelected = false;
                }

                return;
            }

            InitView();

            UpdateToViewMediaList();
        }

    }
}
