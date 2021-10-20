using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DownKyi.ViewModels.DownloadManager
{
    public class ViewDownloadFinishedViewModel : BaseViewModel
    {
        public const string Tag = "PageDownloadManagerDownloadFinished";

        public ViewDownloadFinishedViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

        }
    }
}
