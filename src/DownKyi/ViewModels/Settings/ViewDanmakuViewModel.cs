using DownKyi.Core.Settings;
using DownKyi.Core.Utils.Validator;
using DownKyi.Events;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Drawing.Text;

namespace DownKyi.ViewModels.Settings
{
    public class ViewDanmakuViewModel : BaseViewModel
    {
        public const string Tag = "PageSettingsDanmaku";

        private bool isOnNavigatedTo;

        #region 页面属性申明

        private bool topFilter;
        public bool TopFilter
        {
            get { return topFilter; }
            set { SetProperty(ref topFilter, value); }
        }

        private bool bottomFilter;
        public bool BottomFilter
        {
            get { return bottomFilter; }
            set { SetProperty(ref bottomFilter, value); }
        }

        private bool scrollFilter;
        public bool ScrollFilter
        {
            get { return scrollFilter; }
            set { SetProperty(ref scrollFilter, value); }
        }

        private int screenWidth;
        public int ScreenWidth
        {
            get { return screenWidth; }
            set { SetProperty(ref screenWidth, value); }
        }

        private int screenHeight;
        public int ScreenHeight
        {
            get { return screenHeight; }
            set { SetProperty(ref screenHeight, value); }
        }

        private List<string> fonts;
        public List<string> Fonts
        {
            get { return fonts; }
            set { SetProperty(ref fonts, value); }
        }

        private string selectedFont;
        public string SelectedFont
        {
            get { return selectedFont; }
            set { SetProperty(ref selectedFont, value); }
        }

        private int fontSize;
        public int FontSize
        {
            get { return fontSize; }
            set { SetProperty(ref fontSize, value); }
        }

        private int lineCount;
        public int LineCount
        {
            get { return lineCount; }
            set { SetProperty(ref lineCount, value); }
        }

        private bool sync;
        public bool Sync
        {
            get { return sync; }
            set { SetProperty(ref sync, value); }
        }

        private bool async;
        public bool Async
        {
            get { return async; }
            set { SetProperty(ref async, value); }
        }

        #endregion

        public ViewDanmakuViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

            #region 属性初始化

            // 弹幕字体
            Fonts = new List<string>();
            var fontCollection = new InstalledFontCollection();
            foreach (var font in fontCollection.Families)
            {
                Fonts.Add(font.Name);
            }

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

            // 屏蔽顶部弹幕
            AllowStatus danmakuTopFilter = SettingsManager.GetInstance().GetDanmakuTopFilter();
            TopFilter = danmakuTopFilter == AllowStatus.YES;

            // 屏蔽底部弹幕
            AllowStatus danmakuBottomFilter = SettingsManager.GetInstance().GetDanmakuBottomFilter();
            BottomFilter = danmakuBottomFilter == AllowStatus.YES;

            // 屏蔽滚动弹幕
            AllowStatus danmakuScrollFilter = SettingsManager.GetInstance().GetDanmakuScrollFilter();
            ScrollFilter = danmakuScrollFilter == AllowStatus.YES;

            // 分辨率-宽
            ScreenWidth = SettingsManager.GetInstance().GetDanmakuScreenWidth();

            // 分辨率-高
            ScreenHeight = SettingsManager.GetInstance().GetDanmakuScreenHeight();

            // 弹幕字体
            string danmakuFont = SettingsManager.GetInstance().GetDanmakuFontName();
            if (danmakuFont != null && Fonts.Contains(danmakuFont))
            {
                // 只有系统中存在当前设置的字体，才能显示
                SelectedFont = danmakuFont;
            }

            // 弹幕字体大小
            FontSize = SettingsManager.GetInstance().GetDanmakuFontSize();

            // 弹幕限制行数
            LineCount = SettingsManager.GetInstance().GetDanmakuLineCount();

            // 弹幕布局算法
            DanmakuLayoutAlgorithm layoutAlgorithm = SettingsManager.GetInstance().GetDanmakuLayoutAlgorithm();
            SetLayoutAlgorithm(layoutAlgorithm);

            isOnNavigatedTo = false;
        }

        #region 命令申明

        // 屏蔽顶部弹幕事件
        private DelegateCommand topFilterCommand;
        public DelegateCommand TopFilterCommand => topFilterCommand ?? (topFilterCommand = new DelegateCommand(ExecuteTopFilterCommand));

        /// <summary>
        /// 屏蔽顶部弹幕事件
        /// </summary>
        private void ExecuteTopFilterCommand()
        {
            AllowStatus isTopFilter = TopFilter ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().SetDanmakuTopFilter(isTopFilter);
            PublishTip(isSucceed);
        }

        // 屏蔽底部弹幕事件
        private DelegateCommand bottomFilterCommand;
        public DelegateCommand BottomFilterCommand => bottomFilterCommand ?? (bottomFilterCommand = new DelegateCommand(ExecuteBottomFilterCommand));

        /// <summary>
        /// 屏蔽底部弹幕事件
        /// </summary>
        private void ExecuteBottomFilterCommand()
        {
            AllowStatus isBottomFilter = BottomFilter ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().SetDanmakuBottomFilter(isBottomFilter);
            PublishTip(isSucceed);
        }

