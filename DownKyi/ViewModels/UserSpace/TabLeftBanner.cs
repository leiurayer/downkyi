using DownKyi.Images;
using Prism.Mvvm;

namespace DownKyi.ViewModels.UserSpace
{
    public class TabLeftBanner : BindableBase
    {
        public object Object { get; set; }

        public int Id { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        private VectorImage icon;
        public VectorImage Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        private string iconColor;
        public string IconColor
        {
            get => iconColor;
            set => SetProperty(ref iconColor, value);
        }

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

    }
}
