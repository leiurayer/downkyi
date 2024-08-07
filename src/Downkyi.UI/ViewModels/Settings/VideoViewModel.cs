using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Downkyi.Core.Bili.Models;
using Downkyi.Core.Bili.Utils;
using Downkyi.Core.FileName;
using Downkyi.Core.Settings;
using Downkyi.Core.Settings.Enum;
using Downkyi.Core.Settings.Models;
using Downkyi.UI.Models;
using Downkyi.UI.Mvvm;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Downkyi.UI.ViewModels.Settings;

public partial class VideoViewModel : BaseSettingsViewModel
{
    public const string Key = "Settings_Video";

    #region 页面属性申明

    [ObservableProperty]
    private List<Quality> _videoCodecs = new();

    [ObservableProperty]
    private Quality? _selectedVideoCodec = new();

    [ObservableProperty]
    private List<Quality> _videoQualityList = new();

    [ObservableProperty]
    private Quality? _selectedVideoQuality = new();

    [ObservableProperty]
    private List<Quality> _audioQualityList = new();

    [ObservableProperty]
    private Quality? _selectedAudioQuality = new();

    [ObservableProperty]
    private bool _isTranscodingFlvToMp4;

    [ObservableProperty]
    private bool _isUseDefaultDirectory;

    [ObservableProperty]
    private string _saveVideoDirectory = string.Empty;

    [ObservableProperty]
    private bool _downloadAll;

    [ObservableProperty]
    private bool _downloadAudio;

    [ObservableProperty]
    private bool _downloadVideo;

    [ObservableProperty]
    private bool _downloadDanmaku;

    [ObservableProperty]
    private bool _downloadSubtitle;

    [ObservableProperty]
    private bool _downloadCover;

    [ObservableProperty]
    private ObservableCollection<FileNamePartDisplay> _selectedFileName = new();

    [ObservableProperty]
    private List<FileNamePartDisplay> _optionalFields = new();

    [ObservableProperty]
    private int _selectedOptionalField;

    [ObservableProperty]
    private List<string> _timeFormatList = new();

    [ObservableProperty]
    private string _selectedTimeFormat = string.Empty;

    [ObservableProperty]
    private List<OrderFormatDisplay> _orderFormatList = new();

    [ObservableProperty]
    private OrderFormatDisplay? _selectedOrderFormat = new();

    #endregion

    public VideoViewModel(BaseServices baseServices) : base(baseServices)
    {
        TipSettingUpdated = DictionaryResource.GetString("TipSettingUpdated");
        TipSettingFailed = DictionaryResource.GetString("TipSettingFailed");

        #region 属性初始化

        // 优先下载的视频编码
        VideoCodecs = QualityList.GetCodecIds();

        // 优先下载画质
        VideoQualityList = QualityList.GetResolutions();

        // 优先下载音质
        AudioQualityList = QualityList.GetAudioQualities();
        AudioQualityList[3].Id = AudioQualityList[3].Id + 1000;
        AudioQualityList[4].Id = AudioQualityList[4].Id + 1000;

        // 文件命名中的序号格式
        OrderFormatList.Add(new OrderFormatDisplay { Id = Core.Settings.Enum.OrderFormat.NATURAL, Name = DictionaryResource.GetString("OrderFormatNatural") });
        OrderFormatList.Add(new OrderFormatDisplay { Id = Core.Settings.Enum.OrderFormat.LEADING_ZEROS, Name = DictionaryResource.GetString("OrderFormatLeadingZeros") });

        // 文件命名格式
        SelectedFileName.CollectionChanged += new NotifyCollectionChangedEventHandler((sender, e) =>
        {
            // 当前显示的命名格式part
            var fileName = new List<FileNamePart>();
            foreach (var item in SelectedFileName)
            {
                fileName.Add(item.Id);
            }

            bool isSucceed = SettingsManager.Instance.SetFileNameParts(fileName);
            PublishTip(Key, isSucceed);
        });

        foreach (FileNamePart item in Enum.GetValues(typeof(FileNamePart)))
        {
            string display = DisplayFileNamePart(item);
            OptionalFields.Add(new() { Id = item, Title = display });
        }

        SelectedOptionalField = -1;

        // 文件命名中的时间格式
        TimeFormatList.Add("yyyy-MM-dd");
        TimeFormatList.Add("yyyy.MM.dd");

        #endregion
    }

    #region 命令申明

