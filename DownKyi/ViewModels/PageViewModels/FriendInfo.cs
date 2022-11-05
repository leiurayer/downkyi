using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels.PageViewModels
{
    public class FriendInfo : BindableBase
    {
        protected readonly IEventAggregator eventAggregator;

        public FriendInfo(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public long Mid { get; set; }

        #region 页面属性申明

        private BitmapImage header;
        public BitmapImage Header
        {
            get => header;
            set => SetProperty(ref header, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string sign;
        public string Sign
        {
            get => sign;
            set => SetProperty(ref sign, value);
        }

        #endregion

        #region 命令申明

        // 视频标题点击事件
        private DelegateCommand<object> userCommand;
        public DelegateCommand<object> UserCommand => userCommand ?? (userCommand = new DelegateCommand<object>(ExecuteUserCommand));

        /// <summary>
        /// 视频标题点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteUserCommand(object parameter)
        {
            if (!(parameter is string tag)) { return; }

            NavigateToView.NavigationView(eventAggregator, ViewUserSpaceViewModel.Tag, tag, Mid);
        }

        #endregion
    }
}
