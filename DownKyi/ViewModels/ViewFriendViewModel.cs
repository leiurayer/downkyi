using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Utils;
using DownKyi.ViewModels.Friend;
using DownKyi.ViewModels.PageViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DownKyi.ViewModels
{
    public class ViewFriendViewModel : BaseViewModel
    {
        public const string Tag = "PageFriend";

        private readonly IRegionManager regionManager;

        private long mid = -1;

        #region 页面属性申明

        private VectorImage arrowBack;
        public VectorImage ArrowBack
        {
            get => arrowBack;
            set => SetProperty(ref arrowBack, value);
        }

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

        public ViewFriendViewModel(IRegionManager regionManager, IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.regionManager = regionManager;

            #region 属性初始化

            ArrowBack = NavigationIcon.Instance().ArrowBack;
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            TabHeaders = new ObservableCollection<TabHeader>
            {
                new TabHeader { Id = 0, Title = DictionaryResource.GetString("FriendFollowing") },
                new TabHeader { Id = 1, Title = DictionaryResource.GetString("FriendFollower") },
            };

            #endregion
        }

        #region 命令申明

        // 返回事件
        private DelegateCommand backSpaceCommand;
        public DelegateCommand BackSpaceCommand => backSpaceCommand ?? (backSpaceCommand = new DelegateCommand(ExecuteBackSpace));

        /// <summary>
        /// 返回事件
        /// </summary>
        private void ExecuteBackSpace()
        {
            //InitView();

            ArrowBack.Fill = DictionaryResource.GetColor("ColorText");

            NavigationParam parameter = new NavigationParam
            {
                ViewName = ParentView,
                ParentViewName = null,
                Parameter = null
            };
            eventAggregator.GetEvent<NavigationEvent>().Publish(parameter);
        }

        // 顶部tab点击事件
        private DelegateCommand<object> tabHeadersCommand;
        public DelegateCommand<object> TabHeadersCommand => tabHeadersCommand ?? (tabHeadersCommand = new DelegateCommand<object>(ExecuteTabHeadersCommand));

        /// <summary>
        /// 顶部tab点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteTabHeadersCommand(object parameter)
        {
            if (!(parameter is TabHeader tabHeader)) { return; }

            NavigationView(tabHeader.Id);
        }

        #endregion

        /// <summary>
        /// 进入子页面
        /// </summary>
        /// <param name="id"></param>
        private void NavigationView(int id)
        {
            NavigationParameters param = new NavigationParameters()
            {
               { "mid", mid },
            };

            switch (id)
            {
                case 0:
                    regionManager.RequestNavigate("FriendContentRegion", ViewFollowerViewModel.Tag, param);
                    break;
                case 1:
                    regionManager.RequestNavigate("FriendContentRegion", ViewFollowingViewModel.Tag, param);
                    break;
            }
        }

        /// <summary>
        /// 导航到VideoDetail页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            // 根据传入参数不同执行不同任务
            var parameter = navigationContext.Parameters.GetValue<Dictionary<string, object>>("Parameter");
            if (parameter == null)
            {
                return;
            }

            mid = (long)parameter["mid"];
            SelectTabId = (int)parameter["friendId"];

            NavigationView(SelectTabId);
        }

    }
}
