using DownKyi.Images;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels.PageViewModels
{
    public class HistoryMedia : BindableBase
    {
        protected readonly IEventAggregator eventAggregator;

        public HistoryMedia(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        // bvid
        public string Bvid { get; set; }

        // 播放url
        public string Url { get; set; }

        // UP主的mid
        public long UpMid { get; set; }

        // 类型
        public string Business { get; set; }

        #region 页面属性申明

        // 是否选中
        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        // 封面
        private BitmapImage cover;
        public BitmapImage Cover
        {
            get => cover;
            set => SetProperty(ref cover, value);
        }

        // 视频标题
        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        // 分P的标题
        private string subTitle;
        public string SubTitle
        {
            get => subTitle;
            set => SetProperty(ref subTitle, value);
        }

        // 时长
        private long duration;
        public long Duration
        {
            get => duration;
            set => SetProperty(ref duration, value);
        }

        // tag标签
        private string tagName;
        public string TagName
        {
            get => tagName;
            set => SetProperty(ref tagName, value);
        }

        // new_desc 剧集或分P描述
        private string partdesc;
        public string Partdesc
        {
            get => partdesc;
            set => SetProperty(ref partdesc, value);
        }

        // 观看进度
        private string progress;
        public string Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }

        // 观看平台
        private VectorImage platform;
        public VectorImage Platform
        {
            get => platform;
            set => SetProperty(ref platform, value);
        }

        // UP主的昵称
        private string upName;
        public string UpName
        {
            get => upName;
            set => SetProperty(ref upName, value);
        }

        // UP主的头像
        private BitmapImage upHeader;
        public BitmapImage UpHeader
        {
            get => upHeader;
            set => SetProperty(ref upHeader, value);
        }

        // 是否显示Partdesc
        private Visibility partdescVisibility;
        public Visibility PartdescVisibility
        {
            get => partdescVisibility;
            set => SetProperty(ref partdescVisibility, value);
        }

        // 是否显示UP主信息和分区信息
        private Visibility upAndTagVisibility;
        public Visibility UpAndTagVisibility
        {
            get => upAndTagVisibility;
            set => SetProperty(ref upAndTagVisibility, value);
        }

        #endregion

        #region 命令申明

        // 视频标题点击事件
        private DelegateCommand<object> titleCommand;
        public DelegateCommand<object> TitleCommand => titleCommand ?? (titleCommand = new DelegateCommand<object>(ExecuteTitleCommand));

        /// <summary>
        /// 视频标题点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteTitleCommand(object parameter)
        {
            if (!(parameter is string tag)) { return; }

            NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, tag, Url);
        }

        // UP主头像点击事件
        private DelegateCommand<object> upCommand;
        public DelegateCommand<object> UpCommand => upCommand ?? (upCommand = new DelegateCommand<object>(ExecuteUpCommand));

        /// <summary>
        /// UP主头像点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteUpCommand(object parameter)
        {
            if (!(parameter is string tag)) { return; }

            NavigateToView.NavigateToViewUserSpace(eventAggregator, tag, UpMid);
        }

        #endregion

    }
}
