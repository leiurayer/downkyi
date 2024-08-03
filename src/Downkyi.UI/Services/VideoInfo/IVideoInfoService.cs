using Downkyi.UI.Models;

namespace Downkyi.UI.Services.VideoInfo;

public interface IVideoInfoService
{
    VideoInfoView? GetVideoView(string input);
}