using Prism.Mvvm;

namespace DownKyi.Images
{
    public class VectorImage : BindableBase
    {
        private double width;
        public double Width
        {
            get { return width; }
            set { SetProperty(ref width, value); }
        }

        private double height;
        public double Height
        {
            get { return height; }
            set { SetProperty(ref height, value); }
        }

        private string data;
        public string Data
        {
            get { return data; }
            set { SetProperty(ref data, value); }
        }

        private string fill;
        public string Fill
        {
            get { return fill; }
            set { SetProperty(ref fill, value); }
        }

    }
}
