namespace DownKyi.Core.Aria2cNet.Server
{
    /// <summary>
    /// 文件预分配
    /// </summary>
    public enum AriaConfigFileAllocation
    {
        NONE = 1, // 没有预分配
        PREALLOC, // 预分配，默认
        FALLOC // NTFS建议使用
    }
}
