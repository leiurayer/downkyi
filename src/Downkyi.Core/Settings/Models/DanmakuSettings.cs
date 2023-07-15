using Downkyi.Core.Settings.Enum;

namespace Downkyi.Core.Settings.Models;

/// <summary>
/// 弹幕
/// </summary>
public class DanmakuSettings
{
    public AllowStatus DanmakuTopFilter { get; set; } = AllowStatus.NONE;
    public AllowStatus DanmakuBottomFilter { get; set; } = AllowStatus.NONE;
    public AllowStatus DanmakuScrollFilter { get; set; } = AllowStatus.NONE;
    public AllowStatus IsCustomDanmakuResolution { get; set; } = AllowStatus.NONE;
    public int DanmakuScreenWidth { get; set; } = -1;
    public int DanmakuScreenHeight { get; set; } = -1;
    public string DanmakuFontName { get; set; } = null;
    public int DanmakuFontSize { get; set; } = -1;
    public int DanmakuLineCount { get; set; } = -1;
    public DanmakuLayoutAlgorithm DanmakuLayoutAlgorithm { get; set; } = DanmakuLayoutAlgorithm.NONE;
}