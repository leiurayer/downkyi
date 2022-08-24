using DownKyi.Core.BiliApi.Users.Models;
using DownKyi.Core.Storage;
using DownKyi.Events;
using DownKyi.Images;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels.UserSpace
{
    public class ViewSeasonsSeriesViewModel : BaseViewModel
    {
        public const string Tag = "PageUserSpaceSeasonsSeries";

        private long mid = -1;

        #region 页面属性申明

        private ObservableCollection<SeasonsSeries> seasonsSeries;
        public ObservableCollection<SeasonsSeries> SeasonsSeries
        {
            get => seasonsSeries;
            set => SetProperty(ref seasonsSeries, value);
        }

        private int selectedItem;
        public int SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        #endregion

        public ViewSeasonsSeriesViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化

            SeasonsSeries = new ObservableCollection<SeasonsSeries>();

            #endregion
        }

        #region 命令申明

        // 视频选择事件
        private DelegateCommand<object> seasonsSeriesCommand;
        public DelegateCommand<object> SeasonsSeriesCommand => seasonsSeriesCommand ?? (seasonsSeriesCommand = new DelegateCommand<object>(ExecuteSeasonsSeriesCommand));

        /// <summary>
        /// 视频选择事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteSeasonsSeriesCommand(object parameter)
        {
            if (!(parameter is SeasonsSeries seasonsSeries)) { return; }

            // 应该用枚举的，偷懒直接用数字
            int type = 0;
            if (seasonsSeries.TypeImage == NormalIcon.Instance().SeasonsSeries)
            {
                type = 1;
            }
            else if (seasonsSeries.TypeImage == NormalIcon.Instance().Channel1)
            {
                type = 2;
            }
            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "mid", mid },
                { "id", seasonsSeries.Id },
                { "name", seasonsSeries.Name },
                { "count", seasonsSeries.Count },
                { "type", type }
            };

            // 进入视频页面
            NavigationParam param = new NavigationParam
            {
                ViewName = ViewModels.ViewSeasonsSeriesViewModel.Tag,
                ParentViewName = ViewUserSpaceViewModel.Tag,
                Parameter = data
            };
            eventAggregator.GetEvent<NavigationEvent>().Publish(param);

            SelectedItem = -1;
        }

        #endregion

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);

            SeasonsSeries.Clear();
            SelectedItem = -1;
        }

        /// <summary>
        /// 接收mid参数
        /// </summary>
        /// <param name="navigationContext"></param>
        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            SeasonsSeries.Clear();
            SelectedItem = -1;

            // 根据传入参数不同执行不同任务
            var parameter = navigationContext.Parameters.GetValue<SpaceSeasonsSeries>("object");
            if (parameter == null)
            {
                return;
            }

            // 传入mid
            mid = navigationContext.Parameters.GetValue<long>("mid");

            foreach (var item in parameter.SeasonsList)
            {
                if (item.Meta.Total <= 0) { continue; }

                BitmapImage image = null;
                if (item.Meta.Cover == null || item.Meta.Cover == "")
                {
                    image = new BitmapImage(new Uri($"pack://application:,,,/Resources/video-placeholder.png"));
                }
                else
                {
                    StorageCover storageCover = new StorageCover();
                    string cover = null;
                    await Task.Run(() =>
                    {
                        cover = storageCover.GetCover(item.Meta.Cover);
                    });
                    image = storageCover.GetCoverThumbnail(cover, 190, 190);
                }

                // 当地时区
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                DateTime dateCTime = startTime.AddSeconds(item.Meta.Ptime);
                string mtime = dateCTime.ToString("yyyy-MM-dd");

                SeasonsSeries.Add(new SeasonsSeries
                {
                    Id = item.Meta.SeasonId,
                    Cover = image,
                    TypeImage = NormalIcon.Instance().SeasonsSeries,
                    Name = item.Meta.Name,
                    Count = item.Meta.Total,
                    Ctime = mtime
                });
            }

            foreach (var item in parameter.SeriesList)
            {
                if (item.Meta.Total <= 0) { continue; }

                BitmapImage image = null;
                if (item.Meta.Cover == null || item.Meta.Cover == "")
                {
                    image = new BitmapImage(new Uri($"pack://application:,,,/Resources/video-placeholder.png"));
                }
                else
                {
                    StorageCover storageCover = new StorageCover();
                    string cover = null;
                    await Task.Run(() =>
                    {
                        cover = storageCover.GetCover(item.Meta.Cover);
                    });
                    image = storageCover.GetCoverThumbnail(cover, 190, 190);
                }

                // 当地时区
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                DateTime dateCTime = startTime.AddSeconds(item.Meta.Mtime);
                string mtime = dateCTime.ToString("yyyy-MM-dd");

                SeasonsSeries.Add(new SeasonsSeries
                {
                    Id = item.Meta.SeriesId,
                    Cover = image,
                    TypeImage = NormalIcon.Instance().Channel1,
                    Name = item.Meta.Name,
                    Count = item.Meta.Total,
                    Ctime = mtime
                });
            }

        }
    }
}
