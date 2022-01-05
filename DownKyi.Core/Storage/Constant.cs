using System;

namespace DownKyi.Core.Storage
{
    /// <summary>
    /// 存储到本地时使用的一些常量
    /// </summary>
    internal static class Constant
    {
        // 根目录
        private static string Root { get; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/Downkyi";

        // Aria
        public static string Aria { get; } = $"{Root}/Aria";

        // 日志
        public static string Logs { get; } = $"{Root}/Logs";

        // 数据库
        public static string Database { get; } = $"{Root}/Storage";

        // 历史(搜索、下载) (加密)
        public static string Download { get; } = $"{Database}/Download.db";
        public static string History { get; } = $"{Database}/History.db";

        // 配置
        public static string Config { get; } = $"{Root}/Config";

        // 设置
        public static string Settings { get; } = $"{Config}/Settings.json";

        // 登录cookies
        public static string Login { get; } = $"{Config}/Login";

        // Bilibili
        private static string Bilibili { get; } = $"{Root}/Bilibili";

        // 弹幕
        public static string Danmaku { get; } = $"{Bilibili}/Danmakus";

        // 字幕
        public static string Subtitle { get; } = $"{Bilibili}/Subtitle";

        // 评论
        // TODO

        // 头图
        public static string Toutu { get; } = $"{Bilibili}/Toutu";

        // 封面
        public static string Cover { get; } = $"{Bilibili}/Cover";

        // 封面文件索引
        public static string CoverIndex { get; } = $"{Cover}/Index.db";

        // 视频快照
        public static string Snapshot { get; } = $"{Bilibili}/Snapshot";

        // 视频快照文件索引
        public static string SnapshotIndex { get; } = $"{Cover}/Index.db";

        // 用户头像
        public static string Header { get; } = $"{Bilibili}/Header";

        // 用户头像文件索引
        public static string HeaderIndex { get; } = $"{Header}/Index.db";

    }
}
