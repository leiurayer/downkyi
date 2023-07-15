using System.Security.Cryptography;
using System.Text;

namespace Downkyi.Core.Utils.Encryptor;

public static partial class Encryptor
{
    /// <summary>
    /// DES加密字符串
    /// </summary>
    /// <param name="encryptString">待加密的字符串</param>
    /// <param name="encryptKey">加密密钥,要求为8位</param>
    /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
    public static string EncryptString(string encryptString, string encryptKey)
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey[..8]);//转换为字节
            byte[] rgbIV = Encoding.UTF8.GetBytes(encryptKey[..8]);
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
#pragma warning disable SYSLIB0021 // 类型或成员已过时
            var dCSP = new DESCryptoServiceProvider();//实例化数据加密标准
#pragma warning restore SYSLIB0021 // 类型或成员已过时
            var mStream = new MemoryStream();//实例化内存流
                                             //将数据流链接到加密转换的流
            var cStream = new CryptoStream(mStream, dCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            // 转base64
            return Convert.ToBase64String(mStream.ToArray());
        }
        catch (Exception e)
        {
            Log.Log.Logger.Error(e);
            return encryptString;
        }
    }

    /// <summary>
    /// DES解密字符串
    /// </summary>
    /// <param name="decryptString">待解密的字符串</param>
    /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
    /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
    public static string DecryptString(string decryptString, string decryptKey)
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
            byte[] rgbIV = Encoding.UTF8.GetBytes(decryptKey);
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
#pragma warning disable SYSLIB0021 // 类型或成员已过时
            var DCSP = new DESCryptoServiceProvider();
#pragma warning restore SYSLIB0021 // 类型或成员已过时
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
        catch (Exception e)
        {
            Log.Log.Logger.Error(e);
            return decryptString;
        }
    }

}