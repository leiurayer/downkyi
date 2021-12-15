using DownKyi.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

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


            //// 下载列表发生变化时执行的任务
            //DownloadingList.CollectionChanged += new NotifyCollectionChangedEventHandler((object sender, NotifyCollectionChangedEventArgs e) =>
            //{
            //    // save the downloading list and finished list.
            //    //SaveHistory();
            //});

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
        }

        // 继续所有下载事件
        private DelegateCommand continueAllDownloadingCommand;
        public DelegateCommand ContinueAllDownloadingCommand => continueAllDownloadingCommand ?? (continueAllDownloadingCommand = new DelegateCommand(ExecuteContinueAllDownloadingCommand));

        /// <summary>
        /// 继续所有下载事件
        /// </summary>
        private void ExecuteContinueAllDownloadingCommand()
        {
        }

        // 删除所有下载事件
        private DelegateCommand deleteAllDownloadingCommand;
        public DelegateCommand DeleteAllDownloadingCommand => deleteAllDownloadingCommand ?? (deleteAllDownloadingCommand = new DelegateCommand(ExecuteDeleteAllDownloadingCommand));

        /// <summary>
        /// 删除所有下载事件
        /// </summary>
        private void ExecuteDeleteAllDownloadingCommand()
        {
        }

        #endregion

    }
}
