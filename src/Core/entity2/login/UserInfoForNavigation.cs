using Newtonsoft.Json;

namespace Core.entity2.login
{
    // https://api.bilibili.com/x/web-interface/nav
    [JsonObject]
    public class UserInfoForNavigationOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public UserInfoForNavigation Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class UserInfoForNavigation : BaseEntity
    {
        //public int allowance_count { get; set; }
        //public int answer_status { get; set; }
        //public int email_verified { get; set; }
        [JsonProperty("face")]
        public string Face { get; set; }
        //public bool has_shop { get; set; }
        [JsonProperty("isLogin")]
        public bool IsLogin { get; set; }
        //public NavDataLevelInfo level_info { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        //public int mobile_verified { get; set; }
        [JsonProperty("money")]
        public float Money { get; set; }
        //public int moral { get; set; }
        //public NavDataOfficial official { get; set; }
        //public NavDataOfficialVerify officialVerify { get; set; }
        //public NavDataPendant pendant { get; set; }
        //public int scores { get; set; }
        //public string shop_url { get; set; }
        [JsonProperty("uname")]
        public string Name { get; set; }
        //public long vipDueDate { get; set; }
        [JsonProperty("vipStatus")]
        public int VipStatus { get; set; }
        //public int vipType { get; set; }
        //public int vip_avatar_subscript { get; set; }
        //public NavDataVipLabel vip_label { get; set; }
        //public string vip_nickname_color { get; set; }
        //public int vip_pay_type { get; set; }
        //public int vip_theme_type { get; set; }
        [JsonProperty("wallet")]
        public UserInfoWallet Wallet { get; set; }
    }

    //public class NavDataLevelInfo
    //{
    //    public int current_exp { get; set; }
    //    public int current_level { get; set; }
    //    public int current_min { get; set; }
    //    //public int next_exp { get; set; } // 当等级为6时，next_exp为string类型，值为"--"
    //}

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

    [JsonObject]
    public class UserInfoWallet : BaseEntity
    {
        [JsonProperty("bcoin_balance")]
        public float BcoinBalance { get; set; }
        [JsonProperty("coupon_balance")]
        public float CouponBalance { get; set; }
        [JsonProperty("coupon_due_time")]
        public long CouponDueTime { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
    }

}
