using System.Text.RegularExpressions;

namespace Downkyi.Core.Utils.Validator;

public static class Number
{

    /// <summary>
    /// 字符串转数字（长整型）
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static long GetInt(string value)
    {
        return IsInt(value) ? long.Parse(value) : -1;
    }

    /// <summary>
    /// 是否为数字
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsInt(string value)
    {
        return Regex.IsMatch(value, @"^\d+$");
    }

}