using System.Runtime.Serialization.Formatters.Binary;

namespace Downkyi.Core.Utils;

public static class ObjectHelper
{
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
            using Stream stream = File.Create(file);
#pragma warning disable SYSLIB0011 // 类型或成员已过时
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
#pragma warning restore SYSLIB0011 // 类型或成员已过时

            return true;
        }
        catch (IOException e)
        {
            Log.Log.Logger.Error(e);
            return false;
        }
        catch (Exception e)
        {
            Log.Log.Logger.Error(e);
            return false;
        }
    }

    /// <summary>
    /// 从磁盘读取序列化对象
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public static object? ReadObjectFromDisk(string file)
    {
        try
        {
            using Stream stream = File.Open(file, FileMode.Open);
#pragma warning disable SYSLIB0011 // 类型或成员已过时
            var formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
#pragma warning restore SYSLIB0011 // 类型或成员已过时
        }
        catch (IOException e)
        {
            Log.Log.Logger.Error(e);
            return null;
        }
        catch (Exception e)
        {
            Log.Log.Logger.Error(e);
            return null;
        }
    }
}