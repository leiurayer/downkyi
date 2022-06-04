using DownKyi.ViewModels.PageViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace DownKyi.ViewModels.Friend
{
    public class ViewFollowerViewModel : BaseViewModel
    {
        public const string Tag = "PageFriendFollower";

        #region 页面属性申明

        private ObservableCollection<TabHeader> tabHeaders;
        public ObservableCollection<TabHeader> TabHeaders
        {
            get => tabHeaders;
            set => SetProperty(ref tabHeaders, value);
        }

        private int selectTabId;
        public int SelectTabId
        {
            get => selectTabId;
            set => SetProperty(ref selectTabId, value);
        }

        #endregion

        public ViewFollowerViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化
            TabHeaders = new ObservableCollection<TabHeader>();

            int i = TabHeaders.Count;

            #endregion
        }

        #region 命令申明

        // 左侧tab点击事件
        private DelegateCommand<object> leftTabHeadersCommand;
        public DelegateCommand<object> LeftTabHeadersCommand => leftTabHeadersCommand ?? (leftTabHeadersCommand = new DelegateCommand<object>(ExecuteLeftTabHeadersCommand));

        /// <summary>
        /// 左侧tab点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteLeftTabHeadersCommand(object parameter)
        {
            if (!(parameter is TabHeader tabHeader)) { return; }

            //NavigationParameters param = new NavigationParameters();

            //switch (tabHeader.Id)
            //{
            //    case 0:
            //        regionManager.RequestNavigate("ToolboxContentRegion", ViewBiliHelperViewModel.Tag, param);
            //        break;
            //    case 1:
            //        regionManager.RequestNavigate("ToolboxContentRegion", ViewDelogoViewModel.Tag, param);
            //        break;
            //    case 2:
            //        regionManager.RequestNavigate("ToolboxContentRegion", ViewExtractMediaViewModel.Tag, param);
            //        break;
            //}
        }

        #endregion

        /// <summary>
        /// 导航到页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            // 进入设置页面时显示的设置项
            SelectTabId = 0;

        }

    }
}
