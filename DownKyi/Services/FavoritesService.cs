using DownKyi.Core.BiliApi.Favorites;
using DownKyi.Core.Storage;
using DownKyi.Core.Utils;
using DownKyi.Models;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace DownKyi.Services
{
    public class FavoritesService : IFavoritesService
    {
        /// <summary>
        /// 获取收藏夹元数据
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public Favorites GetFavorites(long mediaId)
        {
            var favoritesMetaInfo = FavoritesInfo.GetFavoritesInfo(mediaId);
            if (favoritesMetaInfo == null) { return null; }

            // 查询、保存封面
            StorageCover storageCover = new StorageCover();
            string coverUrl = favoritesMetaInfo.Cover;
            string cover = storageCover.GetCover(favoritesMetaInfo.Id, "Favorites", favoritesMetaInfo.Mid, coverUrl);

            // 获取用户头像
            string upName;
            string header;
            if (favoritesMetaInfo.Upper != null)
            {
                upName = favoritesMetaInfo.Upper.Name;
                StorageHeader storageHeader = new StorageHeader();
                header = storageHeader.GetHeader(favoritesMetaInfo.Upper.Mid, favoritesMetaInfo.Upper.Name, favoritesMetaInfo.Upper.Face);
            }
            else
            {
                upName = "";
                header = null;
            }

            // 为Favorites赋值
            Favorites favorites = new Favorites();
            App.PropertyChangeAsync(new Action(() =>
            {
                favorites.CoverUrl = coverUrl;

                favorites.Cover = cover == null ? null : new BitmapImage(new Uri(cover));
                favorites.Title = favoritesMetaInfo.Title;

                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
                DateTime dateTime = startTime.AddSeconds(favoritesMetaInfo.Ctime);
                favorites.CreateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

                favorites.PlayNumber = Format.FormatNumber(favoritesMetaInfo.CntInfo.Play);
                favorites.LikeNumber = Format.FormatNumber(favoritesMetaInfo.CntInfo.ThumbUp);
                favorites.FavoriteNumber = Format.FormatNumber(favoritesMetaInfo.CntInfo.Collect);
                favorites.ShareNumber = Format.FormatNumber(favoritesMetaInfo.CntInfo.Share);
                favorites.Description = favoritesMetaInfo.Intro;
                favorites.MediaCount = favoritesMetaInfo.MediaCount;

                favorites.UpName = upName;
                if (header != null)
                {
                    StorageHeader storageHeader = new StorageHeader();
                    favorites.UpHeader = storageHeader.GetHeaderThumbnail(header, 48, 48);

                    favorites.UpperMid = favoritesMetaInfo.Upper.Mid;
                }
                else
                {
                    favorites.UpHeader = null;
                }
            }));

            return favorites;
        }

        /// <summary>
        /// 获取收藏夹内容明细列表
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public List<FavoritesMedia> GetFavoritesMediaList(long mediaId)
        {
            throw new NotImplementedException();
        }
    }
}
