using DownKyi.Core.Logging;
using DownKyi.Core.Settings;
using DownKyi.Core.Settings.Models;
using DownKyi.Events;
using DownKyi.ViewModels;
using Prism.Events;

namespace DownKyi.Utils
{
    public static class NavigateToView
    {
        public static string Tag = "NavigateToView";

        /// <summary>
        /// 导航到用户空间，
        /// 如果传入的mid与本地登录的mid一致，
        /// 则进入我的用户空间。
        /// </summary>
        /// <param name="mid"></param>
        public static void NavigateToViewUserSpace(IEventAggregator eventAggregator, string parentViewName, long mid)
        {
            UserInfoSettings userInfo = SettingsManager.GetInstance().GetUserInfo();
            if (userInfo != null && userInfo.Mid == mid)
            {
                NavigationView(eventAggregator, ViewMySpaceViewModel.Tag, parentViewName, mid);
            }
            else
            {
                NavigationView(eventAggregator, ViewUserSpaceViewModel.Tag, parentViewName, mid);
            }
        }

        /// <summary>
        /// 导航到其他页面
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="param"></param>
        public static void NavigationView(IEventAggregator eventAggregator, string viewName, string parentViewName, object param)
        {
            LogManager.Debug(Tag, $"NavigationView: {viewName}, Parameter: {param}");

            NavigationParam parameter = new NavigationParam
            {
                ViewName = viewName,
                ParentViewName = parentViewName,
                Parameter = param
            };
            eventAggregator.GetEvent<NavigationEvent>().Publish(parameter);
        }

    }
}
