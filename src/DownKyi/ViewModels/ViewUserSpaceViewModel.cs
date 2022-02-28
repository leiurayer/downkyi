using DownKyi.Core.BiliApi.Users;
using DownKyi.Core.BiliApi.Users.Models;
using DownKyi.Core.Storage;
using DownKyi.Core.Utils;
using DownKyi.CustomControl;
using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Utils;
using DownKyi.ViewModels.UserSpace;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels
{
    public class ViewUserSpaceViewModel : BaseViewModel
    {
        public const string Tag = "PageUserSpace";

        private readonly IRegionManager regionManager;

        // mid
        private long mid = -1;

        #region 页面属性申明

        private VectorImage arrowBack;
        public VectorImage ArrowBack
        {
            get => arrowBack;
            set => SetProperty(ref arrowBack, value);
        }

        private GifImage loading;
        public GifImage Loading
        {
            get => loading;
            set => SetProperty(ref loading, value);
        }

        private Visibility noDataVisibility;
        public Visibility NoDataVisibility
        {
            get => noDataVisibility;
            set => SetProperty(ref noDataVisibility, value);
        }

        private Visibility loadingVisibility;
        public Visibility LoadingVisibility
        {
            get => loadingVisibility;
            set => SetProperty(ref loadingVisibility, value);
        }

        private Visibility viewVisibility;
        public Visibility ViewVisibility
        {
            get => viewVisibility;
            set => SetProperty(ref viewVisibility, value);
        }

        private Visibility contentVisibility;
        public Visibility ContentVisibility
        {
            get => contentVisibility;
            set => SetProperty(ref contentVisibility, value);
        }

        private string topNavigationBg;
        public string TopNavigationBg
        {
            get => topNavigationBg;
            set => SetProperty(ref topNavigationBg, value);
        }

        private BitmapImage background;
        public BitmapImage Background
        {
            get => background;
            set => SetProperty(ref background, value);
        }

        private BitmapImage header;
        public BitmapImage Header
        {
            get => header;
            set => SetProperty(ref header, value);
        }

        private string userName;
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        private BitmapImage sex;
        public BitmapImage Sex
        {
            get => sex;
            set => SetProperty(ref sex, value);
        }

        private BitmapImage level;
        public BitmapImage Level
        {
            get => level;
            set => SetProperty(ref level, value);
        }

        private Visibility vipTypeVisibility;
        public Visibility VipTypeVisibility
        {
            get => vipTypeVisibility;
            set => SetProperty(ref vipTypeVisibility, value);
        }

        private string vipType;
        public string VipType
        {
            get => vipType;
            set => SetProperty(ref vipType, value);
        }

        private string sign;
        public string Sign
        {
            get => sign;
            set => SetProperty(ref sign, value);
        }

        private string isFollowed;
        public string IsFollowed
        {
            get => isFollowed;
            set => SetProperty(ref isFollowed, value);
        }

        private ObservableCollection<TabLeftBanner> tabLeftBanners;
        public ObservableCollection<TabLeftBanner> TabLeftBanners
        {
            get => tabLeftBanners;
            set => SetProperty(ref tabLeftBanners, value);
        }

        private ObservableCollection<TabRightBanner> tabRightBanners;
        public ObservableCollection<TabRightBanner> TabRightBanners
        {
            get => tabRightBanners;
            set => SetProperty(ref tabRightBanners, value);
        }

        #endregion

        public ViewUserSpaceViewModel(IRegionManager regionManager, IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.regionManager = regionManager;

            #region 属性初始化

            // 返回按钮
            ArrowBack = NavigationIcon.Instance().ArrowBack;
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            // 初始化loading gif
            Loading = new GifImage(Properties.Resources.loading);
            Loading.StartAnimate();

            TopNavigationBg = "#00FFFFFF"; // 透明

            TabLeftBanners = new ObservableCollection<TabLeftBanner>();
            TabRightBanners = new ObservableCollection<TabRightBanner>();

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
            NavigationParam parameter = new NavigationParam
            {
                ViewName = ParentView,
                ParentViewName = null,
                Parameter = null
            };
            eventAggregator.GetEvent<NavigationEvent>().Publish(parameter);
        }


        // 左侧tab点击事件
        private DelegateCommand<object> tabLeftBannersCommand;
        public DelegateCommand<object> TabLeftBannersCommand => tabLeftBannersCommand ?? (tabLeftBannersCommand = new DelegateCommand<object>(ExecuteTabLeftBannersCommand));

        /// <summary>
        /// 左侧tab点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteTabLeftBannersCommand(object parameter)
        {
            if (!(parameter is TabLeftBanner banner)) { return; }

            NavigationParameters param = new NavigationParameters()
            {
               { "object", banner.Object },
               { "mid", mid },
            };

            switch (banner.Id)
            {
                case 0:
                    regionManager.RequestNavigate("UserSpaceContentRegion", ViewArchiveViewModel.Tag, param);
                    break;
                case 1:
                    regionManager.RequestNavigate("UserSpaceContentRegion", UserSpace.ViewChannelViewModel.Tag, param);
                    break;
            }
        }

        #endregion

        /// <summary>
        /// 初始化页面
        /// </summary>
        private void InitView()
        {
            TopNavigationBg = "#00FFFFFF"; // 透明
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");
            Background = null;

            Header = null;
            UserName = "";
            Sex = null;
            Level = null;
            VipTypeVisibility = Visibility.Collapsed;
            VipType = "";
            Sign = "";

            TabLeftBanners.Clear();
            TabRightBanners.Clear();

            // 将内容置空，使其不指向任何页面
            regionManager.RequestNavigate("UserSpaceContentRegion", "");

            ContentVisibility = Visibility.Collapsed;
            ViewVisibility = Visibility.Collapsed;
            LoadingVisibility = Visibility.Visible;
            NoDataVisibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        private async void UpdateSpaceInfo()
        {
            bool isNoData = true;
            Uri toutuUri = null;
            string headerUri = null;
            Uri sexUri = null;
            Uri levelUri = null;

            await Task.Run(() =>
            {
                // 背景图片
                SpaceSettings spaceSettings = Core.BiliApi.Users.UserSpace.GetSpaceSettings(mid);
                if (spaceSettings != null)
                {
                    StorageCover storageCover = new StorageCover();
                    string toutu = storageCover.GetCover($"https://i0.hdslb.com/{spaceSettings.Toutu.Limg}");
                    toutuUri = new Uri(toutu);
                }
                else
                {
                    toutuUri = new Uri("pack://application:,,,/Resources/backgound/9-绿荫秘境.png");
                }

                // 用户信息
                UserInfoForSpace userInfo = UserInfo.GetUserInfoForSpace(mid);
                if (userInfo != null)
                {
                    isNoData = false;

                    // 头像
                    StorageHeader storageHeader = new StorageHeader();
                    headerUri = storageHeader.GetHeader(mid, userInfo.Name, userInfo.Face);
                    // 用户名
                    UserName = userInfo.Name;
                    // 性别
                    if (userInfo.Sex == "男")
                    {
                        sexUri = new Uri($"pack://application:,,,/Resources/sex/male.png");
                    }
                    else if (userInfo.Sex == "女")
                    {
                        sexUri = new Uri($"pack://application:,,,/Resources/sex/female.png");
                    }
                    // 显示vip信息
                    if (userInfo.Vip.Label.Text == null || userInfo.Vip.Label.Text == "")
                    {
                        VipTypeVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        VipTypeVisibility = Visibility.Visible;
                        VipType = userInfo.Vip.Label.Text;
                    }
                    // 等级
                    levelUri = new Uri($"pack://application:,,,/Resources/level/lv{userInfo.Level}.png");
                    // 签名
                    Sign = userInfo.Sign;

                    // 是否关注此UP
                    IsFollowed = userInfo.IsFollowed ?
                    DictionaryResource.GetString("Followed") : DictionaryResource.GetString("NotFollowed");
                }
                else
                {
                    // 没有数据
                    isNoData = true;
                }
            });

            // 是否获取到数据
            if (isNoData)
            {
                TopNavigationBg = "#00FFFFFF"; // 透明
                ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");
                Background = null;

                ViewVisibility = Visibility.Collapsed;
                LoadingVisibility = Visibility.Collapsed;
                NoDataVisibility = Visibility.Visible;
                return;
            }
            else
            {
                // 头像
                StorageHeader storageHeader = new StorageHeader();
                Header = storageHeader.GetHeaderThumbnail(headerUri, 64, 64);
                // 性别
                Sex = sexUri == null ? null : new BitmapImage(sexUri);
                // 等级
                Level = new BitmapImage(levelUri);

                ArrowBack.Fill = DictionaryResource.GetColor("ColorText");
                TopNavigationBg = DictionaryResource.GetColor("ColorMask100");
                Background = new BitmapImage(toutuUri);

                ViewVisibility = Visibility.Visible;
                LoadingVisibility = Visibility.Collapsed;
                NoDataVisibility = Visibility.Collapsed;
            }

            ContentVisibility = Visibility.Visible;

            // 投稿视频
            List<SpacePublicationListTypeVideoZone> publicationTypes = null;
            await Task.Run(() =>
            {
                publicationTypes = Core.BiliApi.Users.UserSpace.GetPublicationType(mid);
            });
            if (publicationTypes != null && publicationTypes.Count > 0)
            {
                TabLeftBanners.Add(new TabLeftBanner
                {
                    Object = publicationTypes,
                    Id = 0,
                    Icon = NormalIcon.Instance().VideoUp,
                    IconColor = "#FF02B5DA",
                    Title = DictionaryResource.GetString("Publication"),
                    IsSelected = true
                });
            }

            // 频道
            List<SpaceChannelList> channelList = null;
            await Task.Run(() =>
            {
                channelList = Core.BiliApi.Users.UserSpace.GetChannelList(mid);
            });
            if (channelList != null && channelList.Count > 0)
            {
                TabLeftBanners.Add(new TabLeftBanner
                {
                    Object = channelList,
                    Id = 1,
                    Icon = NormalIcon.Instance().Channel,
                    IconColor = "#FF23C9ED",
                    Title = DictionaryResource.GetString("Channel")
                });
            }

            // 收藏夹
            // 订阅

            // 关系状态数
            UserRelationStat relationStat = null;
            await Task.Run(() =>
            {
                relationStat = UserStatus.GetUserRelationStat(mid);
            });
            if (relationStat != null)
            {
                TabRightBanners.Add(new TabRightBanner
                {
                    IsEnabled = true,
                    LabelColor = DictionaryResource.GetColor("ColorPrimary"),
                    CountColor = DictionaryResource.GetColor("ColorPrimary"),
                    Label = DictionaryResource.GetString("FollowingCount"),
                    Count = Format.FormatNumber(relationStat.Following)
                });
                TabRightBanners.Add(new TabRightBanner
                {
                    IsEnabled = true,
                    LabelColor = DictionaryResource.GetColor("ColorPrimary"),
                    CountColor = DictionaryResource.GetColor("ColorPrimary"),
                    Label = DictionaryResource.GetString("FollowerCount"),
                    Count = Format.FormatNumber(relationStat.Follower)
                });
            }

            // UP主状态数，需要任意用户登录，否则不会返回任何数据
            UpStat upStat = null;
            await Task.Run(() =>
            {
                upStat = UserStatus.GetUpStat(mid);
            });
            if (upStat != null && upStat.Archive != null && upStat.Article != null)
            {
                TabRightBanners.Add(new TabRightBanner
                {
                    IsEnabled = false,
                    LabelColor = DictionaryResource.GetColor("ColorTextGrey"),
                    CountColor = DictionaryResource.GetColor("ColorTextDark"),
                    Label = DictionaryResource.GetString("LikesCount"),
                    Count = Format.FormatNumber(upStat.Likes)
                });

                long archiveView = 0;
                if (upStat.Archive != null)
                {
                    archiveView = upStat.Archive.View;
                }
                TabRightBanners.Add(new TabRightBanner
                {
                    IsEnabled = false,
                    LabelColor = DictionaryResource.GetColor("ColorTextGrey"),
                    CountColor = DictionaryResource.GetColor("ColorTextDark"),
                    Label = DictionaryResource.GetString("ArchiveViewCount"),
                    Count = Format.FormatNumber(archiveView)
                });

                long articleView = 0;
                if (upStat.Article != null)
                {
                    articleView = upStat.Article.View;
                }
                TabRightBanners.Add(new TabRightBanner
                {
                    IsEnabled = false,
                    LabelColor = DictionaryResource.GetColor("ColorTextGrey"),
                    CountColor = DictionaryResource.GetColor("ColorTextDark"),
                    Label = DictionaryResource.GetString("ArticleViewCount"),
                    Count = Format.FormatNumber(articleView)
                });
            }

        }

        /// <summary>
        /// 接收mid参数
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            // 根据传入参数不同执行不同任务
            long parameter = navigationContext.Parameters.GetValue<long>("Parameter");
            if (parameter == 0)
            {
                return;
            }
            mid = parameter;

            InitView();
            UpdateSpaceInfo();
        }

    }
}
