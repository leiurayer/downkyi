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
            int videoCodecs = SettingsManager.GetInstance().GetVideoCodecs();
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

                return;
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
            List<string> sortList = new List<string>();
            List<Quality> audioQualities = Constant.GetAudioQualities();

            if (playUrl.Dash.Audio != null && playUrl.Dash.Audio.Count > 0)
            {
                foreach (PlayUrlDashVideo audio in playUrl.Dash.Audio)
                {
                    // 音质id大于设置音质时，跳过
                    if (audio.Id > defaultAudioQuality) { continue; }

                    Quality audioQuality = audioQualities.FirstOrDefault(t => { return t.Id == audio.Id; });
                    if (audioQuality != null)
                    {
                        ListHelper.AddUnique(audioQualityFormatList, audioQuality.Name);
                    }
                }
            }

            if (audioQualities[3].Id <= defaultAudioQuality - 1000 && playUrl.Dash.Dolby != null)
            {
                if (playUrl.Dash.Dolby.Audio != null && playUrl.Dash.Dolby.Audio.Count > 0)
                {
                    ListHelper.AddUnique(audioQualityFormatList, audioQualities[3].Name);
                }
            }

            if (audioQualities[4].Id <= defaultAudioQuality - 1000 && playUrl.Dash.Flac != null)
            {
                if (playUrl.Dash.Flac.Audio != null)
                {
                    ListHelper.AddUnique(audioQualityFormatList, audioQualities[4].Name);
                }
            }

            //audioQualityFormatList.Sort(new StringLogicalComparer<string>());
            //audioQualityFormatList.Reverse();

            foreach (var item in audioQualities)
            {
                if (audioQualityFormatList.Contains(item.Name))
                {
                    sortList.Add(item.Name);
                }
            }
            sortList.Reverse();

            return new ObservableCollection<string>(sortList);
        }

        /// <summary>
        /// 设置画质 & 视频编码
        /// </summary>
        /// <param name="playUrl"></param>
        /// <param name="defaultQuality"></param>
        /// <param name="userInfo"></param>
        /// <param name="videoCodecs"></param>
        /// <returns></returns>
        private static List<VideoQuality> GetVideoQualityList(PlayUrl playUrl, UserInfoSettings userInfo, int defaultQuality, int videoCodecs)
        {
            List<VideoQuality> videoQualityList = new List<VideoQuality>();
            List<Quality> codeIds = Constant.GetCodecIds();

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
                //string codecName = GetVideoCodecName(video.Codecs);
                string codecName = codeIds.FirstOrDefault(t => t.Id == video.CodecId).Name;
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
                if (selectedVideoQuality == null) { continue; }

                // 设置选中的视频编码
                string videoCodecsName = codeIds.FirstOrDefault(t => t.Id == videoCodecs).Name;
                if (videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].VideoCodecList.Contains(videoCodecsName))
                {
                    videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].SelectedVideoCodec = videoCodecsName;
                }
                else
                {
                    // 当获取的视频没有设置的视频编码时
                    foreach (Quality codec in codeIds)
                    {
                        if (videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].VideoCodecList.Contains(codec.Name))
                        {
                            videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].SelectedVideoCodec = codec.Name;
                        }

                        if (codec.Id == videoCodecs)
                        {
                            break;
                        }
                    }

                    // 若默认编码为AVC，但画质为杜比视界时，
                    // 上面的foreach不会选中HEVC编码，
                    // 而杜比视界只有HEVC编码，
                    // 因此这里再判断并设置一次
                    if (videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].SelectedVideoCodec == null &&
                        videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].VideoCodecList.Count() > 0)
                    {
                        videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].SelectedVideoCodec =
                            videoQualityList[videoQualityList.IndexOf(selectedVideoQuality)].VideoCodecList[0];
                    }
                }

            }

            return videoQualityList;
        }

    }
}
