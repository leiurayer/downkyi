using DownKyi.ViewModels.PageViewModels;
using System.Collections.Generic;

namespace DownKyi.Services
{
    public interface IInfoService
    {
        VideoInfoView GetVideoView();

        List<VideoSection> GetVideoSections(bool noUgc);

        List<VideoPage> GetVideoPages();

        void GetVideoStream(VideoPage page);
    }
}
