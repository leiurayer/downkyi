using DownKyi.Images;
using DownKyi.Models;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

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
                switch (downloading.Downloading.DownloadStatus)
                {
                    case DownloadStatus.NOT_STARTED:
                    case DownloadStatus.WAIT_FOR_DOWNLOAD:
                        downloading.Downloading.DownloadStatus = DownloadStatus.PAUSE;
                        downloading.DownloadStatusTitle = DictionaryResource.GetString("Pausing");
                        downloading.StartOrPause = ButtonIcon.Instance().Start;
                        downloading.StartOrPause.Fill = DictionaryResource.GetColor("ColorPrimary");
                        break;
                    case DownloadStatus.PAUSE_STARTED:
                        break;
                    case DownloadStatus.PAUSE:
                        break;
                    //case DownloadStatus.PAUSE_TO_WAIT:
                    case DownloadStatus.DOWNLOADING:
                        downloading.Downloading.DownloadStatus = DownloadStatus.PAUSE;
                        downloading.DownloadStatusTitle = DictionaryResource.GetString("Pausing");
                        downloading.StartOrPause = ButtonIcon.Instance().Start;
                        downloading.StartOrPause.Fill = DictionaryResource.GetColor("ColorPrimary");
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
                switch (downloading.Downloading.DownloadStatus)
                {
                    case DownloadStatus.NOT_STARTED:
                    case DownloadStatus.WAIT_FOR_DOWNLOAD:
                        downloading.Downloading.DownloadStatus = DownloadStatus.WAIT_FOR_DOWNLOAD;
                        downloading.DownloadStatusTitle = DictionaryResource.GetString("Waiting");
                        break;
                    case DownloadStatus.PAUSE_STARTED:
                        downloading.Downloading.DownloadStatus = DownloadStatus.WAIT_FOR_DOWNLOAD;
                        downloading.DownloadStatusTitle = DictionaryResource.GetString("Waiting");
                        break;
                    case DownloadStatus.PAUSE:
                        downloading.Downloading.DownloadStatus = DownloadStatus.WAIT_FOR_DOWNLOAD;
                        downloading.DownloadStatusTitle = DictionaryResource.GetString("Waiting");
                        break;
                    //case DownloadStatus.PAUSE_TO_WAIT:
                    //    break;
                    case DownloadStatus.DOWNLOADING:
                        break;
                    case DownloadStatus.DOWNLOAD_SUCCEED:
                        // 下载成功后会从下载列表中删除
                        // 不会出现此分支
                        break;
                    case DownloadStatus.DOWNLOAD_FAILED:
                        downloading.Downloading.DownloadStatus = DownloadStatus.WAIT_FOR_DOWNLOAD;
                        downloading.DownloadStatusTitle = DictionaryResource.GetString("Waiting");
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
        private async void ExecuteDeleteAllDownloadingCommand()
        {
            // 使用Clear()不能触发NotifyCollectionChangedAction.Remove事件
            // 因此遍历删除
            // DownloadingList中元素被删除后不能继续遍历
            await Task.Run(() =>
            {
                List<DownloadingItem> list = DownloadingList.ToList();
                foreach (DownloadingItem item in list)
                {
                    App.PropertyChangeAsync(new Action(() =>
                    {
                        App.DownloadingList.Remove(item);
                    }));
                }
            });
        }

        #endregion

    }
}