        // 屏蔽滚动弹幕事件
        private DelegateCommand scrollFilterCommand;
        public DelegateCommand ScrollFilterCommand => scrollFilterCommand ?? (scrollFilterCommand = new DelegateCommand(ExecuteScrollFilterCommand));

        /// <summary>
        /// 屏蔽滚动弹幕事件
        /// </summary>
        private void ExecuteScrollFilterCommand()
        {
            AllowStatus isScrollFilter = ScrollFilter ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().SetDanmakuScrollFilter(isScrollFilter);
            PublishTip(isSucceed);
        }

        // 设置分辨率-宽事件
        private DelegateCommand<string> screenWidthCommand;
        public DelegateCommand<string> ScreenWidthCommand => screenWidthCommand ?? (screenWidthCommand = new DelegateCommand<string>(ExecuteScreenWidthCommand));

        /// <summary>
        /// 设置分辨率-宽事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteScreenWidthCommand(string parameter)
        {
            int width = (int)Number.GetInt(parameter);
            ScreenWidth = width;

            bool isSucceed = SettingsManager.GetInstance().SetDanmakuScreenWidth(ScreenWidth);
            PublishTip(isSucceed);
        }

        // 设置分辨率-高事件
        private DelegateCommand<string> screenHeightCommand;
        public DelegateCommand<string> ScreenHeightCommand => screenHeightCommand ?? (screenHeightCommand = new DelegateCommand<string>(ExecuteScreenHeightCommand));

        /// <summary>
        /// 设置分辨率-高事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteScreenHeightCommand(string parameter)
        {
            int height = (int)Number.GetInt(parameter);
            ScreenHeight = height;

            bool isSucceed = SettingsManager.GetInstance().SetDanmakuScreenHeight(ScreenHeight);
            PublishTip(isSucceed);
        }

        // 弹幕字体选择事件
        private DelegateCommand<string> fontSelectCommand;
        public DelegateCommand<string> FontSelectCommand => fontSelectCommand ?? (fontSelectCommand = new DelegateCommand<string>(ExecuteFontSelectCommand));

        /// <summary>
        /// 弹幕字体选择事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteFontSelectCommand(string parameter)
        {
            bool isSucceed = SettingsManager.GetInstance().SetDanmakuFontName(parameter);
            PublishTip(isSucceed);
        }

        // 弹幕字体大小事件
        private DelegateCommand<string> fontSizeCommand;
        public DelegateCommand<string> FontSizeCommand => fontSizeCommand ?? (fontSizeCommand = new DelegateCommand<string>(ExecuteFontSizeCommand));

        /// <summary>
        /// 弹幕字体大小事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteFontSizeCommand(string parameter)
        {
            int fontSize = (int)Number.GetInt(parameter);
            FontSize = fontSize;

            bool isSucceed = SettingsManager.GetInstance().SetDanmakuFontSize(FontSize);
            PublishTip(isSucceed);
        }

        // 弹幕限制行数事件
        private DelegateCommand<string> lineCountCommand;
        public DelegateCommand<string> LineCountCommand => lineCountCommand ?? (lineCountCommand = new DelegateCommand<string>(ExecuteLineCountCommand));

        /// <summary>
        /// 弹幕限制行数事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteLineCountCommand(string parameter)
        {
            int lineCount = (int)Number.GetInt(parameter);
            LineCount = lineCount;

            bool isSucceed = SettingsManager.GetInstance().SetDanmakuLineCount(LineCount);
            PublishTip(isSucceed);
        }

        // 弹幕布局算法事件
        private DelegateCommand<string> layoutAlgorithmCommand;
        public DelegateCommand<string> LayoutAlgorithmCommand => layoutAlgorithmCommand ?? (layoutAlgorithmCommand = new DelegateCommand<string>(ExecuteLayoutAlgorithmCommand));

        /// <summary>
        /// 弹幕布局算法事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteLayoutAlgorithmCommand(string parameter)
        {
            DanmakuLayoutAlgorithm layoutAlgorithm;
            switch (parameter)
            {
                case "Sync":
                    layoutAlgorithm = DanmakuLayoutAlgorithm.SYNC;
                    break;
                case "Async":
                    layoutAlgorithm = DanmakuLayoutAlgorithm.ASYNC;
                    break;
                default:
                    layoutAlgorithm = DanmakuLayoutAlgorithm.SYNC;
                    break;
            }

            bool isSucceed = SettingsManager.GetInstance().SetDanmakuLayoutAlgorithm(layoutAlgorithm);
            PublishTip(isSucceed);

            if (isSucceed)
            {
                SetLayoutAlgorithm(layoutAlgorithm);
            }
        }

        #endregion

        /// <summary>
        /// 设置弹幕同步算法
        /// </summary>
        /// <param name="layoutAlgorithm"></param>
        private void SetLayoutAlgorithm(DanmakuLayoutAlgorithm layoutAlgorithm)
        {
            switch (layoutAlgorithm)
            {
                case DanmakuLayoutAlgorithm.SYNC:
                    Sync = true;
                    Async = false;
                    break;
                case DanmakuLayoutAlgorithm.ASYNC:
                    Sync = false;
                    Async = true;
                    break;
                case DanmakuLayoutAlgorithm.NONE:
                    Sync = false;
                    Async = false;
                    break;
                default:
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
