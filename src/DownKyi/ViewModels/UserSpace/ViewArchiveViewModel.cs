using DownKyi.Core.BiliApi.Users.Models;
using DownKyi.Core.BiliApi.Zone;
using DownKyi.Utils;
using Prism.Events;
using Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace DownKyi.ViewModels.UserSpace
{
    public class ViewArchiveViewModel : BaseViewModel
    {
        public const string Tag = "ArchiveView";

        #region 页面属性申明

        private ObservableCollection<PublicationZone> publicationZones;
        public ObservableCollection<PublicationZone> PublicationZones
        {
            get => publicationZones;
            set => SetProperty(ref publicationZones, value);
        }

        #endregion

        public ViewArchiveViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化

            PublicationZones = new ObservableCollection<PublicationZone>();

            #endregion
        }

        #region 命令申明
        #endregion

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);

            PublicationZones.Clear();
        }

        /// <summary>
        /// 接收mid参数
        /// </summary>
        /// <param name="navigationContext"></param>
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            PublicationZones.Clear();

            // 根据传入参数不同执行不同任务
            var parameter = navigationContext.Parameters.GetValue<List<SpacePublicationListTypeVideoZone>>("object");
            if (parameter == null)
            {
                return;
            }

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
