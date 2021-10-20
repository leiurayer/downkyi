using DownKyi.Core.FFmpeg;
using DownKyi.Events;
using DownKyi.Utils;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DownKyi.ViewModels.Toolbox
{
    public class ViewDelogoViewModel : BaseViewModel
    {
        public const string Tag = "PageToolboxDelogo";

        // 是否正在执行去水印任务
        private bool isDelogo = false;

        #region 页面属性申明

        private string videoPath;
        public string VideoPath
        {
            get { return videoPath; }
            set { SetProperty(ref videoPath, value); }
        }

        private int logoWidth;
        public int LogoWidth
        {
            get { return logoWidth; }
            set { SetProperty(ref logoWidth, value); }
        }

        private int logoHeight;
        public int LogoHeight
        {
            get { return logoHeight; }
            set { SetProperty(ref logoHeight, value); }
        }

        private int logoX;
        public int LogoX
        {
            get { return logoX; }
            set { SetProperty(ref logoX, value); }
        }

        private int logoY;
        public int LogoY
        {
            get { return logoY; }
            set { SetProperty(ref logoY, value); }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        #endregion

        public ViewDelogoViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化

            VideoPath = string.Empty;

            LogoWidth = -1;
            LogoHeight = -1;
            LogoX = -1;
            LogoY = -1;

            #endregion
        }

        #region 命令申明

        // 选择视频事件
        private DelegateCommand selectVideoCommand;
        public DelegateCommand SelectVideoCommand => selectVideoCommand ?? (selectVideoCommand = new DelegateCommand(ExecuteSelectVideoCommand));

        /// <summary>
        /// 选择视频事件
        /// </summary>
        private void ExecuteSelectVideoCommand()
        {
            if (isDelogo)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipWaitTaskFinished"));
                return;
            }

            VideoPath = SelectVideoFile();
        }

        // 去水印事件
        private DelegateCommand delogoCommand;
        public DelegateCommand DelogoCommand => delogoCommand ?? (delogoCommand = new DelegateCommand(ExecuteDelogoCommand));

        /// <summary>
        /// 去水印事件
        /// </summary>
        private async void ExecuteDelogoCommand()
        {
            if (isDelogo)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipWaitTaskFinished"));
                return;
            }

            if (VideoPath == "")
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipNoSeletedVideo"));
                return;
            }

            if (LogoWidth == -1)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipInputRightLogoWidth"));
                return;
            }
            if (LogoHeight == -1)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipInputRightLogoHeight"));
                return;
            }
            if (LogoX == -1)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipInputRightLogoX"));
                return;
            }
            if (LogoY == -1)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipInputRightLogoY"));
                return;
            }

            // 新文件名
            string newFileName = VideoPath.Insert(VideoPath.Length - 4, "_delogo");
            Status = string.Empty;

            await Task.Run(() =>
            {
                // 执行去水印程序
                isDelogo = true;
                FFmpegHelper.Delogo(VideoPath, newFileName, LogoX, LogoY, LogoWidth, LogoHeight, new Action<string>((output) =>
                {
                    Status += output + "\n";
                }));
                isDelogo = false;
            });
        }

        // Status改变事件
        private DelegateCommand<object> statusCommand;
        public DelegateCommand<object> StatusCommand => statusCommand ?? (statusCommand = new DelegateCommand<object>(ExecuteStatusCommand));

        /// <summary>
        /// Status改变事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteStatusCommand(object parameter)
        {
            if (!(parameter is TextBox output)) { return; }

            // TextBox滚动到底部
            output.ScrollToEnd();
        }

        #endregion

        /// <summary>
        /// 选择视频dialog
        /// </summary>
        /// <returns></returns>
        private string SelectVideoFile()
        {
            // 选择文件
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "mp4 (*.mp4)|*.mp4"
            };
            var showDialog = dialog.ShowDialog();
            if (showDialog == true)
            {
                return dialog.FileName;
            }
            else { return ""; }
        }

    }
}
