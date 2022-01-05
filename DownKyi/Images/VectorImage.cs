using Prism.Mvvm;

namespace DownKyi.Images
{
    public class VectorImage : BindableBase
    {
        private double width;
        public double Width
        {
            get => width;
            set => SetProperty(ref width, value);
        }

        private double height;
        public double Height
        {
            get => height;
            set => SetProperty(ref height, value);
        }

        private string data;
        public string Data
        {
            get => data;
            set => SetProperty(ref data, value);
        }

        private string fill;
        public string Fill
        {
            get => fill;
            set => SetProperty(ref fill, value);
        }

    }
}
