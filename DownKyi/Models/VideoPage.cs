using DownKyi.Core.BiliApi.VideoStream.Models;
using Prism.Mvvm;
using System.Collections.Generic;

namespace DownKyi.Models
{
    public class VideoPage : BindableBase
    {
        public PlayUrl PlayUrl { get; set; }

        public long Avid { get; set; }
        public string Bvid { get; set; }
        public long Cid { get; set; }
        public long EpisodeId { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }

        private int order;
        public int Order
        {
            get { return order; }
            set { SetProperty(ref order, value); }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private string duration;
        public string Duration
        {
            get { return duration; }
            set { SetProperty(ref duration, value); }
        }

        private List<string> audioQualityFormatList;
        public List<string> AudioQualityFormatList
        {
            get { return audioQualityFormatList; }
            set { SetProperty(ref audioQualityFormatList, value); }
        }

        private string audioQualityFormat;
        public string AudioQualityFormat
        {
            get { return audioQualityFormat; }
            set { SetProperty(ref audioQualityFormat, value); }
        }

        private List<VideoQuality> videoQualityList;
        public List<VideoQuality> VideoQualityList
        {
            get { return videoQualityList; }
            set { SetProperty(ref videoQualityList, value); }
        }

        private VideoQuality videoQuality;
        public VideoQuality VideoQuality
        {
            get { return videoQuality; }
            set { SetProperty(ref videoQuality, value); }
        }

    }
}
