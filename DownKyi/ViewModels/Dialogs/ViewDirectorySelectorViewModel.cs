using DownKyi.Core.Settings;
using DownKyi.Core.Utils;
using DownKyi.Events;
using DownKyi.Images;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System.Collections.Generic;

namespace DownKyi.ViewModels.Dialogs
{
    public class ViewDirectorySelectorViewModel : BaseDialogViewModel
    {
        public const string Tag = "DialogDirectorySelector";
        private readonly IEventAggregator eventAggregator;

        // 历史文件夹的数量
        private readonly int maxDirectoryListCount = 20;

        #region 页面属性申明

        private VectorImage cloudDownloadIcon;
        public VectorImage CloudDownloadIcon
        {
            get { return cloudDownloadIcon; }
            set { SetProperty(ref cloudDownloadIcon, value); }
        }

        private VectorImage folderIcon;
        public VectorImage FolderIcon
        {
            get { return folderIcon; }
            set { SetProperty(ref folderIcon, value); }
        }

        private bool isDefaultDownloadDirectory;
        public bool IsDefaultDownloadDirectory
        {
            get { return isDefaultDownloadDirectory; }
            set { SetProperty(ref isDefaultDownloadDirectory, value); }
        }

        private List<string> directoryList;
        public List<string> DirectoryList
        {
            get { return directoryList; }
            set { SetProperty(ref directoryList, value); }
        }

        private string directory;
        public string Directory
        {
            get { return directory; }
            set
            {
                SetProperty(ref directory, value);

                DriveName = directory.Substring(0, 1).ToUpper();
                DriveNameFreeSpace = Format.FormatFileSize(HardDisk.GetHardDiskFreeSpace(DriveName));
            }
        }

        private string driveName;
        public string DriveName
        {
            get { return driveName; }
            set { SetProperty(ref driveName, value); }
        }

        private string driveNameFreeSpace;
        public string DriveNameFreeSpace
        {
            get { return driveNameFreeSpace; }
            set { SetProperty(ref driveNameFreeSpace, value); }
        }

        #endregion

        public ViewDirectorySelectorViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            #region 属性初始化

            Title = DictionaryResource.GetString("DownloadDirectorySelector");

            CloudDownloadIcon = NormalIcon.Instance().CloudDownload;
            CloudDownloadIcon.Fill = DictionaryResource.GetColor("ColorPrimary");

            FolderIcon = NormalIcon.Instance().Folder;
            FolderIcon.Fill = DictionaryResource.GetColor("ColorPrimary");

            #endregion

            // 历史下载目录
            DirectoryList = SettingsManager.GetInstance().GetHistoryVideoRootPaths();
            string directory = SettingsManager.GetInstance().GetSaveVideoRootPath();
            if (!DirectoryList.Contains(directory))
            {
                ListHelper.InsertUnique(DirectoryList, directory, 0);
            }
            Directory = directory;

            // 是否使用默认下载目录
            IsDefaultDownloadDirectory = SettingsManager.GetInstance().IsUseSaveVideoRootPath() == AllowStatus.YES;
        }

        #region 命令申明

        // 浏览文件夹事件
        private DelegateCommand browseCommand;
        public DelegateCommand BrowseCommand => browseCommand ?? (browseCommand = new DelegateCommand(ExecuteBrowseCommand));

        /// <summary>
        /// 浏览文件夹事件
        /// </summary>
        private void ExecuteBrowseCommand()
        {
            string directory = SetDirectory();

            if (directory == null)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("WarningNullDirectory"));
                Directory = string.Empty;
            }
            else
            {
                ListHelper.InsertUnique(DirectoryList, directory, 0);
                Directory = directory;

                if (DirectoryList.Count > maxDirectoryListCount)
                {
                    DirectoryList.RemoveAt(maxDirectoryListCount);
                }
            }
        }

        // 确认下载事件
        private DelegateCommand downloadCommand;
        public DelegateCommand DownloadCommand => downloadCommand ?? (downloadCommand = new DelegateCommand(ExecuteDownloadCommand));

        /// <summary>
        /// 确认下载事件
        /// </summary>
        private void ExecuteDownloadCommand()
        {
            if (Directory == null || Directory == string.Empty)
            {
                return;
            }

            // 设此文件夹为默认下载文件夹
            if (IsDefaultDownloadDirectory)
            {
                SettingsManager.GetInstance().IsUseSaveVideoRootPath(AllowStatus.YES);
            }
            else
            {
                SettingsManager.GetInstance().IsUseSaveVideoRootPath(AllowStatus.NO);
            }

            // 将Directory移动到第一项
            // 如果直接在ComboBox中选择的就需要
            // 否则选中项不会在下次出现在第一项
            ListHelper.InsertUnique(DirectoryList, Directory, 0);

            // 将更新后的DirectoryList写入历史中
            SettingsManager.GetInstance().SetSaveVideoRootPath(Directory);
            SettingsManager.GetInstance().SetHistoryVideoRootPaths(DirectoryList);

            ButtonResult result = ButtonResult.OK;
            IDialogParameters parameters = new DialogParameters
            {
                { "directory", Directory }
            };

            RaiseRequestClose(new DialogResult(result, parameters));
        }

        #endregion

        /// <summary>
        /// 设置下载路径
        /// </summary>
        /// <returns></returns>
        private string SetDirectory()
        {
            // 下载目录
            string path;

            // 弹出选择下载目录的窗口
            path = DialogUtils.SetDownloadDirectory();
            if (path == null || path == "")
            {
                return null;
            }

            return path;
        }

    }
}
