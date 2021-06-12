using Core.aria2cNet.client.entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.aria2cNet.client
{
    public class AriaClientWebSocket
    {
        private static readonly string JSONRPC = "2.0";
        private static readonly string TOKEN = "downkyi";

        public static ClientWebSocket webSocket;           // 用于连接到Aria2Rpc的客户端
        public static CancellationToken cancellationToken; // 传播有关应取消操作的通知
        public static bool Status;                         // 储存连接状态

        /// <summary>
        /// 连接Aria2Rpc服务器
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static async Task<bool> ConnectServerAsync(string uri)
        {
            webSocket = new ClientWebSocket();           // 用于连接到Aria2Rpc的客户端
            cancellationToken = new CancellationToken(); // 传播有关应取消操作的通知
            Status = false;                              // 储存连接状态

            try
            {
                //连接服务器
                await webSocket.ConnectAsync(new Uri(uri), cancellationToken);
            }
            catch
            {
                Status = false;
            }
            //检查连接是否成功
            if (webSocket.State == WebSocketState.Open)
            {
                Status = true;
            }
            return Status;
        }

        /// <summary>
        /// add
        /// </summary>
        /// <param name="uris"></param>
        /// <returns></returns>
        public static async Task<string> AddUriAsync(List<string> uris)
        {
            AriaSendOption option = new AriaSendOption
            {
                Out = "index测试.html",
                Dir = "home/"
            };

            List<object> ariaParams = new List<object>
            {
                "token:" + TOKEN,
                uris,
                option
            };

            AriaSendData ariaSend = new AriaSendData
            {
                Id = Guid.NewGuid().ToString("N"),
                Jsonrpc = JSONRPC,
                Method = "aria2.addUri",
                Params = ariaParams
            };

            string sendJson = JsonConvert.SerializeObject(ariaSend);

            string result = await SendAndReceiveAsync(sendJson);
            return result;
        }


        /// <summary>
        /// 发送并接受数据
        /// </summary>
        /// <param name="sendJson"></param>
        /// <returns></returns>
        private static async Task<string> SendAndReceiveAsync(string sendJson)
        {
            string result;
            try
            {
                //发送json数据
                await webSocket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(sendJson)), WebSocketMessageType.Text, true, cancellationToken);
                byte[] receive = new byte[1024];
                //接收数据
                await webSocket.ReceiveAsync(new ArraySegment<byte>(receive), new CancellationToken());
                result = Encoding.UTF8.GetString(receive).TrimEnd('\0');
            }
            catch
            {
                result = "{\"id\":null,\"jsonrpc\":\"2.0\",\"error\":{\"code\":-2020,\"message\":\"连接错误\"}}";
            }
            return result;
        }



    }
}
