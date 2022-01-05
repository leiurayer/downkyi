using DownKyi.Core.BiliApi.VideoStream.Models;
using Prism.Mvvm;
using System.Collections.Generic;

namespace DownKyi.ViewModels.PageViewModels
{
    public class VideoPage : BindableBase
    {
        public PlayUrl PlayUrl { get; set; }

        public long Avid { get; set; }
        public string Bvid { get; set; }
        public long Cid { get; set; }
        public long EpisodeId { get; set; }

        public string FirstFrame { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        private int order;
        public int Order
        {
            get => order;
            set => SetProperty(ref order, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string duration;
        public string Duration
        {
            get => duration;
            set => SetProperty(ref duration, value);
        }

        private List<string> audioQualityFormatList;
        public List<string> AudioQualityFormatList
        {
            get => audioQualityFormatList;
            set => SetProperty(ref audioQualityFormatList, value);
        }

        private string audioQualityFormat;
        public string AudioQualityFormat
        {
            get => audioQualityFormat;
            set => SetProperty(ref audioQualityFormat, value);
        }

        private List<VideoQuality> videoQualityList;
        public List<VideoQuality> VideoQualityList
        {
            get => videoQualityList;
            set => SetProperty(ref videoQualityList, value);
        }

        private VideoQuality videoQuality;
        public VideoQuality VideoQuality
        {
            get => videoQuality;
            set => SetProperty(ref videoQuality, value);
        }

    }
}
