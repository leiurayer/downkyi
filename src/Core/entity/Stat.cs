namespace Core.entity
{
    // https://api.bilibili.com/x/relation/stat?vmid=42018135
    public class Stat
    {
        //public int code { get; set; }
        public StatData data { get; set; }
        //public string message { get; set; }
        //public int ttl { get; set; }
    }

    public class StatData
    {
        public int black { get; set; } // 黑名单
        public int follower { get; set; } // 粉丝数
        public int following { get; set; } // 关注数
        public long mid { get; set; } // 用户id
        public int whisper { get; set; } // 悄悄关注数
    }

}
