using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.Cheese;
using DownKyi.Core.BiliApi.Cheese.Models;
using DownKyi.Core.BiliApi.VideoStream;
using DownKyi.Core.BiliApi.VideoStream.Models;
using DownKyi.Core.Settings;
using DownKyi.Core.Storage;
using DownKyi.Core.Utils;
using DownKyi.Utils;
using DownKyi.ViewModels.PageViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace DownKyi.Services
{
    public class CheeseInfoService : IInfoService
    {
        private readonly CheeseView cheeseView;

        public CheeseInfoService(string input)
        {
            if (input == null)
            {
                return;
            }

            if (ParseEntrance.IsCheeseSeasonUrl(input))
            {
                long seasonId = ParseEntrance.GetCheeseSeasonId(input);
                cheeseView = CheeseInfo.CheeseViewInfo(seasonId);
            }

            if (ParseEntrance.IsCheeseEpisodeUrl(input))
            {
                long episodeId = ParseEntrance.GetCheeseEpisodeId(input);
                cheeseView = CheeseInfo.CheeseViewInfo(-1, episodeId);
            }
        }

        /// <summary>
        /// 获取视频剧集
        /// </summary>
        /// <returns></returns>
        public List<VideoPage> GetVideoPages()
        {
            List<VideoPage> pages = new List<VideoPage>();
            if (cheeseView == null) { return pages; }
            if (cheeseView.Episodes == null) { return pages; }
            if (cheeseView.Episodes.Count == 0) { return pages; }

            int order = 0;
            foreach (CheeseEpisode episode in cheeseView.Episodes)
            {
                order++;
                string name = episode.Title;

                string duration = Format.FormatDuration(episode.Duration - 1);

                VideoPage page = new VideoPage
                {
                    Avid = episode.Aid,
                    Bvid = null,
                    Cid = episode.Cid,
                    EpisodeId = episode.Id,
                    FirstFrame = episode.Cover,
                    Order = order,
                    Name = name,
                    Duration = "N/A"
                };

                // UP主信息
                if (cheeseView.UpInfo != null)
                {
                    page.Owner = new Core.BiliApi.Models.VideoOwner
                    {
                        Name = cheeseView.UpInfo.Name,
                        Face = cheeseView.UpInfo.Avatar,
                        Mid = cheeseView.UpInfo.Mid,
                    };
                }
                else
                {
                    page.Owner = new Core.BiliApi.Models.VideoOwner
                    {
                        Name = "",
                        Face = "",
                        Mid = -1,
                    };
                }

                // 文件命名中的时间格式
                string timeFormat = SettingsManager.GetInstance().GetFileNamePartTimeFormat();
                // 视频发布时间
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
                DateTime dateTime = startTime.AddSeconds(episode.ReleaseDate);
                page.PublishTime = dateTime.ToString(timeFormat);

                pages.Add(page);
            }

            return pages;
        }

        /// <summary>
        /// 获取视频章节与剧集
        /// </summary>
        /// <returns></returns>
        public List<VideoSection> GetVideoSections()
        {
            return null;
        }

        /// <summary>
        /// 获取视频流的信息，从VideoPage返回
        /// </summary>
        /// <param name="page"></param>
        public void GetVideoStream(VideoPage page)
        {
            PlayUrl playUrl = VideoStream.GetCheesePlayUrl(page.Avid, page.Bvid, page.Cid, page.EpisodeId);
            Utils.VideoPageInfo(playUrl, page);
        }

        /// <summary>
        /// 获取视频信息
        /// </summary>
        /// <returns></returns>
        public VideoInfoView GetVideoView()
        {
            if (cheeseView == null) { return null; }

            // 查询、保存封面
            // 将SeasonId保存到avid字段中
            // 每集封面的cid保存到cid字段，EpisodeId保存到bvid字段中
            StorageCover storageCover = new StorageCover();
            string coverUrl = cheeseView.Cover;
            string cover = storageCover.GetCover(cheeseView.SeasonId, "cheese", -1, coverUrl);

            // 获取用户头像
            string upName;
            string header;
            if (cheeseView.UpInfo != null)
            {
                upName = cheeseView.UpInfo.Name;
                StorageHeader storageHeader = new StorageHeader();
                header = storageHeader.GetHeader(cheeseView.UpInfo.Mid, cheeseView.UpInfo.Name, cheeseView.UpInfo.Avatar);
            }
            else
            {
                upName = "";
                header = null;
            }

            // 为videoInfoView赋值
            VideoInfoView videoInfoView = new VideoInfoView();
            App.PropertyChangeAsync(new Action(() =>
            {
                videoInfoView.CoverUrl = coverUrl;

                videoInfoView.Cover = cover == null ? null : new BitmapImage(new Uri(cover));
                videoInfoView.Title = cheeseView.Title;

                // 分区id
                // 课堂的type id B站没有定义，这里自定义为-10
                videoInfoView.TypeId = -10;

                videoInfoView.VideoZone = DictionaryResource.GetString("Cheese");
                videoInfoView.CreateTime = "";

                videoInfoView.PlayNumber = Format.FormatNumber(cheeseView.Stat.Play);
                videoInfoView.DanmakuNumber = Format.FormatNumber(0);
                videoInfoView.LikeNumber = Format.FormatNumber(0);
                videoInfoView.CoinNumber = Format.FormatNumber(0);
                videoInfoView.FavoriteNumber = Format.FormatNumber(0);
                videoInfoView.ShareNumber = Format.FormatNumber(0);
                videoInfoView.ReplyNumber = Format.FormatNumber(0);
                videoInfoView.Description = cheeseView.Subtitle;

                videoInfoView.UpName = upName;
                if (header != null)
                {
                    StorageHeader storageHeader = new StorageHeader();
                    videoInfoView.UpHeader = storageHeader.GetHeaderThumbnail(header, 48, 48);

                    videoInfoView.UpperMid = cheeseView.UpInfo.Mid;
                }
                else
                {
                    videoInfoView.UpHeader = null;
                }
            }));

            return videoInfoView;
        }
    }
}
