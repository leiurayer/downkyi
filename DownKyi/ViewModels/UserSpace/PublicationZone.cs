using Prism.Mvvm;
using System.Windows.Media;

namespace DownKyi.ViewModels.UserSpace
{
    public class PublicationZone : BindableBase
    {
        public int Tid { get; set; }

        private DrawingImage icon;
        public DrawingImage Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
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
    }
}
