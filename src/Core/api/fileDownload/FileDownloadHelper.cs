using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Core.api.fileDownload
{
    public class FileDownloadHelper
    {
        private const int DOWNLOAD_BUFFER_SIZE = 102400; //每次下载量 100KB
        private const int THREAD_BUFFER_SIZE = 10485760; //每个线程每次最大下载大小  设为10MB  不能太小 否则会创建太多的request对象
        public delegate void ErrorMakedEventHandler(string errorstring);
        public event ErrorMakedEventHandler ErrorMakedEvent;
        public delegate void DownloadEventHandler(FileDownloadEvent e);
        public event DownloadEventHandler DownloadEvent;
        public delegate void StopEventHandler();
        public event StopEventHandler StopEvent;
        private object locker = new object();
        private long downloadSize = 0; //已经下载的字节
        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
        private ManualResetEvent mre = new ManualResetEvent(true); //初始化不等待
        private AutoResetEvent eventFinished = new AutoResetEvent(false);

        private void ThreadWork(string url, FileStream fs, Queue<ThreadDownloadInfo> downQueue)
        {
            mre.WaitOne();
            if (cancelTokenSource.IsCancellationRequested)
            {
                return;
            }

            Monitor.Enter(downQueue);
            if (downQueue.Count == 0)
            {
                return;
            }
            ThreadDownloadInfo downInfo = downQueue.Dequeue();
            Monitor.Exit(downQueue);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AddRange(downInfo.StartLength); //设置Range值
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream ns = response.GetResponseStream();
            byte[] nbytes = new byte[DOWNLOAD_BUFFER_SIZE];
            int temp = 0;
            int nReadSize = 0;
            byte[] buffer = new byte[downInfo.Length]; //文件写入缓冲
            nReadSize = ns.Read(nbytes, 0, Math.Min(DOWNLOAD_BUFFER_SIZE, downInfo.Length));
            while (temp < downInfo.Length)
            {
                mre.WaitOne();
                Buffer.BlockCopy(nbytes, 0, buffer, temp, nReadSize);
                lock (locker)
                {
                    downloadSize += nReadSize;
                }
                temp += nReadSize;
                nReadSize = ns.Read(nbytes, 0, Math.Min(DOWNLOAD_BUFFER_SIZE, downInfo.Length - temp));
            }

            lock (locker)
            {
                fs.Seek(downInfo.StartLength, SeekOrigin.Begin);
                fs.Write(buffer, 0, buffer.Length);
            }

            ns.Close();
            ThreadWork(url, fs, downQueue);
        }

        public async void StartDownload(FileDownloadInfo info)
        {
            if (string.IsNullOrEmpty(info.DownLoadUrl))
            { throw new Exception("下载地址不能为空！"); }
            if (!info.DownLoadUrl.ToLower().StartsWith("http://") || !info.DownLoadUrl.ToLower().StartsWith("https://"))
            { throw new Exception("非法的下载地址！"); }

            downloadSize = 0;
            await Task.Run(() =>
            {
                try
                {
                    long totalSize = 0;
                    long threadInitedLength = 0; //分配线程任务的下载量

                    #region 获取文件信息
                    //打开网络连接
                    HttpWebRequest initRequest = (HttpWebRequest)WebRequest.Create(info.DownLoadUrl);
                    WebResponse initResponse = initRequest.GetResponse();
                    FileInfo fileMsg = FileInfo.GetFileMessage(initResponse);
                    totalSize = fileMsg.Length;
                    if ((!string.IsNullOrEmpty(fileMsg.FileName)) && info.LocalSaveFolder != null)
                    {
                        info.SavePath = Path.Combine(info.LocalSaveFolder, fileMsg.FileName);
                    }
                    //ReaderWriterLock readWriteLock = new ReaderWriterLock();
                    #endregion

                    #region 读取配置文件
                    string configPath = info.SavePath.Substring(0, info.SavePath.LastIndexOf(".")) + ".cfg";
                    FileDownloadConfig initInfo = null;
                    if (File.Exists(configPath) && (info.IsNew == false))
                    {
                        initInfo = FileDownloadConfig.ReadConfig(configPath);
                        downloadSize = (long)initInfo.DownloadSize;
                        totalSize = (long)initInfo.TotalSize;
                    }
                    #endregion

                    #region  计算速度
                    //Stopwatch MyStopWatch = new Stopwatch();
                    long lastDownloadSize = 0; //上次下载量
                    bool isSendCompleteEvent = false; //是否完成
                    Timer timer = new Timer(new TimerCallback((o) =>
                    {
                        if (!isSendCompleteEvent)
                        {
                            FileDownloadEvent e = new FileDownloadEvent();
                            e.DownloadSize = downloadSize;
                            e.TotalSize = totalSize;
                            if (totalSize > 0 && downloadSize == totalSize)
                            {
                                e.Speed = 0;
                                isSendCompleteEvent = true;
                                eventFinished.Set();
                            }
                            else
                            {
                                e.Speed = downloadSize - lastDownloadSize;
                                lastDownloadSize = downloadSize; //更新上次下载量
                            }

                            DownloadEvent(e);
                        }

                    }), null, 0, 1000);
                    #endregion

                    string tempPath = info.SavePath.Substring(0, info.SavePath.LastIndexOf(".")) + ".dat";

                    #region 多线程下载
                    //分配下载队列
                    Queue<ThreadDownloadInfo> downQueue = null;
                    if (initInfo == null || info.IsNew)
                    {
                        downQueue = new Queue<ThreadDownloadInfo>(); //下载信息队列
                        while (threadInitedLength < totalSize)
                        {
                            ThreadDownloadInfo downInfo = new ThreadDownloadInfo();
                            downInfo.StartLength = threadInitedLength;
                            downInfo.Length = (int)Math.Min(Math.Min(THREAD_BUFFER_SIZE, totalSize - threadInitedLength), totalSize / info.ThreadCount); //下载量
                            downQueue.Enqueue(downInfo);
                            threadInitedLength += downInfo.Length;
                        }
                    }
                    else
                    {
                        downQueue = initInfo.DownloadQueue;
                    }

                    FileStream fs = new FileStream(tempPath, FileMode.OpenOrCreate);
                    fs.SetLength(totalSize);
                    int threads = info.ThreadCount;

                    for (int i = 0; i < info.ThreadCount; i++)
                    {
                        ThreadPool.QueueUserWorkItem((state) =>
                        {
                            ThreadWork(info.DownLoadUrl, fs, downQueue);
                            if (Interlocked.Decrement(ref threads) == 0)
                            {
                                (state as AutoResetEvent).Set();
                            }
                        }, eventFinished);
                    }

                    //等待所有线程完成
                    eventFinished.WaitOne();
                    if (fs != null)
                    {
                        fs.Close();
                    }
                    fs = null;
                    if (File.Exists(info.SavePath))
                    {
                        File.Delete(info.SavePath);
                    }

                    if (downloadSize == totalSize)
                    {
                        File.Move(tempPath, info.SavePath);
                        File.Delete(configPath);
                    }

                    if (cancelTokenSource.IsCancellationRequested && StopEvent != null)
                    {
                        StopEvent();
                        //保存配置文件
                        FileDownloadConfig.SaveConfig(configPath, downloadSize, totalSize, downQueue);
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    ErrorMakedEvent?.Invoke(ex.Message);
                }
            });

        }

        public void Stop()
        {
            cancelTokenSource.Cancel();
        }

        public void Suspend()
        {
            mre.Reset();
        }

        public void Resume()
        {
            mre.Set();
        }

        //#region  获取文件信息
        //public class FileMessage
        //{
        //    public long Length { get; set; }
        //    public string FileName { get; set; }
        //}

        //public FileMessage GetFileMessage(WebResponse response)
        //{
        //    FileMessage info = new FileMessage();

        //    if (response.Headers["Content-Disposition"] != null)
        //    {
        //        Match match = Regex.Match(response.Headers["Content-Disposition"], "filename=(.*)");
        //        if (match.Success)
        //        {
        //            string fileName = match.Groups[1].Value;
        //            Encoding encoding = Encoding.UTF8;
        //            string str = (response as HttpWebResponse).CharacterSet;
        //            if (!string.IsNullOrEmpty(str))
        //            {
        //                encoding = Encoding.GetEncoding(str);
        //            }
        //            info.FileName = System.Web.HttpUtility.UrlDecode(fileName, encoding);
        //        }
        //    }

        //    if (response.Headers["Content-Length"] != null)
        //    {
        //        info.Length = long.Parse(response.Headers.Get("Content-Length"));
        //    }
        //    else
        //    {
        //        info.Length = response.ContentLength;
        //    }

        //    return info;
        //}


        //#endregion

        //private void SaveConfig(string configPath, long downloadSize, long totalSize, Queue downQueue)
        //{
        //    stringBuilder sb = new stringBuilder();
        //    sb.Append(downloadSize + ";" + totalSize + ";");
        //    foreach (ThreadDownloadInfo info in downQueue)
        //    {
        //        sb.Append("(" + info.startLength + ",");
        //        sb.Append(info.length + ");");
        //    }

        //    byte[] buffer = Encoding.UTF8.GetBytes(sb.Tostring());
        //    string str = Convert.ToBase64string(buffer);
        //    File.WriteAllText(configPath, str);
        //}

        //private List ReadConfig(string configPath)
        //{
        //    List list = new List();
        //    string str = File.ReadAllText(configPath);
        //    byte[] buffer = Convert.FromBase64string(str);
        //    str =Encoding.UTF8.Getstring(buffer);
        //    lock (locker)
        //    {
        //        string[] split = str.Split(';');
        //        long downloadSize = Convert.ToInt64(split[0]);
        //        long totalSize = Convert.ToInt64(split[1]);
        //        Queue downQueue = new Queue(); //下载信息队列
        //        foreach (Match match in Regex.Matches(str, "\\((\\d+),(\\d+)\\);"))
        //        {
        //            ThreadDownloadInfo downInfo = new ThreadDownloadInfo();
        //            downInfo.startLength = Convert.ToInt64(match.Groups[1].Value);
        //            downInfo.length = Convert.ToInt32(match.Groups[2].Value);
        //            downQueue.Enqueue(downInfo);
        //        }

        //        list.Add(downloadSize);
        //        list.Add(totalSize);
        //        list.Add(downQueue);
        //    }

        //    return list;
        //}


    }

}
