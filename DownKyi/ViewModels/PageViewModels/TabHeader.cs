using DownKyi.Images;
using Prism.Mvvm;

namespace DownKyi.ViewModels.PageViewModels
{
    public class TabHeader : BindableBase
    {
        private int id;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

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

        private string subTitle;
        public string SubTitle
        {
            get => subTitle;
            set => SetProperty(ref subTitle, value);
        }

    }
}
