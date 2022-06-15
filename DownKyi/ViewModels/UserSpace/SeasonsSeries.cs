using DownKyi.Images;
using Prism.Mvvm;
using System.Windows.Media;

namespace DownKyi.ViewModels.UserSpace
{
    public class SeasonsSeries : BindableBase
    {
        public long Id { get; set; }

        private ImageSource cover;
        public ImageSource Cover
        {
            get => cover;
            set => SetProperty(ref cover, value);
        }

        private VectorImage typeImage;
        public VectorImage TypeImage
        {
            get => typeImage;
            set => SetProperty(ref typeImage, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private int count;
        public int Count
        {
            get => count;
            set => SetProperty(ref count, value);
        }

        private string ctime;
        public string Ctime
        {
            get => ctime;
            set => SetProperty(ref ctime, value);
        }

    }
}
