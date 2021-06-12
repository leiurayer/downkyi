namespace Core.settings
{
    public partial class Settings
    {
        // 是否屏蔽顶部弹幕
        private readonly ALLOW_STATUS danmakuTopFilter = ALLOW_STATUS.NO;

        // 是否屏蔽底部弹幕
        private readonly ALLOW_STATUS danmakuBottomFilter = ALLOW_STATUS.NO;

        // 是否屏蔽滚动弹幕
        private readonly ALLOW_STATUS danmakuScrollFilter = ALLOW_STATUS.NO;

        // 是否自定义分辨率
        private readonly ALLOW_STATUS isCustomDanmakuResolution = ALLOW_STATUS.NO;

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
        public ALLOW_STATUS GetDanmakuTopFilter()
        {
            if (settingsEntity.DanmakuTopFilter == 0)
            {
                // 第一次获取，先设置默认值
                SetDanmakuTopFilter(danmakuTopFilter);
                return danmakuTopFilter;
            }
            return settingsEntity.DanmakuTopFilter;
        }

        /// <summary>
        /// 设置是否屏蔽顶部弹幕
        /// </summary>
        /// <param name="danmakuFilter"></param>
        /// <returns></returns>
        public bool SetDanmakuTopFilter(ALLOW_STATUS danmakuFilter)
        {
            settingsEntity.DanmakuTopFilter = danmakuFilter;
            return SetEntity();
        }

        /// <summary>
        /// 获取是否屏蔽底部弹幕
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS GetDanmakuBottomFilter()
        {
            if (settingsEntity.DanmakuBottomFilter == 0)
            {
                // 第一次获取，先设置默认值
                SetDanmakuBottomFilter(danmakuBottomFilter);
                return danmakuBottomFilter;
            }
            return settingsEntity.DanmakuBottomFilter;
        }

        /// <summary>
        /// 设置是否屏蔽底部弹幕
        /// </summary>
        /// <param name="danmakuFilter"></param>
        /// <returns></returns>
        public bool SetDanmakuBottomFilter(ALLOW_STATUS danmakuFilter)
        {
            settingsEntity.DanmakuBottomFilter = danmakuFilter;
            return SetEntity();
        }

        /// <summary>
        /// 获取是否屏蔽滚动弹幕
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS GetDanmakuScrollFilter()
        {
            if (settingsEntity.DanmakuScrollFilter == 0)
            {
                // 第一次获取，先设置默认值
                SetDanmakuScrollFilter(danmakuScrollFilter);
                return danmakuScrollFilter;
            }
            return settingsEntity.DanmakuScrollFilter;
        }

        /// <summary>
        /// 设置是否屏蔽滚动弹幕
        /// </summary>
        /// <param name="danmakuFilter"></param>
        /// <returns></returns>
        public bool SetDanmakuScrollFilter(ALLOW_STATUS danmakuFilter)
        {
            settingsEntity.DanmakuScrollFilter = danmakuFilter;
            return SetEntity();
        }

        /// <summary>
        /// 获取是否自定义分辨率
        /// </summary>
        /// <returns></returns>
        public ALLOW_STATUS IsCustomDanmakuResolution()
        {
            if (settingsEntity.IsCustomDanmakuResolution == 0)
            {
                // 第一次获取，先设置默认值
                IsCustomDanmakuResolution(isCustomDanmakuResolution);
                return isCustomDanmakuResolution;
            }
            return settingsEntity.IsCustomDanmakuResolution;
        }

        /// <summary>
        /// 设置是否自定义分辨率
        /// </summary>
        /// <param name="isCustomResolution"></param>
        /// <returns></returns>
        public bool IsCustomDanmakuResolution(ALLOW_STATUS isCustomResolution)
        {
            settingsEntity.IsCustomDanmakuResolution = isCustomResolution;
            return SetEntity();
        }

        /// <summary>
        /// 获取分辨率-宽
        /// </summary>
        /// <returns></returns>
        public int GetDanmakuScreenWidth()
        {
            if (settingsEntity.DanmakuScreenWidth == 0)
            {
                // 第一次获取，先设置默认值
                SetDanmakuScreenWidth(danmakuScreenWidth);
                return danmakuScreenWidth;
            }
            return settingsEntity.DanmakuScreenWidth;
        }

        /// <summary>
        /// 设置分辨率-宽
        /// </summary>
        /// <param name="screenWidth"></param>
        /// <returns></returns>
        public bool SetDanmakuScreenWidth(int screenWidth)
        {
            settingsEntity.DanmakuScreenWidth = screenWidth;
            return SetEntity();
        }

        /// <summary>
        /// 获取分辨率-高
        /// </summary>
        /// <returns></returns>
        public int GetDanmakuScreenHeight()
        {
            if (settingsEntity.DanmakuScreenHeight == 0)
            {
                // 第一次获取，先设置默认值
                SetDanmakuScreenHeight(danmakuScreenHeight);
                return danmakuScreenHeight;
            }
            return settingsEntity.DanmakuScreenHeight;
        }

        /// <summary>
        /// 设置分辨率-高
        /// </summary>
        /// <param name="screenHeight"></param>
        /// <returns></returns>
        public bool SetDanmakuScreenHeight(int screenHeight)
        {
            settingsEntity.DanmakuScreenHeight = screenHeight;
            return SetEntity();
        }

        /// <summary>
        /// 获取弹幕字体
        /// </summary>
        /// <returns></returns>
        public string GetDanmakuFontName()
        {
            if (settingsEntity.DanmakuFontName == null)
            {
                // 第一次获取，先设置默认值
                SetDanmakuFontName(danmakuFontName);
                return danmakuFontName;
            }
            return settingsEntity.DanmakuFontName;
        }

        /// <summary>
        /// 设置弹幕字体
        /// </summary>
        /// <param name="danmakuFontName"></param>
        /// <returns></returns>
        public bool SetDanmakuFontName(string danmakuFontName)
        {
            settingsEntity.DanmakuFontName = danmakuFontName;
            return SetEntity();
        }

        /// <summary>
        /// 获取弹幕字体大小
        /// </summary>
        /// <returns></returns>
        public int GetDanmakuFontSize()
        {
            if (settingsEntity.DanmakuFontSize == 0)
            {
                // 第一次获取，先设置默认值
                SetDanmakuFontSize(danmakuFontSize);
                return danmakuFontSize;
            }
            return settingsEntity.DanmakuFontSize;
        }

        /// <summary>
        /// 设置弹幕字体大小
        /// </summary>
        /// <param name="danmakuFontSize"></param>
        /// <returns></returns>
        public bool SetDanmakuFontSize(int danmakuFontSize)
        {
            settingsEntity.DanmakuFontSize = danmakuFontSize;
            return SetEntity();
        }

        /// <summary>
        /// 获取弹幕限制行数
        /// </summary>
        /// <returns></returns>
        public int GetDanmakuLineCount()
        {
            if (settingsEntity.DanmakuLineCount == 0)
            {
                // 第一次获取，先设置默认值
                SetDanmakuLineCount(danmakuLineCount);
                return danmakuLineCount;
            }
            return settingsEntity.DanmakuLineCount;
        }

        /// <summary>
        /// 设置弹幕限制行数
        /// </summary>
        /// <param name="danmakuLineCount"></param>
        /// <returns></returns>
        public bool SetDanmakuLineCount(int danmakuLineCount)
        {
            settingsEntity.DanmakuLineCount = danmakuLineCount;
            return SetEntity();
        }

        /// <summary>
        /// 获取弹幕布局算法
        /// </summary>
        /// <returns></returns>
        public DanmakuLayoutAlgorithm GetDanmakuLayoutAlgorithm()
        {
            if (settingsEntity.DanmakuLayoutAlgorithm == 0)
            {
                // 第一次获取，先设置默认值
                SetDanmakuLayoutAlgorithm(danmakuLayoutAlgorithm);
                return danmakuLayoutAlgorithm;
            }
            return settingsEntity.DanmakuLayoutAlgorithm;
        }

        /// <summary>
        /// 设置弹幕布局算法
        /// </summary>
        /// <param name="danmakuLayoutAlgorithm"></param>
        /// <returns></returns>
        public bool SetDanmakuLayoutAlgorithm(DanmakuLayoutAlgorithm danmakuLayoutAlgorithm)
        {
            settingsEntity.DanmakuLayoutAlgorithm = danmakuLayoutAlgorithm;
            return SetEntity();
        }
        

    }
}
