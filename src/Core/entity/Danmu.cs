using System.Collections.Generic;

// 注释掉未使用的属性
namespace Core.entity
{

    public class DanmuDate
    {
        //{"code":0,"message":"0","ttl":1,"data":["2020-07-01","2020-07-02","2020-07-03","2020-07-04","2020-07-05","2020-07-06","2020-07-07","2020-07-08"]}
        //public int code { get; set; }
        //public string message { get; set; }
        //public int ttl { get; set; }
        public List<string> data { get; set; }
    }

    public class DanmuFromWeb
    {
        //public int code { get; set; }
        public string message { get; set; }
        //public int ttl { get; set; }
    }



}
