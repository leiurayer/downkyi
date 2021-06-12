using Brotli;
using Core.api.login;
using Core.history;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Core
{
    public static class Utils
    {
        /// <summary>
        /// 随机的UserAgent
        /// </summary>
        /// <returns>userAgent</returns>
        public static string GetUserAgent()
        {
            string[] userAgents = {
                // Chrome
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/84.0.4147.125 Safari/537.36",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.111 Safari/537.36",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.141 Safari/537.36",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/88.0.4324.182 Safari/537.36",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.77 Safari/537.36",

                // 新版Edge
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36 Edg/83.0.478.58",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36 Edg/90.0.818.66",
                
                // IE 11
                "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 10.0; WOW64; Trident/7.0; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.30729; .NET CLR 3.5.30729)",
                
                // 火狐
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:69.0) Gecko/20100101 Firefox/69.0",

                // Opera
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36 OPR/63.0.3368.43",

                // MacOS Chrome
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_5) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.106 Safari/537.36",

                // MacOS Safari
                "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_5) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.1.1 Safari/605.1.15"
            };

            var time = DateTime.Now;

            Random random = new Random(time.GetHashCode());
            int number = random.Next(0, userAgents.Length - 1);

            return userAgents[number];
        }

        /// <summary>
        /// 发送get或post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="referer"></param>
        /// <param name="method"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string RequestWeb(string url, string referer = null, string method = "GET", Dictionary<string, string> parameters = null, int retry = 3)
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
                    if (i > 0) builder.Append("&");
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
                request.UserAgent = GetUserAgent();
                //request.ContentType = "application/json,text/html,application/xhtml+xml,application/xml;charset=UTF-8";
                request.Headers["accept-language"] = "zh-CN,zh;q=0.9,en-US;q=0.8,en;q=0.7";
                request.Headers["accept-encoding"] = "gzip, deflate, br";

                //request.Headers["sec-fetch-dest"] = "empty";
                //request.Headers["sec-fetch-mode"] = "cors";
                //request.Headers["sec-fetch-site"] = "same-site";

                // referer
                if (referer != null)
                {
                    request.Referer = referer;
                }

                // 构造cookie
                if (!url.Contains("getLogin"))
                {
                    request.Headers["origin"] = "https://www.bilibili.com";

                    CookieContainer cookies = LoginHelper.GetInstance().GetLoginInfoCookies();
                    if (cookies != null)
                    {
                        request.CookieContainer = cookies;
                    }
                }

                //// post请求，发送参数
                //if (method == "POST" && parameters != null)
                //{
                //    StringBuilder builder = new StringBuilder();
                //    int i = 0;
                //    foreach (var item in parameters)
                //    {
                //        if (i > 0) builder.Append("&");
                //        builder.AppendFormat("{0}={1}", item.Key, item.Value);
                //        i++;
                //    }
                //    byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
                //    request.ContentLength = data.Length;

                //    Stream reqStream = request.GetRequestStream();
                //    reqStream.Write(data, 0, data.Length);
                //    reqStream.Close();

                //    Console.WriteLine("\n" + builder.ToString() + "\t" + data.Length + "\n");
                //}

                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Stream responseStream = response.GetResponseStream();
                //StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                //string str = streamReader.ReadToEnd();
                //streamReader.Close();
                //responseStream.Close();

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
                return RequestWeb(url, referer, method, parameters, retry - 1);
            }
            catch (IOException e)
            {
                Console.WriteLine("RequestWeb()发生IO异常: {0}", e);
                return RequestWeb(url, referer, method, parameters, retry - 1);
            }
            catch (Exception e)
            {
                Console.WriteLine("RequestWeb()发生其他异常: {0}", e);
                return RequestWeb(url, referer, method, parameters, retry - 1);
            }
        }

        /// <summary>
        /// 解析二维码登录返回的url，用于设置cookie
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static CookieContainer ParseCookie(string url)
        {
            CookieContainer cookieContainer = new CookieContainer();

            if (url == null || url == "") { return cookieContainer; }

            string[] strList = url.Split('?');
            if (strList.Count() < 2) { return cookieContainer; }

            string[] strList2 = strList[1].Split('&');
            if (strList2.Count() == 0) { return cookieContainer; }

            // 获取expires
            string expires = strList2.FirstOrDefault(it => it.Contains("Expires")).Split('=')[1];
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddSeconds(int.Parse(expires));

            foreach (var item in strList2)
            {
                string[] strList3 = item.Split('=');
                if (strList3.Count() < 2) { continue; }

                string name = strList3[0];
                string value = strList3[1];

                // 不需要
                if (name == "Expires" || name == "gourl") { continue; }

                // 添加cookie
                cookieContainer.Add(new Cookie(name, value, "/", ".bilibili.com") { Expires = dateTime });
#if DEBUG
                Console.WriteLine(name + ": " + value + "\t" + cookieContainer.Count);
#endif
            }

            return cookieContainer;
        }

        /// <summary>
        /// 将CookieContainer中的所有的Cookie读出来
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();

            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });

            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies) lstCookies.Add(c);
            }

            return lstCookies;
        }

        /// <summary>
        /// 写入cookies到磁盘
        /// </summary>
        /// <param name="file"></param>
        /// <param name="cookieJar"></param>
        /// <returns></returns>
        public static bool WriteCookiesToDisk(string file, CookieContainer cookieJar)
        {
            return WriteObjectToDisk(file, cookieJar);
        }

        /// <summary>
        /// 从磁盘读取cookie
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static CookieContainer ReadCookiesFromDisk(string file)
        {
            return (CookieContainer)ReadObjectFromDisk(file);
        }

        /// <summary>
        /// 写入历史数据到磁盘
        /// </summary>
        /// <param name="file"></param>
        /// <param name="history"></param>
        /// <returns></returns>
        public static bool WriteHistoryToDisk(string file, HistoryEntity history)
        {
            return WriteObjectToDisk(file, history);
        }

        /// <summary>
        /// 从磁盘读取历史数据
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static HistoryEntity ReadHistoryFromDisk(string file)
        {
            return (HistoryEntity)ReadObjectFromDisk(file);
        }

        /// <summary>
        /// 写入序列化对象到磁盘
        /// </summary>
        /// <param name="file"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool WriteObjectToDisk(string file, object obj)
        {
            try
            {
                using (Stream stream = File.Create(file))
                {
#if DEBUG
                    Console.Out.Write("Writing object to disk... ");
#endif
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, obj);
#if DEBUG
                    Console.Out.WriteLine("Done.");
#endif
                    return true;
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("WriteObjectToDisk()发生IO异常: {0}", e);
                return false;
            }
            catch (Exception e)
            {
                //Console.Out.WriteLine("Problem writing object to disk: " + e.GetType());
                Console.WriteLine("WriteObjectToDisk()发生异常: {0}", e);
                return false;
            }
        }

        /// <summary>
        /// 从磁盘读取序列化对象
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static object ReadObjectFromDisk(string file)
        {
            try
            {
                using (Stream stream = File.Open(file, FileMode.Open))
                {
#if DEBUG
                    Console.Out.Write("Reading object from disk... ");
#endif
                    BinaryFormatter formatter = new BinaryFormatter();
#if DEBUG
                    Console.Out.WriteLine("Done.");
#endif
                    return formatter.Deserialize(stream);
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("ReadObjectFromDisk()发生IO异常: {0}", e);
                return null;
            }
            catch (Exception e)
            {
                //Console.Out.WriteLine("Problem reading object from disk: " + e.GetType());
                Console.WriteLine("ReadObjectFromDisk()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="version">版本 1 ~ 40</param>
        /// <param name="pixel">像素点大小</param>
        /// <param name="icon_path">图标路径</param>
        /// <param name="icon_size">图标尺寸</param>
        /// <param name="icon_border">图标边框厚度</param>
        /// <param name="white_edge">二维码白边</param>
        /// <returns>位图</returns>
        public static Bitmap EncodeQRCode(string msg, int version, int pixel, string icon_path, int icon_size, int icon_border, bool white_edge)
        {
            QRCoder.QRCodeGenerator code_generator = new QRCoder.QRCodeGenerator();

            QRCoder.QRCodeData code_data = code_generator.CreateQrCode(msg, QRCoder.QRCodeGenerator.ECCLevel.H/* 这里设置容错率的一个级别 */, true, false, QRCoder.QRCodeGenerator.EciMode.Utf8, version);

            QRCoder.QRCode code = new QRCoder.QRCode(code_data);

            Bitmap icon;
            if (icon_path == null || icon_path == "")
            {
                icon = null;
            }
            else
            {
                icon = new Bitmap(icon_path);
            }

            Bitmap bmp = code.GetGraphic(pixel, Color.Black, Color.White, icon, icon_size, icon_border, white_edge);
            return bmp;
        }

    }

}
