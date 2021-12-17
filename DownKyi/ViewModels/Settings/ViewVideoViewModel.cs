using DownKyi.Core.Settings;
using DownKyi.Events;
using DownKyi.Models;
using DownKyi.Services;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
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

        private List<Resolution> videoQualityList;
        public List<Resolution> VideoQualityList
        {
            get => videoQualityList;
            set => SetProperty(ref videoQualityList, value);
        }

        private Resolution selectedVideoQuality;
        public Resolution SelectedVideoQuality
        {
            get => selectedVideoQuality;
            set => SetProperty(ref selectedVideoQuality, value);
        }

        private bool isAddVideoOrder;
        public bool IsAddVideoOrder
        {
            get => isAddVideoOrder;
            set => SetProperty(ref isAddVideoOrder, value);
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

        private bool isCreateFolderForMedia;
        public bool IsCreateFolderForMedia
        {
            get => isCreateFolderForMedia;
            set => SetProperty(ref isCreateFolderForMedia, value);
        }

        private bool isDownloadDanmaku;
        public bool IsDownloadDanmaku
        {
            get => isDownloadDanmaku;
            set => SetProperty(ref isDownloadDanmaku, value);
        }

        private bool isDownloadCover;
        public bool IsDownloadCover
        {
            get => isDownloadCover;
            set => SetProperty(ref isDownloadCover, value);
        }

        #endregion

        public ViewVideoViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

            #region 属性初始化

            // 优先下载的视频编码
            VideoCodecs = new List<string>
            {
                "H.264/AVC",
                "H.265/HEVC"
            };

            // 优先下载画质
            VideoQualityList = new ResolutionService().GetResolution();

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

            //// 是否在下载的视频前增加序号
            //AllowStatus isAddOrder = SettingsManager.GetInstance().IsAddOrder();
            //IsAddVideoOrder = isAddOrder == AllowStatus.YES;

            // 是否下载flv视频后转码为mp4
            AllowStatus isTranscodingFlvToMp4 = SettingsManager.GetInstance().IsTranscodingFlvToMp4();
            IsTranscodingFlvToMp4 = isTranscodingFlvToMp4 == AllowStatus.YES;

            // 是否使用默认下载目录
            AllowStatus isUseSaveVideoRootPath = SettingsManager.GetInstance().IsUseSaveVideoRootPath();
            IsUseDefaultDirectory = isUseSaveVideoRootPath == AllowStatus.YES;

            // 默认下载目录
            SaveVideoDirectory = SettingsManager.GetInstance().GetSaveVideoRootPath();

            //// 是否为不同视频分别创建文件夹
            //AllowStatus isCreateFolderForMedia = SettingsManager.GetInstance().IsCreateFolderForMedia();
            //IsCreateFolderForMedia = isCreateFolderForMedia == AllowStatus.YES;

            //// 是否在下载视频的同时下载弹幕
            //AllowStatus isDownloadDanmaku = SettingsManager.GetInstance().IsDownloadDanmaku();
            //IsDownloadDanmaku = isDownloadDanmaku == AllowStatus.YES;

            //// 是否在下载视频的同时下载封面
            //AllowStatus isDownloadCover = SettingsManager.GetInstance().IsDownloadCover();
            //IsDownloadCover = isDownloadCover == AllowStatus.YES;

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
            if (!(parameter is Resolution resolution)) { return; }

            bool isSucceed = SettingsManager.GetInstance().SetQuality(resolution.Id);
            PublishTip(isSucceed);
        }

        //// 是否在下载的视频前增加序号事件
        //private DelegateCommand IisAddVideoOrderCommand;
        //public DelegateCommand IsAddVideoOrderCommand => IisAddVideoOrderCommand ?? (IisAddVideoOrderCommand = new DelegateCommand(ExecuteIsAddVideoOrderCommand));

        ///// <summary>
        ///// 是否在下载的视频前增加序号事件
        ///// </summary>
        //private void ExecuteIsAddVideoOrderCommand()
        //{
        //    AllowStatus isAddOrder = IsAddVideoOrder ? AllowStatus.YES : AllowStatus.NO;

        //    bool isSucceed = SettingsManager.GetInstance().IsAddOrder(isAddOrder);
        //    PublishTip(isSucceed);
        //}

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

        //// 是否为不同视频分别创建文件夹事件
        //private DelegateCommand isCreateFolderForMediaCommand;
        //public DelegateCommand IsCreateFolderForMediaCommand => isCreateFolderForMediaCommand ?? (isCreateFolderForMediaCommand = new DelegateCommand(ExecuteIsCreateFolderForMediaCommand));

        ///// <summary>
        ///// 是否为不同视频分别创建文件夹事件
        ///// </summary>
        //private void ExecuteIsCreateFolderForMediaCommand()
        //{
        //    AllowStatus isCreateFolderForMedia = IsCreateFolderForMedia ? AllowStatus.YES : AllowStatus.NO;

        //    bool isSucceed = SettingsManager.GetInstance().IsCreateFolderForMedia(isCreateFolderForMedia);
        //    PublishTip(isSucceed);
        //}

        //// 是否在下载视频的同时下载弹幕事件
        //private DelegateCommand isDownloadDanmakuCommand;
        //public DelegateCommand IsDownloadDanmakuCommand => isDownloadDanmakuCommand ?? (isDownloadDanmakuCommand = new DelegateCommand(ExecuteIsDownloadDanmakuCommand));

        ///// <summary>
        ///// 是否在下载视频的同时下载弹幕事件
        ///// </summary>
        //private void ExecuteIsDownloadDanmakuCommand()
        //{
        //    AllowStatus isDownloadDanmaku = IsDownloadDanmaku ? AllowStatus.YES : AllowStatus.NO;

        //    bool isSucceed = SettingsManager.GetInstance().IsDownloadDanmaku(isDownloadDanmaku);
        //    PublishTip(isSucceed);
        //}

        //// 是否在下载视频的同时下载封面事件
        //private DelegateCommand isDownloadCoverCommand;
        //public DelegateCommand IsDownloadCoverCommand => isDownloadCoverCommand ?? (isDownloadCoverCommand = new DelegateCommand(ExecuteIsDownloadCoverCommand));

        ///// <summary>
        ///// 是否在下载视频的同时下载封面事件
        ///// </summary>
        //private void ExecuteIsDownloadCoverCommand()
        //{
        //    AllowStatus isDownloadCover = IsDownloadCover ? AllowStatus.YES : AllowStatus.NO;

        //    bool isSucceed = SettingsManager.GetInstance().IsDownloadCover(isDownloadCover);
        //    PublishTip(isSucceed);
        //}

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

    }
}
