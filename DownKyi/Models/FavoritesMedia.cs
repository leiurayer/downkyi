using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace DownKyi.Models
{
    public class FavoritesMedia : BindableBase
    {
        public long Avid { get; set; }
        public string Bvid { get; set; }
        public long UpperMid { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set { SetProperty(ref isSelected, value); }
        }

        private int order;
        public int Order
        {
            get { return order; }
            set { SetProperty(ref order, value); }
        }

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

        private string favoriteNumber;
        public string FavoriteNumber
        {
            get { return favoriteNumber; }
            set { SetProperty(ref favoriteNumber, value); }
        }

        private string duration;
        public string Duration
        {
            get { return duration; }
            set { SetProperty(ref duration, value); }
        }

        private string upName;
        public string UpName
        {
            get { return upName; }
            set { SetProperty(ref upName, value); }
        }
    }
}
