using Core.aria2cNet.client;
using System;
using System.Threading;

namespace Core.aria2cNet
{
    public class AriaManager
    {
        // gid对应项目的状态
        public delegate void TellStatusHandler(long totalLength, long completedLength, long speed, string gid);
        public event TellStatusHandler TellStatus;
        protected virtual void OnTellStatus(long totalLength, long completedLength, long speed, string gid)
        {
            TellStatus?.Invoke(totalLength, completedLength, speed, gid);
        }

        // 全局下载状态
        public delegate void GetGlobalStatusHandler(long speed);
        public event GetGlobalStatusHandler GetGlobalStatus;
        protected virtual void OnGetGlobalStatus(long speed)
        {
            GetGlobalStatus?.Invoke(speed);
        }

        // 下载结果回调
        public delegate void DownloadFinishHandler(bool isSuccess, string downloadPath, string gid, string msg = null);
        public event DownloadFinishHandler DownloadFinish;
        protected virtual void OnDownloadFinish(bool isSuccess, string downloadPath, string gid, string msg = null)
        {
            DownloadFinish?.Invoke(isSuccess, downloadPath, gid, msg);
        }

        public DownloadStatus GetDownloadStatus(string gid)
        {
            string filePath = "";
            while (true)
            {
                var status = AriaClient.TellStatus(gid);
                if (status == null || status.Result == null) { continue; }

                if (status.Result.Result == null && status.Result.Error != null)
                {
                    if (status.Result.Error.Message.Contains("is not found"))
                    {
                        OnDownloadFinish(false, null, gid, status.Result.Error.Message);
                        return DownloadStatus.ABORT;
                    }
                }

                if (status.Result.Result.Files != null && status.Result.Result.Files.Count >= 1)
                {
                    filePath = status.Result.Result.Files[0].Path;
                }

                long totalLength = long.Parse(status.Result.Result.TotalLength);
                long completedLength = long.Parse(status.Result.Result.CompletedLength);
                long speed = long.Parse(status.Result.Result.DownloadSpeed);
                // 回调
                OnTellStatus(totalLength, completedLength, speed, gid);

                if (status.Result.Result.Status == "complete")
                {
                    break;
                }
                if (status.Result.Result.ErrorCode != null && status.Result.Result.ErrorCode != "0")
                {
                    Console.WriteLine(status.Result.Result.ErrorMessage);
                    OnDownloadFinish(false, null, gid, status.Result.Result.ErrorMessage);
                    return DownloadStatus.FAILED;
                }

                // 降低CPU占用
                Thread.Sleep(500);
            }
            OnDownloadFinish(true, filePath, gid, null);
            return DownloadStatus.SUCCESS;
        }

    }


    /// <summary>
    /// 下载状态
    /// </summary>
    public enum DownloadStatus
    {
        SUCCESS = 1,
        FAILED,
        ABORT
    }

}
