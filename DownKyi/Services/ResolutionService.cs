using DownKyi.Models;
using System.Collections.Generic;

namespace DownKyi.Services
{
    public class ResolutionService : IResolutionService
    {
        public List<Resolution> GetResolution()
        {
            List<Resolution> resolutions = new List<Resolution>
            {
                new Resolution { Name = "HDR 真彩", Id = 125 },
                new Resolution { Name = "4K 超清", Id = 120 },
                new Resolution { Name = "1080P 60帧", Id = 116 },
                new Resolution { Name = "1080P 高码率", Id = 112 },
                new Resolution { Name = "1080P 高清", Id = 80 },
                new Resolution { Name = "720P 60帧", Id = 74 },
                new Resolution { Name = "720P 高清", Id = 64 },
                new Resolution { Name = "480P 清晰", Id = 32 },
                new Resolution { Name = "360P 流畅", Id = 16 }
            };

            return resolutions;
        }
    }
}
