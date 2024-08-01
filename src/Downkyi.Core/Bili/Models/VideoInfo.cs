namespace Downkyi.Core.Bili.Models;

public class VideoInfo
{
    public long Aid { get; set; }
    public string Bvid { get; set; } = string.Empty;
    public long Cid { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PublishTime { get; set; } = string.Empty;
}