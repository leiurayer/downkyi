using DownKyi.Core.BiliApi.Bangumi;
using DownKyi.Core.BiliApi.Bangumi.Models;
using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.VideoStream;
using DownKyi.Core.BiliApi.VideoStream.Models;
using DownKyi.Core.Settings;
using DownKyi.Core.Storage;
using DownKyi.Core.Utils;
using DownKyi.Utils;
using DownKyi.ViewModels.PageViewModels;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;

namespace DownKyi.Services
{
    public class BangumiInfoService : IInfoService
    {
        private readonly BangumiSeason bangumiSeason;

        public BangumiInfoService(string input)
        {
            if (input == null)
            {
                return;
            }

            if (ParseEntrance.IsBangumiSeasonId(input) || ParseEntrance.IsBangumiSeasonUrl(input))
            {
                long seasonId = ParseEntrance.GetBangumiSeasonId(input);
                bangumiSeason = BangumiInfo.BangumiSeasonInfo(seasonId);
            }

            if (ParseEntrance.IsBangumiEpisodeId(input) || ParseEntrance.IsBangumiEpisodeUrl(input))
            {
                long episodeId = ParseEntrance.GetBangumiEpisodeId(input);
                bangumiSeason = BangumiInfo.BangumiSeasonInfo(-1, episodeId);
            }

            if (ParseEntrance.IsBangumiMediaId(input) || ParseEntrance.IsBangumiMediaUrl(input))
            {
                long mediaId = ParseEntrance.GetBangumiMediaId(input);
                BangumiMedia bangumiMedia = BangumiInfo.BangumiMediaInfo(mediaId);
                bangumiSeason = BangumiInfo.BangumiSeasonInfo(bangumiMedia.SeasonId);
            }
        }

