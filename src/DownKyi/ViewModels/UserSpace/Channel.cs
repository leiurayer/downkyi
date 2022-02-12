using Prism.Mvvm;
using System.Windows.Media;

namespace DownKyi.ViewModels.UserSpace
{
    public class Channel : BindableBase
    {
        public long Cid { get; set; }

        private ImageSource cover;
        public ImageSource Cover
        {
            get => cover;
            set => SetProperty(ref cover, value);
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
