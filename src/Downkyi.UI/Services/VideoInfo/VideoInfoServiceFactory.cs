using Microsoft.Extensions.DependencyInjection;
using Downkyi.Core.Bili.Utils;

namespace Downkyi.UI.Services.VideoInfo;

public class VideoInfoServiceFactory(IServiceProvider serviceProvider) : IVideoInfoServiceFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IVideoInfoService Create(string input)
    {
        // 视频
        if (ParseEntrance.IsAvUrl(input) || ParseEntrance.IsBvUrl(input))
        {
            return _serviceProvider.GetRequiredService<VideoInfoService>();
        }

        // 番剧（电影、电视剧）
        if (ParseEntrance.IsBangumiSeasonUrl(input) || ParseEntrance.IsBangumiEpisodeUrl(input) || ParseEntrance.IsBangumiMediaUrl(input))
        {
            return _serviceProvider.GetRequiredService<BangumiInfoService>();
        }

        // 课程
        if (ParseEntrance.IsCheeseSeasonUrl(input) || ParseEntrance.IsCheeseEpisodeUrl(input))
        {
            return _serviceProvider.GetRequiredService<BangumiInfoService>();
        }

        throw new ArgumentException("Invalid type", nameof(input));
    }
}