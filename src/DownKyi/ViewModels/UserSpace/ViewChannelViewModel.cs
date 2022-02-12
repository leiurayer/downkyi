using DownKyi.Core.BiliApi.Users.Models;
using DownKyi.Core.Storage;
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
        public const string Tag = "Channel";

        #region 页面属性申明

        private ObservableCollection<Channel> channels;
        public ObservableCollection<Channel> Channels
        {
            get => channels;
            set => SetProperty(ref channels, value);
        }

        #endregion

        public ViewChannelViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化

            Channels = new ObservableCollection<Channel>();

            #endregion
        }

        #region 命令申明
        #endregion

        public override void OnNavigatedFrom(NavigationContext navigationContext)
        {
            base.OnNavigatedFrom(navigationContext);

            Channels.Clear();
        }

        /// <summary>
        /// 接收mid参数
        /// </summary>
        /// <param name="navigationContext"></param>
        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            Channels.Clear();

            // 根据传入参数不同执行不同任务
            var parameter = navigationContext.Parameters.GetValue<List<SpaceChannelList>>("object");
            if (parameter == null)
            {
                return;
            }

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
