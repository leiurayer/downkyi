using DownKyi.Core.BiliApi.Users;
using DownKyi.Core.BiliApi.Users.Models;
using DownKyi.Utils;
using DownKyi.ViewModels.PageViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DownKyi.ViewModels.Friends
{
    public class ViewFollowingViewModel : BaseViewModel
    {
        public const string Tag = "PageFriendsFollowing";

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

        public ViewFollowingViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化
            List<FollowingGroup> followingGroup = UserRelation.GetFollowingGroup();

            TabHeaders = new ObservableCollection<TabHeader>()
            {
                //new TabHeader{Id = 0, Title = DictionaryResource.GetString("FriendFollowing") },
                new TabHeader{Id = -1, Title = "全部关注" },
                new TabHeader{Id = -2, Title = "悄悄关注" },
            };

            foreach (FollowingGroup tag in followingGroup)
            {
                TabHeaders.Add(new TabHeader { Id = tag.TagId, Title = tag.Name, SubTitle = tag.Count.ToString() });
            }

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
