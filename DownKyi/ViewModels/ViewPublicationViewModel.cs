using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Utils;
using DownKyi.ViewModels.PageViewModels;
using DownKyi.ViewModels.UserSpace;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownKyi.ViewModels
{
    public class ViewPublicationViewModel : BaseViewModel
    {
        public const string Tag = "PagePublication";

        private long mid = -1;

        // 每页视频数量，暂时在此写死，以后在设置中增加选项
        private readonly int VideoNumberInPage = 30;

        #region 页面属性申明

        private VectorImage arrowBack;
        public VectorImage ArrowBack
        {
            get => arrowBack;
            set => SetProperty(ref arrowBack, value);
        }

        private ObservableCollection<TabHeader> tabHeaders;
        public ObservableCollection<TabHeader> TabHeaders
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

        private int countPage;
        public int CountPage
        {
            get => countPage;
            set => SetProperty(ref countPage, value);
        }

        private int currentPage;
        public int CurrentPage
        {
            get => currentPage;
            set => SetProperty(ref currentPage, value);
        }

        #endregion

        public ViewPublicationViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化

            ArrowBack = NavigationIcon.Instance().ArrowBack;
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            TabHeaders = new ObservableCollection<TabHeader>();

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
            ArrowBack.Fill = DictionaryResource.GetColor("ColorText");

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

            // 页面选择
            CountPage = (int)Math.Ceiling(double.Parse(tabHeader.SubTitle) / VideoNumberInPage);
            CurrentPage = 1;
        }

        #endregion

        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void InitView()
        {
            ArrowBack.Fill = DictionaryResource.GetColor("ColorTextDark");

            TabHeaders.Clear();
            SelectTabId = -1;
        }

        /// <summary>
        /// 导航到VideoDetail页面时执行
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            InitView();

            // 根据传入参数不同执行不同任务
            var parameter = navigationContext.Parameters.GetValue<Dictionary<string, object>>("Parameter");
            if (parameter == null)
            {
                return;
            }

            mid = (long)parameter["mid"];
            int tid = (int)parameter["tid"];
            List<PublicationZone> zones = (List<PublicationZone>)parameter["list"];

            foreach (var item in zones)
            {
                TabHeaders.Add(new TabHeader
                {
                    Id = item.Tid,
                    Title = item.Name,
                    SubTitle = item.Count.ToString()
                });
            }

            // 初始选中项
            var selectTab = TabHeaders.FirstOrDefault(item => item.Id == tid);
            SelectTabId = TabHeaders.IndexOf(selectTab);

            // 页面选择
            CountPage = (int)Math.Ceiling(double.Parse(selectTab.SubTitle) / VideoNumberInPage);
            CurrentPage = 1;
        }

    }
}
