using DownKyi.Images;
using DownKyi.Models;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using System.Collections.ObjectModel;

namespace DownKyi.ViewModels.DownloadManager
{
    public class ViewDownloadingViewModel : BaseViewModel
    {
        public const string Tag = "PageDownloadManagerDownloading";

        #region 页面属性申明

        private ObservableCollection<DownloadingItem> downloadingList;
        public ObservableCollection<DownloadingItem> DownloadingList
        {
            get => downloadingList;
            set => SetProperty(ref downloadingList, value);
        }

        #endregion

        public ViewDownloadingViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            // 初始化DownloadingList
            DownloadingList = App.DownloadingList;
        }

        #region 命令申明

        // 暂停所有下载事件
        private DelegateCommand pauseAllDownloadingCommand;
        public DelegateCommand PauseAllDownloadingCommand => pauseAllDownloadingCommand ?? (pauseAllDownloadingCommand = new DelegateCommand(ExecutePauseAllDownloadingCommand));

        /// <summary>
        /// 暂停所有下载事件
        /// </summary>
        private void ExecutePauseAllDownloadingCommand()
        {
            foreach (DownloadingItem downloading in downloadingList)
            {
                switch (downloading.DownloadStatus)
                {
                    case DownloadStatus.NOT_STARTED:
                    case DownloadStatus.WAIT_FOR_DOWNLOAD:
                        downloading.DownloadStatus = DownloadStatus.PAUSE_STARTED;
                        break;
                    case DownloadStatus.PAUSE_STARTED:
                        break;
                    case DownloadStatus.PAUSE:
                        break;
                    case DownloadStatus.DOWNLOADING:
                        downloading.DownloadStatus = DownloadStatus.PAUSE;
                        break;
                    case DownloadStatus.DOWNLOAD_SUCCEED:
                        // 下载成功后会从下载列表中删除
                        // 不会出现此分支
                        break;
                    case DownloadStatus.DOWNLOAD_FAILED:
                        break;
                    default:
                        break;
                }

                downloading.StartOrPause = ButtonIcon.Instance().Start;
                downloading.StartOrPause.Fill = DictionaryResource.GetColor("ColorPrimary");
            }
        }

        // 继续所有下载事件
        private DelegateCommand continueAllDownloadingCommand;
        public DelegateCommand ContinueAllDownloadingCommand => continueAllDownloadingCommand ?? (continueAllDownloadingCommand = new DelegateCommand(ExecuteContinueAllDownloadingCommand));

        /// <summary>
        /// 继续所有下载事件
        /// </summary>
        private void ExecuteContinueAllDownloadingCommand()
        {
            foreach (DownloadingItem downloading in downloadingList)
            {
                switch (downloading.DownloadStatus)
                {
                    case DownloadStatus.NOT_STARTED:
                    case DownloadStatus.WAIT_FOR_DOWNLOAD:
                        break;
                    case DownloadStatus.PAUSE_STARTED:
                        downloading.DownloadStatus = DownloadStatus.WAIT_FOR_DOWNLOAD;
                        break;
                    case DownloadStatus.PAUSE:
                        downloading.DownloadStatus = DownloadStatus.DOWNLOADING;
                        break;
                    case DownloadStatus.DOWNLOADING:
                        break;
                    case DownloadStatus.DOWNLOAD_SUCCEED:
                        // 下载成功后会从下载列表中删除
                        // 不会出现此分支
                        break;
                    case DownloadStatus.DOWNLOAD_FAILED:
                        downloading.DownloadStatus = DownloadStatus.WAIT_FOR_DOWNLOAD;
                        break;
                    default:
                        break;
                }

                downloading.StartOrPause = ButtonIcon.Instance().Pause;
                downloading.StartOrPause.Fill = DictionaryResource.GetColor("ColorPrimary");
            }
        }

        // 删除所有下载事件
        private DelegateCommand deleteAllDownloadingCommand;
        public DelegateCommand DeleteAllDownloadingCommand => deleteAllDownloadingCommand ?? (deleteAllDownloadingCommand = new DelegateCommand(ExecuteDeleteAllDownloadingCommand));

        /// <summary>
        /// 删除所有下载事件
        /// </summary>
        private void ExecuteDeleteAllDownloadingCommand()
        {
            DownloadingList.Clear();
        }

        #endregion

    }
}
