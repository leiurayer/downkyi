using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.Video;
using DownKyi.Core.BiliApi.Video.Models;
using DownKyi.Core.BiliApi.VideoStream;
using DownKyi.Core.Storage;
using DownKyi.Core.Utils;
using DownKyi.ViewModels.PageViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace DownKyi.Services
{
    public class VideoInfoService : IInfoService
    {
        private readonly VideoView videoView;

        public VideoInfoService(string input)
        {
            if (input == null)
            {
                return;
            }

            if (ParseEntrance.IsAvId(input) || ParseEntrance.IsAvUrl(input))
            {
                long avid = ParseEntrance.GetAvId(input);
                videoView = VideoInfo.VideoViewInfo(null, avid);
            }

            if (ParseEntrance.IsBvId(input) || ParseEntrance.IsBvUrl(input))
            {
                string bvid = ParseEntrance.GetBvId(input);
                videoView = VideoInfo.VideoViewInfo(bvid);
            }
        }

        /// <summary>
        /// 获取视频剧集
        /// </summary>
        /// <returns></returns>
        public List<ViewModels.PageViewModels.VideoPage> GetVideoPages()
        {
            if (videoView == null) { return null; }
            if (videoView.Pages == null) { return null; }
            if (videoView.Pages.Count == 0) { return null; }

            List<ViewModels.PageViewModels.VideoPage> videoPages = new List<ViewModels.PageViewModels.VideoPage>();

            int order = 0;
            foreach (var page in videoView.Pages)
            {
                order++;

                // 标题
                string name;
                if (videoView.Pages.Count == 1)
                {
                    name = videoView.Title;
                }
                else
                {
                    //name = page.part;
                    if (page.Part == "")
                    {
                        // 如果page.part为空字符串
                        name = $"{videoView.Title}-P{order}";
                    }
                    else
                    {
                        name = page.Part;
                    }
                }

                ViewModels.PageViewModels.VideoPage videoPage = new ViewModels.PageViewModels.VideoPage
                {
                    Avid = videoView.Aid,
                    Bvid = videoView.Bvid,
                    Cid = page.Cid,
                    EpisodeId = -1,
                    FirstFrame = page.FirstFrame,
                    Order = order,
                    Name = name,
                    Duration = "N/A"
                };

                // UP主信息
                videoPage.Owner = videoView.Owner;
                if (videoPage.Owner == null)
                {
                    videoPage.Owner = new Core.BiliApi.Models.VideoOwner
                    {
                        Name = "",
                        Face = "",
                        Mid = -1,
                    };
                }

                // 视频发布时间
                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
                DateTime dateTime = startTime.AddSeconds(videoView.Pubdate);
                videoPage.PublishTime = dateTime.ToString("yyyy-MM-dd");

                videoPages.Add(videoPage);
            }

            return videoPages;
        }

        /// <summary>
        /// 获取视频章节与剧集
        /// </summary>
        /// <returns></returns>
        public List<VideoSection> GetVideoSections()
        {
            if (videoView == null) { return null; }
            if (videoView.UgcSeason == null) { return null; }
            if (videoView.UgcSeason.Sections == null) { return null; }
            if (videoView.UgcSeason.Sections.Count == 0) { return null; }

            List<VideoSection> videoSections = new List<VideoSection>();

            foreach (UgcSection section in videoView.UgcSeason.Sections)
            {
                List<ViewModels.PageViewModels.VideoPage> pages = new List<ViewModels.PageViewModels.VideoPage>();
                int order = 0;
                foreach (var episode in section.Episodes)
                {
                    order++;
                    ViewModels.PageViewModels.VideoPage page = new ViewModels.PageViewModels.VideoPage
                    {
                        Avid = episode.Aid,
                        Bvid = episode.Bvid,
                        Cid = episode.Cid,
                        EpisodeId = -1,
                        FirstFrame = episode.Page.FirstFrame,
                        Order = order,
                        Name = episode.Title,
                        Duration = "N/A"
                    };
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

            videoSections[0].IsSelected = true;

            return videoSections;
        }

        /// <summary>
        /// 获取视频流的信息，从VideoPage返回
        /// </summary>
        /// <param name="page"></param>
        public void GetVideoStream(ViewModels.PageViewModels.VideoPage page)
        {
            var playUrl = VideoStream.GetVideoPlayUrl(page.Avid, page.Bvid, page.Cid);
            Utils.VideoPageInfo(playUrl, page);
        }

        /// <summary>
        /// 获取视频信息
        /// </summary>
        /// <returns></returns>
        public VideoInfoView GetVideoView()
        {
            if (videoView == null) { return null; }

            // 查询、保存封面
            StorageCover storageCover = new StorageCover();
            string coverUrl = videoView.Pic;
            string cover = storageCover.GetCover(videoView.Aid, videoView.Bvid, videoView.Cid, coverUrl);

            // 分区
            string videoZone = string.Empty;
            var zoneList = Core.BiliApi.Zone.VideoZone.Instance().GetZones();
            var zone = zoneList.Find(it => it.Id == videoView.Tid);
            if (zone != null)
            {
                var zoneParent = zoneList.Find(it => it.Id == zone.ParentId);
                if (zoneParent != null)
                {
                    videoZone = zoneParent.Name + ">" + zone.Name;
                }
                else
                {
                    videoZone = zone.Name;
                }
            }
            else
            {
                videoZone = videoView.Tname;
            }

            // 获取用户头像
            string upName;
            string header;
            if (videoView.Owner != null)
            {
                upName = videoView.Owner.Name;
                StorageHeader storageHeader = new StorageHeader();
                header = storageHeader.GetHeader(videoView.Owner.Mid, videoView.Owner.Name, videoView.Owner.Face);
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
                videoInfoView.Title = videoView.Title;

                // 分区id
                videoInfoView.TypeId = videoView.Tid;

                videoInfoView.VideoZone = videoZone;

                DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
                DateTime dateTime = startTime.AddSeconds(videoView.Pubdate);
                videoInfoView.CreateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

                videoInfoView.PlayNumber = Format.FormatNumber(videoView.Stat.View);
                videoInfoView.DanmakuNumber = Format.FormatNumber(videoView.Stat.Danmaku);
                videoInfoView.LikeNumber = Format.FormatNumber(videoView.Stat.Like);
                videoInfoView.CoinNumber = Format.FormatNumber(videoView.Stat.Coin);
                videoInfoView.FavoriteNumber = Format.FormatNumber(videoView.Stat.Favorite);
                videoInfoView.ShareNumber = Format.FormatNumber(videoView.Stat.Share);
                videoInfoView.ReplyNumber = Format.FormatNumber(videoView.Stat.Reply);
                videoInfoView.Description = videoView.Desc;

                videoInfoView.UpName = upName;
                if (header != null)
                {
                    StorageHeader storageHeader = new StorageHeader();
                    videoInfoView.UpHeader = storageHeader.GetHeaderThumbnail(header, 48, 48);

                    videoInfoView.UpperMid = videoView.Owner.Mid;
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
