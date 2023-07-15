using Downkyi.Core.FileName;
using Downkyi.Core.Settings.Enum;

namespace Downkyi.Core.Settings.Models;

/// <summary>
/// 视频
/// </summary>
public class VideoSettings
{
    public int VideoCodecs { get; set; } = -1; // AVC or HEVC
    public int Quality { get; set; } = -1; // 画质
    public int AudioQuality { get; set; } = -1; // 音质
    public AllowStatus IsTranscodingFlvToMp4 { get; set; } = AllowStatus.NONE; // 是否将flv转为mp4
    public string SaveVideoRootPath { get; set; } = null; // 视频保存路径
    public List<string> HistoryVideoRootPaths { get; set; } = null; // 历史视频保存路径
    public AllowStatus IsUseSaveVideoRootPath { get; set; } = AllowStatus.NONE; // 是否使用默认视频保存路径
    public VideoContentSettings VideoContent { get; set; } = null; // 下载内容
    public List<FileNamePart> FileNameParts { get; set; } = null; // 文件命名格式
    public string FileNamePartTimeFormat { get; set; } = null; // 文件命名中的时间格式
    public OrderFormat OrderFormat { get; set; } = OrderFormat.NOT_SET; // 文件命名中的序号格式
}