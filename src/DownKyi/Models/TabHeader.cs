using DownKyi.Images;
using Prism.Mvvm;

namespace DownKyi.Models
{
    public class TabHeader : BindableBase
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        private VectorImage image;
        public VectorImage Image
        {
            get { return image; }
            set { SetProperty(ref image, value); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string subTitle;
        public string SubTitle
        {
            get { return subTitle; }
            set { SetProperty(ref subTitle, value); }
        }

    }
}
