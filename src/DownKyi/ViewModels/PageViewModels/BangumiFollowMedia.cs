using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels.PageViewModels
{
    public class BangumiFollowMedia : BindableBase
    {
        protected readonly IEventAggregator eventAggregator;

        public BangumiFollowMedia(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        // media id
        public long MediaId { get; set; }

        // season id
        public long SeasonId { get; set; }

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

        // 视频类型名称
        private string seasonTypeName;
        public string SeasonTypeName
        {
            get => seasonTypeName;
            set => SetProperty(ref seasonTypeName, value);
        }

        // 地区
        private string area;
        public string Area
        {
            get => area;
            set => SetProperty(ref area, value);
        }

        // 标记是否会员
        private string badge;
        public string Badge
        {
            get => badge;
            set => SetProperty(ref badge, value);
        }

        // 简介
        private string evaluate;
        public string Evaluate
        {
            get => evaluate;
            set => SetProperty(ref evaluate, value);
        }

        // 视频更新进度
        private string indexShow;
        public string IndexShow
        {
            get => indexShow;
            set => SetProperty(ref indexShow, value);
        }

        // 观看进度
        private string progress;
        public string Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
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

            NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, tag, $"{ParseEntrance.BangumiMediaUrl}md{MediaId}");
        }

        #endregion

    }
}
