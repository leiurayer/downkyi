using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Utils;
using DownKyi.ViewModels.PageViewModels;
using DownKyi.ViewModels.Settings;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;

namespace DownKyi.ViewModels
{
    public class ViewSettingsViewModel : BaseViewModel
    {
        public const string Tag = "PageSettings";

        private readonly IRegionManager regionManager;

        #region 页面属性申明

        private VectorImage arrowBack;
        public VectorImage ArrowBack
        {
            get => arrowBack;
            set => SetProperty(ref arrowBack, value);
        }

        private List<TabHeader> tabHeaders;
        public List<TabHeader> TabHeaders
        {
            get => tabHeaders;
            set => SetProperty(ref tabHeaders, value);
        }

        private int selectTabId;
        public int SelectTabId
        {
            get => selectTabId;
            set => SetProperty(ref selectTabId, value);
        }

        #endregion


        public ViewSettingsViewModel(IRegionManager regionManager, IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.regionManager = regionManager;

            #region 属性初始化

            ArrowBack = NavigationIcon.Instance().ArrowBack;
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            TabHeaders = new List<TabHeader>
            {
                new TabHeader { Id = 0, Title = DictionaryResource.GetString("Basic") },
                new TabHeader { Id = 1, Title = DictionaryResource.GetString("Network") },
                new TabHeader { Id = 2, Title = DictionaryResource.GetString("Video") },
                new TabHeader { Id = 3, Title = DictionaryResource.GetString("SettingDanmaku") },
                new TabHeader { Id = 4, Title = DictionaryResource.GetString("About") }
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
                    regionManager.RequestNavigate("SettingsContentRegion", ViewBasicViewModel.Tag, param);
                    break;
                case 1:
                    regionManager.RequestNavigate("SettingsContentRegion", ViewNetworkViewModel.Tag, param);
                    break;
                case 2:
                    regionManager.RequestNavigate("SettingsContentRegion", ViewVideoViewModel.Tag, param);
                    break;
                case 3:
                    regionManager.RequestNavigate("SettingsContentRegion", ViewDanmakuViewModel.Tag, param);
                    break;
                case 4:
                    regionManager.RequestNavigate("SettingsContentRegion", ViewAboutViewModel.Tag, param);
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

            // 进入设置页面时显示的设置项
            SelectTabId = 0;
            regionManager.RequestNavigate("SettingsContentRegion", ViewBasicViewModel.Tag, new NavigationParameters());
        }

    }
}
