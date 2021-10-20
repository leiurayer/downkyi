using DownKyi.Models;
using System.Collections.Generic;

namespace DownKyi.Services
{
    public interface IInfoService
    {
        VideoInfoView GetVideoView();

        List<VideoSection> GetVideoSections();

        List<VideoPage> GetVideoPages();

        void GetVideoStream(VideoPage page);
    }
}
