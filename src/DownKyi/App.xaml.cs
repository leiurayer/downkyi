using DownKyi.Core.Settings;
using DownKyi.Services.Download;
using DownKyi.Utils;
using DownKyi.ViewModels;
using DownKyi.ViewModels.Dialogs;
using DownKyi.ViewModels.DownloadManager;
using DownKyi.ViewModels.Settings;
using DownKyi.ViewModels.Toolbox;
using DownKyi.ViewModels.UserSpace;
using DownKyi.Views;
using DownKyi.Views.Dialogs;
using DownKyi.Views.DownloadManager;
using DownKyi.Views.Settings;
using DownKyi.Views.Toolbox;
using DownKyi.Views.UserSpace;
using Prism.Ioc;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DownKyi
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static Dictionary<string, object> Dictionary = new Dictionary<string, object>();
        public static ObservableCollection<DownloadingItem> DownloadingList { get; set; }
        public static ObservableCollection<DownloadedItem> DownloadedList { get; set; }

        // 下载服务
        private IDownloadService downloadService;

        protected override Window CreateShell()
        {
            // 设置主题
            DictionaryResource.LoadTheme("ThemeDefault");
            //DictionaryResource.LoadTheme("ThemeDiy");

            // 切换语言
            DictionaryResource.LoadLanguage("Default");
            //DictionaryResource.LoadLanguage("en_US");

            // 初始化数据
            DownloadingList = new ObservableCollection<DownloadingItem>();
            DownloadedList = new ObservableCollection<DownloadedItem>();

            // 下载数据存储服务
            DownloadStorageService downloadStorageService = new DownloadStorageService();

            // 从数据库读取
            List<DownloadingItem> downloadingItems = downloadStorageService.GetDownloading();
            List<DownloadedItem> downloadedItems = downloadStorageService.GetDownloaded();
            DownloadingList.AddRange(downloadingItems);
            DownloadedList.AddRange(downloadedItems);

            // 下载列表发生变化时执行的任务
            DownloadingList.CollectionChanged += new NotifyCollectionChangedEventHandler(async (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                await Task.Run(() =>
                {
                    if (e.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (object item in e.NewItems)
                        {
                            if (item is DownloadingItem downloading)
                            {
                                //Console.WriteLine("DownloadingList添加");
                                downloadStorageService.AddDownloading(downloading);
                            }
                        }
                    }
                    if (e.Action == NotifyCollectionChangedAction.Remove)
                    {
                        foreach (object item in e.OldItems)
                        {
                            if (item is DownloadingItem downloading)
                            {
                                //Console.WriteLine("DownloadingList移除");
                                downloadStorageService.RemoveDownloading(downloading);
                            }
                        }
                    }
                });
            });

            // 下载完成列表发生变化时执行的任务
            DownloadedList.CollectionChanged += new NotifyCollectionChangedEventHandler(async (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                await Task.Run(() =>
                {
                    if (e.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (object item in e.NewItems)
                        {
                            if (item is DownloadedItem downloaded)
                            {
                                //Console.WriteLine("DownloadedList添加");
                                downloadStorageService.AddDownloaded(downloaded);
                            }
                        }
                    }
                    if (e.Action == NotifyCollectionChangedAction.Remove)
                    {
                        foreach (object item in e.OldItems)
                        {
                            if (item is DownloadedItem downloaded)
                            {
                                //Console.WriteLine("DownloadedList移除");
                                downloadStorageService.RemoveDownloaded(downloaded);
                            }
                        }
                    }
                });
            });

            // 启动下载服务
            downloadService = new AriaDownloadService(DownloadingList, DownloadedList);
            downloadService.Start();

            return Container.Resolve<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                SplashWindow sw = new SplashWindow();
                // 储存
                Dictionary["SplashWindow"] = sw;
                // 不能用Show
                sw.ShowDialog();
            });
            // 设置单线程
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // 关闭下载服务
            downloadService.End();

            base.OnExit(e);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // pages
            containerRegistry.RegisterForNavigation<ViewIndex>(ViewIndexViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewLogin>(ViewLoginViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewVideoDetail>(ViewVideoDetailViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewSettings>(ViewSettingsViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewToolbox>(ViewToolboxViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewDownloadManager>(ViewDownloadManagerViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewPublicFavorites>(ViewPublicFavoritesViewModel.Tag);

            containerRegistry.RegisterForNavigation<ViewUserSpace>(ViewUserSpaceViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewPublication>(ViewPublicationViewModel.Tag);
            containerRegistry.RegisterForNavigation<Views.ViewChannel>(ViewModels.ViewChannelViewModel.Tag);

            containerRegistry.RegisterForNavigation<ViewMySpace>(ViewMySpaceViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewMyFavorites>(ViewMyFavoritesViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewMyBangumiFollow>(ViewMyBangumiFollowViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewMyToViewVideo>(ViewMyToViewVideoViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewMyHistory>(ViewMyHistoryViewModel.Tag);

            // downloadManager pages
            containerRegistry.RegisterForNavigation<ViewDownloading>(ViewDownloadingViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewDownloadFinished>(ViewDownloadFinishedViewModel.Tag);

            // settings pages
            containerRegistry.RegisterForNavigation<ViewBasic>(ViewBasicViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewNetwork>(ViewNetworkViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewVideo>(ViewVideoViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewDanmaku>(ViewDanmakuViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewAbout>(ViewAboutViewModel.Tag);

            // tools pages
            containerRegistry.RegisterForNavigation<ViewBiliHelper>(ViewBiliHelperViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewDelogo>(ViewDelogoViewModel.Tag);
            containerRegistry.RegisterForNavigation<ViewExtractMedia>(ViewExtractMediaViewModel.Tag);

            // UserSpace
            containerRegistry.RegisterForNavigation<ViewArchive>(ViewArchiveViewModel.Tag);
            containerRegistry.RegisterForNavigation<Views.UserSpace.ViewChannel>(ViewModels.UserSpace.ViewChannelViewModel.Tag);

            // dialogs
            containerRegistry.RegisterDialog<ViewAlertDialog>(ViewAlertDialogViewModel.Tag);
            containerRegistry.RegisterDialog<ViewDownloadSetter>(ViewDownloadSetterViewModel.Tag);
            containerRegistry.RegisterDialog<ViewParsingSelector>(ViewParsingSelectorViewModel.Tag);

        }

        /// <summary>
        /// 异步修改绑定到UI的属性
        /// </summary>
        /// <param name="callback"></param>
        public static void PropertyChangeAsync(Action callback)
        {
            if (Current == null) { return; }

            Current.Dispatcher.Invoke(callback);
        }

        /// <summary>
        /// 下载完成列表排序
        /// </summary>
        /// <param name="finishedSort"></param>
        public static void SortDownloadedList(DownloadFinishedSort finishedSort)
        {
            List<DownloadedItem> list = DownloadedList.ToList();
            switch (finishedSort)
            {
                case DownloadFinishedSort.DOWNLOAD:
                    // 按下载先后排序
                    list.Sort((x, y) => { return x.Downloaded.FinishedTimestamp.CompareTo(y.Downloaded.FinishedTimestamp); });
                    break;
                case DownloadFinishedSort.NUMBER:
                    // 按序号排序
                    list.Sort((x, y) =>
                    {
                        int compare = x.MainTitle.CompareTo(y.MainTitle);
                        return compare == 0 ? x.Order.CompareTo(y.Order) : compare;
                    });
                    break;
                default:
                    break;
            }

            // 更新下载完成列表
            // 如果有更好的方法再重写
            DownloadedList.Clear();
            foreach (DownloadedItem item in list)
            {
                DownloadedList.Add(item);
            }
        }

    }
}
