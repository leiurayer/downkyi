using System;
using System.IO;
using System.Net;

namespace Core
{
    // from https://www.jianshu.com/p/f31910ed8435
    public class FileDownloadUtil
    {
        private string url; //文件下载网络地址
        private string referer; //访问url的referer
        private string path; //文件下载位置，如d:/download
        private string filename; //文件名，如test.jpg
        private string fileId; //文件ID，文件唯一标识，一般为UUID

        private System.Threading.Timer FileTimer; // 定时器
        private readonly int SPD_INTERVAL_SEC = 1; // 每隔多少秒计算一次速度
        private long FileTemp = 0; // 临时储存长度
        private float FileSpeed = 0; // //SPD_INTERVAL_SEC秒下载字节数

        // 下载进度回调
        public delegate void ProgressChangedHandler(int progress, string fileId);
        public event ProgressChangedHandler ProgressChanged;
        protected virtual void OnProgressChanged(int progress, string fileId)
        {
            ProgressChanged?.Invoke(progress, fileId);
        }

        public delegate void ProgressChanged2Handler(long totalBytes, long totalDownloadedByte, float speed, string fileId);
        public event ProgressChanged2Handler ProgressChanged2;
        protected virtual void OnProgressChanged2(long totalBytes, long totalDownloadedByte, float speed, string fileId)
        {
            ProgressChanged2?.Invoke(totalBytes, totalDownloadedByte, speed, fileId);
        }

        // 下载结果回调
        public delegate void DownloadFinishHandler(bool isSuccess, string downloadPath, string fileId, string msg = null);
        public event DownloadFinishHandler DownloadFinish;
        protected virtual void OnDownloadFinish(bool isSuccess, string downloadPath, string fileId, string msg = null)
        {
            DownloadFinish?.Invoke(isSuccess, downloadPath, fileId, msg);
        }

        //通过网络链接直接下载任意文件
        public FileDownloadUtil Init(string url, string referer, string path, string filename, string fileId)
        {
            this.url = url;
            this.referer = referer;
            this.path = path;
            this.filename = filename;
            this.fileId = fileId;

            return this;
        }

        public void Download()
        {
            Download(url, path, filename, fileId);
        }

        private void Download(string url, string path, string filename, string fileId)
        {

            if (!Directory.Exists(path))   //判断文件夹是否存在
                Directory.CreateDirectory(path);
            path = path + "\\" + filename;

            // 临时文件 "bilidownkyi\\"
            string tempFile = Path.GetTempPath() + "downkyi." + Guid.NewGuid().ToString("N");
            //string tempFile = path + ".temp";

            try
            {
                if (File.Exists(tempFile))
                {
                    File.Delete(tempFile);    //存在则删除
                }
                if (File.Exists(path))
                {
                    File.Delete(path);    //存在则删除
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Download()发生IO异常: {0}", e);
            }

            FileStream fs = null;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream responseStream = null;
            try
            {
                //创建临时文件
                fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.Timeout = 60 * 1000;
                request.UserAgent = Utils.GetUserAgent();
                //request.ContentType = "text/html;charset=UTF-8";
                request.Headers["accept-language"] = "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7";
                //request.Headers["accept-encoding"] = "gzip, deflate, br";
                request.Headers["origin"] = "https://www.bilibili.com";
                request.Referer = referer;

                // 构造cookie
                // web端请求视频链接时没有加入cookie
                //if (!url.Contains("getLogin"))
                //{
                //    CookieContainer cookies = Login.GetLoginInfoCookies();
                //    if (cookies != null)
                //    {
                //        request.CookieContainer = cookies;
                //    }
                //}

                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                responseStream = response.GetResponseStream();
                byte[] bArr = new byte[1024];
                long totalBytes = response.ContentLength; //通过响应头获取文件大小，前提是响应头有文件大小
                int size = responseStream.Read(bArr, 0, (int)bArr.Length); //读取响应流到bArr，读取大小
                float percent = 0;  //用来保存计算好的百分比
                long totalDownloadedByte = 0;  //总共下载字节数

                FileTimer = new System.Threading.Timer(SpeedTimer, null, 0, SPD_INTERVAL_SEC * 1000);
                while (size > 0) //while (totalBytes > totalDownloadedByte) //while循环读取响应流
                {
                    fs.Write(bArr, 0, size); //写到临时文件
                    // 定期刷新数据到磁盘，影响下载速度
                    //fs.Flush(true);

                    totalDownloadedByte += size;
                    FileTemp += size;
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;

                    // 下载进度回调
                    OnProgressChanged((int)percent, fileId);
                    OnProgressChanged2(totalBytes, totalDownloadedByte, FileSpeed, fileId);
                }

                try
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);    //存在则删除
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Download()发生IO异常: {0}", e);
                }

                if (fs != null)
                    fs.Close();
                File.Move(tempFile, path); //重命名为正式文件
                OnDownloadFinish(true, path, fileId, null);  //下载完成，成功回调
            }
            catch (Exception ex)
            {
                Console.WriteLine("Download()发生异常: {0}", ex);
                OnDownloadFinish(false, null, fileId, ex.Message);  //下载完成，失败回调
            }
            finally
            {
                if (fs != null)
                    fs.Close();
                if (request != null)
                    request.Abort();
                if (response != null)
                    response.Close();
                if (responseStream != null)
                    responseStream.Close();

                try
                {
                    if (File.Exists(tempFile))
                    {
                        File.Delete(tempFile);    //存在则删除
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("Download()发生IO异常: {0}", e);
                }
            }
        }

        private void SpeedTimer(object state)
        {
            FileSpeed = FileTemp / SPD_INTERVAL_SEC; //SPD_INTERVAL_SEC秒下载字节数,
            FileTemp = 0; //清空临时储存
        }

    }
}
