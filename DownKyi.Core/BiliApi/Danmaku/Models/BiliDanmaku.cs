namespace DownKyi.Core.BiliApi.Danmaku.Models
{
    public class BiliDanmaku
    {
        public long Id { get; set; }          //弹幕dmID
        public int Progress { get; set; }     //出现时间
        public int Mode { get; set; }         //弹幕类型
        public int Fontsize { get; set; }     //文字大小
        public uint Color { get; set; }       //弹幕颜色
        public string MidHash { get; set; }   //发送者UID的HASH
        public string Content { get; set; }   //弹幕内容
        public long Ctime { get; set; }       //发送时间
        public int Weight { get; set; }       //权重
        //public string Action { get; set; }    //动作？
        public int Pool { get; set; }         //弹幕池

        public override string ToString()
        {
            string separator = "\n";
            return $"id: {Id}{separator}" +
                $"progress: {Progress}{separator}" +
                $"mode: {Mode}{separator}" +
                $"fontsize: {Fontsize}{separator}" +
                $"color: {Color}{separator}" +
                $"midHash: {MidHash}{separator}" +
                $"content: {Content}{separator}" +
                $"ctime: {Ctime}{separator}" +
                $"weight: {Weight}{separator}" +
                //$"action: {Action}{separator}" +
                $"pool: {Pool}";
        }
    }
}
