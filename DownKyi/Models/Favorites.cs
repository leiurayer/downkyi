using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace DownKyi.Models
{
    public class Favorites : BindableBase
    {
        public string CoverUrl { get; set; }
        public long UpperMid { get; set; }

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

        private string likeNumber;
        public string LikeNumber
        {
            get { return likeNumber; }
            set { SetProperty(ref likeNumber, value); }
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

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private int mediaCount;
        public int MediaCount
        {
            get { return mediaCount; }
            set { SetProperty(ref mediaCount, value); }
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
