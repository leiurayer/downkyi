using DownKyi.Core.BiliApi.Users.Models;
using DownKyi.Core.BiliApi.Zone;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace DownKyi.ViewModels.UserSpace
{
    public class ViewArchiveViewModel : BaseViewModel
    {
        public const string Tag = "PageUserSpaceArchive";

        private long mid = -1;

        #region 页面属性申明

        private ObservableCollection<PublicationZone> publicationZones;
        public ObservableCollection<PublicationZone> PublicationZones
        {
            get => publicationZones;
            set => SetProperty(ref publicationZones, value);
        }

        private int selectedItem;
        public int SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        #endregion

        public ViewArchiveViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化

            PublicationZones = new ObservableCollection<PublicationZone>();

            #endregion
        }

        #region 命令申明

        // 视频选择事件
        private DelegateCommand<object> publicationZonesCommand;
        public DelegateCommand<object> PublicationZonesCommand => publicationZonesCommand ?? (publicationZonesCommand = new DelegateCommand<object>(ExecutePublicationZonesCommand));

        /// <summary>
        /// 视频选择事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecutePublicationZonesCommand(object parameter)
        {
            if (!(parameter is PublicationZone zone)) { return; }

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "mid", mid },
                { "tid", zone.Tid },
                { "list", PublicationZones.ToList() }
            };

            // 进入视频页面
            NavigateToView.NavigationView(eventAggregator, ViewPublicationViewModel.Tag, ViewUserSpaceViewModel.Tag, data);

            SelectedItem = -1;
        }

        #endregion

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);

            PublicationZones.Clear();
            SelectedItem = -1;
        }

        /// <summary>
        /// 接收mid参数
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            PublicationZones.Clear();
            SelectedItem = -1;

            // 根据传入参数不同执行不同任务
            var parameter = navigationContext.Parameters.GetValue<List<SpacePublicationListTypeVideoZone>>("object");
            if (parameter == null)
            {
                return;
            }

            // 传入mid
            mid = navigationContext.Parameters.GetValue<long>("mid");

            int VideoCount = 0;
            foreach (var zone in parameter)
            {
                VideoCount += zone.Count;
                string iconKey = VideoZoneIcon.Instance().GetZoneImageKey(zone.Tid);
                publicationZones.Add(new PublicationZone
                {
                    Tid = zone.Tid,
                    Icon = (DrawingImage)Application.Current.Resources[iconKey],
                    Name = zone.Name,
                    Count = zone.Count
                });
            }

            // 全部
            publicationZones.Insert(0, new PublicationZone
            {
                Tid = 0,
                Icon = (DrawingImage)Application.Current.Resources["videoUpDrawingImage"],
                Name = DictionaryResource.GetString("AllPublicationZones"),
                Count = VideoCount
            });

        }
    }
}
