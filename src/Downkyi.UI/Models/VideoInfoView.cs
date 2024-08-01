using CommunityToolkit.Mvvm.ComponentModel;

namespace Downkyi.UI.Models
{
    public partial class VideoInfoView : ObservableObject
    {
        public string CoverUrl { get; set; } = string.Empty;
        public long UpperMid { get; set; }
        public int TypeId { get; set; }

        [ObservableProperty]
        private string _cover = string.Empty;

        [ObservableProperty] 
        private string _title  = string.Empty;

        [ObservableProperty]
        private string _videoZone = string.Empty;

        [ObservableProperty]
        private string _createTime = string.Empty;

        [ObservableProperty]
        private string _playNumber = string.Empty;

        [ObservableProperty]
        private string _danmakuNumber = string.Empty;

        [ObservableProperty]
        private string _likeNumber = string.Empty;

        [ObservableProperty]
        private string _coinNumber = string.Empty;

        [ObservableProperty]
        private string _favoriteNumber = string.Empty;

        [ObservableProperty]
        private string _shareNumber = string.Empty;

        [ObservableProperty]
        private string _replyNumber = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;

        [ObservableProperty]
        private string _upName = string.Empty;

        [ObservableProperty]
        private string _upHeader = string.Empty;

    }
}
