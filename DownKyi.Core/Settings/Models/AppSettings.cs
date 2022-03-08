namespace DownKyi.Core.Settings.Models
{
    public class AppSettings
    {
        public BasicSettings Basic { get; set; } = new BasicSettings();
        public NetworkSettings Network { get; set; } = new NetworkSettings();
        public VideoSettings Video { get; set; } = new VideoSettings();
        public DanmakuSettings Danmaku { get; set; } = new DanmakuSettings();
        public AboutSettings About { get; set; } = new AboutSettings();
        public UserInfoSettings UserInfo { get; set; } = new UserInfoSettings();
    }
}
