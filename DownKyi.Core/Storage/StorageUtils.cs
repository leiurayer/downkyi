using DownKyi.Core.Logging;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;

namespace DownKyi.Core.Storage
{
    internal static class StorageUtils
    {
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="localFile"></param>
        /// <returns></returns>
        public static bool DownloadImage(string url, string localFile)
        {
            try
            {
                WebClient mywebclient = new WebClient();
                mywebclient.DownloadFile(url, localFile);
            }
            catch (Exception e)
            {
                Utils.Debug.Console.PrintLine("DownloadImage()发生异常: {0}", e);
                LogManager.Error("StorageUtils", e);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Bitmap转BitmapImage
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            BitmapImage result = new BitmapImage();
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);
                stream.Position = 0;
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
            }
            return result;
        }

    }

}
