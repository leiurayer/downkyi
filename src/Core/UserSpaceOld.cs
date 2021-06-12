using Core.entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Core
{
    public static class UserSpaceOld
    {

        /// <summary>
        /// 获取我创建的收藏夹
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public static FavFolderData GetCreatedFavFolder(long mid)
        {
            string url = $"https://api.bilibili.com/x/v3/fav/folder/created/list?up_mid={mid}&ps=50";
            return GetAllFavFolder(url);
        }

        /// <summary>
        /// 获取我收藏的收藏夹
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public static FavFolderData GetCollectedFavFolder(long mid)
        {
            string url = $"https://api.bilibili.com/x/v3/fav/folder/collected/list?up_mid={mid}&ps=50";
            return GetAllFavFolder(url);
        }

        private static FavFolderData GetAllFavFolder(string baseUrl)
        {
            FavFolderData userFavoriteData = new FavFolderData
            {
                count = 0,
                list = new List<FavFolderDataList>()
            };
            int i = 0;
            while (true)
            {
                i++;
                string url = baseUrl + $"&pn={i}";

                var data = GetFavFolder(url);
                if (data == null)
                { break; }
                if (data.count == 0 || data.list == null)
                { break; }

                userFavoriteData.list.AddRange(data.list);
            }
            userFavoriteData.count = userFavoriteData.list.Count;
            return userFavoriteData;
        }

        private static FavFolderData GetFavFolder(string url)
        {
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                FavFolder favFolder = JsonConvert.DeserializeObject<FavFolder>(response);
                if (favFolder == null || favFolder.data == null) { return null; }

                return favFolder.data;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetFavFolder()发生异常: {0}", e);
                return null;
            }
        }

        /// <summary>
        /// 获得某个收藏夹的内容
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public static List<FavResourceDataMedia> GetAllFavResource(long mediaId)
        {
            string baseUrl = $"https://api.bilibili.com/x/v3/fav/resource/list?media_id={mediaId}&ps=20";
            List<FavResourceDataMedia> medias = new List<FavResourceDataMedia>();

            int i = 0;
            while (true)
            {
                i++;
                string url = baseUrl + $"&pn={i}";

                var data = GetFavResource(url);
                if (data == null || data.Count == 0)
                { break; }

                medias.AddRange(data);
            }
            return medias;
        }

        private static List<FavResourceDataMedia> GetFavResource(string url)
        {
            string referer = "https://www.bilibili.com";
            string response = Utils.RequestWeb(url, referer);

            try
            {
                FavResource favResource = JsonConvert.DeserializeObject<FavResource>(response);
                if (favResource == null || favResource.data == null) { return null; }
                return favResource.data.medias;
            }
            catch (Exception e)
            {
                Console.WriteLine("GetFavResource()发生异常: {0}", e);
                return null;
            }
        }


        /// <summary>
        /// 获取订阅番剧的数量
        /// 废弃
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        //public static int GetBangumiFollowList(long mid)
        //{
        //    string url = $"https://space.bilibili.com/ajax/Bangumi/getList?mid={mid}";
        //    string referer = "https://www.bilibili.com";
        //    string response = Utils.RequestWeb(url, referer);

        //    try
        //    {
        //        BangumiList bangumiList = JsonConvert.DeserializeObject<BangumiList>(response);
        //        if (bangumiList == null || bangumiList.data == null) { return -1; }

        //        return bangumiList.data.count;
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("发生异常: {0}", e);
        //        return 0;
        //    }
        //}

    }
}
