namespace DownKyi.Images
{
    public class NavigationIcon
    {
        private static NavigationIcon instance;
        public static NavigationIcon Instance()
        {
            if (instance == null)
            {
                instance = new NavigationIcon();
            }
            return instance;
        }

        public NavigationIcon()
        {
            ArrowBack = new VectorImage
            {
                Height = 24,
                Width = 24,
                Data = @"M16.62 2.99c-.49-.49-1.28-.49-1.77 0L6.54 11.3c-.39.39-.39 1.02 0 1.41l8.31 8.31c.49.49 1.28.49 1.77
                         0s.49-1.28 0-1.77L9.38 12l7.25-7.25c.48-.48.48-1.28-.01-1.76z",
                Fill = "#FF000000"
            };
        }

        public VectorImage ArrowBack { get; private set; }
    }
}
