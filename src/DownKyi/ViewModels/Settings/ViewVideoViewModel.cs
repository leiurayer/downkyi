using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.FileName;
using DownKyi.Core.Settings;
using DownKyi.Events;
using DownKyi.Models;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DownKyi.ViewModels.Settings
{
    public class ViewVideoViewModel : BaseViewModel
    {
        public const string Tag = "PageSettingsVideo";

        private bool isOnNavigatedTo;

        #region 页面属性申明

        private List<string> videoCodecs;
        public List<string> VideoCodecs
        {
            get => videoCodecs;
            set => SetProperty(ref videoCodecs, value);
        }

        private string selectedVideoCodec;
        public string SelectedVideoCodec
        {
            get => selectedVideoCodec;
            set => SetProperty(ref selectedVideoCodec, value);
        }

        private List<Quality> videoQualityList;
        public List<Quality> VideoQualityList
        {
            get => videoQualityList;
            set => SetProperty(ref videoQualityList, value);
        }

        private Quality selectedVideoQuality;
        public Quality SelectedVideoQuality
        {
            get => selectedVideoQuality;
            set => SetProperty(ref selectedVideoQuality, value);
        }

        private List<Quality> audioQualityList;
        public List<Quality> AudioQualityList
        {
            get => audioQualityList;
            set => SetProperty(ref audioQualityList, value);
        }

        private Quality selectedAudioQuality;
        public Quality SelectedAudioQuality
        {
            get => selectedAudioQuality;
            set => SetProperty(ref selectedAudioQuality, value);
        }

        private bool isTranscodingFlvToMp4;
        public bool IsTranscodingFlvToMp4
        {
            get => isTranscodingFlvToMp4;
            set => SetProperty(ref isTranscodingFlvToMp4, value);
        }

        private bool isUseDefaultDirectory;
        public bool IsUseDefaultDirectory
        {
            get => isUseDefaultDirectory;
            set => SetProperty(ref isUseDefaultDirectory, value);
        }

        private string saveVideoDirectory;
        public string SaveVideoDirectory
        {
            get => saveVideoDirectory;
            set => SetProperty(ref saveVideoDirectory, value);
        }

        private ObservableCollection<DisplayFileNamePart> selectedFileName;
        public ObservableCollection<DisplayFileNamePart> SelectedFileName
        {
            get => selectedFileName;
            set => SetProperty(ref selectedFileName, value);
        }

        private ObservableCollection<DisplayFileNamePart> optionalFields;
        public ObservableCollection<DisplayFileNamePart> OptionalFields
        {
            get => optionalFields;
            set => SetProperty(ref optionalFields, value);
        }

        private int selectedOptionalField;
        public int SelectedOptionalField
        {
            get => selectedOptionalField;
            set => SetProperty(ref selectedOptionalField, value);
        }

        #endregion


        public ViewVideoViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

            #region 属性初始化

            // 优先下载的视频编码
            VideoCodecs = new List<string>
            {
                "H.264/AVC",
                "H.265/HEVC",
            };

            // 优先下载画质
            VideoQualityList = Constant.GetResolutions();

            // 优先下载音质
            AudioQualityList = Constant.GetAudioQualities();

            // 文件命名格式
            SelectedFileName = new ObservableCollection<DisplayFileNamePart>();
            OptionalFields = new ObservableCollection<DisplayFileNamePart>();
            foreach (FileNamePart item in Enum.GetValues(typeof(FileNamePart)))
            {
                string display = DisplayFileNamePart(item);
                OptionalFields.Add(new DisplayFileNamePart { Id = item, Title = display });
            }

            SelectedOptionalField = -1;

            #endregion

        }

        /// <summary>
        /// 导航到VideoDetail页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            isOnNavigatedTo = true;

            // 优先下载的视频编码
            VideoCodecs videoCodecs = SettingsManager.GetInstance().GetVideoCodecs();
            SelectedVideoCodec = GetVideoCodecsString(videoCodecs);

            // 优先下载画质
            int quality = SettingsManager.GetInstance().GetQuality();
            SelectedVideoQuality = VideoQualityList.FirstOrDefault(t => { return t.Id == quality; });

            // 优先下载音质
            int audioQuality = SettingsManager.GetInstance().GetAudioQuality();
            SelectedAudioQuality = AudioQualityList.FirstOrDefault(t => { return t.Id == audioQuality; });

            // 是否下载flv视频后转码为mp4
            AllowStatus isTranscodingFlvToMp4 = SettingsManager.GetInstance().IsTranscodingFlvToMp4();
            IsTranscodingFlvToMp4 = isTranscodingFlvToMp4 == AllowStatus.YES;

            // 是否使用默认下载目录
            AllowStatus isUseSaveVideoRootPath = SettingsManager.GetInstance().IsUseSaveVideoRootPath();
            IsUseDefaultDirectory = isUseSaveVideoRootPath == AllowStatus.YES;

            // 默认下载目录
            SaveVideoDirectory = SettingsManager.GetInstance().GetSaveVideoRootPath();

            // 文件命名格式
            List<FileNamePart> fileNameParts = SettingsManager.GetInstance().GetFileNameParts();
            SelectedFileName.Clear();
            foreach (FileNamePart item in fileNameParts)
            {
                string display = DisplayFileNamePart(item);
                SelectedFileName.Add(new DisplayFileNamePart { Id = item, Title = display });
            }

            isOnNavigatedTo = false;
        }

        #region 命令申明

        // 优先下载的视频编码事件
        private DelegateCommand<string> videoCodecsCommand;
        public DelegateCommand<string> VideoCodecsCommand => videoCodecsCommand ?? (videoCodecsCommand = new DelegateCommand<string>(ExecuteVideoCodecsCommand));

        /// <summary>
        /// 优先下载的视频编码事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteVideoCodecsCommand(string parameter)
        {
            VideoCodecs videoCodecs = GetVideoCodecs(parameter);

            bool isSucceed = SettingsManager.GetInstance().SetVideoCodecs(videoCodecs);
            PublishTip(isSucceed);
        }

        // 优先下载画质事件
        private DelegateCommand<object> videoQualityCommand;
        public DelegateCommand<object> VideoQualityCommand => videoQualityCommand ?? (videoQualityCommand = new DelegateCommand<object>(ExecuteVideoQualityCommand));

        /// <summary>
        /// 优先下载画质事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteVideoQualityCommand(object parameter)
        {
            if (!(parameter is Quality resolution)) { return; }

            bool isSucceed = SettingsManager.GetInstance().SetQuality(resolution.Id);
            PublishTip(isSucceed);
        }

        // 优先下载音质事件
        private DelegateCommand<object> audioQualityCommand;
        public DelegateCommand<object> AudioQualityCommand => audioQualityCommand ?? (audioQualityCommand = new DelegateCommand<object>(ExecuteAudioQualityCommand));

        /// <summary>
        /// 优先下载音质事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteAudioQualityCommand(object parameter)
        {
            if (!(parameter is Quality quality)) { return; }

            bool isSucceed = SettingsManager.GetInstance().SetAudioQuality(quality.Id);
            PublishTip(isSucceed);
        }

        // 是否下载flv视频后转码为mp4事件
        private DelegateCommand isTranscodingFlvToMp4Command;
        public DelegateCommand IsTranscodingFlvToMp4Command => isTranscodingFlvToMp4Command ?? (isTranscodingFlvToMp4Command = new DelegateCommand(ExecuteIsTranscodingFlvToMp4Command));

        /// <summary>
        /// 是否下载flv视频后转码为mp4事件
        /// </summary>
        private void ExecuteIsTranscodingFlvToMp4Command()
        {
            AllowStatus isTranscodingFlvToMp4 = IsTranscodingFlvToMp4 ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().IsTranscodingFlvToMp4(isTranscodingFlvToMp4);
            PublishTip(isSucceed);
        }

        // 是否使用默认下载目录事件
        private DelegateCommand isUseDefaultDirectoryCommand;
        public DelegateCommand IsUseDefaultDirectoryCommand => isUseDefaultDirectoryCommand ?? (isUseDefaultDirectoryCommand = new DelegateCommand(ExecuteIsUseDefaultDirectoryCommand));

        /// <summary>
        /// 是否使用默认下载目录事件
        /// </summary>
        private void ExecuteIsUseDefaultDirectoryCommand()
        {
            AllowStatus isUseDefaultDirectory = IsUseDefaultDirectory ? AllowStatus.YES : AllowStatus.NO;

            bool isSucceed = SettingsManager.GetInstance().IsUseSaveVideoRootPath(isUseDefaultDirectory);
            PublishTip(isSucceed);
        }

        // 修改默认下载目录事件
        private DelegateCommand changeSaveVideoDirectoryCommand;
        public DelegateCommand ChangeSaveVideoDirectoryCommand => changeSaveVideoDirectoryCommand ?? (changeSaveVideoDirectoryCommand = new DelegateCommand(ExecuteChangeSaveVideoDirectoryCommand));

        /// <summary>
        /// 修改默认下载目录事件
        /// </summary>
        private void ExecuteChangeSaveVideoDirectoryCommand()
        {
            string directory = DialogUtils.SetDownloadDirectory();
            if (directory == "") { return; }

            bool isSucceed = SettingsManager.GetInstance().SetSaveVideoRootPath(directory);
            PublishTip(isSucceed);

            if (isSucceed)
            {
                SaveVideoDirectory = directory;
            }
        }

        // 选中文件名字段点击事件
        private DelegateCommand<object> selectedFileNameCommand;
        public DelegateCommand<object> SelectedFileNameCommand => selectedFileNameCommand ?? (selectedFileNameCommand = new DelegateCommand<object>(ExecuteSelectedFileNameCommand));

        /// <summary>
        /// 选中文件名字段点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteSelectedFileNameCommand(object parameter)
        {
            bool isSucceed = SelectedFileName.Remove((DisplayFileNamePart)parameter);
            if (!isSucceed)
            {
                PublishTip(isSucceed);
                return;
            }

            List<FileNamePart> fileName = new List<FileNamePart>();
            foreach (DisplayFileNamePart item in SelectedFileName)
            {
                fileName.Add(item.Id);
            }

            isSucceed = SettingsManager.GetInstance().SetFileNameParts(fileName);
            PublishTip(isSucceed);
        }

        // 可选文件名字段点击事件
        private DelegateCommand<object> optionalFieldsCommand;
        public DelegateCommand<object> OptionalFieldsCommand => optionalFieldsCommand ?? (optionalFieldsCommand = new DelegateCommand<object>(ExecuteOptionalFieldsCommand));

        /// <summary>
        /// 可选文件名字段点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteOptionalFieldsCommand(object parameter)
        {
            if (SelectedOptionalField == -1)
            {
                return;
            }

            SelectedFileName.Add((DisplayFileNamePart)parameter);

            List<FileNamePart> fileName = new List<FileNamePart>();
            foreach (DisplayFileNamePart item in SelectedFileName)
            {
                fileName.Add(item.Id);
            }

            bool isSucceed = SettingsManager.GetInstance().SetFileNameParts(fileName);
            PublishTip(isSucceed);

            SelectedOptionalField = -1;
        }

        // 重置选中文件名字段
        private DelegateCommand resetCommand;
        public DelegateCommand ResetCommand => resetCommand ?? (resetCommand = new DelegateCommand(ExecuteResetCommand));

        /// <summary>
        /// 重置选中文件名字段
        /// </summary>
        private void ExecuteResetCommand()
        {
            bool isSucceed = SettingsManager.GetInstance().SetFileNameParts(null);
            PublishTip(isSucceed);

            List<FileNamePart> fileNameParts = SettingsManager.GetInstance().GetFileNameParts();
            SelectedFileName.Clear();
            foreach (FileNamePart item in fileNameParts)
            {
                string display = DisplayFileNamePart(item);
                SelectedFileName.Add(new DisplayFileNamePart { Id = item, Title = display });
            }
        }


        #endregion

        /// <summary>
        /// 返回VideoCodecs的字符串
        /// </summary>
        /// <param name="videoCodecs"></param>
        /// <returns></returns>
        private string GetVideoCodecsString(VideoCodecs videoCodecs)
        {
            string codec;
            switch (videoCodecs)
            {
                case Core.Settings.VideoCodecs.NONE:
                    codec = "";
                    break;
                case Core.Settings.VideoCodecs.AVC:
                    codec = "H.264/AVC";
                    break;
                case Core.Settings.VideoCodecs.HEVC:
                    codec = "H.265/HEVC";
                    break;
                default:
                    codec = "";
                    break;
            }
            return codec;
        }

        /// <summary>
        /// 返回VideoCodecs
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private VideoCodecs GetVideoCodecs(string str)
        {
            VideoCodecs videoCodecs;
            switch (str)
            {
                case "H.264/AVC":
                    videoCodecs = Core.Settings.VideoCodecs.AVC;
                    break;
                case "H.265/HEVC":
                    videoCodecs = Core.Settings.VideoCodecs.HEVC;
                    break;
                default:
                    videoCodecs = Core.Settings.VideoCodecs.NONE;
                    break;
            }
            return videoCodecs;
        }

        /// <summary>
        /// 发送需要显示的tip
        /// </summary>
        /// <param name="isSucceed"></param>
        private void PublishTip(bool isSucceed)
        {
            if (isOnNavigatedTo) { return; }

            if (isSucceed)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipSettingUpdated"));
            }
            else
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipSettingFailed"));
            }
        }

        /// <summary>
        /// 文件名字段显示
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string DisplayFileNamePart(FileNamePart item)
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
}
