using DownKyi.Core.BiliApi.Models;
using Newtonsoft.Json;

namespace DownKyi.Core.BiliApi.Users.Models
{
    public class SpacePublicationListType : BaseModel
    {
        [JsonProperty("1")]
        public SpacePublicationListTypeVideoZone Douga { get; set; }
        [JsonProperty("13")]
        public SpacePublicationListTypeVideoZone Anime { get; set; }
        [JsonProperty("167")]
        public SpacePublicationListTypeVideoZone Guochuang { get; set; }
        [JsonProperty("3")]
        public SpacePublicationListTypeVideoZone Music { get; set; }
        [JsonProperty("129")]
        public SpacePublicationListTypeVideoZone Dance { get; set; }
        [JsonProperty("4")]
        public SpacePublicationListTypeVideoZone Game { get; set; }
        [JsonProperty("36")]
        public SpacePublicationListTypeVideoZone Technology { get; set; }
        [JsonProperty("188")]
        public SpacePublicationListTypeVideoZone Digital { get; set; }
        [JsonProperty("224")]
        public SpacePublicationListTypeVideoZone Sports { get; set; }
        [JsonProperty("223")]
        public SpacePublicationListTypeVideoZone Car { get; set; }
        [JsonProperty("160")]
        public SpacePublicationListTypeVideoZone Life { get; set; }
        [JsonProperty("211")]
        public SpacePublicationListTypeVideoZone Food { get; set; }
        [JsonProperty("217")]
        public SpacePublicationListTypeVideoZone Animal { get; set; }
        [JsonProperty("119")]
        public SpacePublicationListTypeVideoZone Kichiku { get; set; }
        [JsonProperty("155")]
        public SpacePublicationListTypeVideoZone Fashion { get; set; }
        [JsonProperty("202")]
        public SpacePublicationListTypeVideoZone Information { get; set; }
        [JsonProperty("5")]
        public SpacePublicationListTypeVideoZone Ent { get; set; }
        [JsonProperty("181")]
        public SpacePublicationListTypeVideoZone Cinephile { get; set; }
        [JsonProperty("177")]
        public SpacePublicationListTypeVideoZone Documentary { get; set; }
        [JsonProperty("23")]
        public SpacePublicationListTypeVideoZone Movie { get; set; }
        [JsonProperty("11")]
        public SpacePublicationListTypeVideoZone Tv { get; set; }
    }
}
