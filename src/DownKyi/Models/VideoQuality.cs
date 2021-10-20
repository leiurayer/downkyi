using Prism.Mvvm;
using System.Collections.Generic;

namespace DownKyi.Models
{
    public class VideoQuality : BindableBase
    {
        private int quality;
        public int Quality
        {
            get { return quality; }
            set { SetProperty(ref quality, value); }
        }

        private string qualityFormat;
        public string QualityFormat
        {
            get { return qualityFormat; }
            set { SetProperty(ref qualityFormat, value); }
        }

        private List<string> videoCodecList;
        public List<string> VideoCodecList
        {
            get { return videoCodecList; }
            set { SetProperty(ref videoCodecList, value); }
        }

        private string selectedVideoCodec;
        public string SelectedVideoCodec
        {
            get { return selectedVideoCodec; }
            set { SetProperty(ref selectedVideoCodec, value); }
        }

    }
}