    /// <summary>
    /// 加载页面时执行
    /// </summary>
    [RelayCommand]
    private void OnLoaded()
    {
        IsOnNavigatedTo = true;

        // 优先下载的视频编码
        int videoCodecs = SettingsManager.Instance.GetVideoCodecs();
        SelectedVideoCodec = VideoCodecs.FirstOrDefault(t => { return t.Id == videoCodecs; });

        // 优先下载画质
        int quality = SettingsManager.Instance.GetQuality();
        SelectedVideoQuality = VideoQualityList.FirstOrDefault(t => { return t.Id == quality; });

        // 优先下载音质
        int audioQuality = SettingsManager.Instance.GetAudioQuality();
        SelectedAudioQuality = AudioQualityList.FirstOrDefault(t => { return t.Id == audioQuality; });

        // 是否下载flv视频后转码为mp4
        AllowStatus isTranscodingFlvToMp4 = SettingsManager.Instance.IsTranscodingFlvToMp4();
        IsTranscodingFlvToMp4 = isTranscodingFlvToMp4 == AllowStatus.YES;

        // 是否使用默认下载目录
        AllowStatus isUseSaveVideoRootPath = SettingsManager.Instance.IsUseSaveVideoRootPath();
        IsUseDefaultDirectory = isUseSaveVideoRootPath == AllowStatus.YES;

        // 默认下载目录
        SaveVideoDirectory = SettingsManager.Instance.GetSaveVideoRootPath();

        // 下载内容
        VideoContentSettings videoContent = SettingsManager.Instance.GetVideoContent();

        DownloadAudio = videoContent.DownloadAudio;
        DownloadVideo = videoContent.DownloadVideo;
        DownloadDanmaku = videoContent.DownloadDanmaku;
        DownloadSubtitle = videoContent.DownloadSubtitle;
        DownloadCover = videoContent.DownloadCover;

        if (DownloadAudio && DownloadVideo && DownloadDanmaku && DownloadSubtitle && DownloadCover)
        {
            DownloadAll = true;
        }
        else
        {
            DownloadAll = false;
        }

        // 文件命名格式
        List<FileNamePart> fileNameParts = SettingsManager.Instance.GetFileNameParts();
        SelectedFileName.Clear();
        foreach (FileNamePart item in fileNameParts)
        {
            int index;
            if (SelectedFileName.Count == 0) { index = 0; }
            else { index = SelectedFileName.Max(it => it.Index) + 1; }

            string display = DisplayFileNamePart(item);
            SelectedFileName.Add(new FileNamePartDisplay
            {
                Index = index,
                Id = item,
                Title = display,
            });
        }

        // 文件命名中的时间格式
        SelectedTimeFormat = SettingsManager.Instance.GetFileNamePartTimeFormat();

        // 文件命名中的序号格式
        OrderFormat orderFormat = SettingsManager.Instance.GetOrderFormat();
        SelectedOrderFormat = OrderFormatList.FirstOrDefault(t => { return t.Id == orderFormat; });

        IsOnNavigatedTo = false;
    }

