namespace Core.entity
{
    // https://api.bilibili.com/x/space/myinfo
    public class MyInfo
    {
        //public int code { get; set; }
        public MyInfoData data { get; set; }
        public string message { get; set; }
        //public int ttl { get; set; }
    }

    public class MyInfoData
    {
        public long birthday { get; set; }
        public float coins { get; set; }
        public int email_status { get; set; }
        public string face { get; set; }
        public int follower { get; set; }
        public int following { get; set; }
        public int identification { get; set; }
        public int is_deleted { get; set; }
        public int is_fake_account { get; set; }
        public int is_tourist { get; set; }
        public int jointime { get; set; }
        public int level { get; set; }
        public MyInfoDataLevelExp level_exp { get; set; }
        public long mid { get; set; }
        public int moral { get; set; }
        public string name { get; set; }
        public MyInfoDataNamePlate nameplate { get; set; }
        public MyInfoDataOfficial official { get; set; }
        public MyInfoDataPendant pendant { get; set; }
        public int pin_prompting { get; set; }
        public int rank { get; set; }
        public string sex { get; set; }
        public string sign { get; set; }
        public int silence { get; set; }
        public int tel_status { get; set; }
        public MyInfoDataVip vip { get; set; }
    }

    public class MyInfoDataLevelExp
    {
        public int current_exp { get; set; }
        public int current_level { get; set; }
        public int current_min { get; set; }
        public int next_exp { get; set; }
    }

    public class MyInfoDataNamePlate
    {
        public string condition { get; set; }
        public string image { get; set; }
        public string image_small { get; set; }
        public string level { get; set; }
        public string name { get; set; }
        public int nid { get; set; }
    }

    public class MyInfoDataOfficial
    {
        public string desc { get; set; }
        public int role { get; set; }
        public string title { get; set; }
        public int type { get; set; }
    }

    public class MyInfoDataPendant
    {
        public int expire { get; set; }
        public string image { get; set; }
        public string image_enhance { get; set; }
        public string name { get; set; }
        public int pid { get; set; }
    }

    public class MyInfoDataVip
    {
        public int avatar_subscript { get; set; }
        public long due_date { get; set; }
        public MyInfoDataVipLabel label { get; set; }
        public string nickname_color { get; set; }
        public int status { get; set; }
        public int theme_type { get; set; }
        public int type { get; set; }
        public int vip_pay_type { get; set; }
    }

    public class MyInfoDataVipLabel
    {
        public string label_theme { get; set; }
        public string path { get; set; }
        public string text { get; set; }
    }

}
