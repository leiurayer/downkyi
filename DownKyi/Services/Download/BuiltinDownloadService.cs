using DownKyi.Core.BiliApi.VideoStream.Models;
using DownKyi.Models;
using DownKyi.Utils;
using DownKyi.ViewModels.DownloadManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace DownKyi.Services.Download
{
    public class BuiltinDownloadService : DownloadService, IDownloadService
    {
        public BuiltinDownloadService(ObservableCollection<DownloadingItem> downloadingList, ObservableCollection<DownloadedItem> downloadedList) : base(downloadingList, downloadedList)
        {
            Tag = "BuiltinDownloadService";
        }

        #region 音视频

        /// <summary>
        /// 下载音频，返回下载文件路径
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        public override string DownloadAudio(DownloadingItem downloading)
        {
            PlayUrlDashVideo downloadAudio = BaseDownloadAudio(downloading);

            return DownloadVideo(downloading, downloadAudio);
        }

        /// <summary>
        /// 下载视频，返回下载文件路径
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        public override string DownloadVideo(DownloadingItem downloading)
        {
            PlayUrlDashVideo downloadVideo = BaseDownloadVideo(downloading);

            return DownloadVideo(downloading, downloadVideo);
        }

        /// <summary>
        /// 将下载音频和视频的函数中相同代码抽象出来
        /// </summary>
        /// <param name="downloading"></param>
        /// <param name="downloadVideo"></param>
        /// <returns></returns>
        private string DownloadVideo(DownloadingItem downloading, PlayUrlDashVideo downloadVideo)
        {

            return null;
        }

        #endregion

        /// <summary>
        /// 下载封面
        /// </summary>
        /// <param name="downloading"></param>
        /// <param name="coverUrl"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public override string DownloadCover(DownloadingItem downloading, string coverUrl, string fileName)
        {
            return BaseDownloadCover(downloading, coverUrl, fileName);
        }

        /// <summary>
        /// 下载弹幕
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        public override string DownloadDanmaku(DownloadingItem downloading)
        {
            return BaseDownloadDanmaku(downloading);
        }

        /// <summary>
        /// 下载字幕
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        public override List<string> DownloadSubtitle(DownloadingItem downloading)
        {
            return BaseDownloadSubtitle(downloading);
        }

        /// <summary>
        /// 混流音频和视频
        /// </summary>
        /// <param name="downloading"></param>
        /// <param name="audioUid"></param>
        /// <param name="videoUid"></param>
        /// <returns></returns>
        public override string MixedFlow(DownloadingItem downloading, string audioUid, string videoUid)
        {
            return BaseMixedFlow(downloading, audioUid, videoUid);
        }

        /// <summary>
        /// 解析视频流的下载链接
        /// </summary>
        /// <param name="downloading"></param>
        public override void Parse(DownloadingItem downloading)
        {
            BaseParse(downloading);
        }

        /// <summary>
        /// 停止下载服务(转换await和Task.Wait两种调用形式)
        /// </summary>
        private async Task EndTask()
        {
            // 停止基本任务
            await BaseEndTask();
        }

        /// <summary>
        /// 停止下载服务
        /// </summary>
        public void End()
        {
            Task.Run(EndTask).Wait();
        }

        public void Start()
        {
            // 启动基本服务
            BaseStart();
        }

        /// <summary>
        /// 强制暂停
        /// </summary>
        /// <param name="downloading"></param>
        protected override void Pause(DownloadingItem downloading)
        {
            cancellationToken.ThrowIfCancellationRequested();

            downloading.DownloadStatusTitle = DictionaryResource.GetString("Pausing");
            if (downloading.Downloading.DownloadStatus == DownloadStatus.PAUSE)
            {
                throw new OperationCanceledException("Stop thread by pause");
            }
            // 是否存在
            var isExist = IsExist(downloading);
            if (!isExist)
            {
                throw new OperationCanceledException("Task is deleted");
            }
        }

        /// <summary>
        /// 是否存在于下载列表中
        /// </summary>
        /// <param name="downloading"></param>
        /// <returns></returns>
        private bool IsExist(DownloadingItem downloading)
        {
            bool isExist = downloadingList.Contains(downloading);
            if (isExist)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #region 内建下载器



        #endregion

    }
}
