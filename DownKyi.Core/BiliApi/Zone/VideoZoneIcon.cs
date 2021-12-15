namespace DownKyi.Core.BiliApi.Zone
{
    /// <summary>
    /// 视频分区图标
    /// </summary>
    public class VideoZoneIcon
    {
        private static VideoZoneIcon instance;

        /// <summary>
        /// 获取VideoZoneIcon实例
        /// </summary>
        /// <returns></returns>
        public static VideoZoneIcon Instance()
        {
            if (instance == null)
            {
                instance = new VideoZoneIcon();
            }
            return instance;
        }

        /// <summary>
        /// 隐藏VideoZoneIcon()方法，必须使用单例模式
        /// </summary>
        private VideoZoneIcon() { }

        /// <summary>
        /// 根据tid，获取视频分区图标
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public string GetZoneImageKey(int tid)
        {
            switch (tid)
            {
                case 1:
                    return "Zone.dougaDrawingImage";
                case 13:
                    return "Zone.animeDrawingImage";
                case 167:
                    return "Zone.guochuangDrawingImage";
                case 3:
                    return "Zone.musicDrawingImage";
                case 129:
                    return "Zone.danceDrawingImage";
                case 4:
                    return "Zone.gameDrawingImage";
                case 36:
                    return "Zone.techDrawingImage";
                case 188:
                    return "Zone.digitalDrawingImage";
                case 234:
                    return "Zone.sportsDrawingImage";
                case 223:
                    return "Zone.carDrawingImage";
                case 160:
                    return "Zone.lifeDrawingImage";
                case 211:
                    return "Zone.foodDrawingImage";
                case 217:
                    return "Zone.animalDrawingImage";
                case 119:
                    return "Zone.kichikuDrawingImage";
                case 155:
                    return "Zone.fashionDrawingImage";
                case 202:
                    return "Zone.informationDrawingImage";
                case 5:
                    return "Zone.entDrawingImage";
                case 181:
                    return "Zone.cinephileDrawingImage";
                case 177:
                    return "Zone.documentaryDrawingImage";
                case 23:
                    return "Zone.movieDrawingImage";
                case 11:
                    return "Zone.teleplayDrawingImage";
                default:
                    return "videoUpDrawingImage";
            }
        }

    }
}
