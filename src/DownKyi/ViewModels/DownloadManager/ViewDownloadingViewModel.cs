using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DownKyi.ViewModels.DownloadManager
{
    public class ViewDownloadingViewModel : BaseViewModel
    {
        public const string Tag = "PageDownloadManagerDownloading";

        public ViewDownloadingViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

        }
    }
}
