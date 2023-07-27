using Downkyi.Core.Bili.Utils;
using Downkyi.Core.Settings;
using Downkyi.Core.Settings.Models;
using Downkyi.UI.ViewModels.User;
using Downkyi.UI.ViewModels.Video;

namespace Downkyi.UI.Services;

public class MainSearchService : IMainSearchService
{
    private readonly INavigationService NavigationService;

    public MainSearchService(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }

    public bool BiliInput(string input)
    {
        // 移除剪贴板id
        //string validId = input.Replace(AppConstant.ClipboardId, "");
        
        string validId = input;

        // 参数
        Dictionary<string, object> parameter = new()
        {
            { "key", "MainSearchService" },
            { "value", new object() },
        };

        // 视频
        if (ParseEntrance.IsAvId(validId))
        {
            parameter["value"] = $"{ParseEntrance.VideoUrl}{validId.ToLower()}";
            NavigationService.ForwardAsync(VideoDetailViewModel.Key, parameter);
        }
        else if (ParseEntrance.IsAvUrl(validId))
        {
            parameter["value"] = validId;
            NavigationService.ForwardAsync(VideoDetailViewModel.Key, parameter);
        }
        else if (ParseEntrance.IsBvId(validId))
        {
            parameter["value"] = $"{ParseEntrance.VideoUrl}{input}";
            NavigationService.ForwardAsync(VideoDetailViewModel.Key, parameter);
        }
        else if (ParseEntrance.IsBvUrl(validId))
        {
            parameter["value"] = validId;
            NavigationService.ForwardAsync(VideoDetailViewModel.Key, parameter);
        }
        // 番剧（电影、电视剧）
        else if (ParseEntrance.IsBangumiSeasonId(validId))
        {
            parameter["value"] = $"{ParseEntrance.BangumiUrl}{input.ToLower()}";
            NavigationService.ForwardAsync(VideoDetailViewModel.Key, parameter);
        }
        else if (ParseEntrance.IsBangumiSeasonUrl(validId))
        {
            parameter["value"] = validId;
            NavigationService.ForwardAsync(VideoDetailViewModel.Key, parameter);
        }
        else if (ParseEntrance.IsBangumiEpisodeId(validId))
        {
            parameter["value"] = $"{ParseEntrance.BangumiUrl}{input.ToLower()}";
            NavigationService.ForwardAsync(VideoDetailViewModel.Key, parameter);
        }
        else if (ParseEntrance.IsBangumiEpisodeUrl(validId))
        {
            parameter["value"] = validId;
            NavigationService.ForwardAsync(VideoDetailViewModel.Key, parameter);
        }
        else if (ParseEntrance.IsBangumiMediaId(validId))
        {
            parameter["value"] = $"{ParseEntrance.BangumiMediaUrl}{input.ToLower()}";
            NavigationService.ForwardAsync(VideoDetailViewModel.Key, parameter);
        }
        else if (ParseEntrance.IsBangumiMediaUrl(validId))
        {
            parameter["value"] = validId;
            NavigationService.ForwardAsync(VideoDetailViewModel.Key, parameter);
        }
        // 课程
        else if (ParseEntrance.IsCheeseSeasonUrl(validId)
            || ParseEntrance.IsCheeseEpisodeUrl(validId))
        {
            parameter["value"] = validId;
            NavigationService.ForwardAsync(VideoDetailViewModel.Key, parameter);
        }
        // 用户（参数传入mid）
        else if (ParseEntrance.IsUserId(validId) || ParseEntrance.IsUserUrl(validId))
        {
            NavigateToViewUserSpace(ParseEntrance.GetUserId(input));
        }
        // 收藏夹
        else if (ParseEntrance.IsFavoritesId(validId) || ParseEntrance.IsFavoritesUrl(validId))
        {
            parameter["value"] = ParseEntrance.GetFavoritesId(input);
            NavigationService.ForwardAsync(PublicFavoritesViewModel.Key, parameter);
        }
        else
        {
            return false;
        }
        return true;
    }

    public bool SearchKey(string input)
    {
        // TODO
        return false;
    }

    /// <summary>
    /// 导航到用户空间，
    /// 如果传入的mid与本地登录的mid一致，
    /// 则进入我的用户空间。
    /// </summary>
    /// <param name="mid"></param>
    private void NavigateToViewUserSpace(long mid)
    {
        Dictionary<string, object> parameter = new()
        {
            { "key", "MainSearchService" },
            { "value", mid },
        };

        UserInfoSettings userInfo = SettingsManager.GetInstance().GetUserInfo();
        if (userInfo != null && userInfo.Mid == mid)
        {
            NavigationService.ForwardAsync(MySpaceViewModel.Key, parameter);
        }
        else
        {
            NavigationService.ForwardAsync(UserSpaceViewModel.Key, parameter);
        }
    }

}