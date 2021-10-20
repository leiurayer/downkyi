using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.Login;
using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using DownKyi.Core.Settings.Models;
using DownKyi.Core.Storage;
using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels
{
    public class ViewIndexViewModel : BaseViewModel
    {
        public const string Tag = "PageIndex";

        #region 页面属性申明

        private Visibility loginPanelVisibility;
        public Visibility LoginPanelVisibility
        {
            get { return loginPanelVisibility; }
            set { SetProperty(ref loginPanelVisibility, value); }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private BitmapImage header;
        public BitmapImage Header
        {
            get { return header; }
            set { SetProperty(ref header, value); }
        }

        private VectorImage textLogo;
        public VectorImage TextLogo
        {
            get { return textLogo; }
            set { SetProperty(ref textLogo, value); }
        }

        private string inputText;
        public string InputText
        {
            get { return inputText; }
            set { SetProperty(ref inputText, value); }
        }

        private VectorImage generalSearch;
        public VectorImage GeneralSearch
        {
            get { return generalSearch; }
            set { SetProperty(ref generalSearch, value); }
        }

        private VectorImage settings;
        public VectorImage Settings
        {
            get { return settings; }
            set { SetProperty(ref settings, value); }
        }

        private VectorImage downloadManager;
        public VectorImage DownloadManager
        {
            get { return downloadManager; }
            set { SetProperty(ref downloadManager, value); }
        }

        private VectorImage toolbox;
        public VectorImage Toolbox
        {
            get { return toolbox; }
            set { SetProperty(ref toolbox, value); }
        }

        #endregion


        public ViewIndexViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化

            Header = new BitmapImage(new Uri("pack://application:,,,/Resources/default_header.jpg"));

            TextLogo = LogoIcon.Instance().TextLogo;
            TextLogo.Fill = DictionaryResource.GetColor("ColorPrimary");

            GeneralSearch = ButtonIcon.Instance().GeneralSearch;
            GeneralSearch.Fill = DictionaryResource.GetColor("ColorPrimary");

            Settings = ButtonIcon.Instance().Settings;
            Settings.Fill = DictionaryResource.GetColor("ColorPrimary");

            DownloadManager = ButtonIcon.Instance().DownloadManage;
            DownloadManager.Fill = DictionaryResource.GetColor("ColorPrimary");

            Toolbox = ButtonIcon.Instance().Toolbox;
            Toolbox.Fill = DictionaryResource.GetColor("ColorPrimary");

            #endregion

        }

        #region 命令申明

        // View加载后执行的事件
        public DelegateCommand loadedCommand;
        public DelegateCommand LoadedCommand => loadedCommand ?? (loadedCommand = new DelegateCommand(ExecuteViewLoaded));

        /// <summary>
        /// View加载后执行
        /// </summary>
        private void ExecuteViewLoaded() { }

        // 输入确认事件
        public DelegateCommand inputCommand;
        public DelegateCommand InputCommand => inputCommand ?? (inputCommand = new DelegateCommand(ExecuteInput));

        /// <summary>
        /// 处理输入事件
        /// </summary>
        private void ExecuteInput()
        {
            EnterBili();
        }

        // 登录事件
        private DelegateCommand loginCommand;
        public DelegateCommand LoginCommand => loginCommand ?? (loginCommand = new DelegateCommand(ExecuteLogin));

        /// <summary>
        /// 进入登录页面
        /// </summary>
        private void ExecuteLogin()
        {
            if (UserName == null)
            {
                NavigationView(ViewLoginViewModel.Tag, null);
            }
            else
            {
                // 进入用户空间
                var userInfo = SettingsManager.GetInstance().GetUserInfo();
                if (userInfo != null && userInfo.Mid != -1)
                {
                    NavigationView(ViewMySpaceViewModel.Tag, userInfo.Mid);
                }
            }
        }

        // 进入设置页面
        private DelegateCommand settingsCommand;
        public DelegateCommand SettingsCommand => settingsCommand ?? (settingsCommand = new DelegateCommand(ExecuteSettingsCommand));

        /// <summary>
        /// 进入设置页面
        /// </summary>
        private void ExecuteSettingsCommand()
        {
            NavigationView(ViewSettingsViewModel.Tag, null);
        }

        // 进入下载管理页面
        private DelegateCommand downloadManagerCommand;
        public DelegateCommand DownloadManagerCommand => downloadManagerCommand ?? (downloadManagerCommand = new DelegateCommand(ExecuteDownloadManagerCommand));

        /// <summary>
        /// 进入下载管理页面
        /// </summary>
        private void ExecuteDownloadManagerCommand()
        {
            NavigationView(ViewDownloadManagerViewModel.Tag, null);
        }

        // 进入工具箱页面
        private DelegateCommand toolboxCommand;
        public DelegateCommand ToolboxCommand => toolboxCommand ?? (toolboxCommand = new DelegateCommand(ExecuteToolboxCommand));

        /// <summary>
        /// 进入工具箱页面
        /// </summary>
        private void ExecuteToolboxCommand()
        {
            NavigationView(ViewToolboxViewModel.Tag, null);
        }

        #endregion


        #region 业务逻辑

        /// <summary>
        /// 进入B站链接的处理逻辑，
        /// 只负责处理输入，并跳转到视频详情页。<para/>
        /// 不是支持的格式，则进入搜索页面。
        /// 支持的格式有：<para/>
        /// av号：av170001, AV170001, https://www.bilibili.com/video/av170001 <para/>
        /// BV号：BV17x411w7KC, https://www.bilibili.com/video/BV17x411w7KC <para/>
        /// 番剧（电影、电视剧）ss号：ss32982, SS32982, https://www.bilibili.com/bangumi/play/ss32982 <para/>
        /// 番剧（电影、电视剧）ep号：ep317925, EP317925, https://www.bilibili.com/bangumi/play/ep317925 <para/>
        /// 番剧（电影、电视剧）md号：md28228367, MD28228367, https://www.bilibili.com/bangumi/media/md28228367 <para/>
        /// 课程ss号：https://www.bilibili.com/cheese/play/ss205 <para/>
        /// 课程ep号：https://www.bilibili.com/cheese/play/ep3489 <para/>
        /// 收藏夹：ml1329019876, ML1329019876, https://www.bilibili.com/medialist/detail/ml1329019876 <para/>
        /// 用户空间：uid928123, UID928123, uid:928123, UID:928123, https://space.bilibili.com/928123
        /// </summary>
        private void EnterBili()
        {
            if (InputText == null || InputText == string.Empty) { return; }

            LogManager.Debug(Tag, $"InputText: {InputText}");

            // 视频
            if (ParseEntrance.IsAvId(InputText))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, $"{ParseEntrance.VideoUrl}{InputText.ToLower()}");
            }
            else if (ParseEntrance.IsAvUrl(InputText))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, InputText);
            }
            else if (ParseEntrance.IsBvId(InputText))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, $"{ParseEntrance.VideoUrl}{InputText}");
            }
            else if (ParseEntrance.IsBvUrl(InputText))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, InputText);
            }
            // 番剧（电影、电视剧）
            else if (ParseEntrance.IsBangumiSeasonId(InputText))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, $"{ParseEntrance.BangumiUrl}{InputText.ToLower()}");
            }
            else if (ParseEntrance.IsBangumiSeasonUrl(InputText))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, InputText);
            }
            else if (ParseEntrance.IsBangumiEpisodeId(InputText))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, $"{ParseEntrance.BangumiUrl}{InputText.ToLower()}");
            }
            else if (ParseEntrance.IsBangumiEpisodeUrl(InputText))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, InputText);
            }
            else if (ParseEntrance.IsBangumiMediaId(InputText))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, $"{ParseEntrance.BangumiMediaUrl}{InputText.ToLower()}");
            }
            else if (ParseEntrance.IsBangumiMediaUrl(InputText))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, InputText);
            }
            // 课程
            else if (ParseEntrance.IsCheeseSeasonUrl(InputText) || ParseEntrance.IsCheeseEpisodeUrl(InputText))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, InputText);
            }
            // 用户（参数传入mid）
            else if (ParseEntrance.IsUserId(InputText))
            {
                NavigateToViewUserSpace(ParseEntrance.GetUserId(InputText));
            }
            else if (ParseEntrance.IsUserUrl(InputText))
            {
                NavigateToViewUserSpace(ParseEntrance.GetUserId(InputText));
            }
            // 收藏夹
            else if (ParseEntrance.IsFavoritesId(InputText))
            {
                NavigationView(ViewPublicFavoritesViewModel.Tag, ParseEntrance.GetFavoritesId(InputText));
            }
            else if (ParseEntrance.IsFavoritesUrl(InputText))
            {
                NavigationView(ViewPublicFavoritesViewModel.Tag, ParseEntrance.GetFavoritesId(InputText));
            }
            // TODO 关键词搜索
            else
            {
            }

            InputText = string.Empty;
        }

        /// <summary>
        /// 导航到用户空间，
        /// 如果传入的mid与本地登录的mid一致，
        /// 则进入我的用户空间。
        /// </summary>
        /// <param name="mid"></param>
        private void NavigateToViewUserSpace(long mid)
        {
            var userInfo = SettingsManager.GetInstance().GetUserInfo();
            if (userInfo != null && userInfo.Mid == mid)
            {
                NavigationView(ViewMySpaceViewModel.Tag, mid);
            }
            else
            {
                NavigationView(ViewUserSpaceViewModel.Tag, mid);
            }
        }

        /// <summary>
        /// 导航到其他页面
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="param"></param>
        private void NavigationView(string viewName, object param)
        {
            LogManager.Debug(Tag, $"NavigationView: {viewName}, Parameter: {param}");

            NavigationParam parameter = new NavigationParam
            {
                ViewName = viewName,
                ParentViewName = Tag,
                Parameter = param
            };
            eventAggregator.GetEvent<NavigationEvent>().Publish(parameter);
        }

        /// <summary>
        /// 更新用户登录信息
        /// </summary>
        private async void UpdateUserInfo()
        {
            LoginPanelVisibility = Visibility.Hidden;

            // 检查本地是否存在login文件，没有则说明未登录
            if (!File.Exists(StorageManager.GetLogin()))
            {
                LoginPanelVisibility = Visibility.Visible;
                Header = new BitmapImage(new Uri("pack://application:,,,/Resources/default_header.jpg"));
                UserName = null;
                return;
            }

            await Task.Run(new Action(() =>
            {
                // 获取用户信息
                var userInfo = LoginInfo.GetUserInfoForNavigation();
                if (userInfo != null)
                {
                    SettingsManager.GetInstance().SetUserInfo(new UserInfoSettings
                    {
                        Mid = userInfo.Mid,
                        Name = userInfo.Name,
                        IsLogin = userInfo.IsLogin,
                        IsVip = userInfo.VipStatus == 1
                    });
                }
                else
                {
                    SettingsManager.GetInstance().SetUserInfo(new UserInfoSettings
                    {
                        Mid = -1,
                        Name = "",
                        IsLogin = false,
                        IsVip = false
                    });
                }

                PropertyChangeAsync(new Action(() =>
                {
                    LoginPanelVisibility = Visibility.Visible;

                    if (userInfo != null)
                    {
                        if (userInfo.Face != null)
                        {
                            Header = new StorageHeader().GetHeaderThumbnail(userInfo.Mid, userInfo.Name, userInfo.Face, 36, 36);
                        }
                        else
                        {
                            Header = new BitmapImage(new Uri("pack://application:,,,/Resources/default_header.jpg"));
                        }
                        UserName = userInfo.Name;
                    }
                    else
                    {
                        Header = new BitmapImage(new Uri("pack://application:,,,/Resources/default_header.jpg"));
                        UserName = null;
                    }
                }));
            }));
        }

        #endregion

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            DownloadManager = ButtonIcon.Instance().DownloadManage;
            DownloadManager.Height = 27;
            DownloadManager.Width = 32;
            DownloadManager.Fill = DictionaryResource.GetColor("ColorPrimary");

            // 根据传入参数不同执行不同任务
            string parameter = navigationContext.Parameters.GetValue<string>("Parameter");
            if (parameter == null)
            {
                return;
            }
            // 启动
            if (parameter == "start")
            {
                UpdateUserInfo();
            }
            // 从登录页面返回
            if (parameter == "login")
            {
                UpdateUserInfo();
            }
            // 注销
            if (parameter == "logout")
            {
                UpdateUserInfo();
            }

        }

    }
}
