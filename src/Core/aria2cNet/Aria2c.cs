using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// 代码来源
// https://blog.csdn.net/haibin258/article/details/87905628

// Aria2详细信息请看
// https://aria2.github.io/manual/en/html/aria2c.html#rpc-interface

// 废弃
namespace Core.aria2cNet
{
    //定义一个类用于转码成JSON字符串发送给Aria2 json-rpc接口
    public class JsonClass
    {
        public string Jsonrpc { get; set; }
        public string Id { get; set; }
        public string Method { get; set; }
        public List<string> Params { get; set; }
    }

    public class Aria2
    {
        public static ClientWebSocket webSocket;           // 用于连接到Aria2Rpc的客户端
        public static CancellationToken cancellationToken; // 传播有关应取消操作的通知

        /// <summary>
        /// 连接Aria2Rpc服务器
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<bool> ConnectServer(string uri)
        {
            webSocket = new ClientWebSocket();           // 用于连接到Aria2Rpc的客户端
            cancellationToken = new CancellationToken(); // 传播有关应取消操作的通知
            bool status = false;  //储存连接状态
            try
            {
                //连接服务器
                await webSocket.ConnectAsync(new Uri(uri), cancellationToken);
            }
            catch
            {
                status = false;
            }
            //检查连接是否成功
            if (webSocket.State == WebSocketState.Open)
            {
                status = true;
            }
            return status;
        }


        /// <summary>
        /// JsonClass类转为json格式
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static string ToJson(JsonClass json)
        {
            string str = "{" + "\"jsonrpc\":\"" + json.Jsonrpc + "\",\"id\":\"" + json.Id + "\",\"method\":\"" + json.Method + "\",\"params\":[";
            for (int i = 0; i < json.Params.Count; i++)
            {
                if (json.Method == "aria2.addUri")
                {
                    str += "[\"" + json.Params[i] + "\"]";
                }
                else if (json.Method == "aria2.tellStatus")
                {
                    if (i == 0)
                    {
                        str += "\"" + json.Params[i] + "\"";
                    }
                    else
                    {
                        str += "[\"" + json.Params[i] + "\"]";
                    }
                }
                else
                {
                    str += "\"" + json.Params[i] + "\"";
                }
                if (json.Params.Count - 1 > i)
                {
                    str += ",";
                }
            }
            str += "]}";
            //最后得到类似
            //{"jsonrpc":"2.0","id":"qwer","method":"aria2.addUri","params:"[["http://www.baidu.com"]]}
            return str;
        }


        /// <summary>
        /// 发送json并返回json消息
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        private static async Task<string> SendAndReceive(JsonClass json)
        {
            string str = ToJson(json);
            try
            {
                //发送json数据
                await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(str)), WebSocketMessageType.Text, true, cancellationToken);
                var result = new byte[1024];
                //接收数据
                await webSocket.ReceiveAsync(new ArraySegment<byte>(result), new CancellationToken());
                str += "\r\n" + Encoding.UTF8.GetString(result, 0, result.Length) + "\r\n";
            }
            catch
            {
                str = "连接错误";
            }
            return str;
        }


        /// <summary>
        /// 添加新的下载
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<string> AddUri(string uri)
        {
            var json = new JsonClass
            {
                Jsonrpc = "2.0",
                Id = "qwer",
                Method = "aria2.addUri"
            };
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(uri);
            json.Params = paramslist;
            string str = await SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 上传“.torrent”文件添加BitTorrent下载
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static async Task<string> AddTorrent(string path)
        {
            string str = "";
            string fsbase64 = "";
            byte[] fs = File.ReadAllBytes(path);
            fsbase64 = Convert.ToBase64String(fs);  //转为Base64编码

            var json = new JsonClass();
            json.Jsonrpc = "2.0";
            json.Id = "qwer";
            json.Method = "aria2.addTorrent";
            List<string> paramslist = new List<string>();
            //添加“.torrent”文件本地地址
            paramslist.Add(fsbase64);
            json.Params = paramslist;
            str = await SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 删除已经停止的任务，强制删除请使用ForceRemove方法
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> Remove(string gid)
        {
            string str = "";
            var json = new JsonClass
            {
                Jsonrpc = "2.0",
                Id = "qwer",
                Method = "aria2.remove"
            };
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.Params = paramslist;
            str = await SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 强制删除
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> ForceRemove(string gid)
        {
            string str = "";
            var json = new JsonClass
            {
                Jsonrpc = "2.0",
                Id = "qwer",
                Method = "aria2.forceRemove"
            };
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.Params = paramslist;
            str = await SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 暂停下载,强制暂停请使用ForcePause方法
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> Pause(string gid)
        {
            string str = "";
            var json = new JsonClass
            {
                Jsonrpc = "2.0",
                Id = "qwer",
                Method = "aria2.pause"
            };
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.Params = paramslist;
            str = await SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 暂停全部任务
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> PauseAll()
        {
            string str = "";
            var json = new JsonClass();
            json.Jsonrpc = "2.0";
            json.Id = "qwer";
            json.Method = "aria2.pauseAll";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add("");
            json.Params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 强制暂停下载
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> ForcePause(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.Jsonrpc = "2.0";
            json.Id = "qwer";
            json.Method = "aria2.forcePause";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.Params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 强制暂停全部下载
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> ForcePauseAll()
        {
            string str = "";
            var json = new JsonClass();
            json.Jsonrpc = "2.0";
            json.Id = "qwer";
            json.Method = "aria2.forcePauseAll";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add("");
            json.Params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 把正在下载的任务状态改为等待下载
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> PauseToWaiting(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.Jsonrpc = "2.0";
            json.Id = "qwer";
            json.Method = "aria2.unpause";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.Params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 把全部在下载的任务状态改为等待下载
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> PauseToWaitingAll()
        {
            string str = "";
            var json = new JsonClass();
            json.Jsonrpc = "2.0";
            json.Id = "qwer";
            json.Method = "aria2.unpauseAll";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add("");
            json.Params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 返回下载进度
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> TellStatus(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.Jsonrpc = "2.0";
            json.Id = "qwer";
            json.Method = "aria2.tellStatus";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            paramslist.Add("completedLength\", \"totalLength\",\"downloadSpeed");

            json.Params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 返回全局统计信息，例如整体下载和上载速度
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> GetGlobalStat(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.Jsonrpc = "2.0";
            json.Id = "qwer";
            json.Method = "aria2.getGlobalStat";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.Params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 返回由gid（字符串）表示的下载中使用的URI 。响应是一个结构数组
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> GetUris(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.Jsonrpc = "2.0";
            json.Id = "qwer";
            json.Method = "aria2.getUris";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.Params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }


        /// <summary>
        /// 返回由gid（字符串）表示的下载文件列表
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static async Task<string> GetFiles(string gid)
        {
            string str = "";
            var json = new JsonClass();
            json.Jsonrpc = "2.0";
            json.Id = "qwer";
            json.Method = "aria2.getFiles";
            List<string> paramslist = new List<string>();
            //添加下载地址
            paramslist.Add(gid);
            json.Params = paramslist;
            str = await Aria2.SendAndReceive(json);
            return str;
        }

    }
}
