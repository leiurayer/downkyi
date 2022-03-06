using DownKyi.Core.BiliApi.BiliUtils;
using DownKyi.Core.BiliApi.VideoStream.Models;
using DownKyi.Core.Settings;
using DownKyi.Core.Settings.Models;
using DownKyi.Core.Utils;
using DownKyi.ViewModels.PageViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DownKyi.Services
{
    internal static class Utils
    {
        /// <summary>
        /// 从视频流更新VideoPage
        /// </summary>
        /// <param name="playUrl"></param>
        /// <param name="page"></param>
        internal static void VideoPageInfo(PlayUrl playUrl, VideoPage page)
        {
            if (playUrl == null)
            {
                return;
            }

            // 视频流信息
            page.PlayUrl = playUrl;

            // 获取设置
            UserInfoSettings userInfo = SettingsManager.GetInstance().GetUserInfo();
            int defaultQuality = SettingsManager.GetInstance().GetQuality();
            VideoCodecs videoCodecs = SettingsManager.GetInstance().GetVideoCodecs();
            int defaultAudioQuality = SettingsManager.GetInstance().GetAudioQuality();

            // 未登录时，最高仅720P
            if (userInfo.Mid == -1)
            {
                if (defaultQuality > 64)
                {
                    defaultQuality = 64;
                }
            }

            // 非大会员账户登录时，如果设置的画质高于1080P，则这里限制为1080P
            if (!userInfo.IsVip)
            {
                if (defaultQuality > 80)
                {
                    defaultQuality = 80;
                }
            }

            if (playUrl.Durl != null)
            {
                // 音质

                // 画质

                // 视频编码

                // 时长

                return;
            }

            if (playUrl.Dash != null)
            {
                // 如果video列表或者audio列表没有内容，则返回false
                if (playUrl.Dash.Video == null) { return; }
                if (playUrl.Dash.Video.Count == 0) { return; }

                // 音质
                page.AudioQualityFormatList = GetAudioQualityFormatList(playUrl, defaultAudioQuality);
                if (page.AudioQualityFormatList.Count > 0)
                {
                    page.AudioQualityFormat = page.AudioQualityFormatList[0];
                }

                // 画质 & 视频编码
                page.VideoQualityList = GetVideoQualityList(playUrl, userInfo, defaultQuality, videoCodecs);
                if (page.VideoQualityList.Count > 0)
                {
                    page.VideoQuality = page.VideoQualityList[0];
                }

                // 时长
                page.Duration = Format.FormatDuration(playUrl.Dash.Duration);
            }
        }

        /// <summary>
        /// 设置音质
        /// </summary>
        /// <param name="playUrl"></param>
        /// <param name="defaultAudioQuality"></param>
        /// <returns></returns>
        private static ObservableCollection<string> GetAudioQualityFormatList(PlayUrl playUrl, int defaultAudioQuality)
        {
            List<string> audioQualityFormatList = new List<string>();

            if (playUrl.Dash.Audio == null)
            {
                return new ObservableCollection<string>();
            }

            foreach (PlayUrlDashVideo audio in playUrl.Dash.Audio)
            {
                // 音质id大于设置画质时，跳过
                if (audio.Id > defaultAudioQuality) { continue; }

                Quality audioQuality = Constant.GetAudioQualities().FirstOrDefault(t => { return t.Id == audio.Id; });
                if (audioQuality != null)
                {
                    ListHelper.AddUnique(audioQualityFormatList, audioQuality.Name);
                }
            }

            audioQualityFormatList.Sort(new StringLogicalComparer<string>());
            audioQualityFormatList.Reverse();

            return new ObservableCollection<string>(audioQualityFormatList);
        }

        /// <summary>
        /// 设置画质 & 视频编码
        /// </summary>
        /// <param name="playUrl"></param>
        /// <param name="defaultQuality"></param>
        /// <param name="userInfo"></param>
        /// <param name="videoCodecs"></param>
        /// <returns></returns>
        private static List<VideoQuality> GetVideoQualityList(PlayUrl playUrl, UserInfoSettings userInfo, int defaultQuality, VideoCodecs videoCodecs)
        {
            List<VideoQuality> videoQualityList = new List<VideoQuality>();

            if (playUrl.Dash.Video == null)
            {
                return videoQualityList;
            }

            foreach (PlayUrlDashVideo video in playUrl.Dash.Video)
            {
                // 画质id大于设置画质时，跳过
                if (video.Id > defaultQuality) { continue; }

                // 非大会员账户登录时
                if (!userInfo.IsVip)
                {
                    // 如果画质为720P60，跳过
                    if (video.Id == 74) { continue; }
                }

                string qualityFormat = string.Empty;
                PlayUrlSupportFormat selectedQuality = playUrl.SupportFormats.FirstOrDefault(t => t.Quality == video.Id);
                if (selectedQuality != null)
                {
                    qualityFormat = selectedQuality.NewDescription;
                }

                // 寻找是否已存在这个画质
                // 不存在则添加，存在则修改
                string codecName = GetVideoCodecName(video.Codecs);
                VideoQuality videoQualityExist = videoQualityList.FirstOrDefault(t => t.Quality == video.Id);
                if (videoQualityExist == null)
                {
                    List<string> videoCodecList = new List<string>();
                    if (codecName != string.Empty)
                    {
                        ListHelper.AddUnique(videoCodecList, codecName);
                    }

                    VideoQuality videoQuality = new VideoQuality()
                    {
                        Quality = video.Id,
                        QualityFormat = qualityFormat,
                        VideoCodecList = videoCodecList
                    };
                    videoQualityList.Add(videoQuality);
                }
                else
                {
                    if (!videoQualityList[videoQualityList.IndexOf(videoQualityExist)].VideoCodecList.Exists(t => t.Equals(codecName)))
                    {
                        if (codecName != string.Empty)
                        {
                            videoQualityList[videoQualityList.IndexOf(videoQualityExist)].VideoCodecList.Add(codecName);
                        }
                    }
                }

                // 设置选中的视频编码
                VideoQuality selectedVideoQuality = videoQualityList.FirstOrDefault(t => t.Quality == video.Id);
                switch (videoCodecs)
                {
                    case VideoCodecs.AVC:
                        if (videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].VideoCodecList.Contains("H.264/AVC"))
                        {
                            videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].SelectedVideoCodec = "H.264/AVC";
                        }
                        break;
                    case VideoCodecs.HEVC:
                        if (videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].VideoCodecList.Contains("H.265/HEVC"))
                        {
                            videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].SelectedVideoCodec = "H.265/HEVC";
                        }
                        break;
                    case VideoCodecs.NONE:
                        break;
                    default:
                        break;
                }

                if (videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].VideoCodecList.Count == 1)
                {
                    // 当获取的视频没有设置的视频编码时，执行
                    videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].SelectedVideoCodec = videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].VideoCodecList[0];
                }
            }

            return videoQualityList;
        }

        /// <summary>
        /// 根据输入的字符串判断是AVC还是HEVC
        /// </summary>
        /// <param name="origin"></param>
        /// <returns></returns>
        internal static string GetVideoCodecName(string origin)
        {
            return origin.Contains("avc") ? "H.264/AVC" : origin.Contains("hev") ? "H.265/HEVC" : origin.Contains("dvh") || origin.Contains("hvc") ? "Dolby Vision" : "";
        }

    }
}
