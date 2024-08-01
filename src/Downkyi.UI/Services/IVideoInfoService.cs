using Downkyi.UI.Models;

namespace Downkyi.UI.Services;

public interface IVideoInfoService
{
    VideoInfoView? GetVideoView(string input);
}