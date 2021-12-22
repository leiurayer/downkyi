using DownKyi.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DownKyi.ViewModels.DownloadManager
{
    public class ViewDownloadFinishedViewModel : BaseViewModel
    {
        public const string Tag = "PageDownloadManagerDownloadFinished";

        #region 页面属性申明

        private ObservableCollection<DownloadedItem> downloadedList;
        public ObservableCollection<DownloadedItem> DownloadedList
        {
            get { return downloadedList; }
            set { SetProperty(ref downloadedList, value); }
        }

        #endregion

        public ViewDownloadFinishedViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            DownloadedList = App.DownloadedList;
        }
    }
}
