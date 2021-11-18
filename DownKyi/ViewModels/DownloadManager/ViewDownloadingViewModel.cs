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
    public class ViewDownloadingViewModel : BaseViewModel
    {
        public const string Tag = "PageDownloadManagerDownloading";

        #region 页面属性申明

        private ObservableCollection<DownloadingItem> downloadingList;
        public ObservableCollection<DownloadingItem> DownloadingList
        {
            get { return downloadingList; }
            set { SetProperty(ref downloadingList, value); }
        }

        #endregion

        public ViewDownloadingViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            DownloadingList = App.DownloadingList;
        }
    }
}
