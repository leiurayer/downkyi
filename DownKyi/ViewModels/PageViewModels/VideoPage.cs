using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.Models;
using DownKyi.Core.BiliApi.VideoStream.Models;
using DownKyi.Core.Logging;
using DownKyi.Core.Utils;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DownKyi.ViewModels.PageViewModels
{
    public class VideoPage : BindableBase
    {
        public PlayUrl PlayUrl { get; set; }

        public long Avid { get; set; }
        public string Bvid { get; set; }
        public long Cid { get; set; }
        public long EpisodeId { get; set; }
        public VideoOwner Owner { get; set; }
        public string PublishTime { get; set; }

        public string FirstFrame { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        private int order;
        public int Order
        {
            get => order;
            set => SetProperty(ref order, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string duration;
        public string Duration
        {
            get => duration;
            set => SetProperty(ref duration, value);
        }

        private ObservableCollection<string> audioQualityFormatList;
        public ObservableCollection<string> AudioQualityFormatList
        {
            get => audioQualityFormatList;
            set => SetProperty(ref audioQualityFormatList, value);
        }

        private string audioQualityFormat;
        public string AudioQualityFormat
        {
            get => audioQualityFormat;
            set => SetProperty(ref audioQualityFormat, value);
        }

        private List<VideoQuality> videoQualityList;
        public List<VideoQuality> VideoQualityList
        {
            get => videoQualityList;
            set => SetProperty(ref videoQualityList, value);
        }

        private VideoQuality videoQuality;
        public VideoQuality VideoQuality
        {
            get => videoQuality;
            set => SetProperty(ref videoQuality, value);
        }

        #region

        // 视频画质选择事件
        private DelegateCommand videoQualitySelectedCommand;
        public DelegateCommand VideoQualitySelectedCommand => videoQualitySelectedCommand ?? (videoQualitySelectedCommand = new DelegateCommand(ExecuteVideoQualitySelectedCommand));

        /// <summary>
        /// 视频画质选择事件
        /// </summary>
        private void ExecuteVideoQualitySelectedCommand()
        {
            // 杜比视界
            string dolby = string.Empty;
            try
            {
                var qualities = Constant.GetAudioQualities();
                dolby = qualities[3].Name;
            }
            catch (Exception e)
            {
                Core.Utils.Debugging.Console.PrintLine("ExecuteVideoQualitySelectedCommand()发生异常: {0}", e);
                LogManager.Error("ExecuteVideoQualitySelectedCommand", e);
            }

            if (VideoQuality != null && VideoQuality.Quality == 126 && PlayUrl != null && PlayUrl.Dash != null && PlayUrl.Dash.Dolby != null)
            {
                ListHelper.AddUnique(AudioQualityFormatList, dolby);
                AudioQualityFormat = dolby;
            }
            else
            {
                if (AudioQualityFormatList.Contains(dolby))
                {
                    AudioQualityFormatList.Remove(dolby);
                    AudioQualityFormat = AudioQualityFormatList[0];
                }
            }
        }

        #endregion

    }
}
