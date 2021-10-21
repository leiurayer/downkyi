using DownKyi.Core.BiliApi.Favorites.Models;
using DownKyi.Core.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownKyi.Core.BiliApi.Favorites
{
    public static class FavoritesInfo
    {

        /// <summary>
        /// 获取收藏夹元数据
        /// </summary>
        /// <param name="mediaId"></param>
        public static FavoritesMetaInfo GetFavoritesInfo(long mediaId)
        {
            string url = $"https://api.bilibili.com/x/v3/fav/folder/info?media_id={mediaId}";
            string referer = "https://www.bilibili.com";
            string response = WebClient.RequestWeb(url, referer);

            try
            {
                var info = JsonConvert.DeserializeObject<FavoritesMetaInfoOrigin>(response);
                if (info != null) { return info.Data; }
                else { return null; }
            }
            catch (Exception e)
            {
                Utils.Debug.Console.PrintLine("GetFavoritesInfo()发生异常: {0}", e);
                LogManager.Error("FavoritesInfo", e);
                return null;
            }
        }

    }
}
