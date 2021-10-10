namespace DownKyi.Core.Settings.Models
{
    public class UserInfoSettings
    {
        public long Mid { get; set; }
        public string Name { get; set; }
        public bool IsLogin { get; set; } // 是否登录
        public bool IsVip { get; set; } // 是否为大会员，未登录时为false
    }
}
