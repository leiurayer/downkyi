using DownKyi.Models;

namespace DownKyi.Services.Download
{
    public interface IDownloadService
    {
        void Parse(DownloadingItem downloading);
        string DownloadAudio(DownloadingItem downloading);
        string DownloadVideo(DownloadingItem downloading);
        void DownloadDanmaku(DownloadingItem downloading);
        void DownloadSubtitle(DownloadingItem downloading);
        void DownloadCover(DownloadingItem downloading);
        void MixedFlow(DownloadingItem downloading, string audioUid, string videoUid);

        void Start();
        void End();
    }
}
