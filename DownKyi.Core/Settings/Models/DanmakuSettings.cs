namespace DownKyi.Core.Settings.Models
{
    /// <summary>
    /// 弹幕
    /// </summary>
    public class DanmakuSettings
    {
        public AllowStatus DanmakuTopFilter { get; set; }
        public AllowStatus DanmakuBottomFilter { get; set; }
        public AllowStatus DanmakuScrollFilter { get; set; }
        public AllowStatus IsCustomDanmakuResolution { get; set; }
        public int DanmakuScreenWidth { get; set; }
        public int DanmakuScreenHeight { get; set; }
        public string DanmakuFontName { get; set; }
        public int DanmakuFontSize { get; set; }
        public int DanmakuLineCount { get; set; }
        public DanmakuLayoutAlgorithm DanmakuLayoutAlgorithm { get; set; }
    }
}
