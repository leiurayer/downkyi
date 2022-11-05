using DownKyi.Core.BiliApi.Users.Models;
using DownKyi.Core.Storage;
using DownKyi.Utils;
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
    public class ViewChannelViewModel : BaseViewModel
    {
        public const string Tag = "PageUserSpaceChannel";

        private long mid = -1;

        #region 页面属性申明

        private ObservableCollection<Channel> channels;
        public ObservableCollection<Channel> Channels
        {
            get => channels;
            set => SetProperty(ref channels, value);
        }

        private int selectedItem;
        public int SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
        }

        #endregion

        public ViewChannelViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化

            Channels = new ObservableCollection<Channel>();

            #endregion
        }

        #region 命令申明

        // 视频选择事件
        private DelegateCommand<object> channelsCommand;
        public DelegateCommand<object> ChannelsCommand => channelsCommand ?? (channelsCommand = new DelegateCommand<object>(ExecuteChannelsCommand));

        /// <summary>
        /// 视频选择事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteChannelsCommand(object parameter)
        {
            if (!(parameter is Channel channel)) { return; }

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "mid", mid },
                { "cid", channel.Cid },
                { "name", channel.Name },
                { "count", channel.Count }
            };

            // 进入视频页面
            NavigateToView.NavigationView(eventAggregator, ViewModels.ViewChannelViewModel.Tag, ViewUserSpaceViewModel.Tag, data);

            SelectedItem = -1;
        }

        #endregion

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);

            Channels.Clear();
            SelectedItem = -1;
        }

        /// <summary>
        /// 接收mid参数
        /// </summary>
        /// <param name="navigationContext"></param>
        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            Channels.Clear();
            SelectedItem = -1;

            // 根据传入参数不同执行不同任务
            var parameter = navigationContext.Parameters.GetValue<List<SpaceChannelList>>("object");
            if (parameter == null)
            {
                return;
            }

            // 传入mid
            mid = navigationContext.Parameters.GetValue<long>("mid");

            foreach (var channel in parameter)
            {
                if (channel.Count <= 0) { continue; }

                BitmapImage image = null;
                if (channel.Cover == null || channel.Cover == "")
                {
                    image = new BitmapImage(new Uri($"pack://application:,,,/Resources/video-placeholder.png"));
                }
                else
                {
                    StorageCover storageCover = new StorageCover();
                    string cover = null;
                    await Task.Run(() =>
                    {
                        cover = storageCover.GetCover(channel.Cover);
                    });
                    image = storageCover.GetCoverThumbnail(cover, 190, 190);
                }

                // 当地时区
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                DateTime dateCTime = startTime.AddSeconds(channel.Mtime);
                string mtime = dateCTime.ToString("yyyy-MM-dd");

                Channels.Add(new Channel
                {
                    Cid = channel.Cid,
                    Cover = image,
                    Name = channel.Name,
                    Count = channel.Count,
                    Ctime = mtime
                });
            }

        }
    }
}
