using Core.entity2.users;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Core.api.users
{
    /// <summary>
    /// 用户空间信息
    /// </summary>
    public class UserSpace
    {
        private static UserSpace instance;

        /// <summary>
        /// 获取UserSpace实例
        /// </summary>
        /// <returns></returns>
        public static UserSpace GetInstance()
        {
            if (instance == null)
            {
                instance = new UserSpace();
            }
            return instance;
        }

        /// <summary>
        /// 隐藏UserSpace()方法，必须使用单例模式
        /// </summary>
        private UserSpace() { }

        /// <summary>
        /// 查询空间设置
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public SpaceSettings GetSpaceSettings(long mid)
        {
            string url = $"https://space.bilibili.com/ajax/settings/getSettings?mid={mid}";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var settings = JsonConvert.DeserializeObject<SpaceSettingsOrigin>(response);
                if (settings == null || settings.Data == null || !settings.Status) { return null; }

                return settings.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetSpaceSettings()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 获取用户投稿视频的所有分区
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <returns></returns>
        public List<SpacePublicationListTypeVideoZone> GetPublicationType(long mid)
        {
            int pn = 1;
            int ps = 1;
            var publication = GetPublication(mid, pn, ps);
            return GetPublicationType(publication);
        }

        /// <summary>
        /// 获取用户投稿视频的所有分区
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <returns></returns>
        public List<SpacePublicationListTypeVideoZone> GetPublicationType(SpacePublicationList publication)
        {
            if (publication == null || publication.Tlist == null)
            {
                return null;
            }

            List<SpacePublicationListTypeVideoZone> result = new List<SpacePublicationListTypeVideoZone>();
            JObject typeList = JObject.Parse(publication.Tlist.ToString("N"));
            foreach (var item in typeList)
            {
                var value = JsonConvert.DeserializeObject<SpacePublicationListTypeVideoZone>(item.Value.ToString());
                result.Add(value);
            }
            return result;
        }

        /// <summary>
        /// 查询用户所有的投稿视频明细
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <param name="order">排序</param>
        /// <param name="tid">视频分区</param>
        /// <param name="keyword">搜索关键词</param>
        /// <returns></returns>
        public List<SpacePublicationListVideo> GetAllPublication(long mid, PublicationOrder order = PublicationOrder.PUBDATE, int tid = 0, string keyword = "")
        {
            List<SpacePublicationListVideo> result = new List<SpacePublicationListVideo>();

            int i = 0;
            while (true)
            {
                i++;
                int ps = 100;

                var data = GetPublication(mid, i, ps, tid, order, keyword);
                //if (data == null) { continue; }

                if (data == null || data.Vlist == null || data.Vlist.Count == 0)
                { break; }

                result.AddRange(data.Vlist);
            }
            return result;
        }

        /// <summary>
        /// 查询用户投稿视频明细
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <param name="pn">页码</param>
        /// <param name="ps">每页的视频数</param>
        /// <param name="order">排序</param>
        /// <param name="tid">视频分区</param>
        /// <param name="keyword">搜索关键词</param>
        /// <returns></returns>
        public SpacePublicationList GetPublication(long mid, int pn, int ps, int tid = 0, PublicationOrder order = PublicationOrder.PUBDATE, string keyword = "")
        {
            string url = $"https://api.bilibili.com/x/space/arc/search?mid={mid}&pn={pn}&ps={ps}&order={order.ToString("G").ToLower()}&tid={tid}&keyword={keyword}";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var spacePublication = JsonConvert.DeserializeObject<SpacePublicationOrigin>(response);
                if (spacePublication == null || spacePublication.Data == null) { return null; }
                return spacePublication.Data.List;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetPublication()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 查询用户频道列表
        /// </summary>
        /// <param name="mid">用户id</param>
        /// <returns></returns>
        public List<SpaceChannelList> GetChannelList(long mid)
        {
            string url = $"https://api.bilibili.com/x/space/channel/list?mid={mid}";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var spaceChannel = JsonConvert.DeserializeObject<SpaceChannelOrigin>(response);
                if (spaceChannel == null || spaceChannel.Data == null) { return null; }
                return spaceChannel.Data.List;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetChannelList()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 查询用户频道中的所有视频
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public List<SpaceChannelVideoArchive> GetAllChannelVideoList(long mid, long cid)
        {
            List<SpaceChannelVideoArchive> result = new List<SpaceChannelVideoArchive>();

            int i = 0;
            while (true)
            {
                i++;
                int ps = 100;

                var data = GetChannelVideoList(mid, cid, i, ps);
                if (data == null || data.Count == 0)
                { break; }

                result.AddRange(data);
            }
            return result;
        }

        /// <summary>
        /// 查询用户频道中的视频
        /// </summary>
        /// <param name="mid"></param>
        /// <param name="cid"></param>
        /// <param name="pn"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public List<SpaceChannelVideoArchive> GetChannelVideoList(long mid, long cid, int pn, int ps)
        {
            string url = $"https://api.bilibili.com/x/space/channel/video?mid={mid}&cid={cid}&pn={pn}&ps={ps}";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var spaceChannelVideo = JsonConvert.DeserializeObject<SpaceChannelVideoOrigin>(response);
                if (spaceChannelVideo == null || spaceChannelVideo.Data == null || spaceChannelVideo.Data.List == null)
                { return null; }
                return spaceChannelVideo.Data.List.Archives;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetChannelVideoList()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 查询用户创建的视频收藏夹
        /// </summary>
        /// <param name="mid">目标用户UID</param>
        /// <param name="pn">页码</param>
        /// <param name="ps">每页项数</param>
        /// <returns></returns>
        public List<SpaceFavoriteFolderList> GetCreatedFavoriteFolder(long mid, int pn, int ps)
        {
            string url = $"https://api.bilibili.com/x/v3/fav/folder/created/list?up_mid={mid}&pn={pn}&ps={ps}";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var favoriteFolder = JsonConvert.DeserializeObject<SpaceFavoriteFolderOrigin>(response);
                if (favoriteFolder == null || favoriteFolder.Data == null || favoriteFolder.Data.List == null)
                { return null; }
                return favoriteFolder.Data.List;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetCreatedFavoriteFolder()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 查询所有的用户创建的视频收藏夹
        /// </summary>
        /// <param name="mid">目标用户UID</param>
        /// <returns></returns>
        public List<SpaceFavoriteFolderList> GetAllCreatedFavoriteFolder(long mid)
        {
            List<SpaceFavoriteFolderList> result = new List<SpaceFavoriteFolderList>();

            int i = 0;
            while (true)
            {
                i++;
                int ps = 50;

                var data = GetCreatedFavoriteFolder(mid, i, ps);
                if (data == null || data.Count == 0)
                { break; }

                result.AddRange(data);
            }
            return result;
        }

        /// <summary>
        /// 查询用户收藏的视频收藏夹
        /// </summary>
        /// <param name="mid">目标用户UID</param>
        /// <param name="pn">页码</param>
        /// <param name="ps">每页项数</param>
        /// <returns></returns>
        public List<SpaceFavoriteFolderList> GetCollectedFavoriteFolder(long mid, int pn, int ps)
        {
            string url = $"https://api.bilibili.com/x/v3/fav/folder/collected/list?up_mid={mid}&pn={pn}&ps={ps}";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var favoriteFolder = JsonConvert.DeserializeObject<SpaceFavoriteFolderOrigin>(response);
                if (favoriteFolder == null || favoriteFolder.Data == null || favoriteFolder.Data.List == null)
                { return null; }
                return favoriteFolder.Data.List;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetCollectedFavoriteFolder()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 查询所有的用户收藏的视频收藏夹
        /// </summary>
        /// <param name="mid">目标用户UID</param>
        /// <returns></returns>
        public List<SpaceFavoriteFolderList> GetAllCollectedFavoriteFolder(long mid)
        {
            List<SpaceFavoriteFolderList> result = new List<SpaceFavoriteFolderList>();

            int i = 0;
            while (true)
            {
                i++;
                int ps = 50;

                var data = GetCollectedFavoriteFolder(mid, i, ps);
                if (data == null || data.Count == 0)
                { break; }

                result.AddRange(data);
            }
            return result;
        }

        /// <summary>
        /// 查询视频收藏夹的内容
        /// </summary>
        /// <param name="mediaId">收藏夹ID</param>
        /// <param name="pn">页码</param>
        /// <param name="ps">每页项数</param>
        /// <returns></returns>
        public List<SpaceFavoriteFolderMedia> GetFavoriteFolderResource(long mediaId, int pn, int ps)
        {
            string url = $"https://api.bilibili.com/x/v3/fav/resource/list?media_id={mediaId}&pn={pn}&ps={ps}";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var resource = JsonConvert.DeserializeObject<SpaceFavoriteFolderResourceOrigin>(response);
                if (resource == null || resource.Data == null || resource.Data.Medias == null)
                { return null; }
                return resource.Data.Medias;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetFavoriteFolderResource()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 查询视频收藏夹的所有内容
        /// </summary>
        /// <param name="mediaId">收藏夹ID</param>
        /// <returns></returns>
        public List<SpaceFavoriteFolderMedia> GetAllFavoriteFolderResource(long mediaId)
        {
            List<SpaceFavoriteFolderMedia> result = new List<SpaceFavoriteFolderMedia>();

            int i = 0;
            while (true)
            {
                i++;
                int ps = 20;

                var data = GetFavoriteFolderResource(mediaId, i, ps);
                if (data == null || data.Count == 0)
                { break; }

                result.AddRange(data);
            }
            return result;
        }

        /// <summary>
        /// 查询用户发布的课程列表
        /// </summary>
        /// <param name="mid">目标用户UID</param>
        /// <param name="pn">页码</param>
        /// <param name="ps">每页项数</param>
        /// <returns></returns>
        public List<SpaceCheese> GetCheese(long mid, int pn, int ps)
        {
            string url = $"https://api.bilibili.com/pugv/app/web/season/page?mid={mid}&pn={pn}&ps={ps}";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var cheese = JsonConvert.DeserializeObject<SpaceCheeseOrigin>(response);
                if (cheese == null || cheese.Data == null || cheese.Data.Items == null)
                { return null; }
                return cheese.Data.Items;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetCheese()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 查询用户发布的所有课程列表
        /// </summary>
        /// <param name="mid">目标用户UID</param>
        /// <returns></returns>
        public List<SpaceCheese> GetAllCheese(long mid)
        {
            List<SpaceCheese> result = new List<SpaceCheese>();

            int i = 0;
            while (true)
            {
                i++;
                int ps = 50;

                var data = GetCheese(mid, i, ps);
                if (data == null || data.Count == 0)
                { break; }

                result.AddRange(data);
            }
            return result;
        }

        /// <summary>
        /// 查询用户追番（追剧）明细
        /// </summary>
        /// <param name="mid">目标用户UID</param>
        /// <param name="type">查询类型</param>
        /// <param name="pn">页码</param>
        /// <param name="ps">每页项数</param>
        /// <returns></returns>
        public BangumiFollowData GetBangumiFollow(long mid, BangumiType type, int pn, int ps)
        {
            string url = $"https://api.bilibili.com/x/space/bangumi/follow/list?vmid={mid}&type={type.ToString("D")}&pn={pn}&ps={ps}";
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                var bangumiFollow = JsonConvert.DeserializeObject<BangumiFollowOrigin>(response);
                if (bangumiFollow == null || bangumiFollow.Data == null)
                { return null; }
                return bangumiFollow.Data;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetBangumiFollow()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 查询用户所有的追番（追剧）明细
        /// </summary>
        /// <param name="mid">目标用户UID</param>
        /// <param name="type">查询类型</param>
        /// <returns></returns>
        public List<BangumiFollow> GetAllBangumiFollow(long mid, BangumiType type)
        {
            List<BangumiFollow> result = new List<BangumiFollow>();

            int i = 0;
            while (true)
            {
                i++;
                int ps = 30;

                var data = GetBangumiFollow(mid, type, i, ps);
                if (data == null || data.List == null || data.List.Count == 0)
                { break; }

                result.AddRange(data.List);
            }
            return result;
        }

    }


    public enum PublicationOrder
    {
        PUBDATE = 1, // 最新发布，默认
        CLICK, // 最多播放
        STOW // 最多收藏
    }

    public enum BangumiType
    {
        ANIME = 1, // 番剧
        EPISODE = 2 // 剧集、电影
    }

}
