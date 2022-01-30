using DownKyi.Core.Aria2cNet.Client;
using DownKyi.Core.Aria2cNet.Client.Entity;
using DownKyi.Core.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DownKyi.Core.Aria2cNet
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

        // 下载结果回调
        public delegate void DownloadFinishHandler(bool isSuccess, string downloadPath, string gid, string msg = null);
        public event DownloadFinishHandler DownloadFinish;
        protected virtual void OnDownloadFinish(bool isSuccess, string downloadPath, string gid, string msg = null)
        {
            DownloadFinish?.Invoke(isSuccess, downloadPath, gid, msg);
        }

        // 全局下载状态
        public delegate void GetGlobalStatusHandler(long speed);
        public event GetGlobalStatusHandler GlobalStatus;
        protected virtual void OnGlobalStatus(long speed)
        {
            GlobalStatus?.Invoke(speed);
        }

        /// <summary>
        /// 获取gid下载项的状态
        /// 
        /// TODO
        /// 对于下载的不同状态的返回值的测试
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public DownloadResult GetDownloadStatus(string gid, Action action = null)
        {
            string filePath = "";
            while (true)
            {
                Task<AriaTellStatus> status = AriaClient.TellStatus(gid);
                if (status == null || status.Result == null) { continue; }

                if (status.Result.Result == null && status.Result.Error != null)
                {
                    if (status.Result.Error.Message.Contains("is not found"))
                    {
                        OnDownloadFinish(false, null, gid, status.Result.Error.Message);
                        return DownloadResult.ABORT;
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

                // 在外部执行
                if (action != null)
                {
                    action.Invoke();
                }

                if (status.Result.Result.Status == "complete")
                {
                    break;
                }
                if (status.Result.Result.ErrorCode != null && status.Result.Result.ErrorCode != "0")
                {
                    if (status.Result != null)
                    {
                        Utils.Debugging.Console.PrintLine("ErrorMessage: " + status.Result.Result.ErrorMessage);
                        LogManager.Error("AriaManager", status.Result.Result.ErrorMessage);
                    }

                    //// 如果返回状态码不是200，则继续
                    //if (status.Result.Result.ErrorMessage.Contains("The response status is not successful"))
                    //{
                    //    Thread.Sleep(1000);
                    //    continue;
                    //}

                    // aira中删除记录
                    Task<AriaRemove> ariaRemove1 = AriaClient.RemoveDownloadResultAsync(gid);
                    Utils.Debugging.Console.PrintLine(ariaRemove1);
                    if (ariaRemove1.Result != null)
                    {
                        LogManager.Debug("AriaManager", ariaRemove1.Result.Result);
                    }

                    // 返回回调信息，退出函数
                    OnDownloadFinish(false, null, gid, status.Result.Result.ErrorMessage);
                    return DownloadResult.FAILED;
                }

                // 降低CPU占用
                Thread.Sleep(100);
            }
            OnDownloadFinish(true, filePath, gid, null);
            return DownloadResult.SUCCESS;
        }

        /// <summary>
        /// 获取全局下载速度
        /// </summary>
        public async void GetGlobalStatus()
        {
            while (true)
            {
                // 查询全局status
                AriaGetGlobalStat globalStatus = await AriaClient.GetGlobalStatAsync();
                if (globalStatus == null || globalStatus.Result == null) { continue; }

                long globalSpeed = long.Parse(globalStatus.Result.DownloadSpeed);
                // 回调
                OnGlobalStatus(globalSpeed);

                // 降低CPU占用
                Thread.Sleep(100);
            }
        }

    }
}
