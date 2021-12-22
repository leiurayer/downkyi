using Prism.Mvvm;
using System;
using System.Windows.Media.Imaging;

namespace DownKyi.Models
{
    [Serializable]
    public class VideoInfoView : BindableBase
    {
        public string CoverUrl { get; set; }
        public long UpperMid { get; set; }
        public int TypeId { get; set; }

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

        private string videoZone;
        public string VideoZone
        {
            get => videoZone;
            set => SetProperty(ref videoZone, value);
        }

        private string createTime;
        public string CreateTime
        {
            get => createTime;
            set => SetProperty(ref createTime, value);
        }

        private string playNumber;
        public string PlayNumber
        {
            get => playNumber;
            set => SetProperty(ref playNumber, value);
        }

        private string danmakuNumber;
        public string DanmakuNumber
        {
            get => danmakuNumber;
            set => SetProperty(ref danmakuNumber, value);
        }

        private string likeNumber;
        public string LikeNumber
        {
            get => likeNumber;
            set => SetProperty(ref likeNumber, value);
        }

        private string coinNumber;
        public string CoinNumber
        {
            get => coinNumber;
            set => SetProperty(ref coinNumber, value);
        }

        private string favoriteNumber;
        public string FavoriteNumber
        {
            get => favoriteNumber;
            set => SetProperty(ref favoriteNumber, value);
        }

        private string shareNumber;
        public string ShareNumber
        {
            get => shareNumber;
            set => SetProperty(ref shareNumber, value);
        }

        private string replyNumber;
        public string ReplyNumber
        {
            get => replyNumber;
            set => SetProperty(ref replyNumber, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private string upName;
        public string UpName
        {
            get => upName;
            set => SetProperty(ref upName, value);
        }

        private BitmapImage upHeader;
        public BitmapImage UpHeader
        {
            get => upHeader;
            set => SetProperty(ref upHeader, value);
        }

    }
}
