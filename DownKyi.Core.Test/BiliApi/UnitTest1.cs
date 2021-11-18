using Brotli;
using DownKyi.Core.BiliApi.Login;
using DownKyi.Core.BiliApi.Models.Json;
using DownKyi.Core.BiliApi.VideoStream;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace DownKyi.Core.Test.BiliApi
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var player = VideoStream.PlayerV2(464087531, "BV1HL411u765", 439599721);

            foreach (var subtitle in player.Subtitle.Subtitles)
            {
                string referer = "https://www.bilibili.com";
                string response = RequestWeb($"https:{subtitle.SubtitleUrl}", referer);

                try
                {
                    //Console.WriteLine(subtitle.SubtitleUrl);

                    //string json = @"D:\test.json";
                    //WebClient mywebclient = new WebClient();
                    //mywebclient.DownloadFile($"https:{subtitle.SubtitleUrl}", json);

                    //StreamReader streamReader = File.OpenText(json);
                    //string jsonWordTemplate = streamReader.ReadToEnd();
                    //streamReader.Close();

                    var subtitleJson = JsonConvert.DeserializeObject<SubtitleJson>(response);
                    if (subtitleJson == null) { return; }

                    string srt = subtitleJson.ToSubRip();
                    File.WriteAllText($"D:/{subtitle.LanDoc}.srt", srt);
                }
                catch (Exception e)
                {
                    Utils.Debugging.Console.PrintLine("PlayerV2()发生异常: {0}", e);
                }
            }

        }


        public string RequestWeb(string url, string referer = null, string method = "GET", Dictionary<string, string> parameters = null, int retry = 3)
        {
            // 重试次数
            if (retry <= 0) { return ""; }

            // post请求，发送参数
            if (method == "POST" && parameters != null)
            {
                StringBuilder builder = new StringBuilder();
                int i = 0;
                foreach (var item in parameters)
                {
                    if (i > 0)
                    {
                        builder.Append("&");
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
                request.Timeout = 30 * 1000;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36";
                //request.ContentType = "application/json,text/html,application/xhtml+xml,application/xml;charset=UTF-8";
                request.Headers["accept-language"] = "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7";
                request.Headers["accept-encoding"] = "gzip, deflate, br";

                // referer
                if (referer != null)
                {
                    request.Referer = referer;
                }

                // 构造cookie
                if (!url.Contains("getLogin"))
                {
                    request.Headers["origin"] = "https://www.bilibili.com";

                    CookieContainer cookies = LoginHelper.GetLoginInfoCookies();
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
                        using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                html = reader.ReadToEnd();
                            }
                        }
                    }
                    else if (response.ContentEncoding.ToLower().Contains("deflate"))
                    {
                        using (DeflateStream stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress))
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                html = reader.ReadToEnd();
                            }
                        }
                    }
                    else if (response.ContentEncoding.ToLower().Contains("br"))
                    {
                        using (BrotliStream stream = new BrotliStream(response.GetResponseStream(), CompressionMode.Decompress))
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                html = reader.ReadToEnd();
                            }
                        }
                    }
                    else
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                            {
                                html = reader.ReadToEnd();
                            }
                        }
                    }
                }

                return html;
            }
            catch (WebException e)
            {
                Console.WriteLine("RequestWeb()发生Web异常: {0}", e);
                Logging.LogManager.Error(e);
                return RequestWeb(url, referer, method, parameters, retry - 1);
            }
            catch (IOException e)
            {
                Console.WriteLine("RequestWeb()发生IO异常: {0}", e);
                Logging.LogManager.Error(e);
                return RequestWeb(url, referer, method, parameters, retry - 1);
            }
            catch (Exception e)
            {
                Console.WriteLine("RequestWeb()发生其他异常: {0}", e);
                Logging.LogManager.Error(e);
                return RequestWeb(url, referer, method, parameters, retry - 1);
            }
        }
    }
}
