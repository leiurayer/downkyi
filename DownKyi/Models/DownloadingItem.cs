using DownKyi.Core.BiliApi.VideoStream.Models;
using DownKyi.Images;
using DownKyi.Utils;
using Prism.Commands;
using System.Collections.Generic;

namespace DownKyi.Models
{
    public class DownloadingItem : DownloadBaseItem
    {
        public DownloadingItem() : base()
        {
            // 初始化下载的文件列表
            DownloadFiles = new List<string>();

            // 暂停继续按钮
            StartOrPause = ButtonIcon.Instance().Pause;
            StartOrPause.Fill = DictionaryResource.GetColor("ColorPrimary");

            // 删除按钮
            Delete = ButtonIcon.Instance().Delete;
            Delete.Fill = DictionaryResource.GetColor("ColorPrimary");
        }

        public PlayUrl PlayUrl { get; set; }

        // Aria相关
        public string Gid { get; set; }

        // 下载的文件
        public List<string> DownloadFiles { get; private set; }

        // 视频类别
        public PlayStreamType PlayStreamType { get; set; }


        // 正在下载内容（音频、视频、弹幕、字幕、封面）
        private string downloadContent;
        public string DownloadContent
        {
            get => downloadContent;
            set => SetProperty(ref downloadContent, value);
        }

        // 下载状态
        public DownloadStatus DownloadStatus { get; set; }

        // 下载状态显示
        private string downloadStatusTitle;
        public string DownloadStatusTitle
        {
            get => downloadStatusTitle;
            set => SetProperty(ref downloadStatusTitle, value);
        }

        // 下载进度
        private float progress;
        public float Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }

        //  已下载大小/文件大小
        private string downloadingFileSize;
        public string DownloadingFileSize
        {
            get => downloadingFileSize;
            set => SetProperty(ref downloadingFileSize, value);
        }

        // 下载的最高速度
        public long MaxSpeed { get; set; }

        //  下载速度
        private string speedDisplay;
        public string SpeedDisplay
        {
            get => speedDisplay;
            set => SetProperty(ref speedDisplay, value);
        }


        #region 控制按钮

        private VectorImage startOrPause;
        public VectorImage StartOrPause
        {
            get => startOrPause;
            set => SetProperty(ref startOrPause, value);
        }

        private VectorImage delete;
        public VectorImage Delete
        {
            get => delete;
            set => SetProperty(ref delete, value);
        }

        #endregion

        #region 命令申明

        // 下载列表暂停继续事件
        private DelegateCommand startOrPauseCommand;
        public DelegateCommand StartOrPauseCommand => startOrPauseCommand ?? (startOrPauseCommand = new DelegateCommand(ExecuteStartOrPauseCommand));

        /// <summary>
        /// 下载列表暂停继续事件
        /// </summary>
        private void ExecuteStartOrPauseCommand()
        {
            switch (DownloadStatus)
            {
                case DownloadStatus.NOT_STARTED:
                case DownloadStatus.WAIT_FOR_DOWNLOAD:
                    DownloadStatus = DownloadStatus.PAUSE_STARTED;
                    StartOrPause = ButtonIcon.Instance().Start;
                    StartOrPause.Fill = DictionaryResource.GetColor("ColorPrimary");
                    break;
                case DownloadStatus.PAUSE_STARTED:
                    DownloadStatus = DownloadStatus.WAIT_FOR_DOWNLOAD;
                    StartOrPause = ButtonIcon.Instance().Pause;
                    StartOrPause.Fill = DictionaryResource.GetColor("ColorPrimary");
                    break;
                case DownloadStatus.PAUSE:
                    DownloadStatus = DownloadStatus.DOWNLOADING;
                    StartOrPause = ButtonIcon.Instance().Pause;
                    StartOrPause.Fill = DictionaryResource.GetColor("ColorPrimary");
                    break;
                case DownloadStatus.DOWNLOADING:
                    DownloadStatus = DownloadStatus.PAUSE;
                    StartOrPause = ButtonIcon.Instance().Start;
                    StartOrPause.Fill = DictionaryResource.GetColor("ColorPrimary");
                    break;
                case DownloadStatus.DOWNLOAD_SUCCEED:
                    // 下载成功后会从下载列表中删除
                    // 不会出现此分支
                    break;
                case DownloadStatus.DOWNLOAD_FAILED:
                    DownloadStatus = DownloadStatus.WAIT_FOR_DOWNLOAD;
                    StartOrPause = ButtonIcon.Instance().Pause;
                    StartOrPause.Fill = DictionaryResource.GetColor("ColorPrimary");
                    break;
                default:
                    break;
            }
        }

        // 下载列表删除事件
        private DelegateCommand deleteCommand;
        public DelegateCommand DeleteCommand => deleteCommand ?? (deleteCommand = new DelegateCommand(ExecuteDeleteCommand));

        /// <summary>
        /// 下载列表删除事件
        /// </summary>
        private void ExecuteDeleteCommand()
        {
            App.DownloadingList.Remove(this);
        }

        #endregion

    }
}
