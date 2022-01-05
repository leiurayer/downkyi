using System.IO;

namespace DownKyi.Core.Storage
{
    public static class StorageManager
    {
        /// <summary>
        /// 获取历史记录的文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetAriaDir()
        {
            CreateDirectory(Constant.Aria);
            return Constant.Aria;
        }

        /// <summary>
        /// 获取历史记录的文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetDownload()
        {
            CreateDirectory(Constant.Database);
            return Constant.Download;
        }

        /// <summary>
        /// 获取历史记录的文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetHistory()
        {
            CreateDirectory(Constant.Database);
            return Constant.History;
        }

        /// <summary>
        /// 获取设置的文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetSettings()
        {
            CreateDirectory(Constant.Config);
            return Constant.Settings;
        }

        /// <summary>
        /// 获取登录cookies的文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetLogin()
        {
            CreateDirectory(Constant.Config);
            return Constant.Login;
        }

        /// <summary>
        /// 获取弹幕的文件夹路径
        /// </summary>
        /// <returns></returns>
        public static string GetDanmaku()
        {
            return CreateDirectory(Constant.Danmaku);
        }

        /// <summary>
        /// 获取字幕的文件夹路径
        /// </summary>
        /// <returns></returns>
        public static string GetSubtitle()
        {
            return CreateDirectory(Constant.Subtitle);
        }

        /// <summary>
        /// 获取头图的文件夹路径
        /// </summary>
        /// <returns></returns>
        public static string GetToutu()
        {
            return CreateDirectory(Constant.Toutu);
        }

        /// <summary>
        /// 获取封面的文件夹路径
        /// </summary>
        /// <returns></returns>
        public static string GetCover()
        {
            return CreateDirectory(Constant.Cover);
        }

        /// <summary>
        /// 获取封面索引的文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetCoverIndex()
        {
            CreateDirectory(Constant.Cover);
            return Constant.CoverIndex;
        }

        /// <summary>
        /// 获取视频快照的文件夹路径
        /// </summary>
        /// <returns></returns>
        public static string GetSnapshot()
        {
            return CreateDirectory(Constant.Snapshot);
        }

        /// <summary>
        /// 获取视频快照索引的文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetSnapshotIndex()
        {
            CreateDirectory(Constant.Snapshot);
            return Constant.SnapshotIndex;
        }

        /// <summary>
        /// 获取用户头像的文件夹路径
        /// </summary>
        /// <returns></returns>
        public static string GetHeader()
        {
            return CreateDirectory(Constant.Header);
        }

        /// <summary>
        /// 获取用户头像索引的文件路径
        /// </summary>
        /// <returns></returns>
        public static string GetHeaderIndex()
        {
            CreateDirectory(Constant.Header);
            return Constant.HeaderIndex;
        }


        /// <summary>
        /// 若文件夹不存在，则创建文件夹
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private static string CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return directory;
        }
    }
}
