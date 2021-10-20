using Prism.Mvvm;
using System;
using System.Windows.Media.Imaging;

namespace DownKyi.Models
{
    [Serializable]
    public class VideoInfoView : BindableBase
    {
        public string CoverUrl { get; set; }

        private BitmapImage cover;
        public BitmapImage Cover
        {
            get { return cover; }
            set { SetProperty(ref cover, value); }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        private string videoZone;
        public string VideoZone
        {
            get { return videoZone; }
            set { SetProperty(ref videoZone, value); }
        }

        private string createTime;
        public string CreateTime
        {
            get { return createTime; }
            set { SetProperty(ref createTime, value); }
        }

        private string playNumber;
        public string PlayNumber
        {
            get { return playNumber; }
            set { SetProperty(ref playNumber, value); }
        }

        private string danmakuNumber;
        public string DanmakuNumber
        {
            get { return danmakuNumber; }
            set { SetProperty(ref danmakuNumber, value); }
        }

        private string likeNumber;
        public string LikeNumber
        {
            get { return likeNumber; }
            set { SetProperty(ref likeNumber, value); }
        }

        private string coinNumber;
        public string CoinNumber
        {
            get { return coinNumber; }
            set { SetProperty(ref coinNumber, value); }
        }

        private string favoriteNumber;
        public string FavoriteNumber
        {
            get { return favoriteNumber; }
            set { SetProperty(ref favoriteNumber, value); }
        }

        private string shareNumber;
        public string ShareNumber
        {
            get { return shareNumber; }
            set { SetProperty(ref shareNumber, value); }
        }

        private string replyNumber;
        public string ReplyNumber
        {
            get { return replyNumber; }
            set { SetProperty(ref replyNumber, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private string upName;
        public string UpName
        {
            get { return upName; }
            set { SetProperty(ref upName, value); }
        }

        private BitmapImage upHeader;
        public BitmapImage UpHeader
        {
            get { return upHeader; }
            set { SetProperty(ref upHeader, value); }
        }

    }
}
