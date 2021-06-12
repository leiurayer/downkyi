using System.Net;
using System.Text.RegularExpressions;

namespace Core.api.fileDownload
{
    internal class FileInfo
    {
        public long Length { get; set; }
        public string FileName { get; set; }

        /// <summary>
        /// 获取远程文件的信息
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static FileInfo GetFileMessage(WebResponse response)
        {
            FileInfo info = new FileInfo();

            if (response.Headers["Content-Disposition"] != null)
            {
                Match match = Regex.Match(response.Headers["Content-Disposition"], "filename=(.*)");
                if (match.Success)
                {
                    string fileName = match.Groups[1].Value;
                    info.FileName = WebUtility.UrlDecode(fileName);
                }
            }

            if (response.Headers["Content-Length"] != null)
            {
                info.Length = long.Parse(response.Headers.Get("Content-Length"));
            }
            else
            {
                info.Length = response.ContentLength;
            }

            return info;
        }

    }
}
