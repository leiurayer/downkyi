using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace BiliSharp
{
    internal static class WebClient
    {
        /// <summary>
        /// 发送get或post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="referer"></param>
        /// <param name="userAgent"></param>
        /// <param name="cookies"></param>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <param name="retry"></param>
        /// <returns></returns>
        internal static string RequestWeb(string url, string referer = null, string userAgent = null, CookieContainer cookies = null, string method = "GET", Dictionary<string, string> parameters = null, int retry = 3)
        {
            // 重试次数
            if (retry <= 0) { return ""; }

            // post请求，发送参数
            if (method == "POST" && parameters != null)
            {
                var builder = new StringBuilder();
                int i = 0;
                foreach (var item in parameters)
                {
                    if (i > 0)
                    {
                        builder.Append('&');
                    }

                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }

                url += "?" + builder.ToString();
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                request.Timeout = 60 * 1000;

                request.ContentType = "application/json,text/html,application/xhtml+xml,application/xml;charset=UTF-8";
                request.Headers["accept-language"] = "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7";
                request.Headers["accept-encoding"] = "gzip, deflate, br";

                // userAgent
                if (userAgent != null)
                {
                    request.UserAgent = userAgent;
                }
                else
                {
                    request.UserAgent = "Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36";
                }

                // referer
                if (referer != null)
                {
                    request.Referer = referer;
                }

                // 构造cookie
                if (!url.Contains("getLogin"))
                {
                    //request.Headers["origin"] = "https://www.bilibili.com";

                    if (cookies != null)
                    {
                        request.CookieContainer = cookies;
                    }
                }

                string html = string.Empty;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.ContentEncoding.ToLower().Contains("gzip"))
                    {
                        using var stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                        using var reader = new StreamReader(stream, Encoding.UTF8);
                        html = reader.ReadToEnd();
                    }
                    else if (response.ContentEncoding.ToLower().Contains("deflate"))
                    {
                        using var stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress);
                        using var reader = new StreamReader(stream, Encoding.UTF8);
                        html = reader.ReadToEnd();
                    }
                    else if (response.ContentEncoding.ToLower().Contains("br"))
                    {
                        using var stream = new BrotliStream(response.GetResponseStream(), CompressionMode.Decompress);
                        using var reader = new StreamReader(stream, Encoding.UTF8);
                        html = reader.ReadToEnd();
                    }
                    else
                    {
                        using var stream = response.GetResponseStream();
                        using var reader = new StreamReader(stream, Encoding.UTF8);
                        html = reader.ReadToEnd();
                    }
                }

                return html;
            }
            catch (Exception)
            {
                return RequestWeb(url, referer, userAgent, cookies, method, parameters, retry - 1);
            }
        }
    }
}