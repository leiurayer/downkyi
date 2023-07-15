namespace Downkyi.Core.Settings.Models;

public class VideoContentSettings
{
    public bool DownloadAudio { get; set; } = true;
    public bool DownloadVideo { get; set; } = true;
    public bool DownloadDanmaku { get; set; } = true;
    public bool DownloadSubtitle { get; set; } = true;
    public bool DownloadCover { get; set; } = true;
}