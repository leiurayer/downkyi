using Newtonsoft.Json;
using System.Collections.Generic;

namespace Core.entity2.users
{
    // https://api.bilibili.com/x/space/arc/search
    [JsonObject]
    public class SpacePublicationOrigin : BaseEntity
    {
        //[JsonProperty("code")]
        //public int Code { get; set; }
        [JsonProperty("data")]
        public SpacePublication Data { get; set; }
        //[JsonProperty("message")]
        //public string Message { get; set; }
        //[JsonProperty("ttl")]
        //public int Ttl { get; set; }
    }

    [JsonObject]
    public class SpacePublication : BaseEntity
    {
        [JsonProperty("list")]
        public SpacePublicationList List { get; set; }
        [JsonProperty("page")]
        public SpacePublicationPage Page { get; set; }
    }

    [JsonObject]
    public class SpacePublicationList : BaseEntity
    {
        [JsonProperty("tlist")]
        public SpacePublicationListType Tlist { get; set; }
        [JsonProperty("vlist")]
        public List<SpacePublicationListVideo> Vlist { get; set; }
    }

    [JsonObject]
    public class SpacePublicationPage : BaseEntity
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("pn")]
        public int Pn { get; set; }
        [JsonProperty("ps")]
        public int Ps { get; set; }
    }

    [JsonObject]
    public class SpacePublicationListType : BaseEntity
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

    [JsonObject]
    public class SpacePublicationListVideo : BaseEntity
    {
        [JsonProperty("aid")]
        public long Aid { get; set; }
        //[JsonProperty("author")]
        //public string Author { get; set; }
        [JsonProperty("bvid")]
        public string Bvid { get; set; }
        //[JsonProperty("comment")]
        //public int Comment { get; set; }
        //[JsonProperty("copyright")]
        //public string Copyright { get; set; }
        [JsonProperty("created")]
        public long Created { get; set; }
        //[JsonProperty("description")]
        //public string Description { get; set; }
        //[JsonProperty("hide_click")]
        //public bool HideClick { get; set; }
        //[JsonProperty("is_pay")]
        //public int IsPay { get; set; }
        //[JsonProperty("is_steins_gate")]
        //public int IsSteinsGate { get; set; }
        //[JsonProperty("is_union_video")]
        //public int IsUnionVideo { get; set; }
        [JsonProperty("length")]
        public string Length { get; set; }
        [JsonProperty("mid")]
        public long Mid { get; set; }
        [JsonProperty("pic")]
        public string Pic { get; set; }
        [JsonProperty("play")]
        public int Play { get; set; }
        //[JsonProperty("review")]
        //public int Review { get; set; }
        //[JsonProperty("subtitle")]
        //public string Subtitle { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("typeid")]
        public int Typeid { get; set; }
        //[JsonProperty("video_review")]
        //public int VideoReview { get; set; }
    }

    [JsonObject]
    public class SpacePublicationListTypeVideoZone : BaseEntity
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("tid")]
        public int Tid { get; set; }
    }

}