        /// <summary>
        /// 获取视频剧集
        /// </summary>
        /// <returns></returns>
        public List<VideoPage> GetVideoPages()
        {
            List<VideoPage> pages = new List<VideoPage>();
            if (bangumiSeason == null) { return pages; }
            if (bangumiSeason.Episodes == null) { return pages; }
            if (bangumiSeason.Episodes.Count == 0) { return pages; }

            int order = 0;
            foreach (BangumiEpisode episode in bangumiSeason.Episodes)
            {
                order++;

                // 标题
                string name;

                // 判断title是否为数字，如果是，则将share_copy作为name，否则将title作为name
                //if (int.TryParse(episode.Title, out int result))
                //{
                //    name = Regex.Replace(episode.ShareCopy, @"《.*?》", "");
                //    //name = episode.ShareCopy;
                //}
                //else
                //{
                //    if (episode.LongTitle != null && episode.LongTitle != "")
                //    {
                //        name = $"{episode.Title} {episode.LongTitle}";
                //    }
                //    else
                //    {
                //        name = episode.Title;
                //    }
                //}

                // 将share_copy作为name，删除《》中的标题
                name = Regex.Replace(episode.ShareCopy, @"^《.*?》", "");

                // 删除前后空白符
                name = name.Trim();

                VideoPage page = new VideoPage
                {
                    Avid = episode.Aid,
                    Bvid = episode.Bvid,
                    Cid = episode.Cid,
                    EpisodeId = -1,
                    FirstFrame = episode.Cover,
                    Order = order,
                    Name = name,
                    Duration = "N/A"
                };

                // UP主信息
                if (bangumiSeason.UpInfo != null)
                {
                    page.Owner = new Core.BiliApi.Models.VideoOwner
                    {
                        Name = bangumiSeason.UpInfo.Name,
                        Face = bangumiSeason.UpInfo.Avatar,
                        Mid = bangumiSeason.UpInfo.Mid,
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
                DateTime dateTime = startTime.AddSeconds(episode.PubTime);
                page.PublishTime = dateTime.ToString(timeFormat);

                pages.Add(page);
            }

            return pages;
        }

        /// <summary>
        /// 获取视频章节与剧集
        /// </summary>
        /// <returns></returns>
        public List<VideoSection> GetVideoSections(bool noUgc = false)
        {
            if (bangumiSeason == null) { return null; }

            List<VideoSection> videoSections = new List<VideoSection>
            {
                new VideoSection
                {
                    Id = bangumiSeason.Positive.Id,
                    Title = bangumiSeason.Positive.Title,
                    IsSelected = true,
                    VideoPages = GetVideoPages()
                }
            };

            // 不需要其他季或花絮内容
            if (noUgc)
            {
                return videoSections;
            }

            if (bangumiSeason.Section == null) { return null; }
            if (bangumiSeason.Section.Count == 0) { return null; }

            foreach (BangumiSection section in bangumiSeason.Section)
            {
                List<VideoPage> pages = new List<VideoPage>();
                int order = 0;
                foreach (BangumiEpisode episode in section.Episodes)
                {
                    order++;

                    // 标题
                    string name = episode.LongTitle != null && episode.LongTitle != "" ? $"{episode.Title} {episode.LongTitle}" : episode.Title;
                    VideoPage page = new VideoPage
                    {
                        Avid = episode.Aid,
                        Bvid = episode.Bvid,
                        Cid = episode.Cid,
                        EpisodeId = -1,
                        FirstFrame = episode.Cover,
                        Order = order,
                        Name = name,
                        Duration = "N/A"
                    };

                    // UP主信息
                    if (bangumiSeason.UpInfo != null)
                    {
                        page.Owner = new Core.BiliApi.Models.VideoOwner
                        {
                            Name = bangumiSeason.UpInfo.Name,
                            Face = bangumiSeason.UpInfo.Avatar,
                            Mid = bangumiSeason.UpInfo.Mid,
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
                    DateTime dateTime = startTime.AddSeconds(episode.PubTime);
                    page.PublishTime = dateTime.ToString(timeFormat);

                    pages.Add(page);
                }

                VideoSection videoSection = new VideoSection
                {
                    Id = section.Id,
                    Title = section.Title,
                    VideoPages = pages
                };
                videoSections.Add(videoSection);
            }

            return videoSections;
        }

        /// <summary>
        /// 获取视频流的信息，从VideoPage返回
        /// </summary>
        /// <param name="page"></param>
        public void GetVideoStream(VideoPage page)
        {
            PlayUrl playUrl = VideoStream.GetBangumiPlayUrl(page.Avid, page.Bvid, page.Cid);
            Utils.VideoPageInfo(playUrl, page);
        }

        /// <summary>
        /// 获取视频信息
        /// </summary>
        /// <returns></returns>
        public VideoInfoView GetVideoView()
        {
            if (bangumiSeason == null) { return null; }

            // 查询、保存封面
            // 将SeasonId保存到avid字段中
            // 每集封面的cid保存到cid字段，EpisodeId保存到bvid字段中
            StorageCover storageCover = new StorageCover();
            string coverUrl = bangumiSeason.Cover;
            string cover = storageCover.GetCover(bangumiSeason.SeasonId, "bangumi", -1, coverUrl);

            // 获取用户头像
            string upName;
            string header;
            if (bangumiSeason.UpInfo != null)
            {
                upName = bangumiSeason.UpInfo.Name;

                StorageHeader storageHeader = new StorageHeader();
                header = storageHeader.GetHeader(bangumiSeason.UpInfo.Mid, bangumiSeason.UpInfo.Name, bangumiSeason.UpInfo.Avatar);
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
                videoInfoView.Title = bangumiSeason.Title;

                // 分区id
                videoInfoView.TypeId = BangumiType.TypeId[bangumiSeason.Type];

                videoInfoView.VideoZone = DictionaryResource.GetString(BangumiType.Type[bangumiSeason.Type]);

                videoInfoView.PlayNumber = Format.FormatNumber(bangumiSeason.Stat.Views);
                videoInfoView.DanmakuNumber = Format.FormatNumber(bangumiSeason.Stat.Danmakus);
                videoInfoView.LikeNumber = Format.FormatNumber(bangumiSeason.Stat.Likes);
                videoInfoView.CoinNumber = Format.FormatNumber(bangumiSeason.Stat.Coins);
                videoInfoView.FavoriteNumber = Format.FormatNumber(bangumiSeason.Stat.Favorites);
                videoInfoView.ShareNumber = Format.FormatNumber(bangumiSeason.Stat.Share);
                videoInfoView.ReplyNumber = Format.FormatNumber(bangumiSeason.Stat.Reply);
                videoInfoView.Description = bangumiSeason.Evaluate;

                videoInfoView.UpName = upName;
                if (header != null)
                {
                    StorageHeader storageHeader = new StorageHeader();
                    videoInfoView.UpHeader = storageHeader.GetHeaderThumbnail(header, 48, 48);

                    videoInfoView.UpperMid = bangumiSeason.UpInfo.Mid;
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
