using DownKyi.Models;
using System.Collections.Generic;

namespace DownKyi.Services.Download
{
    public interface IDownloadService
    {
        void Parse(DownloadingItem downloading);
        string DownloadAudio(DownloadingItem downloading);
        string DownloadVideo(DownloadingItem downloading);
        string DownloadDanmaku(DownloadingItem downloading);
        List<string> DownloadSubtitle(DownloadingItem downloading);
        string DownloadCover(DownloadingItem downloading, string coverUrl, string fileName);
        string MixedFlow(DownloadingItem downloading, string audioUid, string videoUid);

        void Start();
        void End();
    }
}
