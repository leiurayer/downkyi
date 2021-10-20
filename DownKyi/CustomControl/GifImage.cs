using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace DownKyi.CustomControl
{
    public class GifImage : System.Windows.Controls.Image
    {
        /// <summary>
        /// gif动画的System.Drawing.Bitmap
        /// </summary>
        private Bitmap gifBitmap;

        /// <summary>
        /// 用于显示每一帧的BitmapSource
        /// </summary>
        private BitmapSource bitmapSource;

        public GifImage(string gifPath)
        {
            gifBitmap = new Bitmap(gifPath);
            bitmapSource = GetBitmapSource();
            Source = bitmapSource;
        }

        public GifImage(Bitmap gifBitmap)
        {
            this.gifBitmap = gifBitmap;
            bitmapSource = GetBitmapSource();
            Source = bitmapSource;
        }

        /// <summary>
        /// 从System.Drawing.Bitmap中获得用于显示的那一帧图像的BitmapSource
        /// </summary>
        /// <returns></returns>
        private BitmapSource GetBitmapSource()
        {
            IntPtr handle = IntPtr.Zero;

            try
            {
                handle = gifBitmap.GetHbitmap();
                bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            finally
            {
                if (handle != IntPtr.Zero)
                {
                    DeleteObject(handle);
                }
            }
            return bitmapSource;
        }

        /// <summary>
        /// Start animation
        /// </summary>
        public void StartAnimate()
        {
            ImageAnimator.Animate(gifBitmap, OnFrameChanged);
        }

        /// <summary>
        /// Stop animation
        /// </summary>
        public void StopAnimate()
        {
            ImageAnimator.StopAnimate(gifBitmap, OnFrameChanged);
        }

        /// <summary>
        /// Event handler for the frame changed
        /// </summary>
        private void OnFrameChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
            {
                ImageAnimator.UpdateFrames(); // 更新到下一帧
                if (bitmapSource != null)
                {
                    bitmapSource.Freeze();
                }

                //// Convert the bitmap to BitmapSource that can be display in WPF Visual Tree
                bitmapSource = GetBitmapSource();
                Source = bitmapSource;
                InvalidateVisual();
            }));
        }

        /// <summary>
        /// Delete local bitmap resource
        /// Reference: http://msdn.microsoft.com/en-us/library/dd183539(VS.85).aspx
        /// </summary>
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DeleteObject(IntPtr hObject);
    }
}
