using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.FFmpeg;
using Downkyi.UI.Mvvm;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Downkyi.UI.ViewModels.Toolbox;

public partial class ExtractMediaViewModel : ViewModelBase
{
    public const string Key = "Toolbox_ExtractMedia";

    // 是否正在执行任务
    private bool _isExtracting = false;

    #region 页面属性申明

    [ObservableProperty]
    private string _videoPathsStr = string.Empty;

    [ObservableProperty]
    private ObservableCollection<string> _videoPaths = new();

    [ObservableProperty]
    private string _status = string.Empty;

    #endregion

    public ExtractMediaViewModel(BaseServices baseServices) : base(baseServices)
    {
        VideoPaths.CollectionChanged += new NotifyCollectionChangedEventHandler((sender, e) =>
        {
            VideoPathsStr = string.Join(Environment.NewLine, VideoPaths);
        });
    }

    #region 命令申明

    /// <summary>
    /// 选择视频事件
    /// </summary>
    /// <returns></returns>
    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task SelectVideo()
    {
        if (_isExtracting)
        {
            NotificationEvent.Publish(DictionaryResource.GetString("TipWaitTaskFinished"));
            return;
        }

        var videoPathList = await StoragePicker.SelectMultiVideoFileAsync();

        VideoPaths.Clear();
        foreach (var path in videoPathList)
        {
            VideoPaths.Add(path);
        }
    }

    /// <summary>
    /// 提取音频事件
    /// </summary>
    /// <returns></returns>
    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task ExtractAudio()
    {
        if (_isExtracting)
        {
            NotificationEvent.Publish(DictionaryResource.GetString("TipWaitTaskFinished"));
            return;
        }

        if (VideoPaths.Count <= 0)
        {
            NotificationEvent.Publish(DictionaryResource.GetString("TipNoSeletedVideo"));
            return;
        }

        Status = string.Empty;

        await Task.Run(() =>
        {
            _isExtracting = true;
            foreach (var item in VideoPaths)
            {
                // 音频文件名
                string audioFileName = item.Remove(item.Length - 4, 4) + ".aac";
                // 执行提取音频程序
                FFmpegHelper.ExtractAudio(item, audioFileName, new Action<string>((output) =>
                {
                    Status += output + "\n";
                }));
            }
            _isExtracting = false;
        });
    }

    /// <summary>
    /// 提取视频事件
    /// </summary>
    /// <returns></returns>
    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task ExtractVideo()
    {
        if (_isExtracting)
        {
            NotificationEvent.Publish(DictionaryResource.GetString("TipWaitTaskFinished"));
            return;
        }

        if (VideoPaths.Count <= 0)
        {
            NotificationEvent.Publish(DictionaryResource.GetString("TipNoSeletedVideo"));
            return;
        }

        Status = string.Empty;

        await Task.Run(() =>
        {
            _isExtracting = true;
            foreach (var item in VideoPaths)
            {
                // 视频文件名
                string videoFileName = item.Remove(item.Length - 4, 4) + "_onlyVideo.mp4";
                // 执行提取视频程序
                FFmpegHelper.ExtractVideo(item, videoFileName, new Action<string>((output) =>
                {
                    Status += output + "\n";
                }));
            }
            _isExtracting = false;
        });
    }

    #endregion
}