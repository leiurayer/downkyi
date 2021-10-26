using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Utils;
using DownKyi.ViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace DownKyi.Models
{
    public class FavoritesMedia : BindableBase
    {
        protected readonly IEventAggregator eventAggregator;

        public FavoritesMedia(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public long Avid { get; set; }
        public string Bvid { get; set; }
        public long UpperMid { get; set; }

        #region 页面属性申明

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }

        private int order;
        public int Order
        {
            get { return order; }
            set { SetProperty(ref order, value); }
        }

        private BitmapImage cover;
        public BitmapImage Cover
        {
            get { return cover; }
            set { SetProperty(ref cover, value); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string playNumber;
        public string PlayNumber
        {
            get { return playNumber; }
            set { SetProperty(ref playNumber, value); }
        }

        private string danmakuNumber;
        public string DanmakuNumber
        {
            get { return danmakuNumber; }
            set { SetProperty(ref danmakuNumber, value); }
        }

        private string favoriteNumber;
        public string FavoriteNumber
        {
            get { return favoriteNumber; }
            set { SetProperty(ref favoriteNumber, value); }
        }

        private string duration;
        public string Duration
        {
            get { return duration; }
            set { SetProperty(ref duration, value); }
        }

        private string upName;
        public string UpName
        {
            get { return upName; }
            set { SetProperty(ref upName, value); }
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

        // 视频的UP主点击事件
        private DelegateCommand<object> videoUpperCommand;
        public DelegateCommand<object> VideoUpperCommand => videoUpperCommand ?? (videoUpperCommand = new DelegateCommand<object>(ExecuteVideoUpperCommand));

        /// <summary>
        /// 视频的UP主点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteVideoUpperCommand(object parameter)
        {
            if (!(parameter is string tag)) { return; }

            NavigateToView.NavigateToViewUserSpace(eventAggregator, tag, UpperMid);
        }

        #endregion

    }
}
