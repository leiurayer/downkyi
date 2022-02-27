using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels.PageViewModels
{
    public class ChannelMedia : BindableBase
    {
        public long Avid { get; set; }
        public string Bvid { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => SetProperty(ref isSelected, value);
        }

        private BitmapImage cover;
        public BitmapImage Cover
        {
            get => cover;
            set => SetProperty(ref cover, value);
        }

        private string title;
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string duration;
        public string Duration
        {
            get => duration;
            set => SetProperty(ref duration, value);
        }

        private string playNumber;
        public string PlayNumber
        {
            get => playNumber;
            set => SetProperty(ref playNumber, value);
        }

        private string createTime;
        public string CreateTime
        {
            get => createTime;
            set => SetProperty(ref createTime, value);
        }

        // 视频标题点击事件
        private DelegateCommand<object> titleCommand;
        public DelegateCommand<object> TitleCommand => titleCommand ?? (titleCommand = new DelegateCommand<object>(ExecuteTitleCommand));

        /// <summary>
        /// 视频标题点击事件
        /// </summary>
        /// <param name="parameter"></param>
        private void ExecuteTitleCommand(object parameter)
        {
            if (!(parameter is string tag)) { return; }

            string url = "https://www.bilibili.com/video/" + tag;
            System.Diagnostics.Process.Start(url);
        }

    }
}
