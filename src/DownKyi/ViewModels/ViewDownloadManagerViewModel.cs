using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Models;
using DownKyi.Utils;
using DownKyi.ViewModels.DownloadManager;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;

namespace DownKyi.ViewModels
{
    public class ViewDownloadManagerViewModel : BaseViewModel
    {
        public const string Tag = "PageDownloadManager";

        private readonly IRegionManager regionManager;

        #region 页面属性申明

        private VectorImage arrowBack;
        public VectorImage ArrowBack
        {
            get { return arrowBack; }
            set { SetProperty(ref arrowBack, value); }
        }

        private List<TabHeader> tabHeaders;
        public List<TabHeader> TabHeaders
        {
            get { return tabHeaders; }
            set { SetProperty(ref tabHeaders, value); }
        }

        private int selectTabId;
        public int SelectTabId
        {
            get { return selectTabId; }
            set { SetProperty(ref selectTabId, value); }
        }

        #endregion

        public ViewDownloadManagerViewModel(IRegionManager regionManager, IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.regionManager = regionManager;

            #region 属性初始化

            ArrowBack = NavigationIcon.Instance().ArrowBack;
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            TabHeaders = new List<TabHeader>
            {
                new TabHeader { Id = 0, Image = NormalIcon.Instance().Downloading, Title = DictionaryResource.GetString("Downloading") },
                new TabHeader { Id = 1, Image = NormalIcon.Instance().DownloadFinished, Title = DictionaryResource.GetString("DownloadFinished") }
            };

            #endregion
        }

        #region 命令申明

        // 返回事件
        private DelegateCommand backSpaceCommand;
        public DelegateCommand BackSpaceCommand => backSpaceCommand ?? (backSpaceCommand = new DelegateCommand(ExecuteBackSpace));

        /// <summary>
        /// 返回事件
        /// </summary>
        private void ExecuteBackSpace()
        {
            NavigationParam parameter = new NavigationParam
            {
                ViewName = ParentView,
                ParentViewName = null,
                Parameter = null
            };
            eventAggregator.GetEvent<NavigationEvent>().Publish(parameter);
        }

        // 左侧tab点击事件
        private DelegateCommand<object> leftTabHeadersCommand;
        public DelegateCommand<object> LeftTabHeadersCommand => leftTabHeadersCommand ?? (leftTabHeadersCommand = new DelegateCommand<object>(ExecuteLeftTabHeadersCommand));

        /// <summary>
        /// 左侧tab点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteLeftTabHeadersCommand(object parameter)
        {
            if (!(parameter is TabHeader tabHeader)) { return; }

            NavigationParameters param = new NavigationParameters();

            switch (tabHeader.Id)
            {
                case 0:
                    regionManager.RequestNavigate("DownloadManagerContentRegion", ViewDownloadingViewModel.Tag, param);
                    break;
                case 1:
                    regionManager.RequestNavigate("DownloadManagerContentRegion", ViewDownloadFinishedViewModel.Tag, param);
                    break;
                default:
                    break;
            }
        }

        #endregion

        /// <summary>
        /// 导航到VideoDetail页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            //// 进入设置页面时显示的设置项
            SelectTabId = 0;
            regionManager.RequestNavigate("DownloadManagerContentRegion", ViewDownloadingViewModel.Tag, new NavigationParameters());
        }

    }
}
