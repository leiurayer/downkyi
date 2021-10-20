using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DownKyi.ViewModels
{
    public class ViewMySpaceViewModel : BaseViewModel
    {
        public const string Tag = "PageMySpace";

        // mid
        private long mid;

        public ViewMySpaceViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
        }

        /// <summary>
        /// 接收mid参数
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            // 根据传入参数不同执行不同任务
            long parameter = navigationContext.Parameters.GetValue<long>("Parameter");
            if (parameter == 0)
            {
                return;
            }
            mid = parameter;
        }

    }
}
