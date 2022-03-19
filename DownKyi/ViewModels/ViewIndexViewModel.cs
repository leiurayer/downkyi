using DownKyi.Core.BiliApi.Users;
using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using DownKyi.Core.Settings.Models;
using DownKyi.Core.Storage;
using DownKyi.Images;
using DownKyi.Services;
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
            get => loginPanelVisibility;
            set => SetProperty(ref loginPanelVisibility, value);
        }

        private string userName;
        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        private BitmapImage header;
        public BitmapImage Header
        {
            get => header;
            set => SetProperty(ref header, value);
        }

        private VectorImage textLogo;
        public VectorImage TextLogo
        {
            get => textLogo;
            set => SetProperty(ref textLogo, value);
        }

        private string inputText;
        public string InputText
        {
            get => inputText;
            set => SetProperty(ref inputText, value);
        }

        private VectorImage generalSearch;
        public VectorImage GeneralSearch
        {
            get => generalSearch;
            set => SetProperty(ref generalSearch, value);
        }

        private VectorImage settings;
        public VectorImage Settings
        {
            get => settings;
            set => SetProperty(ref settings, value);
        }

        private VectorImage downloadManager;
        public VectorImage DownloadManager
        {
            get => downloadManager;
            set => SetProperty(ref downloadManager, value);
        }

        private VectorImage toolbox;
        public VectorImage Toolbox
        {
            get => toolbox;
            set => SetProperty(ref toolbox, value);
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
                NavigateToView.NavigationView(eventAggregator, ViewLoginViewModel.Tag, Tag, null);
            }
            else
            {
                // 进入用户空间
                var userInfo = SettingsManager.GetInstance().GetUserInfo();
                if (userInfo != null && userInfo.Mid != -1)
                {
                    NavigateToView.NavigationView(eventAggregator, ViewMySpaceViewModel.Tag, Tag, userInfo.Mid);
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
            NavigateToView.NavigationView(eventAggregator, ViewSettingsViewModel.Tag, Tag, null);
        }

        // 进入下载管理页面
        private DelegateCommand downloadManagerCommand;
        public DelegateCommand DownloadManagerCommand => downloadManagerCommand ?? (downloadManagerCommand = new DelegateCommand(ExecuteDownloadManagerCommand));

        /// <summary>
        /// 进入下载管理页面
        /// </summary>
        private void ExecuteDownloadManagerCommand()
        {
            NavigateToView.NavigationView(eventAggregator, ViewDownloadManagerViewModel.Tag, Tag, null);
        }

        // 进入工具箱页面
        private DelegateCommand toolboxCommand;
        public DelegateCommand ToolboxCommand => toolboxCommand ?? (toolboxCommand = new DelegateCommand(ExecuteToolboxCommand));

        /// <summary>
        /// 进入工具箱页面
        /// </summary>
        private void ExecuteToolboxCommand()
        {
            NavigateToView.NavigationView(eventAggregator, ViewToolboxViewModel.Tag, Tag, null);
        }

        #endregion

        #region 业务逻辑

        /// <summary>
        /// 进入B站链接的处理逻辑，
        /// 只负责处理输入，并跳转到视频详情页。<para/>
        /// 不是支持的格式，则进入搜索页面。
        /// </summary>
        private void EnterBili()
        {
            if (InputText == null || InputText == string.Empty) { return; }

            LogManager.Debug(Tag, $"InputText: {InputText}");

            SearchService searchService = new SearchService();
            bool isSupport = searchService.BiliInput(InputText, Tag, eventAggregator);
            if (!isSupport)
            {
                // 关键词搜索
                searchService.SearchKey(InputText, Tag, eventAggregator);
            }

            InputText = string.Empty;
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

            try
            {
                await Task.Run(new Action(() =>
                {
                    // 获取用户信息
                    var userInfo = UserInfo.GetUserInfoForNavigation();
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
            catch (Exception e)
            {
                Core.Utils.Debugging.Console.PrintLine("UpdateUserInfo()发生异常: {0}", e);
                LogManager.Error(Tag, e);
            }
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
