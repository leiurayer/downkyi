using DownKyi.Core.BiliApi.Login;
using DownKyi.Core.Logging;
using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels
{
    public class ViewLoginViewModel : BaseViewModel
    {
        public const string Tag = "PageLogin";

        private CancellationTokenSource tokenSource;

        #region 页面属性申明

        private VectorImage arrowBack;
        public VectorImage ArrowBack
        {
            get { return arrowBack; }
            set { SetProperty(ref arrowBack, value); }
        }

        private BitmapImage loginQRCode;
        public BitmapImage LoginQRCode
        {
            get { return loginQRCode; }
            set { SetProperty(ref loginQRCode, value); }
        }

        private double loginQRCodeOpacity;
        public double LoginQRCodeOpacity
        {
            get { return loginQRCodeOpacity; }
            set { SetProperty(ref loginQRCodeOpacity, value); }
        }

        private Visibility loginQRCodeStatus;
        public Visibility LoginQRCodeStatus
        {
            get { return loginQRCodeStatus; }
            set { SetProperty(ref loginQRCodeStatus, value); }
        }

        #endregion


        public ViewLoginViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化

            ArrowBack = NavigationIcon.Instance().ArrowBack;
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

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
            // 初始化状态
            InitStatus();

            // 结束任务
            tokenSource.Cancel();

            NavigationParam parameter = new NavigationParam
            {
                ViewName = ParentView,
                ParentViewName = null,
                Parameter = "login"
            };
            eventAggregator.GetEvent<NavigationEvent>().Publish(parameter);
        }

        #endregion

        #region 业务逻辑

        /// <summary>
        /// 登录
        /// </summary>
        private void Login()
        {
            var loginUrl = LoginQR.GetLoginUrl();
            if (loginUrl == null) { return; }

            if (loginUrl.Status != true)
            {
                ExecuteBackSpace();
                return;
            }

            if (loginUrl.Data == null || loginUrl.Data.Url == null)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("GetLoginUrlFailed"));
                return;
            }

            PropertyChangeAsync(new Action(() => { LoginQRCode = LoginQR.GetLoginQRCode(loginUrl.Data.Url); }));
            Core.Utils.Debugging.Console.PrintLine(loginUrl.Data.Url + "\n");
            LogManager.Debug(Tag, loginUrl.Data.Url);

            GetLoginStatus(loginUrl.Data.OauthKey);
        }

        /// <summary>
        /// 循环查询登录状态
        /// </summary>
        /// <param name="oauthKey"></param>
        private void GetLoginStatus(string oauthKey)
        {
            CancellationToken cancellationToken = tokenSource.Token;
            while (true)
            {
                Thread.Sleep(1000);
                var loginStatus = LoginQR.GetLoginStatus(oauthKey);
                if (loginStatus == null) { continue; }

                Core.Utils.Debugging.Console.PrintLine(loginStatus.Code + "\n" + loginStatus.Message + "\n" + loginStatus.Url + "\n");

                switch (loginStatus.Code)
                {
                    case -1:
                        // 没有这个oauthKey

                        // 发送通知
                        eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("LoginKeyError"));
                        LogManager.Info(Tag, DictionaryResource.GetString("LoginKeyError"));

                        // 取消任务
                        tokenSource.Cancel();

                        // 创建新任务
                        PropertyChangeAsync(new Action(() => { Task.Run(Login, (tokenSource = new CancellationTokenSource()).Token); }));
                        break;
                    case -2:
                        // 不匹配的oauthKey，超时或已确认的oauthKey

                        // 发送通知
                        eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("LoginTimeOut"));
                        LogManager.Info(Tag, DictionaryResource.GetString("LoginTimeOut"));

                        // 取消任务
                        tokenSource.Cancel();

                        // 创建新任务
                        PropertyChangeAsync(new Action(() => { Task.Run(Login, (tokenSource = new CancellationTokenSource()).Token); }));
                        break;
                    case -4:
                        // 未扫码
                        break;
                    case -5:
                        // 已扫码，未确认
                        PropertyChangeAsync(new Action(() =>
                        {
                            LoginQRCodeStatus = Visibility.Visible;
                            LoginQRCodeOpacity = 0.3;
                        }));
                        break;
                    case 0:
                        // 确认登录

                        // 发送通知
                        eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("LoginSuccessful"));
                        LogManager.Info(Tag, DictionaryResource.GetString("LoginSuccessful"));

                        // 保存登录信息
                        try
                        {
                            bool isSucceed = LoginHelper.SaveLoginInfoCookies(loginStatus.Url);
                            if (!isSucceed)
                            {
                                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("LoginFailed"));
                                LogManager.Error(Tag, DictionaryResource.GetString("LoginFailed"));
                            }
                        }
                        catch (Exception e)
                        {
                            Core.Utils.Debugging.Console.PrintLine("PageLogin 保存登录信息发生异常: {0}", e);
                            LogManager.Error(e);
                            eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("LoginFailed"));
                        }

                        // TODO 其他操作


                        // 取消任务
                        Thread.Sleep(3000);
                        PropertyChangeAsync(new Action(() => { ExecuteBackSpace(); }));
                        break;
                }

                // 判断是否该结束线程，若为true，跳出while循环
                if (cancellationToken.IsCancellationRequested)
                {
                    Core.Utils.Debugging.Console.PrintLine("停止Login线程，跳出while循环");
                    LogManager.Debug(Tag, "登录操作结束");
                    break;
                }
            }
        }

        /// <summary>
        /// 初始化状态
        /// </summary>
        private void InitStatus()
        {
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            LoginQRCode = null;
            LoginQRCodeOpacity = 1;
            LoginQRCodeStatus = Visibility.Hidden;
        }

        #endregion

        /// <summary>
        /// 导航到Login页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            // 初始化状态
            InitStatus();

            Task.Run(Login, (tokenSource = new CancellationTokenSource()).Token);
            //await loginTask;
        }


    }
}
