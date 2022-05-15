using DownKyi.Core.Settings;
using DownKyi.Events;
using DownKyi.Models;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;

namespace DownKyi.ViewModels.Settings
{
    public class ViewBasicViewModel : BaseViewModel
    {
        public const string Tag = "PageSettingsBasic";

        private bool isOnNavigatedTo;

        #region 页面属性申明

        private bool none;
        public bool None
        {
            get { return none; }
            set { SetProperty(ref none, value); }
        }

        private bool closeApp;
        public bool CloseApp
        {
            get { return closeApp; }
            set { SetProperty(ref closeApp, value); }
        }

        private bool closeSystem;
        public bool CloseSystem
        {
            get { return closeSystem; }
            set { SetProperty(ref closeSystem, value); }
        }

        private bool listenClipboard;
        public bool ListenClipboard
        {
            get { return listenClipboard; }
            set { SetProperty(ref listenClipboard, value); }
        }

        private bool autoParseVideo;
        public bool AutoParseVideo
        {
            get { return autoParseVideo; }
            set { SetProperty(ref autoParseVideo, value); }
        }

        private List<ParseScopeDisplay> parseScopes;
        public List<ParseScopeDisplay> ParseScopes
        {
            get { return parseScopes; }
            set { SetProperty(ref parseScopes, value); }
        }

        private ParseScopeDisplay selectedParseScope;
        public ParseScopeDisplay SelectedParseScope
        {
            get { return selectedParseScope; }
            set { SetProperty(ref selectedParseScope, value); }
        }

        private bool autoDownloadAll;
        public bool AutoDownloadAll
        {
            get => autoDownloadAll;
            set => SetProperty(ref autoDownloadAll, value);
        }

        #endregion

        public ViewBasicViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

            #region 属性初始化

            // 解析范围
            ParseScopes = new List<ParseScopeDisplay>()
            {
                new ParseScopeDisplay{ Name = DictionaryResource.GetString("ParseNone"), ParseScope = ParseScope.NONE },
                new ParseScopeDisplay{ Name = DictionaryResource.GetString("ParseSelectedItem"), ParseScope = ParseScope.SELECTED_ITEM },
                new ParseScopeDisplay{ Name = DictionaryResource.GetString("ParseCurrentSection"), ParseScope = ParseScope.CURRENT_SECTION },
                new ParseScopeDisplay{ Name = DictionaryResource.GetString("ParseAll"), ParseScope = ParseScope.ALL }
            };

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

            // 下载完成后的操作
            AfterDownloadOperation afterDownload = SettingsManager.GetInstance().GetAfterDownloadOperation();
            SetAfterDownloadOperation(afterDownload);

            // 是否监听剪贴板
            AllowStatus isListenClipboard = SettingsManager.GetInstance().IsListenClipboard();
            ListenClipboard = isListenClipboard == AllowStatus.YES;

            // 是否自动解析视频
            AllowStatus isAutoParseVideo = SettingsManager.GetInstance().IsAutoParseVideo();
            AutoParseVideo = isAutoParseVideo == AllowStatus.YES;

            // 解析范围
            ParseScope parseScope = SettingsManager.GetInstance().GetParseScope();
            SelectedParseScope = ParseScopes.FirstOrDefault(t => { return t.ParseScope == parseScope; });

            // 解析后是否自动下载解析视频
            AllowStatus isAutoDownloadAll = SettingsManager.GetInstance().IsAutoDownloadAll();
            AutoDownloadAll = isAutoDownloadAll == AllowStatus.YES;

            isOnNavigatedTo = false;
        }

        #region 命令申明

        // 下载完成后的操作事件
        private DelegateCommand<string> afterDownloadOperationCommand;
        public DelegateCommand<string> AfterDownloadOperationCommand => afterDownloadOperationCommand ?? (afterDownloadOperationCommand = new DelegateCommand<string>(ExecuteAfterDownloadOperationCommand));

        /// <summary>
        /// 下载完成后的操作事件
        /// </summary>
        private void ExecuteAfterDownloadOperationCommand(string parameter)
        {
            AfterDownloadOperation afterDownload;
            switch (parameter)
            {
                case "None":
                    afterDownload = AfterDownloadOperation.NONE;
                    break;
                case "CloseApp":
                    afterDownload = AfterDownloadOperation.CLOSE_APP;
                    break;
                case "CloseSystem":
                    afterDownload = AfterDownloadOperation.CLOSE_SYSTEM;
                    break;
                default:
                    afterDownload = AfterDownloadOperation.NONE;
                    break;
            }

            bool isSucceed = SettingsManager.GetInstance().SetAfterDownloadOperation(afterDownload);
            PublishTip(isSucceed);
        }

        // 是否监听剪贴板事件
        private DelegateCommand listenClipboardCommand;
        public DelegateCommand ListenClipboardCommand => listenClipboardCommand ?? (listenClipboardCommand = new DelegateCommand(ExecuteListenClipboardCommand));

        /// <summary>
        /// 是否监听剪贴板事件
        /// </summary>
        private void ExecuteListenClipboardCommand()
        {
            AllowStatus isListenClipboard = ListenClipboard ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().IsListenClipboard(isListenClipboard);
            PublishTip(isSucceed);
        }

        private DelegateCommand autoParseVideoCommand;
        public DelegateCommand AutoParseVideoCommand => autoParseVideoCommand ?? (autoParseVideoCommand = new DelegateCommand(ExecuteAutoParseVideoCommand));

        /// <summary>
        /// 是否自动解析视频
        /// </summary>
        private void ExecuteAutoParseVideoCommand()
        {
            AllowStatus isAutoParseVideo = AutoParseVideo ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().IsAutoParseVideo(isAutoParseVideo);
            PublishTip(isSucceed);
        }

        // 解析范围事件
        private DelegateCommand<object> parseScopesCommand;
        public DelegateCommand<object> ParseScopesCommand => parseScopesCommand ?? (parseScopesCommand = new DelegateCommand<object>(ExecuteParseScopesCommand));

        /// <summary>
        /// 解析范围事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteParseScopesCommand(object parameter)
        {
            if (!(parameter is ParseScopeDisplay parseScope)) { return; }

            bool isSucceed = SettingsManager.GetInstance().SetParseScope(parseScope.ParseScope);
            PublishTip(isSucceed);
        }

        // 解析后是否自动下载解析视频
        private DelegateCommand autoDownloadAllCommand;
        public DelegateCommand AutoDownloadAllCommand => autoDownloadAllCommand ?? (autoDownloadAllCommand = new DelegateCommand(ExecuteAutoDownloadAllCommand));

        /// <summary>
        /// 解析后是否自动下载解析视频
        /// </summary>
        private void ExecuteAutoDownloadAllCommand()
        {
            AllowStatus isAutoDownloadAll = AutoDownloadAll ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().IsAutoDownloadAll(isAutoDownloadAll);
            PublishTip(isSucceed);
        }

        #endregion

        /// <summary>
        /// 设置下载完成后的操作
        /// </summary>
        /// <param name="afterDownload"></param>
        private void SetAfterDownloadOperation(AfterDownloadOperation afterDownload)
        {
            switch (afterDownload)
            {
                case AfterDownloadOperation.NONE:
                    None = true;
                    break;
                case AfterDownloadOperation.OPEN_FOLDER:
                    break;
                case AfterDownloadOperation.CLOSE_APP:
                    CloseApp = true;
                    break;
                case AfterDownloadOperation.CLOSE_SYSTEM:
                    CloseSystem = true;
                    break;
            }
        }

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
