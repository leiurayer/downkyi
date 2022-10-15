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
    public class ViewExtractMediaViewModel : BaseViewModel
    {
        public const string Tag = "PageToolboxExtractMedia";

        // 是否正在执行任务
        private bool isExtracting = false;

        #region 页面属性申明

        private string videoPathsStr;
        public string VideoPathsStr
        {
            get => videoPathsStr;
            set
            {
                SetProperty(ref videoPathsStr, value);
            }
        }

        private string[] videoPaths;
        public string[] VideoPaths
        {
            get => videoPaths;
            set
            {
                videoPaths = value;
                VideoPathsStr = string.Join(Environment.NewLine, value);
            }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { SetProperty(ref status, value); }
        }

        #endregion

        public ViewExtractMediaViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            #region 属性初始化

            VideoPaths = new string[0];

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
            if (isExtracting)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipWaitTaskFinished"));
                return;
            }

            VideoPaths = DialogUtils.SelectMultiVideoFile();
        }

        // 提取音频事件
        private DelegateCommand extractAudioCommand;
        public DelegateCommand ExtractAudioCommand => extractAudioCommand ?? (extractAudioCommand = new DelegateCommand(ExecuteExtractAudioCommand));

        /// <summary>
        /// 提取音频事件
        /// </summary>
        private async void ExecuteExtractAudioCommand()
        {
            if (isExtracting)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipWaitTaskFinished"));
                return;
            }

            if (VideoPaths.Length <= 0)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipNoSeletedVideo"));
                return;
            }

            Status = string.Empty;

            await Task.Run(() =>
            {
                isExtracting = true;
                foreach (var item in VideoPaths)
                {
                    // 音频文件名
                    string audioFileName = item.Remove(item.Length - 4, 4) + ".aac";
                    // 执行提取音频程序
                    FFmpegHelper.ExtractAudio(item, audioFileName, new Action<string>((output) =>
                    {
                        Status += output + "\n";
                    }));
                }
                isExtracting = false;
            });
        }

        // 提取视频事件
        private DelegateCommand extractVideoCommand;
        public DelegateCommand ExtractVideoCommand => extractVideoCommand ?? (extractVideoCommand = new DelegateCommand(ExecuteExtractVideoCommand));

        /// <summary>
        /// 提取视频事件
        /// </summary>
        private async void ExecuteExtractVideoCommand()
        {
            if (isExtracting)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipWaitTaskFinished"));
                return;
            }

            if (VideoPaths.Length <= 0)
            {
                eventAggregator.GetEvent<MessageEvent>().Publish(DictionaryResource.GetString("TipNoSeletedVideo"));
                return;
            }

            Status = string.Empty;

            await Task.Run(() =>
            {
                isExtracting = true;
                foreach (var item in VideoPaths)
                {
                    // 视频文件名
                    string videoFileName = item.Remove(item.Length - 4, 4) + "_onlyVideo.mp4";
                    // 执行提取视频程序
                    FFmpegHelper.ExtractVideo(item, videoFileName, new Action<string>((output) =>
                    {
                        Status += output + "\n";
                    }));
                }
                isExtracting = false;
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

    }
}
