namespace DownKyi.Models
{
    public class AppInfo
    {
        public string Name { get; } = "哔哩下载姬";
        public int VersionCode { get; } = 513;

#if DEBUG
        public string VersionName { get; } = "1.5.6 Debug";
#else
        public string VersionName { get; } = "1.5.6";
#endif

    }
}
