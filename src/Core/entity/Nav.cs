// 注释掉未使用的属性
namespace Core.entity
{
    public class Nav
    {
        //public int code { get; set; }
        public NavData data { get; set; }
        public string message { get; set; }
        //public int ttl { get; set; }
    }

    public class NavData
    {
        //public int allowance_count { get; set; }
        //public int answer_status { get; set; }
        //public int email_verified { get; set; }
        public string face { get; set; }
        //public bool has_shop { get; set; }
        public bool isLogin { get; set; }
        //public NavDataLevelInfo level_info { get; set; }
        public long mid { get; set; }
        //public int mobile_verified { get; set; }
        public float money { get; set; }
        //public int moral { get; set; }
        //public NavDataOfficial official { get; set; }
        //public NavDataOfficialVerify officialVerify { get; set; }
        //public NavDataPendant pendant { get; set; }
        //public int scores { get; set; }
        //public string shop_url { get; set; }
        public string uname { get; set; }
        //public long vipDueDate { get; set; }
        public int vipStatus { get; set; }
        //public int vipType { get; set; }
        //public int vip_avatar_subscript { get; set; }
        //public NavDataVipLabel vip_label { get; set; }
        //public string vip_nickname_color { get; set; }
        //public int vip_pay_type { get; set; }
        //public int vip_theme_type { get; set; }
        public NavDataWallet wallet { get; set; }
    }

    public class NavDataLevelInfo
    {
        public int current_exp { get; set; }
        public int current_level { get; set; }
        public int current_min { get; set; }
        //public int next_exp { get; set; } // 当等级为6时，next_exp为string类型，值为"--"
    }

    //public class NavDataOfficial
    //{
    //    public string desc { get; set; }
    //    public int role { get; set; }
    //    public string title { get; set; }
    //    public int type { get; set; }
    //}

    //public class NavDataOfficialVerify
    //{
    //    public string desc { get; set; }
    //    public int type { get; set; }
    //}

    //public class NavDataPendant
    //{
    //    public int expire { get; set; }
    //    public string image { get; set; }
    //    public string image_enhance { get; set; }
    //    public string name { get; set; }
    //    public int pid { get; set; }
    //}

    //public class NavDataVipLabel
    //{
    //    public string label_theme { get; set; }
    //    public string path { get; set; }
    //    public string text { get; set; }
    //}

    public class NavDataWallet
    {
        public float bcoin_balance { get; set; }
        public float coupon_balance { get; set; }
        public long coupon_due_time { get; set; }
        public long mid { get; set; }
    }

}
