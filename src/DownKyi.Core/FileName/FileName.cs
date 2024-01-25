using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DownKyi.Core.FileName
{
    public class FileName
    {
        private readonly List<FileNamePart> nameParts;
        private string order = "ORDER";
        private string section = "SECTION";
        private string mainTitle = "MAIN_TITLE";
        private string pageTitle = "PAGE_TITLE";
        private string videoZone = "VIDEO_ZONE";
        private string audioQuality = "AUDIO_QUALITY";
        private string videoQuality = "VIDEO_QUALITY";
        private string videoCodec = "VIDEO_CODEC";

        private string videoPublishTime = "VIDEO_PUBLISH_TIME";

        private long avid = -1;
        private string bvid = "BVID";
        private long cid = -1;

        private long upMid = -1;
        private string upName = "UP_NAME";

        private FileName(List<FileNamePart> nameParts)
        {
            this.nameParts = nameParts;
        }

        public static FileName Builder(List<FileNamePart> nameParts)
        {
            return new FileName(nameParts);
        }

        public FileName SetOrder(int order)
        {
            this.order = order.ToString();
            return this;
        }

        public FileName SetOrder(int order, int count)
        {
            int length = Math.Abs(count).ToString().Length;
            this.order = order.ToString("D" + length);

            return this;
        }

        public FileName SetSection(string section)
        {
            this.section = section;
            return this;
        }

        public FileName SetMainTitle(string mainTitle)
        {
            this.mainTitle = mainTitle;
            return this;
        }

        public FileName SetPageTitle(string pageTitle)
        {
            this.pageTitle = pageTitle;
            return this;
        }

        public FileName SetVideoZone(string videoZone)
        {
            this.videoZone = videoZone;
            return this;
        }

        public FileName SetAudioQuality(string audioQuality)
        {
            this.audioQuality = audioQuality;
            return this;
        }

        public FileName SetVideoQuality(string videoQuality)
        {
            this.videoQuality = videoQuality;
            return this;
        }

        public FileName SetVideoCodec(string videoCodec)
        {
            this.videoCodec = videoCodec;
            return this;
        }

        public FileName SetVideoPublishTime(string videoPublishTime)
        {
            this.videoPublishTime = videoPublishTime;
            return this;
        }

        public FileName SetAvid(long avid)
        {
            this.avid = avid;
            return this;
        }

        public FileName SetBvid(string bvid)
        {
            this.bvid = bvid;
            return this;
        }

        public FileName SetCid(long cid)
        {
            this.cid = cid;
            return this;
        }

        public FileName SetUpMid(long upMid)
        {
            this.upMid = upMid;
            return this;
        }

        public FileName SetUpName(string upName)
        {
            this.upName = upName;
            return this;
        }

        public string RelativePath()
        {
            string path = string.Empty;

            foreach (FileNamePart part in nameParts)
            {
                switch (part)
                {
                    case FileNamePart.ORDER:
                        path += order;
                        break;
                    case FileNamePart.SECTION:
                        path += section;
                        break;
                    case FileNamePart.MAIN_TITLE:
                        path += mainTitle;
                        break;
                    case FileNamePart.PAGE_TITLE:
                        path += pageTitle;
                        break;
                    case FileNamePart.VIDEO_ZONE:
                        path += videoZone;
                        break;
                    case FileNamePart.AUDIO_QUALITY:
                        path += audioQuality;
                        break;
                    case FileNamePart.VIDEO_QUALITY:
                        path += videoQuality;
                        break;
                    case FileNamePart.VIDEO_CODEC:
                        path += videoCodec;
                        break;
                    case FileNamePart.VIDEO_PUBLISH_TIME:
                        path += videoPublishTime;
                        break;
                    case FileNamePart.AVID:
                        path += $"av{avid}";
                        break;
                    case FileNamePart.BVID:
                        path += bvid;
                        break;
                    case FileNamePart.CID:
                        path += cid;
                        break;
                    case FileNamePart.UP_MID:
                        path += upMid;
                        break;
                    case FileNamePart.UP_NAME:
                        path += upName;
                        break;
                }

                if (((int)part) >= 100)
                {
                    path += HyphenSeparated.Hyphen[(int)part];
                }
            }

            // 避免连续多个斜杠
            path = Regex.Replace(path, @"//+", "/");
            // 避免以斜杠开头和结尾的情况
            return path.TrimEnd('/').TrimStart('/');
        }

    }
}
