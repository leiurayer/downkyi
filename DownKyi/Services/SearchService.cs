using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Utils;
using DownKyi.ViewModels;
using Prism.Events;

namespace DownKyi.Services
{
    public class SearchService
    {
        /// <summary>
        /// 解析支持的输入
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
