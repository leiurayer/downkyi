using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.FFmpeg;
using Downkyi.UI.Mvvm;

namespace Downkyi.UI.ViewModels.Toolbox;

public partial class DelogoViewModel : ViewModelBase
{
    public const string Key = "Toolbox_Delogo";

    // 是否正在执行去水印任务
    protected bool _isDelogo = false;

    #region 页面属性申明

    [ObservableProperty]
    private string _videoPath = string.Empty;

    [ObservableProperty]
    private int _logoWidth;

    [ObservableProperty]
    private int _logoHeight;

    [ObservableProperty]
    private int _logoX;

    [ObservableProperty]
    private int _logoY;

    [ObservableProperty]
    private string _status = string.Empty;

    #endregion

    public DelogoViewModel(BaseServices baseServices) : base(baseServices)
    {
        #region 属性初始化

        VideoPath = string.Empty;

        LogoWidth = 0;
        LogoHeight = 0;
        LogoX = 0;
        LogoY = 0;

        #endregion
    }

    #region 命令申明

    /// <summary>
    /// 选择视频事件
    /// </summary>
    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task SelectVideo()
    {
        if (_isDelogo)
        {
            NotificationEvent.Publish(DictionaryResource.GetString("TipWaitTaskFinished"));
            return;
        }

        VideoPath = await StoragePicker.SelectVideoFileAsync();
    }

    /// <summary>
    /// 去水印事件
    /// </summary>
    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task Delogo()
    {
        if (_isDelogo)
        {
            NotificationEvent.Publish(DictionaryResource.GetString("TipWaitTaskFinished"));
            return;
        }

        if (VideoPath == "")
        {
            NotificationEvent.Publish(DictionaryResource.GetString("TipNoSeletedVideo"));
            return;
        }

        if (LogoWidth == -1)
        {
            NotificationEvent.Publish(DictionaryResource.GetString("TipInputRightLogoWidth"));
            return;
        }
        if (LogoHeight == -1)
        {
            NotificationEvent.Publish(DictionaryResource.GetString("TipInputRightLogoHeight"));
            return;
        }
        if (LogoX == -1)
        {
            NotificationEvent.Publish(DictionaryResource.GetString("TipInputRightLogoX"));
            return;
        }
        if (LogoY == -1)
        {
            NotificationEvent.Publish(DictionaryResource.GetString("TipInputRightLogoY"));
            return;
        }

        // 新文件名
        string newFileName = VideoPath.Insert(VideoPath.Length - 4, "_delogo");
        Status = string.Empty;

        await Task.Run(() =>
        {
            // 执行去水印程序
            _isDelogo = true;
            FFmpegHelper.Delogo(VideoPath, newFileName, LogoX, LogoY, LogoWidth, LogoHeight, new Action<string>((output) =>
            {
                Status += output + "\n";
            }));
            _isDelogo = false;
        });
    }

    #endregion
}