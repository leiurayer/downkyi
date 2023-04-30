using System;
using System.Text.RegularExpressions;

namespace DownKyi.Models
{
    public class AppInfo
    {
        public string Name { get; } = "哔哩下载姬";
        public int VersionCode { get; }
        public string VersionName { get; }

        const int a = 1;
        const int b = 5;
        const int c = 8;

        public AppInfo()
        {
            VersionCode = a * 10000 + b * 100 + c;

#if DEBUG
            VersionName = $"{a}.{b}.{c}-debug";
#else
            VersionName = $"{a}.{b}.{c}";
#endif
        }

        public static int VersionNameToCode(string versionName)
        {
            int code = 0;

            var isMatch = Regex.IsMatch(versionName, @"^v?([1-9]\d|\d).([1-9]\d|\d).([1-9]\d|\d)$");
            if (!isMatch)
            {
                return 0;
            }

            string pattern = @"([1-9]\d|\d)";
            var m = Regex.Matches(versionName, pattern);
            if (m.Count == 3)
            {
                int i = 2;
                foreach (var item in m)
                {
                    code += int.Parse(item.ToString()) * (int)Math.Pow(100, i);
                    i--;
                }
            }

            return code;
        }

    }
}
