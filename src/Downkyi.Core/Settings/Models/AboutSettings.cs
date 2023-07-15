using Downkyi.Core.Settings.Enum;

namespace Downkyi.Core.Settings.Models;

/// <summary>
/// 关于
/// </summary>
public class AboutSettings
{
    public AllowStatus IsReceiveBetaVersion { get; set; } = AllowStatus.NONE;
    public AllowStatus AutoUpdateWhenLaunch { get; set; } = AllowStatus.NONE;
}