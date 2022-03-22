namespace DownKyi.Core.Settings
{
    public partial class SettingsManager
    {
        // 是否屏蔽顶部弹幕
        private readonly AllowStatus danmakuTopFilter = AllowStatus.NO;

        // 是否屏蔽底部弹幕
        private readonly AllowStatus danmakuBottomFilter = AllowStatus.NO;

        // 是否屏蔽滚动弹幕
        private readonly AllowStatus danmakuScrollFilter = AllowStatus.NO;

        // 是否自定义分辨率
        private readonly AllowStatus isCustomDanmakuResolution = AllowStatus.NO;

        // 分辨率-宽
        private readonly int danmakuScreenWidth = 1920;

        // 分辨率-高
        private readonly int danmakuScreenHeight = 1080;

        // 弹幕字体
        private readonly string danmakuFontName = "黑体";

        // 弹幕字体大小
        private readonly int danmakuFontSize = 50;

        // 弹幕限制行数
        private readonly int danmakuLineCount = 0;

        // 弹幕布局算法
        private readonly DanmakuLayoutAlgorithm danmakuLayoutAlgorithm = DanmakuLayoutAlgorithm.SYNC;


        /// <summary>
        /// 获取是否屏蔽顶部弹幕
        /// </summary>
        /// <returns></returns>
        public AllowStatus GetDanmakuTopFilter()
        {
            appSettings = GetSettings();
            if (appSettings.Danmaku.DanmakuTopFilter == AllowStatus.NONE)
            {
                // 第一次获取，先设置默认值
                SetDanmakuTopFilter(danmakuTopFilter);
                return danmakuTopFilter;
            }
            return appSettings.Danmaku.DanmakuTopFilter;
        }

        /// <summary>
        /// 设置是否屏蔽顶部弹幕
        /// </summary>
        /// <param name="danmakuFilter"></param>
        /// <returns></returns>
        public bool SetDanmakuTopFilter(AllowStatus danmakuFilter)
        {
            appSettings.Danmaku.DanmakuTopFilter = danmakuFilter;
            return SetSettings();
        }

        /// <summary>
        /// 获取是否屏蔽底部弹幕
        /// </summary>
        /// <returns></returns>
        public AllowStatus GetDanmakuBottomFilter()
        {
            appSettings = GetSettings();
            if (appSettings.Danmaku.DanmakuBottomFilter == AllowStatus.NONE)
            {
                // 第一次获取，先设置默认值
                SetDanmakuBottomFilter(danmakuBottomFilter);
                return danmakuBottomFilter;
            }
            return appSettings.Danmaku.DanmakuBottomFilter;
        }

        /// <summary>
        /// 设置是否屏蔽底部弹幕
        /// </summary>
        /// <param name="danmakuFilter"></param>
        /// <returns></returns>
        public bool SetDanmakuBottomFilter(AllowStatus danmakuFilter)
        {
            appSettings.Danmaku.DanmakuBottomFilter = danmakuFilter;
            return SetSettings();
        }

        /// <summary>
        /// 获取是否屏蔽滚动弹幕
        /// </summary>
        /// <returns></returns>
        public AllowStatus GetDanmakuScrollFilter()
        {
            appSettings = GetSettings();
            if (appSettings.Danmaku.DanmakuScrollFilter == AllowStatus.NONE)
            {
                // 第一次获取，先设置默认值
                SetDanmakuScrollFilter(danmakuScrollFilter);
                return danmakuScrollFilter;
            }
            return appSettings.Danmaku.DanmakuScrollFilter;
        }

        /// <summary>
        /// 设置是否屏蔽滚动弹幕
        /// </summary>
        /// <param name="danmakuFilter"></param>
        /// <returns></returns>
        public bool SetDanmakuScrollFilter(AllowStatus danmakuFilter)
        {
            appSettings.Danmaku.DanmakuScrollFilter = danmakuFilter;
            return SetSettings();
        }

        /// <summary>
        /// 获取是否自定义分辨率
        /// </summary>
        /// <returns></returns>
        public AllowStatus IsCustomDanmakuResolution()
        {
            appSettings = GetSettings();
            if (appSettings.Danmaku.IsCustomDanmakuResolution == AllowStatus.NONE)
            {
                // 第一次获取，先设置默认值
                IsCustomDanmakuResolution(isCustomDanmakuResolution);
                return isCustomDanmakuResolution;
            }
            return appSettings.Danmaku.IsCustomDanmakuResolution;
        }

        /// <summary>
        /// 设置是否自定义分辨率
        /// </summary>
        /// <param name="isCustomResolution"></param>
        /// <returns></returns>
        public bool IsCustomDanmakuResolution(AllowStatus isCustomResolution)
        {
            appSettings.Danmaku.IsCustomDanmakuResolution = isCustomResolution;
            return SetSettings();
        }

        /// <summary>
        /// 获取分辨率-宽
        /// </summary>
        /// <returns></returns>
        public int GetDanmakuScreenWidth()
        {
            appSettings = GetSettings();
            if (appSettings.Danmaku.DanmakuScreenWidth == -1)
            {
                // 第一次获取，先设置默认值
                SetDanmakuScreenWidth(danmakuScreenWidth);
                return danmakuScreenWidth;
            }
            return appSettings.Danmaku.DanmakuScreenWidth;
        }

        /// <summary>
        /// 设置分辨率-宽
        /// </summary>
        /// <param name="screenWidth"></param>
        /// <returns></returns>
        public bool SetDanmakuScreenWidth(int screenWidth)
        {
            appSettings.Danmaku.DanmakuScreenWidth = screenWidth;
            return SetSettings();
        }

        /// <summary>
        /// 获取分辨率-高
        /// </summary>
        /// <returns></returns>
        public int GetDanmakuScreenHeight()
        {
            appSettings = GetSettings();
            if (appSettings.Danmaku.DanmakuScreenHeight == -1)
            {
                // 第一次获取，先设置默认值
                SetDanmakuScreenHeight(danmakuScreenHeight);
                return danmakuScreenHeight;
            }
            return appSettings.Danmaku.DanmakuScreenHeight;
        }

        /// <summary>
        /// 设置分辨率-高
        /// </summary>
        /// <param name="screenHeight"></param>
        /// <returns></returns>
        public bool SetDanmakuScreenHeight(int screenHeight)
        {
            appSettings.Danmaku.DanmakuScreenHeight = screenHeight;
            return SetSettings();
        }

        /// <summary>
        /// 获取弹幕字体
        /// </summary>
        /// <returns></returns>
        public string GetDanmakuFontName()
        {
            appSettings = GetSettings();
            if (appSettings.Danmaku.DanmakuFontName == null)
            {
                // 第一次获取，先设置默认值
                SetDanmakuFontName(danmakuFontName);
                return danmakuFontName;
            }
            return appSettings.Danmaku.DanmakuFontName;
        }

        /// <summary>
        /// 设置弹幕字体
        /// </summary>
        /// <param name="danmakuFontName"></param>
        /// <returns></returns>
        public bool SetDanmakuFontName(string danmakuFontName)
        {
            appSettings.Danmaku.DanmakuFontName = danmakuFontName;
            return SetSettings();
        }

        /// <summary>
        /// 获取弹幕字体大小
        /// </summary>
        /// <returns></returns>
        public int GetDanmakuFontSize()
        {
            appSettings = GetSettings();
            if (appSettings.Danmaku.DanmakuFontSize == -1)
            {
                // 第一次获取，先设置默认值
                SetDanmakuFontSize(danmakuFontSize);
                return danmakuFontSize;
            }
            return appSettings.Danmaku.DanmakuFontSize;
        }

        /// <summary>
        /// 设置弹幕字体大小
        /// </summary>
        /// <param name="danmakuFontSize"></param>
        /// <returns></returns>
        public bool SetDanmakuFontSize(int danmakuFontSize)
        {
            appSettings.Danmaku.DanmakuFontSize = danmakuFontSize;
            return SetSettings();
        }

        /// <summary>
        /// 获取弹幕限制行数
        /// </summary>
        /// <returns></returns>
        public int GetDanmakuLineCount()
        {
            appSettings = GetSettings();
            if (appSettings.Danmaku.DanmakuLineCount == -1)
            {
                // 第一次获取，先设置默认值
                SetDanmakuLineCount(danmakuLineCount);
                return danmakuLineCount;
            }
            return appSettings.Danmaku.DanmakuLineCount;
        }

        /// <summary>
        /// 设置弹幕限制行数
        /// </summary>
        /// <param name="danmakuLineCount"></param>
        /// <returns></returns>
        public bool SetDanmakuLineCount(int danmakuLineCount)
        {
            appSettings.Danmaku.DanmakuLineCount = danmakuLineCount;
            return SetSettings();
        }

        /// <summary>
        /// 获取弹幕布局算法
        /// </summary>
        /// <returns></returns>
        public DanmakuLayoutAlgorithm GetDanmakuLayoutAlgorithm()
        {
            appSettings = GetSettings();
            if (appSettings.Danmaku.DanmakuLayoutAlgorithm == DanmakuLayoutAlgorithm.NONE)
            {
                // 第一次获取，先设置默认值
                SetDanmakuLayoutAlgorithm(danmakuLayoutAlgorithm);
                return danmakuLayoutAlgorithm;
            }
            return appSettings.Danmaku.DanmakuLayoutAlgorithm;
        }

        /// <summary>
        /// 设置弹幕布局算法
        /// </summary>
        /// <param name="danmakuLayoutAlgorithm"></param>
        /// <returns></returns>
        public bool SetDanmakuLayoutAlgorithm(DanmakuLayoutAlgorithm danmakuLayoutAlgorithm)
        {
            appSettings.Danmaku.DanmakuLayoutAlgorithm = danmakuLayoutAlgorithm;
            return SetSettings();
        }

    }
}
