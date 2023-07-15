using System.Security.Cryptography;
using System.Text;

namespace Downkyi.Core.Utils.Encryptor;

public sealed class EncryptorFile
{
    string iv = "xIrlMBGX";
    string key = "A2&P!buv";

    /// <summary>
    /// DES加密偏移量，必须是>=8位长的字符串
    /// </summary>
    public string IV
    {
        get { return iv; }
        set { iv = value; }
    }

    /// <summary>
    /// DES加密的私钥，必须是8位长的字符串
    /// </summary>
    public string Key
    {
        get { return key; }
        set { key = value; }
    }

    /// <summary>
    /// 对字符串进行DES加密
    /// </summary>
    /// <param name="sourceString">待加密的字符串</param>
    /// <returns>加密后的BASE64编码的字符串</returns>
    public string Encrypt(string sourceString)
    {
        byte[] btKey = Encoding.Default.GetBytes(key);
        byte[] btIV = Encoding.Default.GetBytes(iv);
#pragma warning disable SYSLIB0021 // 类型或成员已过时
        var des = new DESCryptoServiceProvider();
#pragma warning restore SYSLIB0021 // 类型或成员已过时
        using (MemoryStream ms = new MemoryStream())
        {
            byte[] inData = Encoding.Default.GetBytes(sourceString);
            try
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                {
                    cs.Write(inData, 0, inData.Length);
                    cs.FlushFinalBlock();
                }

                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                throw;
            }
        }
    }

    /// <summary>
    /// 对DES加密后的字符串进行解密
    /// </summary>
    /// <param name="encryptedString">待解密的字符串</param>
    /// <returns>解密后的字符串</returns>
    public string Decrypt(string encryptedString)
    {
        byte[] btKey = Encoding.Default.GetBytes(key);
        byte[] btIV = Encoding.Default.GetBytes(iv);
#pragma warning disable SYSLIB0021 // 类型或成员已过时
        var des = new DESCryptoServiceProvider();
#pragma warning restore SYSLIB0021 // 类型或成员已过时

        using (MemoryStream ms = new MemoryStream())
        {
            byte[] inData = Convert.FromBase64String(encryptedString);
            try
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                {
                    cs.Write(inData, 0, inData.Length);
                    cs.FlushFinalBlock();
                }

                return Encoding.Default.GetString(ms.ToArray());
            }
            catch
            {
                throw;
            }
        }
    }

    /// <summary>
    /// 对文件内容进行DES加密
    /// </summary>
    /// <param name="sourceFile">待加密的文件绝对路径</param>
    /// <param name="destFile">加密后的文件保存的绝对路径</param>
    public void EncryptFile(string sourceFile, string destFile)
    {
        if (!File.Exists(sourceFile)) throw new FileNotFoundException("指定的文件路径不存在！", sourceFile);

        byte[] btKey = Encoding.Default.GetBytes(key);
        byte[] btIV = Encoding.Default.GetBytes(iv);
#pragma warning disable SYSLIB0021 // 类型或成员已过时
        var des = new DESCryptoServiceProvider();
#pragma warning restore SYSLIB0021 // 类型或成员已过时
        byte[] btFile = File.ReadAllBytes(sourceFile);

        using (FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.Write))
        {
            try
            {
                using (CryptoStream cs = new CryptoStream(fs, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                {
                    cs.Write(btFile, 0, btFile.Length);
                    cs.FlushFinalBlock();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }

    /// <summary>
    /// 对文件内容进行DES加密，加密后覆盖掉原来的文件
    /// </summary>
    /// <param name="sourceFile">待加密的文件的绝对路径</param>
    public void EncryptFile(string sourceFile)
    {
        EncryptFile(sourceFile, sourceFile);
    }

    /// <summary>
    /// 对文件内容进行DES解密
    /// </summary>
    /// <param name="sourceFile">待解密的文件绝对路径</param>
    /// <param name="destFile">解密后的文件保存的绝对路径</param>
    public void DecryptFile(string sourceFile, string destFile)
    {
        if (!File.Exists(sourceFile)) throw new FileNotFoundException("指定的文件路径不存在！", sourceFile);

        byte[] btKey = Encoding.Default.GetBytes(key);
        byte[] btIV = Encoding.Default.GetBytes(iv);
#pragma warning disable SYSLIB0021 // 类型或成员已过时
        var des = new DESCryptoServiceProvider();
#pragma warning restore SYSLIB0021 // 类型或成员已过时
        byte[] btFile = File.ReadAllBytes(sourceFile);

        using (FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.Write))
        {
            try
            {
                using (CryptoStream cs = new CryptoStream(fs, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                {
                    cs.Write(btFile, 0, btFile.Length);
                    cs.FlushFinalBlock();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                fs.Close();
            }
        }
    }

    /// <summary>
    /// 对文件内容进行DES解密，加密后覆盖掉原来的文件
    /// </summary>
    /// <param name="sourceFile">待解密的文件的绝对路径</param>
    public void DecryptFile(string sourceFile)
    {
        DecryptFile(sourceFile, sourceFile);
    }
}