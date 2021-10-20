using Prism.Mvvm;
using System.Collections.Generic;

namespace DownKyi.Models
{
    public class VideoSection : BindableBase
    {
        public long Id { get; set; }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }

        private List<VideoPage> videoPages;
        public List<VideoPage> VideoPages
        {
            get { return videoPages; }
            set { SetProperty(ref videoPages, value); }
        }
    }
}
