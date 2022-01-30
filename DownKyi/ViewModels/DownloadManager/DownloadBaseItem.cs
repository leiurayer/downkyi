using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.Zone;
using DownKyi.Models;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Media;

namespace DownKyi.ViewModels.DownloadManager
{
    public class DownloadBaseItem : BindableBase
    {
        // model数据
        private DownloadBase downloadBase;
        public DownloadBase DownloadBase
        {
            get => downloadBase;
            set
            {
                downloadBase = value;

                if (value != null)
                {
                    ZoneImage = (DrawingImage)Application.Current.Resources[VideoZoneIcon.Instance().GetZoneImageKey(DownloadBase.ZoneId)];
                }
            }
        }

        // 视频分区image
        private DrawingImage zoneImage;
        public DrawingImage ZoneImage
        {
            get => zoneImage;
            set => SetProperty(ref zoneImage, value);
        }

        // 视频序号
        public int Order
        {
            get => DownloadBase == null ? 0 : DownloadBase.Order;
            set
            {
                DownloadBase.Order = value;
                RaisePropertyChanged("Order");
            }
        }

        // 视频主标题
        public string MainTitle
        {
            get => DownloadBase == null ? "" : DownloadBase.MainTitle;
            set
            {
                DownloadBase.MainTitle = value;
                RaisePropertyChanged("MainTitle");
            }
        }

        // 视频标题
        public string Name
        {
            get => DownloadBase == null ? "" : DownloadBase.Name;
            set
            {
                DownloadBase.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        // 时长
        public string Duration
        {
            get => DownloadBase == null ? "" : DownloadBase.Duration;
            set
            {
                DownloadBase.Duration = value;
                RaisePropertyChanged("Duration");
            }
        }

        // 视频编码名称，AVC、HEVC
        public string VideoCodecName
        {
            get => DownloadBase == null ? "" : DownloadBase.VideoCodecName;
            set
            {
                DownloadBase.VideoCodecName = value;
                RaisePropertyChanged("VideoCodecName");
            }
        }

        // 视频画质
        public Quality Resolution
        {
            get => DownloadBase == null ? null : DownloadBase.Resolution;
            set
            {
                DownloadBase.Resolution = value;
                RaisePropertyChanged("Resolution");
            }
        }

        // 音频编码
        public Quality AudioCodec
        {
            get => DownloadBase == null ? null : DownloadBase.AudioCodec;
            set
            {
                DownloadBase.AudioCodec = value;
                RaisePropertyChanged("AudioCodec");
            }
        }

        // 文件大小
        public string FileSize
        {
            get => DownloadBase == null ? "" : DownloadBase.FileSize;
            set
            {
                DownloadBase.FileSize = value;
                RaisePropertyChanged("FileSize");
            }
        }

    }
}
