using DownKyi.Core.BiliApi.Favorites;
using DownKyi.Core.Storage;
using DownKyi.Core.Utils;
using DownKyi.ViewModels.PageViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
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
            BitmapImage cover = storageCover.GetCoverThumbnail(favoritesMetaInfo.Id, "Favorites", favoritesMetaInfo.Mid, coverUrl, 300, 188);

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

                favorites.Cover = cover;
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

        ///// <summary>
        ///// 获取收藏夹所有内容明细列表
        ///// </summary>
        ///// <param name="mediaId"></param>
        ///// <param name="result"></param>
        ///// <param name="eventAggregator"></param>
        //public void GetFavoritesMediaList(long mediaId, ObservableCollection<FavoritesMedia> result, IEventAggregator eventAggregator, CancellationToken cancellationToken)
        //{
        //    List<Core.BiliApi.Favorites.Models.FavoritesMedia> medias = FavoritesResource.GetAllFavoritesMedia(mediaId);
        //    if (medias.Count == 0) { return; }

        //    GetFavoritesMediaList(medias, result, eventAggregator, cancellationToken);
        //}

        ///// <summary>
        ///// 获取收藏夹指定页的内容明细列表
        ///// </summary>
        ///// <param name="mediaId"></param>
        ///// <param name="pn"></param>
        ///// <param name="ps"></param>
        ///// <param name="result"></param>
        ///// <param name="eventAggregator"></param>
        //public void GetFavoritesMediaList(long mediaId, int pn, int ps, ObservableCollection<FavoritesMedia> result, IEventAggregator eventAggregator, CancellationToken cancellationToken)
        //{
        //    List<Core.BiliApi.Favorites.Models.FavoritesMedia> medias = FavoritesResource.GetFavoritesMedia(mediaId, pn, ps);
        //    if (medias.Count == 0) { return; }

        //    GetFavoritesMediaList(medias, result, eventAggregator, cancellationToken);
        //}

        /// <summary>
        /// 获取收藏夹内容明细列表
        /// </summary>
        /// <param name="medias"></param>
        /// <param name="result"></param>
        /// <param name="eventAggregator"></param>
        public void GetFavoritesMediaList(List<Core.BiliApi.Favorites.Models.FavoritesMedia> medias, ObservableCollection<FavoritesMedia> result, IEventAggregator eventAggregator, CancellationToken cancellationToken)
        {
            int order = 0;
            foreach (var media in medias)
            {
                if (media.Title == "已失效视频") { continue; }

                order++;

                // 查询、保存封面
                StorageCover storageCover = new StorageCover();
                string coverUrl = media.Cover;
                BitmapImage cover = storageCover.GetCoverThumbnail(media.Id, media.Bvid, -1, coverUrl, 200, 125);

                // 当地时区
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

                // 创建时间
                DateTime dateCTime = startTime.AddSeconds(media.Ctime);
                string ctime = dateCTime.ToString("yyyy-MM-dd");

                // 收藏时间
                DateTime dateFavTime = startTime.AddSeconds(media.FavTime);
                string favTime = dateFavTime.ToString("yyyy-MM-dd");

                App.PropertyChangeAsync(new Action(() =>
                {
                    FavoritesMedia newMedia = new FavoritesMedia(eventAggregator)
                    {
                        Avid = media.Id,
                        Bvid = media.Bvid,
                        Order = order,
                        Cover = cover,
                        Title = media.Title,
                        PlayNumber = media.CntInfo != null ? Format.FormatNumber(media.CntInfo.Play) : "0",
                        DanmakuNumber = media.CntInfo != null ? Format.FormatNumber(media.CntInfo.Danmaku) : "0",
                        FavoriteNumber = media.CntInfo != null ? Format.FormatNumber(media.CntInfo.Collect) : "0",
                        Duration = Format.FormatDuration2(media.Duration),
                        UpName = media.Upper != null ? media.Upper.Name : string.Empty,
                        UpMid = media.Upper != null ? media.Upper.Mid : -1,
                        CreateTime = ctime,
                        FavTime = favTime
                    };

                    if (!result.ToList().Exists(t => t.Avid == newMedia.Avid))
                    {
                        result.Add(newMedia);
                        Thread.Sleep(10);
                    }
                }));

                // 判断是否该结束线程，若为true，跳出循环
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 更新我创建的收藏夹列表
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="tabHeaders"></param>
        public void GetCreatedFavorites(long mid, ObservableCollection<TabHeader> tabHeaders, CancellationToken cancellationToken)
        {
            var favorites = FavoritesInfo.GetAllCreatedFavorites(mid);
            if (favorites.Count == 0) { return; }

            foreach (var item in favorites)
            {
                //cancellationToken.ThrowIfCancellationRequested();

                // 判断是否该结束线程，若为true，跳出循环
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                App.PropertyChangeAsync(new Action(() =>
                {
                    tabHeaders.Add(new TabHeader { Id = (int)item.Id, Title = item.Title, SubTitle = item.MediaCount.ToString() });
                }));
            }
        }

        /// <summary>
        /// 更新我收藏的收藏夹列表
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="tabHeaders"></param>
        public void GetCollectedFavorites(long mid, ObservableCollection<TabHeader> tabHeaders, CancellationToken cancellationToken)
        {
            var favorites = FavoritesInfo.GetAllCollectedFavorites(mid);
            if (favorites.Count == 0) { return; }

            foreach (var item in favorites)
            {
                // 判断是否该结束线程，若为true，跳出循环
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                App.PropertyChangeAsync(new Action(() =>
                {
                    tabHeaders.Add(new TabHeader { Id = (int)item.Id, Title = item.Title, SubTitle = item.MediaCount.ToString() });
                }));
            }
        }

    }
}
