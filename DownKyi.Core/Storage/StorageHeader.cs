using DownKyi.Core.Logging;
using DownKyi.Core.Storage.Database;
using DownKyi.Core.Utils.Encryptor;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace DownKyi.Core.Storage
{
    public class StorageHeader
    {
        // 先判断本地有没有
        // 如果有
        // 则返回本地的图片路径
        // 如果没有
        // 则下载图片并返回本地的图片路径

        /// <summary>
        /// 获取用户头像缩略图
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public BitmapImage GetHeaderThumbnail(long mid, string name, string url, int width, int height)
        {
            string header = GetHeader(mid, name, url);
            if (header == null) { return null; }

            return GetHeaderThumbnail(header, width, height);
        }

        /// <summary>
        /// 获取用户头像缩略图
        /// </summary>
        /// <param name="header"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public BitmapImage GetHeaderThumbnail(string header, int width, int height)
        {
            if (header == null) { return null; }

            var bitmap = new Bitmap(header);
            var thumbnail = bitmap.GetThumbnailImage(width, height, null, IntPtr.Zero);

            return StorageUtils.BitmapToBitmapImage(new Bitmap(thumbnail));
        }

        /// <summary>
        /// 获取用户头像
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public string GetHeader(long mid, string name, string url)
        {
            HeaderDb headerDb = new HeaderDb();
            var header = headerDb.QueryByMid(mid);

            if (header != null)
            {
                string headerPath = $"{StorageManager.GetHeader()}/{header.Md5}";
                if (File.Exists(headerPath))
                {
                    Header newHeader = new Header
                    {
                        Mid = mid,
                        Name = name,
                        Url = url,
                        Md5 = header.Md5
                    };
                    headerDb.Update(newHeader);
                    headerDb.Close();
                    return $"{StorageManager.GetHeader()}/{header.Md5}";
                }
                else
                {
                    string md5 = DownloadImage(url);
                    if (md5 != null)
                    {
                        Header newHeader = new Header
                        {
                            Mid = mid,
                            Name = name,
                            Url = url,
                            Md5 = md5
                        };
                        headerDb.Insert(newHeader);
                        headerDb.Close();
                        return $"{StorageManager.GetHeader()}/{md5}";
                    }
                    else
                    {
                        headerDb.Close();
                        return null;
                    }
                }
            }
            else
            {
                string md5 = DownloadImage(url);
                if (md5 != null)
                {
                    Header newHeader = new Header
                    {
                        Mid = mid,
                        Name = name,
                        Url = url,
                        Md5 = md5
                    };
                    headerDb.Insert(newHeader);
                    headerDb.Close();
                    return $"{StorageManager.GetHeader()}/{md5}";
                }
                else
                {
                    headerDb.Close();
                    return null;
                }
            }
        }


        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string DownloadImage(string url)
        {
            string localFile = Path.GetTempPath() + Guid.NewGuid().ToString("N");

            // 下载
            bool isSuccessed = StorageUtils.DownloadImage(url, localFile);
            if (isSuccessed)
            {
                try
                {
                    string md5 = Hash.GetMD5HashFromFile(localFile);

                    if (File.Exists(localFile))
                    {
                        string destFile = $"{StorageManager.GetHeader()}/{md5}";

                        try
                        {
                            File.Delete(destFile);
                        }
                        catch { }

                        // 移动到指定位置
                        File.Move(localFile, destFile);

                        return md5;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Utils.Debugging.Console.PrintLine("DownloadImage()发生异常: {0}", e);
                    LogManager.Error("StorageHeader", e);
                    return null;
                }
            }

            return null;
        }

    }
}