    /// <summary>
    /// 优先下载的视频编码事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetVideoCodecs()
    {
        bool isSucceed = SettingsManager.Instance.SetVideoCodecs(SelectedVideoCodec!.Id);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 优先下载画质事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetVideoQuality()
    {
        bool isSucceed = SettingsManager.Instance.SetQuality(SelectedVideoQuality!.Id);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 优先下载音质事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void SetAudioQuality()
    {
        bool isSucceed = SettingsManager.Instance.SetAudioQuality(SelectedAudioQuality!.Id);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 是否下载flv视频后转码为mp4事件
    /// </summary>
    [RelayCommand]
    private void SetIsTranscodingFlvToMp4()
    {
        AllowStatus isTranscodingFlvToMp4 = IsTranscodingFlvToMp4 ? AllowStatus.YES : AllowStatus.NO;

        bool isSucceed = SettingsManager.Instance.IsTranscodingFlvToMp4(isTranscodingFlvToMp4);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 是否使用默认下载目录事件
    /// </summary>
    [RelayCommand]
    private void SetIsUseDefaultDirectory()
    {
        AllowStatus isUseDefaultDirectory = IsUseDefaultDirectory ? AllowStatus.YES : AllowStatus.NO;

        bool isSucceed = SettingsManager.Instance.IsUseSaveVideoRootPath(isUseDefaultDirectory);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 修改默认下载目录事件
    /// </summary>
    [RelayCommand(FlowExceptionsToTaskScheduler = true)]
    private async Task ChangeSaveVideoDirectory()
    {
        string directory = await StoragePicker.FolderPicker(SaveVideoDirectory);
        if (directory == "") { return; }

        bool isSucceed = SettingsManager.Instance.SetSaveVideoRootPath(directory);
        PublishTip(Key, isSucceed);

        if (isSucceed)
        {
            SaveVideoDirectory = directory;
        }
    }

    /// <summary>
    /// 所有内容选择事件
    /// </summary>
    [RelayCommand]
    private void SetDownloadAll()
    {
        if (DownloadAll)
        {
            DownloadAudio = true;
            DownloadVideo = true;
            DownloadDanmaku = true;
            DownloadSubtitle = true;
            DownloadCover = true;
        }
        else
        {
            DownloadAudio = false;
            DownloadVideo = false;
            DownloadDanmaku = false;
            DownloadSubtitle = false;
            DownloadCover = false;
        }

        SetVideoContent();
    }

    /// <summary>
    /// 音频选择事件
    /// </summary>
    [RelayCommand]
    private void SetDownloadAudio()
    {
        if (!DownloadAudio)
        {
            DownloadAll = false;
        }

        if (DownloadAudio && DownloadVideo && DownloadDanmaku && DownloadSubtitle && DownloadCover)
        {
            DownloadAll = true;
        }

        SetVideoContent();
    }

    /// <summary>
    /// 视频选择事件
    /// </summary>
    [RelayCommand]
    private void SetDownloadVideo()
    {
        if (!DownloadVideo)
        {
            DownloadAll = false;
        }

        if (DownloadAudio && DownloadVideo && DownloadDanmaku && DownloadSubtitle && DownloadCover)
        {
            DownloadAll = true;
        }

        SetVideoContent();
    }

    /// <summary>
    /// 弹幕选择事件
    /// </summary>
    [RelayCommand]
    private void SetDownloadDanmaku()
    {
        if (!DownloadDanmaku)
        {
            DownloadAll = false;
        }

        if (DownloadAudio && DownloadVideo && DownloadDanmaku && DownloadSubtitle && DownloadCover)
        {
            DownloadAll = true;
        }

        SetVideoContent();
    }

    /// <summary>
    /// 字幕选择事件
    /// </summary>
    [RelayCommand]
    private void SetDownloadSubtitle()
    {
        if (!DownloadSubtitle)
        {
            DownloadAll = false;
        }

        if (DownloadAudio && DownloadVideo && DownloadDanmaku && DownloadSubtitle && DownloadCover)
        {
            DownloadAll = true;
        }

        SetVideoContent();
    }

    /// <summary>
    /// 封面选择事件
    /// </summary>
    [RelayCommand]
    private void SetDownloadCover()
    {
        if (!DownloadCover)
        {
            DownloadAll = false;
        }

        if (DownloadAudio && DownloadVideo && DownloadDanmaku && DownloadSubtitle && DownloadCover)
        {
            DownloadAll = true;
        }

        SetVideoContent();
    }

    /// <summary>
    /// 文件命名中的时间格式事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void TimeFormat()
    {
        bool isSucceed = SettingsManager.Instance.SetFileNamePartTimeFormat(SelectedTimeFormat);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 文件命名中的序号格式事件
    /// </summary>
    /// <param name="parameter"></param>
    [RelayCommand]
    private void OrderFormat()
    {
        bool isSucceed = SettingsManager.Instance.SetOrderFormat(SelectedOrderFormat!.Id);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 重置选中文件名字段
    /// </summary>
    [RelayCommand]
    private void Reset()
    {
        bool isSucceed = SettingsManager.Instance.SetFileNameParts(null);
        PublishTip(Key, isSucceed);

        List<FileNamePart> fileNameParts = SettingsManager.Instance.GetFileNameParts();
        SelectedFileName.Clear();
        foreach (FileNamePart item in fileNameParts)
        {
            int index;
            if (SelectedFileName.Count == 0) { index = 0; }
            else { index = SelectedFileName.Max(it => it.Index) + 1; }

            string display = DisplayFileNamePart(item);
            SelectedFileName.Add(new FileNamePartDisplay
            {
                Index = index,
                Id = item,
                Title = display
            });
        }

        SelectedOptionalField = -1;
    }

    #endregion

    /// <summary>
    /// 保存下载视频内容到设置
    /// </summary>
    private void SetVideoContent()
    {
        VideoContentSettings videoContent = new()
        {
            DownloadAudio = DownloadAudio,
            DownloadVideo = DownloadVideo,
            DownloadDanmaku = DownloadDanmaku,
            DownloadSubtitle = DownloadSubtitle,
            DownloadCover = DownloadCover
        };

        bool isSucceed = SettingsManager.Instance.SetVideoContent(videoContent);
        PublishTip(Key, isSucceed);
    }

    /// <summary>
    /// 可选文件名字段点击
    /// </summary>
    /// <param name="parameter"></param>
    protected void OptionalFieldsCommand(object? parameter)
    {
        if (parameter is not FileNamePartDisplay fileName) { return; }

        int index;
        if (SelectedFileName.Count == 0) { index = 0; }
        else { index = SelectedFileName.Max(it => it.Index) + 1; }

        SelectedFileName.Add(new FileNamePartDisplay
        {
            Index = index,
            Id = fileName.Id,
            Title = fileName.Title,
        });

        SelectedOptionalField = -1;
    }

    /// <summary>
    /// 选中文件名字段右键点击
    /// </summary>
    /// <param name="parameter"></param>
    protected void SelectedFileNameRightButton(object? parameter)
    {
        if (parameter is not FileNamePartDisplay fileName) { return; }

        SelectedFileName.Remove(fileName);

        SelectedOptionalField = -1;
    }

    /// <summary>
    /// 文件名字段显示
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected virtual string DisplayFileNamePart(FileNamePart item) { return string.Empty; }

}