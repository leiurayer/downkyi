using Downkyi.Core.Bili;
using Downkyi.UI.Models;

namespace Downkyi.UI.Services;

public class VideoInfoService : IVideoInfoService
{

    public VideoInfoView? GetVideoView(string input)
    {
        if (input == null) { return null; }

        var video = BiliLocator.Video(input);
        var videoInfo = video.GetVideoInfo();

        

        throw new NotImplementedException();
    }

}