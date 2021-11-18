using System.Collections.Generic;

namespace DownKyi.Core.FileName
{
    /// <summary>
    /// 文件名的分隔符
    /// </summary>
    public static class HyphenSeparated
    {
        public static Dictionary<int, string> Hyphen = new Dictionary<int, string>()
        {
            { 101, "/" },
            { 101, "_" },
            { 102, "-" },
            { 103, "+" },
            { 104, "," },
            { 105, "." },
            { 106, "&" },
            { 107, "#" },
            { 108, "(" },
            { 109, ")" },
            { 110, "[" },
            { 111, "]" },
            { 112, "{" },
            { 113, "}" },
            { 114, " " },
        };
    }
}
