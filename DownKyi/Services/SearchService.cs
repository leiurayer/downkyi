using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Utils;
using DownKyi.ViewModels;
using Prism.Events;

namespace DownKyi.Services
{
    public class SearchService
    {
        /// <summary>
        /// 解析支持的输入，
        /// 支持的格式有：<para/>
        /// av号：av170001, AV170001, https://www.bilibili.com/video/av170001 <para/>
        /// BV号：BV17x411w7KC, https://www.bilibili.com/video/BV17x411w7KC, https://b23.tv/BV17x411w7KC <para/>
        /// 番剧（电影、电视剧）ss号：ss32982, SS32982, https://www.bilibili.com/bangumi/play/ss32982 <para/>
        /// 番剧（电影、电视剧）ep号：ep317925, EP317925, https://www.bilibili.com/bangumi/play/ep317925 <para/>
        /// 番剧（电影、电视剧）md号：md28228367, MD28228367, https://www.bilibili.com/bangumi/media/md28228367 <para/>
        /// 课程ss号：https://www.bilibili.com/cheese/play/ss205 <para/>
        /// 课程ep号：https://www.bilibili.com/cheese/play/ep3489 <para/>
        /// 收藏夹：ml1329019876, ML1329019876, https://www.bilibili.com/medialist/detail/ml1329019876, https://www.bilibili.com/medialist/play/ml1329019876/ <para/>
        /// 用户空间：uid928123, UID928123, uid:928123, UID:928123, https://space.bilibili.com/928123
        /// </summary>
        /// <param name="input"></param>
        /// <param name="parentViewName"></param>
        /// <param name="eventAggregator"></param>
        /// <returns></returns>
        public bool BiliInput(string input, string parentViewName, IEventAggregator eventAggregator)
        {
            // 视频
            if (ParseEntrance.IsAvId(input))
            {
                NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, parentViewName, $"{ParseEntrance.VideoUrl}{input.ToLower()}");
            }
            else if (ParseEntrance.IsAvUrl(input))
            {
                NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, parentViewName, input);
            }
            else if (ParseEntrance.IsBvId(input))
            {
                NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, parentViewName, $"{ParseEntrance.VideoUrl}{input}");
            }
            else if (ParseEntrance.IsBvUrl(input))
            {
                NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, parentViewName, input);
            }
            // 番剧（电影、电视剧）
            else if (ParseEntrance.IsBangumiSeasonId(input))
            {
                NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, parentViewName, $"{ParseEntrance.BangumiUrl}{input.ToLower()}");
            }
            else if (ParseEntrance.IsBangumiSeasonUrl(input))
            {
                NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, parentViewName, input);
            }
            else if (ParseEntrance.IsBangumiEpisodeId(input))
            {
                NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, parentViewName, $"{ParseEntrance.BangumiUrl}{input.ToLower()}");
            }
            else if (ParseEntrance.IsBangumiEpisodeUrl(input))
            {
                NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, parentViewName, input);
            }
            else if (ParseEntrance.IsBangumiMediaId(input))
            {
                NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, parentViewName, $"{ParseEntrance.BangumiMediaUrl}{input.ToLower()}");
            }
            else if (ParseEntrance.IsBangumiMediaUrl(input))
            {
                NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, parentViewName, input);
            }
            // 课程
            else if (ParseEntrance.IsCheeseSeasonUrl(input) || ParseEntrance.IsCheeseEpisodeUrl(input))
            {
                NavigateToView.NavigationView(eventAggregator, ViewVideoDetailViewModel.Tag, parentViewName, input);
            }
            // 用户（参数传入mid）
            else if (ParseEntrance.IsUserId(input))
            {
                NavigateToView.NavigateToViewUserSpace(eventAggregator, ViewIndexViewModel.Tag, ParseEntrance.GetUserId(input));
            }
            else if (ParseEntrance.IsUserUrl(input))
            {
                NavigateToView.NavigateToViewUserSpace(eventAggregator, ViewIndexViewModel.Tag, ParseEntrance.GetUserId(input));
            }
            // 收藏夹
            else if (ParseEntrance.IsFavoritesId(input))
            {
                NavigateToView.NavigationView(eventAggregator, ViewPublicFavoritesViewModel.Tag, parentViewName, ParseEntrance.GetFavoritesId(input));
            }
            else if (ParseEntrance.IsFavoritesUrl(input))
            {
                NavigateToView.NavigationView(eventAggregator, ViewPublicFavoritesViewModel.Tag, parentViewName, ParseEntrance.GetFavoritesId(input));
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 搜索关键词
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parentViewName"></param>
        /// <param name="eventAggregator"></param>
        public void SearchKey(string key, string parentViewName, IEventAggregator eventAggregator)
        {
            // TODO
        }


    }
}
