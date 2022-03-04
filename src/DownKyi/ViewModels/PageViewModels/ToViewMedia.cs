using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels.PageViewModels
{
    public class ToViewMedia : BindableBase
    {
        protected readonly IEventAggregator eventAggregator;

        public ToViewMedia(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        // aid
        public long Aid { get; set; }

        // bvid
        public string Bvid { get; set; }

        // UP主的mid
        public long UpMid { get; set; }

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

            NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, tag, $"{ParseEntrance.VideoUrl}{Bvid}");
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
