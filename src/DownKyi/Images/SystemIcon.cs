namespace DownKyi.Images
{
    public class SystemIcon
    {
        private static SystemIcon instance;
        public static SystemIcon Instance()
        {
            if (instance == null)
            {
                instance = new SystemIcon();
            }
            return instance;
        }

        private SystemIcon()
        {
            Close = new VectorImage
            {
                Height = 12,
                Width = 12,
                Data = @"M25,23.9l-6.6-6.6c-0.3-0.3-0.9-0.3-1.1,0c-0.3,0.3-0.3,0.9,0,1.1l0,0l6.6,6.6l-6.6,6.6c-0.3,0.3-0.3,0.9,0,1.1
                         c0.3,0.3,0.9,0.3,1.1,0l0,0l6.6-6.6l6.6,6.6c0.3,0.3,0.9,0.3,1.1,0.1c0.3-0.3,0.3-0.9,0.1-1.1c0,0,0,0-0.1-0.1L26.1,25l6.6-6.6
                         c0.3-0.3,0.3-0.9,0-1.1c-0.3-0.3-0.9-0.3-1.1,0l0,0L25,23.9L25,23.9z",
                Fill = "#FF000000"
            };

            Maximize = new VectorImage
            {
                Height = 12,
                Width = 12,
                Data = @"M31.1,33H18.9c-1,0-1.9-0.9-1.9-1.9V18.9c0-1,0.9-1.9,1.9-1.9h12.3c1,0,1.9,0.9,1.9,1.9v12.3C33,32.1,32.1,33,31.1,33z
                         M18.9,18.3c-0.3,0-0.6,0.3-0.6,0.6v12.3c0,0.3,0.3,0.6,0.6,0.6h12.3c0.3,0,0.6-0.3,0.6-0.6V18.9c0-0.3-0.3-0.6-0.6-0.6
                         C31.1,18.3,18.9,18.3,18.9,18.3z",
                Fill = "#FF000000"
            };

            Minimize = new VectorImage
            {
                Height = 1.2,
                Width = 12,
                Data = @"M17.8,25.8c-0.5,0-0.8-0.3-0.8-0.8s0.3-0.8,0.8-0.8h14.4c0.5,0,0.8,0.3,0.8,0.8s-0.3,0.8-0.8,0.8H17.8z",
                Fill = "#FF000000"
            };

            Restore = new VectorImage
            {
                Height = 12,
                Width = 12,
                Data = @"M29,33H18.3c-0.7,0-1.3-0.6-1.3-1.3V21c0-0.7,0.6-1.3,1.3-1.3H29c0.7,0,1.3,0.6,1.3,1.3v10.7C30.4,32.4,29.8,33,29,33z
                         M18.3,20.9c-0.1,0-0.1,0.1-0.1,0.1v10.7c0,0.1,0.1,0.1,0.1,0.1H29c0.1,0,0.1-0.1,0.1-0.1V21c0-0.1-0.1-0.1-0.1-0.1H18.3z
                         M31.7,30.4h-1.9v-1.2h1.9c0.1,0,0.1-0.1,0.1-0.1V18.3c0-0.1-0.1-0.1-0.1-0.1H21c-0.1,0-0.1,0.1-0.1,0.1v1.9h-1.2v-1.9
                         c0-0.7,0.6-1.3,1.3-1.3h10.7c0.7,0,1.3,0.6,1.3,1.3v10.7C33,29.8,32.4,30.4,31.7,30.4z",
                Fill = "#FF000000"
            };

            Skin = new VectorImage
            {
                Height = 12,
                Width = 12,
                Data = @"M29.1,18.1l2.7,3.2l-1.3,1.6c-0.1-0.1-0.3-0.1-0.5-0.1c-0.6,0-1,0.5-1,1.2v8.1h-8.2v-8.1c0-0.7-0.5-1.2-1-1.2
                         c-0.1,0-0.3,0-0.5,0.1l-1.3-1.6l2.7-3.2h0.8c0.7,0.9,2.4,1.5,3.3,1.5c0.9,0,2.6-0.4,3.3-1.5H29.1 M29.2,17h-0.9
                         c-0.2,0-0.5,0.1-0.7,0.4c-0.5,0.5-1.8,1.1-2.6,1.1c-0.8,0-2.2-0.5-2.6-1.2c-0.2-0.1-0.5-0.3-0.7-0.3h-0.9c-0.2,0-0.5,0.1-0.6,0.3
                         l-3.1,3.6c-0.1,0.3-0.1,0.7,0,0.8l1.8,2.2C19,24,19.3,24,19.4,24s0.2,0,0.3-0.1v-0.1h0.1c0,0,0.1,0,0.1,0.1v8.2
                         c0,0.5,0.3,0.9,0.8,0.9h8.6c0.5,0,0.8-0.4,0.8-0.9v-8.2c0-0.1,0-0.1,0.1-0.1h0.1l0.1,0.1c0.1,0.1,0.2,0.1,0.3,0.1s0.2,0,0.3-0.1
                         l1.8-2.2c0-0.1,0-0.5-0.1-0.8l-3.1-3.6C29.7,17.1,29.5,17,29.2,17z",
                Fill = "#FF000000"
            };

            Info = new VectorImage
            {
                Height = 20,
                Width = 20,
                Data = @"M11 7h2v2h-2zm0 4h2v6h-2zm1-9C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zm0
                         18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z",
                Fill = "#FF00bcf2"
            };

            Warning = new VectorImage
            {
                Height = 20,
                Width = 20,
                Data = @"M12 5.99L19.53 19H4.47L12 5.99M2.74 18c-.77 1.33.19 3 1.73 3h15.06c1.54 0 2.5-1.67 1.73-3L13.73
                         4.99c-.77-1.33-2.69-1.33-3.46 0L2.74 18zM11 11v2c0 .55.45 1 1 1s1-.45 1-1v-2c0-.55-.45-1-1-1s-1 .45-1 1zm0 5h2v2h-2z",
                Fill = "#FFffb900"
            };

            Error = new VectorImage
            {
                Height = 20,
                Width = 20,
                Data = @"M12 7c.55 0 1 .45 1 1v4c0 .55-.45 1-1 1s-1-.45-1-1V8c0-.55.45-1 1-1zm-.01-5C6.47 2 2 6.48 2 12s4.47
                         10 9.99 10C17.52 22 22 17.52 22 12S17.52 2 11.99 2zM12 20c-4.42 0-8-3.58-8-8s3.58-8 8-8 8 3.58 8
                         8-3.58 8-8 8zm1-3h-2v-2h2v2z",
                Fill = "#FFd83b01"
            };
        }

        public VectorImage Close { get; private set; }
        public VectorImage Maximize { get; private set; }
        public VectorImage Minimize { get; private set; }
        public VectorImage Restore { get; private set; }
        public VectorImage Skin { get; private set; }

        public VectorImage Info { get; private set; }
        public VectorImage Warning { get; private set; }
        public VectorImage Error { get; private set; }
    }
}
