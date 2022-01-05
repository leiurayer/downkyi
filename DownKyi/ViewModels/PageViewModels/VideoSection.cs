using Prism.Mvvm;
using System.Collections.Generic;

namespace DownKyi.ViewModels.PageViewModels
{
    public class VideoSection : BindableBase
    {
        public long Id { get; set; }

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        private List<VideoPage> videoPages;
        public List<VideoPage> VideoPages
        {
            get => videoPages;
            set => SetProperty(ref videoPages, value);
        }
    }
}
