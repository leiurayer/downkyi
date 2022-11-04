using DownKyi.Core.BiliApi.Users;
using DownKyi.Core.BiliApi.Users.Models;
using DownKyi.Utils;
using DownKyi.ViewModels.PageViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DownKyi.ViewModels.Friends
{
    public class ViewFollowingViewModel : BaseViewModel
    {
        public const string Tag = "PageFriendsFollowing";

        // mid
        private long mid = -1;

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

            TabHeaders = new ObservableCollection<TabHeader>();

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
        /// 初始化左侧列表
        /// </summary>
        private async void InitLeftTable()
        {
            TabHeaders.Clear();

            // 用户的关系状态数
            UserRelationStat relationStat = null;
            await Task.Run(() =>
            {
                relationStat = UserStatus.GetUserRelationStat(mid);
            });
            if (relationStat != null)
            {
                TabHeaders.Add(new TabHeader { Id = -1, Title = DictionaryResource.GetString("AllFollowing"), SubTitle = relationStat.Following.ToString() });
                TabHeaders.Add(new TabHeader { Id = -2, Title = DictionaryResource.GetString("WhisperFollowing"), SubTitle = relationStat.Whisper.ToString() });
            }

            // 用户的关注分组
            List<FollowingGroup> followingGroup = null;
            await Task.Run(() =>
            {
                followingGroup = UserRelation.GetFollowingGroup();
            });
            if (followingGroup != null)
            {
                foreach (FollowingGroup tag in followingGroup)
                {
                    TabHeaders.Add(new TabHeader { Id = tag.TagId, Title = tag.Name, SubTitle = tag.Count.ToString() });
                }
            }
        }

        /// <summary>
        /// 导航到页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            // 传入mid
            long parameter = navigationContext.Parameters.GetValue<long>("mid");
            if (parameter == 0)
            {
                return;
            }
            mid = parameter;

            // 是否是从PageFriends的headerTable的item点击进入的
            // true表示加载PageFriends后第一次进入此页面
            // false表示从headerTable的item点击进入的
            bool isFirst = navigationContext.Parameters.GetValue<bool>("isFirst");
            if (isFirst)
            {
                // 初始化左侧列表
                InitLeftTable();

                // 进入页面时显示的设置项
                SelectTabId = 0;
            }
        }

    }
}
