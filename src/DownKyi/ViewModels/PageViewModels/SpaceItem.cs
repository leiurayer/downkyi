using DownKyi.Images;
using Prism.Mvvm;

namespace DownKyi.ViewModels.PageViewModels
{
    public class SpaceItem : BindableBase
    {
        private VectorImage image;
        public VectorImage Image
        {
            get => image;
            set => SetProperty(ref image, value);
        }

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string subtitle;
        public string Subtitle
        {
            get => subtitle;
            set => SetProperty(ref subtitle, value);
        }
    }
}
