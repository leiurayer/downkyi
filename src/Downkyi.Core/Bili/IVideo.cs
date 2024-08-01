using Downkyi.Core.Bili.Models;

namespace Downkyi.Core.Bili;

public interface IVideo
{
    string Input();

    VideoInfo? GetVideoInfo(string? bvid = null, long aid = -1);
}