using System.IO;

namespace DownKyi.Core.Utils
{
    public static class HardDisk
    {
        /// <summary>
        /// 获取指定驱动器的空间总大小
        /// </summary>
        /// <param name="hardDiskName">只需输入代表驱动器的字母即可</param>
        /// <returns></returns>
        public static long GetHardDiskSpace(string hardDiskName)
        {
            long totalSize = new long();
            hardDiskName = $"{hardDiskName}:\\";
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if (drive.Name == hardDiskName)
                {
                    totalSize = drive.TotalSize;
                }
            }

            return totalSize;
        }

        /// <summary>
        /// 获取指定驱动器的剩余空间总大小
        /// </summary>
        /// <param name="hardDiskName">只需输入代表驱动器的字母即可</param>
        /// <returns></returns>
        public static long GetHardDiskFreeSpace(string hardDiskName)
        {
            long freeSpace = new long();
            hardDiskName = $"{hardDiskName}:\\";
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if (drive.Name == hardDiskName)
                {
                    freeSpace = drive.TotalFreeSpace;
                }
            }

            return freeSpace;
        }

    }
}
