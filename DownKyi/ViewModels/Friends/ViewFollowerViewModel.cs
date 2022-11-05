using DownKyi.Core.BiliApi.Users;
using DownKyi.Core.BiliApi.Users.Models;
using DownKyi.Core.Settings.Models;
using DownKyi.Core.Settings;
using DownKyi.Core.Storage;
using DownKyi.CustomControl;
using DownKyi.ViewModels.PageViewModels;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels.Friends
{
    public class ViewFollowerViewModel : BaseViewModel
    {
        public const string Tag = "PageFriendsFollower";

        // mid
        private long mid = -1;

        // 每页数量，暂时在此写死，以后在设置中增加选项
        private readonly int NumberInPage = 20;

        public bool IsEnabled = true;

        #region 页面属性申明

        private string pageName = ViewFriendsViewModel.Tag;
        public string PageName
        {
            get => pageName;
            set => SetProperty(ref pageName, value);
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

        private CustomPagerViewModel pager;
        public CustomPagerViewModel Pager
        {
            get => pager;
            set => SetProperty(ref pager, value);
        }

        private ObservableCollection<FriendInfo> contents;
        public ObservableCollection<FriendInfo> Contents
        {
            get => contents;
            set => SetProperty(ref contents, value);
        }

        #endregion

        public ViewFollowerViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化

            // 初始化loading gif
            Loading = new GifImage(Properties.Resources.loading);
            Loading.StartAnimate();
            LoadingVisibility = Visibility.Collapsed;
            NoDataVisibility = Visibility.Collapsed;

            Contents = new ObservableCollection<FriendInfo>();

            #endregion
        }

        #region 命令申明

        #endregion

        private void LoadContent(List<RelationFollowInfo> contents)
        {
            ContentVisibility = Visibility.Visible;
            LoadingVisibility = Visibility.Collapsed;
            NoDataVisibility = Visibility.Collapsed;
            foreach (var item in contents)
            {
                StorageHeader storageHeader = new StorageHeader();
                BitmapImage header = storageHeader.GetHeaderThumbnail(item.Mid, item.Name, item.Face, 64, 64);
                App.PropertyChangeAsync(new Action(() =>
                {
                    Contents.Add(new FriendInfo(eventAggregator) { Mid = item.Mid, Header = header, Name = item.Name, Sign = item.Sign });
                }));
            }
        }

        private async void UpdateContent(int current)
        {
            // 是否正在获取数据
            // 在所有的退出分支中都需要设为true
            IsEnabled = false;

            Contents.Clear();
            ContentVisibility = Visibility.Collapsed;
            LoadingVisibility = Visibility.Visible;
            NoDataVisibility = Visibility.Collapsed;

            RelationFollow data = null;
            List<RelationFollowInfo> contents = null;
            await Task.Run(() =>
            {
                data = UserRelation.GetFollowers(mid, current, NumberInPage);
                if (data != null && data.List != null && data.List.Count > 0)
                {
                    contents = data.List;
                }
                if (contents == null) { return; }

                LoadContent(contents);
            });

            if (data == null || contents == null)
            {
                ContentVisibility = Visibility.Collapsed;
                LoadingVisibility = Visibility.Collapsed;
                NoDataVisibility = Visibility.Visible;
            }
            else
            {
                UserInfoSettings userInfo = SettingsManager.GetInstance().GetUserInfo();
                if (userInfo != null && userInfo.Mid == mid)
                {
                    Pager.Count = (int)Math.Ceiling((double)data.Total / NumberInPage);
                }
                else
                {
                    int page = (int)Math.Ceiling((double)data.Total / NumberInPage);
                    if (page > 5)
                    {
                        Pager.Count = 5;
                    }
                    else
                    {
                        Pager.Count = page;
                    }
                }

                ContentVisibility = Visibility.Visible;
                LoadingVisibility = Visibility.Collapsed;
                NoDataVisibility = Visibility.Collapsed;
            }

            IsEnabled = true;
        }

        private void OnCountChanged_Pager(int count) { }

        private bool OnCurrentChanged_Pager(int old, int current)
        {
            if (!IsEnabled)
            {
                //Pager.Current = old;
                return false;
            }

            UpdateContent(current);

            return true;
        }

        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitView()
        {
            ContentVisibility = Visibility.Collapsed;
            LoadingVisibility = Visibility.Visible;
            NoDataVisibility = Visibility.Collapsed;

            Contents.Clear();
        }

        /// <summary>
        /// 导航到页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            // 传入mid
            long parameter = navigationContext.Parameters.GetValue<long>("mid");
            if (parameter == 0)
            {
                return;
            }
            mid = parameter;

            // 是否是从PageFriends的headerTable的item点击进入的
            // true表示加载PageFriends后第一次进入此页面
            // false表示从headerTable的item点击进入的
            bool isFirst = navigationContext.Parameters.GetValue<bool>("isFirst");
            if (isFirst)
            {
                InitView();

                //UpdateContent(1);

                // 页面选择
                Pager = new CustomPagerViewModel(1, (int)Math.Ceiling((double)1 / NumberInPage));
                Pager.CurrentChanged += OnCurrentChanged_Pager;
                Pager.CountChanged += OnCountChanged_Pager;
                Pager.Current = 1;
            }
        }
    }
}
