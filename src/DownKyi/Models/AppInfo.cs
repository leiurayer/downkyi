namespace DownKyi.Models
{
    public class AppInfo
    {
        public string Name { get; } = "哔哩下载姬";
        public int VersionCode { get; } = 510;

#if DEBUG
        public string VersionName { get; } = "1.5.4 Beta";
#else
        public string VersionName { get; } = "1.5.4";
#endif

    }
}
