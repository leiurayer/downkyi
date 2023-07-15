using Avalonia.Controls;
using Avalonia.Input;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.FileName;
using Downkyi.UI.Mvvm;
using Downkyi.UI.ViewModels.Settings;

namespace Downkyi.ViewModels.Settings;

public partial class VideoViewModelProxy : VideoViewModel
{
    public VideoViewModelProxy(BaseServices baseServices) : base(baseServices)
    {
    }

    #region 命令申明

    /// <summary>
    /// 可选文件名字段点击事件
    /// </summary>
    /// <param name="args"></param>
    [RelayCommand]
    private void SelectOptionalFields(object args)
    {
        if (args is PointerReleasedEventArgs releasedEventArgs)
        {
            if (releasedEventArgs.InitialPressMouseButton == MouseButton.Left)
            {
                if (releasedEventArgs.Source is Border border)
                {
                    OptionalFieldsCommand(border.Child?.DataContext);
                }
                if (releasedEventArgs.Source is TextBlock text)
                {
                    OptionalFieldsCommand(text.DataContext);
                }
            }
        }
    }

    /// <summary>
    /// 选中文件名字段右键点击事件
    /// </summary>
    /// <param name="args"></param>
    [RelayCommand]
    private void SetSelectedFileName(object args)
    {
        if (args is PointerReleasedEventArgs releasedEventArgs)
        {
            if (releasedEventArgs.InitialPressMouseButton == MouseButton.Right)
            {
                if (releasedEventArgs.Source is Border border)
                {
                    SelectedFileNameRightButton(border.Child?.DataContext);
                }
                if (releasedEventArgs.Source is TextBlock text)
                {
                    SelectedFileNameRightButton(text.DataContext);
                }
            }
        }
    }

    #endregion

    protected override string DisplayFileNamePart(FileNamePart item)
    {
        string display = string.Empty;
        switch (item)
        {
            case FileNamePart.ORDER:
                display = DictionaryResource.GetString("DisplayOrder");
                break;
            case FileNamePart.SECTION:
                display = DictionaryResource.GetString("DisplaySection");
                break;
            case FileNamePart.MAIN_TITLE:
                display = DictionaryResource.GetString("DisplayMainTitle");
                break;
            case FileNamePart.PAGE_TITLE:
                display = DictionaryResource.GetString("DisplayPageTitle");
                break;
            case FileNamePart.VIDEO_ZONE:
                display = DictionaryResource.GetString("DisplayVideoZone");
                break;
            case FileNamePart.AUDIO_QUALITY:
                display = DictionaryResource.GetString("DisplayAudioQuality");
                break;
            case FileNamePart.VIDEO_QUALITY:
                display = DictionaryResource.GetString("DisplayVideoQuality");
                break;
            case FileNamePart.VIDEO_CODEC:
                display = DictionaryResource.GetString("DisplayVideoCodec");
                break;
            case FileNamePart.VIDEO_PUBLISH_TIME:
                display = DictionaryResource.GetString("DisplayVideoPublishTime");
                break;
            case FileNamePart.AVID:
                display = "avid";
                break;
            case FileNamePart.BVID:
                display = "bvid";
                break;
            case FileNamePart.CID:
                display = "cid";
                break;
            case FileNamePart.UP_MID:
                display = DictionaryResource.GetString("DisplayUpMid");
                break;
            case FileNamePart.UP_NAME:
                display = DictionaryResource.GetString("DisplayUpName");
                break;
        }

        if (((int)item) >= 100)
        {
            display = HyphenSeparated.Hyphen[(int)item];
        }

        if (display == " ")
        {
            display = DictionaryResource.GetString("DisplaySpace");
        }

        return display;
    }

}