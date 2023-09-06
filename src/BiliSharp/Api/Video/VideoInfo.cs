using BiliSharp.Api.Models.Video;
using BiliSharp.Api.Sign;
using System.Collections.Generic;

namespace BiliSharp.Api.Video
{
    /// <summary>
    /// 视频基本信息
    /// </summary>
    public static class VideoInfo
    {
        /// <summary>
        /// 获取视频超详细信息(web端)
        /// </summary>
        /// <param name="bvid"></param>
        /// <param name="aid"></param>
        /// <returns></returns>
        public static VideoView GetVideoViewInfo(string bvid, long aid)
        {
            var parameters = new Dictionary<string, object>
            {
                { "platform", "web" },
                //{ "need_operation_card", 1 },
                //{ "web_location", 1446382 },
                { "need_elec", 1 },
                { "aid", aid },
                { "bvid", bvid },
            };
            string query = WbiSign.ParametersToQuery(WbiSign.EncodeWbi(parameters));
            string url = $"https://api.bilibili.com/x/web-interface/wbi/view/detail?{query}";
            return Utils.GetData<VideoView>(url);
        }

    }
}
