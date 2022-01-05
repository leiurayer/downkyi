using DownKyi.Images;
using DownKyi.Models;
using Prism.Commands;
using System.IO;

namespace DownKyi.ViewModels.DownloadManager
{
    public class DownloadedItem : DownloadBaseItem
    {
        public DownloadedItem() : base()
        {
        }

        // model数据
        public Downloaded Downloaded { get; set; }

        //  下载速度
        public string MaxSpeedDisplay
        {
            get => Downloaded.MaxSpeedDisplay;
            set
            {
                Downloaded.MaxSpeedDisplay = value;
                RaisePropertyChanged("MaxSpeedDisplay");
            }
        }

        // 完成时间
        public string FinishedTime
        {
            get => Downloaded.FinishedTime;
            set
            {
                Downloaded.FinishedTime = value;
                RaisePropertyChanged("FinishedTime");
            }
        }

        #region 控制按钮 

        private VectorImage openFolder;
        public VectorImage OpenFolder
        {
            get => openFolder;
            set => SetProperty(ref openFolder, value);
        }

        private VectorImage openVideo;
        public VectorImage OpenVideo
        {
            get => openVideo;
            set => SetProperty(ref openVideo, value);
        }

        private VectorImage removeVideo;
        public VectorImage RemoveVideo
        {
            get => removeVideo;
            set => SetProperty(ref removeVideo, value);
        }

        #endregion

        #region 命令申明

        // 打开文件夹事件
        private DelegateCommand openFolderCommand;
        public DelegateCommand OpenFolderCommand => openFolderCommand ?? (openFolderCommand = new DelegateCommand(ExecuteOpenFolderCommand));

        /// <summary>
        /// 打开文件夹事件
        /// </summary>
        private void ExecuteOpenFolderCommand()
        {
            string videoPath = $"{DownloadBase.FilePath}.mp4";
            if (File.Exists(videoPath))
            {
                System.Diagnostics.Process.Start("Explorer", "/select," + videoPath);
            }
            else
            {
                //eventAggregator.GetEvent<MessageEvent>().Publish("没有找到视频文件，可能被删除或移动！");
            }
        }

        // 打开视频事件
        private DelegateCommand openVideoCommand;
        public DelegateCommand OpenVideoCommand => openVideoCommand ?? (openVideoCommand = new DelegateCommand(ExecuteOpenVideoCommand));

        /// <summary>
        /// 打开视频事件
        /// </summary>
        private void ExecuteOpenVideoCommand()
        {
            string videoPath = $"{DownloadBase.FilePath}.mp4";
            if (File.Exists(videoPath))
            {
                System.Diagnostics.Process.Start(videoPath);
            }
            else
            {
                //eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipAddDownloadingZero"));
                //eventAggregator.GetEvent<MessageEvent>().Publish("没有找到视频文件，可能被删除或移动！");
            }
        }

        // 删除事件
        private DelegateCommand removeVideoCommand;
        public DelegateCommand RemoveVideoCommand => removeVideoCommand ?? (removeVideoCommand = new DelegateCommand(ExecuteRemoveVideoCommand));

        /// <summary>
        /// 删除事件
        /// </summary>
        private void ExecuteRemoveVideoCommand()
        {
            App.DownloadedList.Remove(this);
        }

        #endregion

    }
}
