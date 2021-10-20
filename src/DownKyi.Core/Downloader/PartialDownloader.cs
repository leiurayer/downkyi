using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;

namespace DownKyi.Core.Downloader
{
    /// <summary>
    /// 部分下载器
    /// </summary>
    public class PartialDownloader
    {
        /// <summary>
        /// 这部分完成事件
        /// </summary>
        public event EventHandler DownloadPartCompleted;

        /// <summary>
        /// 部分下载进度改变事件
        /// </summary>
        public event EventHandler DownloadPartProgressChanged;

        /// <summary>
        /// 部分下载停止事件
        /// </summary>
        public event EventHandler DownloadPartStopped;

        private readonly AsyncOperation _aop = AsyncOperationManager.CreateOperation(null);
        readonly int[] _lastSpeeds;
        int _counter;
        private bool _wait;
        private int _to;
        private int _totalBytesRead;

        /// <summary>
        /// 下载已停止
        /// </summary>
        public bool Stopped { get; private set; }

        /// <summary>
        /// 下载已完成
        /// </summary>
        public bool Completed { get; private set; }

        /// <summary>
        /// 下载进度
        /// </summary>
        public int Progress { get; private set; }

        /// <summary>
        /// 下载目录
        /// </summary>
        public string Directory { get; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// 已读字节数
        /// </summary>
        public long TotalBytesRead => _totalBytesRead;

        /// <summary>
        /// 内容长度
        /// </summary>
        public long ContentLength { get; private set; }

        /// <summary>
        /// RangeAllowed
        /// </summary>
        public bool RangeAllowed { get; }

        /// <summary>
        /// url
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// to
        /// </summary>
        public int To
        {
            get => _to;
            set
            {
                _to = value;
                ContentLength = _to - From + 1;
            }
        }

        /// <summary>
        /// from
        /// </summary>
        public int From { get; }

        /// <summary>
        /// 当前位置
        /// </summary>
        public int CurrentPosition => From + _totalBytesRead - 1;

        /// <summary>
        /// 剩余字节数
        /// </summary>
        public int RemainingBytes => (int)(ContentLength - _totalBytesRead);

        /// <summary>
        /// 完整路径
        /// </summary>
        public string FullPath => Path.Combine(Directory, FileName);

        /// <summary>
        /// 下载速度
        /// </summary>
        public int SpeedInBytes
        {
            get
            {
                if (Completed)
                {
                    return 0;
                }

                int totalSpeeds = _lastSpeeds.Sum();
                return totalSpeeds / 10;
            }
        }

        /// <summary>
        /// 部分块下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dir"></param>
        /// <param name="fileGuid"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="rangeAllowed"></param>
        public PartialDownloader(string url, string dir, string fileGuid, int from, int to, bool rangeAllowed)
        {
            From = from;
            _to = to;
            Url = url;
            RangeAllowed = rangeAllowed;
            FileName = fileGuid;
            Directory = dir;
            _lastSpeeds = new int[10];
        }

        void DownloadProcedure(Action<HttpWebRequest> config)
        {
            using (var file = new FileStream(FullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.Delete))
            {
                var sw = new Stopwatch();
                if (WebRequest.Create(Url) is HttpWebRequest req)
                {
                    req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.122 Safari/537.36";
                    req.AllowAutoRedirect = true;
                    req.MaximumAutomaticRedirections = 5;
                    req.ServicePoint.ConnectionLimit += 1;
                    req.ServicePoint.Expect100Continue = true;
                    req.ProtocolVersion = HttpVersion.Version11;
                    config(req);
                    if (RangeAllowed)
                    {
                        req.AddRange(From, _to);
                    }

                    if (req.GetResponse() is HttpWebResponse resp)
                    {
                        ContentLength = resp.ContentLength;
                        if (ContentLength <= 0 || (RangeAllowed && ContentLength != _to - From + 1))
                        {
                            throw new Exception("Invalid response content");
                        }

                        using (var tempStream = resp.GetResponseStream())
                        {
                            int bytesRead;
                            byte[] buffer = new byte[4096];
                            sw.Start();
                            while ((bytesRead = tempStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                if (_totalBytesRead + bytesRead > ContentLength)
                                {
                                    bytesRead = (int)(ContentLength - _totalBytesRead);
                                }

                                file.Write(buffer, 0, bytesRead);
                                _totalBytesRead += bytesRead;
                                _lastSpeeds[_counter] = (int)(_totalBytesRead / Math.Ceiling(sw.Elapsed.TotalSeconds));
                                _counter = (_counter >= 9) ? 0 : _counter + 1;
                                int tempProgress = (int)(_totalBytesRead * 100 / ContentLength);
                                if (Progress != tempProgress)
                                {
                                    Progress = tempProgress;
                                    _aop.Post(state =>
                                    {
                                        DownloadPartProgressChanged?.Invoke(this, EventArgs.Empty);
                                    }, null);
                                }

                                if (Stopped || (RangeAllowed && _totalBytesRead == ContentLength))
                                {
                                    break;
                                }
                            }
                        }
                    }

                    req.Abort();
                }

                sw.Stop();
                if (!Stopped && DownloadPartCompleted != null)
                {
                    _aop.Post(state =>
                    {
                        Completed = true;
                        DownloadPartCompleted(this, EventArgs.Empty);
                    }, null);
                }

                if (Stopped && DownloadPartStopped != null)
                {
                    _aop.Post(state => DownloadPartStopped(this, EventArgs.Empty), null);
                }
            }
        }

        /// <summary>
        /// 启动下载
        /// </summary>
        public void Start(Action<HttpWebRequest> config)
        {
            Stopped = false;
            var procThread = new Thread(_ => DownloadProcedure(config));
            procThread.Start();
        }

        /// <summary>
        /// 下载停止
        /// </summary>
        public void Stop()
        {
            Stopped = true;
        }

        /// <summary>
        /// 暂停等待下载
        /// </summary>
        public void Wait()
        {
            _wait = true;
        }

        /// <summary>
        /// 稍后唤醒
        /// </summary>
        public void ResumeAfterWait()
        {
            _wait = false;
        }
    }
}
