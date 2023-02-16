using DownKyi.Core.Settings;
using DownKyi.Events;
using DownKyi.Models;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace DownKyi.ViewModels.Settings
{
    public class ViewAboutViewModel : BaseViewModel
    {
        public const string Tag = "PageSettingsAbout";

        private bool isOnNavigatedTo;

        #region 页面属性申明

        private string appName;
        public string AppName
        {
            get { return appName; }
            set { SetProperty(ref appName, value); }
        }

        private string appVersion;
        public string AppVersion
        {
            get { return appVersion; }
            set { SetProperty(ref appVersion, value); }
        }

        private bool isReceiveBetaVersion;
        public bool IsReceiveBetaVersion
        {
            get { return isReceiveBetaVersion; }
            set { SetProperty(ref isReceiveBetaVersion, value); }
        }

        private bool autoUpdateWhenLaunch;
        public bool AutoUpdateWhenLaunch
        {
            get { return autoUpdateWhenLaunch; }
            set { SetProperty(ref autoUpdateWhenLaunch, value); }
        }

        #endregion

        public ViewAboutViewModel(IEventAggregator eventAggregator, IDialogService dialogService) : base(eventAggregator, dialogService)
        {

            #region 属性初始化

            AppInfo app = new AppInfo();
            AppName = app.Name;
            AppVersion = app.VersionName;

            #endregion

        }

        /// <summary>
        /// 导航到页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            isOnNavigatedTo = true;

            // 是否接收测试版更新
            var isReceiveBetaVersion = SettingsManager.GetInstance().IsReceiveBetaVersion();
            IsReceiveBetaVersion = isReceiveBetaVersion == AllowStatus.YES;

            // 是否在启动时自动检查更新
            var isAutoUpdateWhenLaunch = SettingsManager.GetInstance().GetAutoUpdateWhenLaunch();
            AutoUpdateWhenLaunch = isAutoUpdateWhenLaunch == AllowStatus.YES;

            isOnNavigatedTo = false;
        }

        #region 命令申明

        // 访问主页事件
        private DelegateCommand appNameCommand;
        public DelegateCommand AppNameCommand => appNameCommand ?? (appNameCommand = new DelegateCommand(ExecuteAppNameCommand));

        /// <summary>
        /// 访问主页事件
        /// </summary>
        private void ExecuteAppNameCommand()
        {
            System.Diagnostics.Process.Start("https://github.com/leiurayer/downkyi");
        }

        // 检查更新事件
        private DelegateCommand checkUpdateCommand;
        public DelegateCommand CheckUpdateCommand => checkUpdateCommand ?? (checkUpdateCommand = new DelegateCommand(ExecuteCheckUpdateCommand));

        /// <summary>
        /// 检查更新事件
        /// </summary>
        private void ExecuteCheckUpdateCommand()
        {
            //eventAggregator.GetEvent<MessageEvent>().Publish("开始查找更新，请稍后~");
            eventAggregator.GetEvent<MessageEvent>().Publish("请前往主页下载最新版~");
        }

        // 意见反馈事件
        private DelegateCommand feedbackCommand;
        public DelegateCommand FeedbackCommand => feedbackCommand ?? (feedbackCommand = new DelegateCommand(ExecuteFeedbackCommand));

        /// <summary>
        /// 意见反馈事件
        /// </summary>
        private void ExecuteFeedbackCommand()
        {
            System.Diagnostics.Process.Start("https://github.com/leiurayer/downkyi/issues");
        }

        // 是否接收测试版更新事件
        private DelegateCommand receiveBetaVersionCommand;
        public DelegateCommand ReceiveBetaVersionCommand => receiveBetaVersionCommand ?? (receiveBetaVersionCommand = new DelegateCommand(ExecuteReceiveBetaVersionCommand));

        /// <summary>
        /// 是否接收测试版更新事件
        /// </summary>
        private void ExecuteReceiveBetaVersionCommand()
        {
            AllowStatus isReceiveBetaVersion = IsReceiveBetaVersion ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().IsReceiveBetaVersion(isReceiveBetaVersion);
            PublishTip(isSucceed);
        }

        // 是否在启动时自动检查更新事件
        private DelegateCommand autoUpdateWhenLaunchCommand;
        public DelegateCommand AutoUpdateWhenLaunchCommand => autoUpdateWhenLaunchCommand ?? (autoUpdateWhenLaunchCommand = new DelegateCommand(ExecuteAutoUpdateWhenLaunchCommand));

        /// <summary>
        /// 是否在启动时自动检查更新事件
        /// </summary>
        private void ExecuteAutoUpdateWhenLaunchCommand()
        {
            AllowStatus isAutoUpdateWhenLaunch = AutoUpdateWhenLaunch ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().SetAutoUpdateWhenLaunch(isAutoUpdateWhenLaunch);
            PublishTip(isSucceed);
        }

        // Brotli.NET许可证查看事件
        private DelegateCommand brotliLicenseCommand;
        public DelegateCommand BrotliLicenseCommand => brotliLicenseCommand ?? (brotliLicenseCommand = new DelegateCommand(ExecuteBrotliLicenseCommand));

        /// <summary>
        /// Brotli.NET许可证查看事件
        /// </summary>
        private void ExecuteBrotliLicenseCommand()
        {
            System.Diagnostics.Process.Start("https://licenses.nuget.org/MIT");
        }

        // Google.Protobuf许可证查看事件
        private DelegateCommand protobufLicenseCommand;
        public DelegateCommand ProtobufLicenseCommand => protobufLicenseCommand ?? (protobufLicenseCommand = new DelegateCommand(ExecuteProtobufLicenseCommand));

        /// <summary>
        /// Google.Protobuf许可证查看事件
        /// </summary>
        private void ExecuteProtobufLicenseCommand()
        {
            System.Diagnostics.Process.Start("https://github.com/protocolbuffers/protobuf/blob/master/LICENSE");
        }

        // Newtonsoft.Json许可证查看事件
        private DelegateCommand newtonsoftLicenseCommand;
        public DelegateCommand NewtonsoftLicenseCommand => newtonsoftLicenseCommand ?? (newtonsoftLicenseCommand = new DelegateCommand(ExecuteNewtonsoftLicenseCommand));

        /// <summary>
        /// Newtonsoft.Json许可证查看事件
        /// </summary>
        private void ExecuteNewtonsoftLicenseCommand()
        {
            System.Diagnostics.Process.Start("https://licenses.nuget.org/MIT");
        }

        // Prism.DryIoc许可证查看事件
        private DelegateCommand prismLicenseCommand;
        public DelegateCommand PrismLicenseCommand => prismLicenseCommand ?? (prismLicenseCommand = new DelegateCommand(ExecutePrismLicenseCommand));

        /// <summary>
        /// Prism.DryIoc许可证查看事件
        /// </summary>
        private void ExecutePrismLicenseCommand()
        {
            System.Diagnostics.Process.Start("https://www.nuget.org/packages/Prism.DryIoc/8.1.97/license");
        }

        // QRCoder许可证查看事件
        private DelegateCommand qRCoderLicenseCommand;
        public DelegateCommand QRCoderLicenseCommand => qRCoderLicenseCommand ?? (qRCoderLicenseCommand = new DelegateCommand(ExecuteQRCoderLicenseCommand));

        /// <summary>
        /// QRCoder许可证查看事件
        /// </summary>
        private void ExecuteQRCoderLicenseCommand()
        {
            System.Diagnostics.Process.Start("https://licenses.nuget.org/MIT");
        }

        // System.Data.SQLite.Core许可证查看事件
        private DelegateCommand sQLiteLicenseCommand;
        public DelegateCommand SQLiteLicenseCommand => sQLiteLicenseCommand ?? (sQLiteLicenseCommand = new DelegateCommand(ExecuteSQLiteLicenseCommand));

        /// <summary>
        /// System.Data.SQLite.Core许可证查看事件
        /// </summary>
        private void ExecuteSQLiteLicenseCommand()
        {
            System.Diagnostics.Process.Start("https://www.sqlite.org/copyright.html");
        }

        // Aria2c许可证查看事件
        private DelegateCommand ariaLicenseCommand;
        public DelegateCommand AriaLicenseCommand => ariaLicenseCommand ?? (ariaLicenseCommand = new DelegateCommand(ExecuteAriaLicenseCommand));

        /// <summary>
        /// Aria2c许可证查看事件
        /// </summary>
        private void ExecuteAriaLicenseCommand()
        {
            System.Diagnostics.Process.Start("aria2_COPYING.txt");
        }

        // FFmpeg许可证查看事件
        private DelegateCommand fFmpegLicenseCommand;
        public DelegateCommand FFmpegLicenseCommand => fFmpegLicenseCommand ?? (fFmpegLicenseCommand = new DelegateCommand(ExecuteFFmpegLicenseCommand));

        /// <summary>
        /// FFmpeg许可证查看事件
        /// </summary>
        private void ExecuteFFmpegLicenseCommand()
        {
            System.Diagnostics.Process.Start("FFmpeg_LICENSE.txt");
        }

        #endregion

        /// <summary>
        /// 发送需要显示的tip
        /// </summary>
        /// <param name="isSucceed"></param>
        private void PublishTip(bool isSucceed)
        {
            if (isOnNavigatedTo) { return; }

            if (isSucceed)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipSettingUpdated"));
            }
            else
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipSettingFailed"));
            }
        }

    }
}
