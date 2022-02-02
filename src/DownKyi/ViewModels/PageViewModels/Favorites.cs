using DownKyi.Images;
using DownKyi.Utils;
using Prism.Mvvm;
using System.Windows.Media.Imaging;

namespace DownKyi.ViewModels.PageViewModels
{
    public class Favorites : BindableBase
    {
        public string CoverUrl { get; set; }
        public long UpperMid { get; set; }

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

        private string likeNumber;
        public string LikeNumber
        {
            get => likeNumber;
            set => SetProperty(ref likeNumber, value);
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

        private VectorImage play;
        public VectorImage Play
        {
            get => play;
            set => SetProperty(ref play, value);
        }

        private VectorImage like;
        public VectorImage Like
        {
            get => like;
            set => SetProperty(ref like, value);
        }

        private VectorImage favorite;
        public VectorImage Favorite
        {
            get => favorite;
            set => SetProperty(ref favorite, value);
        }

        private VectorImage share;
        public VectorImage Share
        {
            get => share;
            set => SetProperty(ref share, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private int mediaCount;
        public int MediaCount
        {
            get => mediaCount;
            set => SetProperty(ref mediaCount, value);
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

        public Favorites()
        {
            #region 属性初始化

            Play = NormalIcon.Instance().Play;
            Play.Fill = DictionaryResource.GetColor("ColorTextGrey2");

            Like = NormalIcon.Instance().Like;
            Like.Fill = DictionaryResource.GetColor("ColorTextGrey2");

            Favorite = NormalIcon.Instance().Favorite;
            Favorite.Fill = DictionaryResource.GetColor("ColorTextGrey2");

            Share = NormalIcon.Instance().Share;
            Share.Fill = DictionaryResource.GetColor("ColorTextGrey2");

            #endregion
        }

    }
}
