using DownKyi.Images;
using DownKyi.Models;
using DownKyi.Utils;
using Prism.Commands;
using System.IO;

namespace DownKyi.ViewModels.DownloadManager
{
    public class DownloadedItem : DownloadBaseItem
    {
        public DownloadedItem() : base()
        {
            // 打开文件夹按钮
            OpenFolder = ButtonIcon.Instance().Folder;
            OpenFolder.Fill = DictionaryResource.GetColor("ColorPrimary");

            // 打开视频按钮
            OpenVideo = ButtonIcon.Instance().Start;
            OpenVideo.Fill = DictionaryResource.GetColor("ColorPrimary");

            // 删除按钮
            RemoveVideo = ButtonIcon.Instance().Trash;
            RemoveVideo.Fill = DictionaryResource.GetColor("ColorWarning");
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
            if (DownloadBase == null) { return; }

            string videoPath = $"{DownloadBase.FilePath}.mp4";
            FileInfo fileInfo = new FileInfo(videoPath);
            if (File.Exists(fileInfo.FullName))
            {
                System.Diagnostics.Process.Start("Explorer", "/select," + fileInfo.FullName);
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
            if (DownloadBase == null) { return; }

            string videoPath = $"{DownloadBase.FilePath}.mp4";
            var fileInfo = new FileInfo(videoPath);
            if (File.Exists(fileInfo.FullName))
            {
                System.Diagnostics.Process.Start(fileInfo.FullName);
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
