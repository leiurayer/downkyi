using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace DownKyi.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IEventAggregator eventAggregator;
        private ClipboardHooker clipboardHooker;

        #region 页面属性申明

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private WindowState winState;
        public WindowState WinState
        {
            get { return winState; }
            set
            {
                if (value == WindowState.Maximized)
                {
                    ResizeIcon = SystemIcon.Instance().Restore;
                }
                else
                {
                    ResizeIcon = SystemIcon.Instance().Maximize;
                }
                SetLeaveStyle(ResizeIcon);

                SetProperty(ref winState, value);
            }
        }

        private VectorImage minimizeIcon;
        public VectorImage MinimizeIcon
        {
            get { return minimizeIcon; }
            set { SetProperty(ref minimizeIcon, value); }
        }

        private VectorImage resizeIcon;
        public VectorImage ResizeIcon
        {
            get { return resizeIcon; }
            set { SetProperty(ref resizeIcon, value); }
        }

        private VectorImage closeIcon;
        public VectorImage CloseIcon
        {
            get { return closeIcon; }
            set { SetProperty(ref closeIcon, value); }
        }

        private VectorImage skinIcon;
        public VectorImage SkinIcon
        {
            get { return skinIcon; }
            set { SetProperty(ref skinIcon, value); }
        }

        private Visibility messageVisibility = Visibility.Hidden;
        public Visibility MessageVisibility
        {
            get { return messageVisibility; }
            set { SetProperty(ref messageVisibility, value); }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        #endregion

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            #region 属性初始化

            Window mainWindow = Application.Current.MainWindow;

            WinState = WindowState.Normal;

            MinimizeIcon = SystemIcon.Instance().Minimize;
            ResizeIcon = SystemIcon.Instance().Maximize;
            CloseIcon = SystemIcon.Instance().Close;
            SkinIcon = SystemIcon.Instance().Skin;

            #endregion

            #region 订阅

            // 订阅导航事件
            eventAggregator.GetEvent<NavigationEvent>().Subscribe((view) =>
            {
                var param = new NavigationParameters
                {
                    { "Parent", view.ParentViewName },
                    { "Parameter", view.Parameter }
                };
                regionManager.RequestNavigate("ContentRegion", view.ViewName, param);
            });

            // 订阅消息发送事件
            string oldMessage;
            eventAggregator.GetEvent<MessageEvent>().Subscribe((message) =>
            {
                MessageVisibility = Visibility.Visible;

                oldMessage = Message;
                Message = message;
                int sleep = 2000;
                if (oldMessage == Message) { sleep = 1500; }

                Thread.Sleep(sleep);

                MessageVisibility = Visibility.Hidden;
            }, ThreadOption.BackgroundThread);

            #endregion

            #region 命令定义

            // window加载后执行的事件
            LoadedCommand = new DelegateCommand(() =>
            {
                clipboardHooker = new ClipboardHooker(Application.Current.MainWindow);
                clipboardHooker.ClipboardUpdated += OnClipboardUpdated;

                var param = new NavigationParameters
                {
                    { "Parent", "" },
                    { "Parameter", "start" }
                };
                regionManager.RequestNavigate("ContentRegion", ViewIndexViewModel.Tag, param);
            });

            // 顶部caption栏的点击事件，包括双击和拖动
            int times = 0;
            DragMoveCommand = new DelegateCommand(() =>
            {
                // caption 双击事件
                times += 1;
                DispatcherTimer timer = new DispatcherTimer
                {
                    Interval = new TimeSpan(0, 0, 0, 0, 300)
                };
                timer.Tick += (s, e) => { timer.IsEnabled = false; times = 0; };
                timer.IsEnabled = true;

                if (times % 2 == 0)
                {
                    timer.IsEnabled = false;
                    times = 0;
                    WinState = WinState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                }

                // caption 拖动事件
                try
                {
                    mainWindow.DragMove();
                }
                catch { }
            });

            // 最小化窗口事件
            MinimizeCommand = new DelegateCommand(() =>
            {
                mainWindow.WindowState = WindowState.Minimized;
            });
            MinimizeEnterCommand = new DelegateCommand(() =>
            {
                SetEnterStyle(MinimizeIcon);
            });
            MinimizeLeaveCommand = new DelegateCommand(() =>
            {
                SetLeaveStyle(MinimizeIcon);
            });

            // 最大化/还原窗口事件
            ResizeCommand = new DelegateCommand(() =>
            {
                WinState = WinState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            });
            ResizeEnterCommand = new DelegateCommand(() =>
            {
                SetEnterStyle(ResizeIcon);
            });
            ResizeLeaveCommand = new DelegateCommand(() =>
            {
                SetLeaveStyle(ResizeIcon);
            });

            // 关闭窗口事件
            CloseCommand = new DelegateCommand(() =>
            {
                if (clipboardHooker != null)
                {
                    clipboardHooker.ClipboardUpdated -= OnClipboardUpdated;
                    clipboardHooker.Dispose();
                }

                mainWindow.Close();
            });
            CloseEnterCommand = new DelegateCommand(() =>
            {
                SetEnterStyle(CloseIcon);
            });
            CloseLeaveCommand = new DelegateCommand(() =>
            {
                SetLeaveStyle(CloseIcon);
            });

            // 皮肤按钮点击事件
            SkinCommand = new DelegateCommand(() =>
            {
                // 设置主题
                DictionaryResource.LoadTheme("ThemeDiy");

                // 切换语言
                DictionaryResource.LoadLanguage("en_US");
            });
            SkinEnterCommand = new DelegateCommand(() =>
            {
                SetEnterStyle(SkinIcon);
            });
            SkinLeaveCommand = new DelegateCommand(() =>
            {
                SetLeaveStyle(SkinIcon);
            });

            #endregion

        }

        #region 命令申明

        public DelegateCommand LoadedCommand { get; private set; }
        public DelegateCommand DragMoveCommand { get; private set; }
        public DelegateCommand MinimizeCommand { get; private set; }
        public DelegateCommand MinimizeEnterCommand { get; private set; }
        public DelegateCommand MinimizeLeaveCommand { get; private set; }
        public DelegateCommand ResizeCommand { get; private set; }
        public DelegateCommand ResizeEnterCommand { get; private set; }
        public DelegateCommand ResizeLeaveCommand { get; private set; }
        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand CloseEnterCommand { get; private set; }
        public DelegateCommand CloseLeaveCommand { get; private set; }
        public DelegateCommand SkinCommand { get; private set; }
        public DelegateCommand SkinEnterCommand { get; private set; }
        public DelegateCommand SkinLeaveCommand { get; private set; }

        #endregion

        /// <summary>
        /// 鼠标进入系统按钮时的图标样式
        /// </summary>
        /// <param name="icon">图标</param>
        private void SetEnterStyle(VectorImage icon)
        {
            icon.Fill = DictionaryResource.GetColor("ColorSystemBtnTint");
        }

        /// <summary>
        /// 鼠标离开系统按钮时的图标样式
        /// </summary>
        /// <param name="icon">图标</param>
        private void SetLeaveStyle(VectorImage icon)
        {
            icon.Fill = DictionaryResource.GetColor("ColorSystemBtnTintDark");
        }

        private void OnClipboardUpdated(object sender, EventArgs e)
        {
            AllowStatus isListenClipboard = SettingsManager.GetInstance().IsListenClipboard();
            if (isListenClipboard != AllowStatus.YES)
            {
                return;
            }

            string input;
            try
            {
                IDataObject data = Clipboard.GetDataObject();
                string[] fs = data.GetFormats();
                input = data.GetData(fs[0]).ToString();
            }
            catch (Exception exc)
            {
                Console.WriteLine("OnClipboardUpdated()发生异常: {0}", exc);
                LogManager.Error("OnClipboardUpdated", exc);
                return;
            }

            // 视频
            if (ParseEntrance.IsAvId(input))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, $"{ParseEntrance.VideoUrl}{input.ToLower()}");
            }
            else if (ParseEntrance.IsAvUrl(input))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, input);
            }
            else if (ParseEntrance.IsBvId(input))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, $"{ParseEntrance.VideoUrl}{input}");
            }
            else if (ParseEntrance.IsBvUrl(input))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, input);
            }
            // 番剧（电影、电视剧）
            else if (ParseEntrance.IsBangumiSeasonId(input))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, $"{ParseEntrance.BangumiUrl}{input.ToLower()}");
            }
            else if (ParseEntrance.IsBangumiSeasonUrl(input))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, input);
            }
            else if (ParseEntrance.IsBangumiEpisodeId(input))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, $"{ParseEntrance.BangumiUrl}{input.ToLower()}");
            }
            else if (ParseEntrance.IsBangumiEpisodeUrl(input))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, input);
            }
            else if (ParseEntrance.IsBangumiMediaId(input))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, $"{ParseEntrance.BangumiMediaUrl}{input.ToLower()}");
            }
            else if (ParseEntrance.IsBangumiMediaUrl(input))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, input);
            }
            // 课程
            else if (ParseEntrance.IsCheeseSeasonUrl(input) || ParseEntrance.IsCheeseEpisodeUrl(input))
            {
                NavigationView(ViewVideoDetailViewModel.Tag, input);
            }
            // 用户（参数传入mid）
            else if (ParseEntrance.IsUserId(input))
            {
                NavigateToViewUserSpace(ParseEntrance.GetUserId(input));
            }
            else if (ParseEntrance.IsUserUrl(input))
            {
                NavigateToViewUserSpace(ParseEntrance.GetUserId(input));
            }
            // 收藏夹
            else if (ParseEntrance.IsFavoritesId(input))
            {
                NavigationView(ViewPublicFavoritesViewModel.Tag, ParseEntrance.GetFavoritesId(input));
            }
            else if (ParseEntrance.IsFavoritesUrl(input))
            {
                NavigationView(ViewPublicFavoritesViewModel.Tag, ParseEntrance.GetFavoritesId(input));
            }
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
            LogManager.Debug("OnClipboardUpdated", $"NavigationView: {viewName}, Parameter: {param}");

            NavigationParam parameter = new NavigationParam
            {
                ViewName = viewName,
                ParentViewName = ViewIndexViewModel.Tag,
                Parameter = param
            };
            eventAggregator.GetEvent<NavigationEvent>().Publish(parameter);
        }

    }
}
