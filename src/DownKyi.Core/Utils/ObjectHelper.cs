using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;

namespace DownKyi.Core.Utils
{
    public static class ObjectHelper
    {

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
                Debug.Console.PrintLine(name + ": " + value + "\t" + cookieContainer.Count);
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
                {
                    foreach (Cookie c in colCookies)
                    {
                        lstCookies.Add(c);
                    }
                }
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
                    Debug.Console.PrintLine("Writing object to disk... ");

                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, obj);

                    Debug.Console.PrintLine("Done.");
                    return true;
                }
            }
            catch (IOException e)
            {
                Debug.Console.PrintLine("WriteObjectToDisk()发生IO异常: {0}", e);
                Logging.LogManager.Error(e);
                return false;
            }
            catch (Exception e)
            {
                Debug.Console.PrintLine("WriteObjectToDisk()发生异常: {0}", e);
                Logging.LogManager.Error(e);
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
                    Debug.Console.PrintLine("Reading object from disk... ");
                    BinaryFormatter formatter = new BinaryFormatter();
                    Debug.Console.PrintLine("Done.");
                    return formatter.Deserialize(stream);
                }
            }
            catch (IOException e)
            {
                Debug.Console.PrintLine("ReadObjectFromDisk()发生IO异常: {0}", e);
                Logging.LogManager.Error(e);
                return null;
            }
            catch (Exception e)
            {
                Debug.Console.PrintLine("ReadObjectFromDisk()发生异常: {0}", e);
                Logging.LogManager.Error(e);
                return null;
            }
        }
    }
}
