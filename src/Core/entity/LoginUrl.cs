// 注释掉未使用的属性
namespace Core.entity
{
    public class LoginUrl
    {
        //public int code { get; set; }
        public LoginData data { get; set; }
        public bool status { get; set; }
        //public long ts { get; set; }
    }

    public class LoginData
    {
        public string oauthKey { get; set; }
        public string url { get; set; }
    }


    public class LoginInfo
    {
        public int code { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public string url { get; set; }
    }

    public class LoginInfoScanning
    {
        public bool status { get; set; }
        public int data { get; set; }
        public string message { get; set; }
    }

    public class LoginInfoReady
    {
        // {"code":0,"status":true,"ts":1594131179,"data":{"url":"https://passport.biligame.com/crossDomain?DedeUserID=42018135&DedeUserID__ckMd5=44e22fa30fe34ac4&Expires=15551000&SESSDATA=92334e44%2C1609683179%2C54db1%2A71&bili_jct=979b94fb3879c68574f02800d8a39484&gourl=https%3A%2F%2Fwww.bilibili.com"}}

        public int code { get; set; }
        public bool status { get; set; }

        //public long ts { get; set; }
        public LoginInfoData data { get; set; }
    }

    public class LoginInfoData
    {
        public string url { get; set; }
    }

}
