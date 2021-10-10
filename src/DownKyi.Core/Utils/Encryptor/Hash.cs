using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DownKyi.Core.Utils.Encryptor
{
    public static class Hash
    {
        /// <summary>
        /// 计算字符串MD5值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMd5Hash(string input)
        {
            if (input == null)
            {
                return null;
            }

            MD5 md5Hash = MD5.Create();

            // 将输入字符串转换为字节数组并计算哈希数据
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // 创建一个 Stringbuilder 来收集字节并创建字符串
            StringBuilder sBuilder = new StringBuilder();

            // 循环遍历哈希数据的每一个字节并格式化为十六进制字符串
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // 返回十六进制字符串
            return sBuilder.ToString();
        }

        /// <summary>
        /// 计算文件MD5值
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception e)
            {
                throw new Exception("GetMD5HashFromFile()发生异常: {0}" + e.Message);
            }
        }

    }
}
