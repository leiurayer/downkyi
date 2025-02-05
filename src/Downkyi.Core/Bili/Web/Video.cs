using Downkyi.BiliSharp.Api.Login;
using Downkyi.BiliSharp.Api.Models.Video;
using Downkyi.BiliSharp.Api.Sign;
using Downkyi.Core.Bili.Models;
using Downkyi.Core.Bili.Utils;

namespace Downkyi.Core.Bili.Web;

internal class Video : IVideo
{
    private readonly VideoView? videoView;

    private readonly string _input = string.Empty;

    internal Video(string input)
    {
        if (input == null)
        {
            return;
        }

        _input = input;

        if (ParseEntrance.IsAvId(input) || ParseEntrance.IsAvUrl(input))
        {
            long avid = ParseEntrance.GetAvId(input);
            videoView = BiliSharp.Api.Video.VideoInfo.GetVideoViewInfo(null, avid);
        }

        if (ParseEntrance.IsBvId(input) || ParseEntrance.IsBvUrl(input))
        {
            string bvid = ParseEntrance.GetBvId(input);
            videoView = BiliSharp.Api.Video.VideoInfo.GetVideoViewInfo(bvid);
        }
    }

    public string Input()
    {
        return _input;
    }

    /// <summary>
    /// 获取视频详情页信息
    /// </summary>
    /// <param name="bvid"></param>
    /// <param name="aid"></param>
    /// <returns></returns>
    public VideoInfo? GetVideoInfo(string? bvid = null, long aid = -1)
    {
        if (videoView == null) { return null; }
        if (videoView.Data == null) { return null; }
        if (videoView.Data.View == null) { return null; }

        /* 视频发布时间 */
        // 当地时区
        DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
        DateTime dateTime = startTime.AddSeconds(videoView.Data.View.Pubdate);
        string publishTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

        VideoInfo videoInfo = new()
        {
            Aid = videoView.Data.View.Aid,
            Bvid = videoView.Data.View.Bvid,
            Cid = videoView.Data.View.Cid,
            Title = videoView.Data.View.Title,
            Description = videoView.Data.View.Desc,
            PublishTime = publishTime,
        };

        return videoInfo;
    }

}