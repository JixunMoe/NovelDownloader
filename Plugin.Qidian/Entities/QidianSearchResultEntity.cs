using System.Collections.Generic;
using Newtonsoft.Json;

namespace moe.jixun.Plugin.Qidian.Entities
{
    public class BookEntity
    {
        [JsonProperty(PropertyName = "action_status")]
        public string ActionStatus { get; set; }

        [JsonProperty(PropertyName = "authorinternalid")]
        public string AuthorInternalId { get; set; }

        [JsonProperty(PropertyName = "authorname")]
        public string AuthorName { get; set; }

        [JsonProperty(PropertyName = "authortagid")]
        public string AuthorTagId { get; set; }

        [JsonProperty(PropertyName = "bookid")]
        public string BookId { get; set; }

        [JsonProperty(PropertyName = "bookname")]
        public string BookName { get; set; }

        [JsonProperty(PropertyName = "bookurl")]
        public string BookUrl { get; set; }

        [JsonProperty(PropertyName = "channelid")]
        public string ChannelId { get; set; }

        [JsonProperty(PropertyName = "channelnane")]
        public string ChannelNane { get; set; }

        [JsonProperty(PropertyName = "chaptercount")]
        public string ChapterCount { get; set; }

        [JsonProperty(PropertyName = "copyright")]
        public string Copyright { get; set; }
        
        [JsonProperty(PropertyName = "coverurl")]
        public string CoverUrl { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "favoritecount")]
        public string FavoriteCount { get; set; }

        [JsonProperty(PropertyName = "finish_flag")]
        public string FinishFlag { get; set; }

        [JsonProperty(PropertyName = "internalsiteid")]
        public string InternalSiteId { get; set; }

        [JsonProperty(PropertyName = "lastchapter")]
        public string LastChapter { get; set; }

        [JsonProperty(PropertyName = "lastchaptername")]
        public string LastChapterName { get; set; }

        [JsonProperty(PropertyName = "lastchapterupdatetime")]
        public string LastChapterUpdateTime { get; set; }

        [JsonProperty(PropertyName = "lastvipchapter")]
        public string LastVipChapter { get; set; }

        [JsonProperty(PropertyName = "lastvipchaptername")]
        public string LastVipChapterName { get; set; }

        [JsonProperty(PropertyName = "lastvipchapterupdatetime")]
        public string LastVipChapterUpdateTime { get; set; }

        /// <summary>
        /// 月票数
        /// </summary>
        [JsonProperty(PropertyName = "monthpasscount")]
        public string MonthPassCount { get; set; }
        
        [JsonProperty(PropertyName = "pricetypeid")]
        public string PriceTypeId { get; set; }

        [JsonProperty(PropertyName = "publisherid")]
        public string PublisherId { get; set; }

        [JsonProperty(PropertyName = "raw_categoryid")]
        public string RawCategoryId { get; set; }

        [JsonProperty(PropertyName = "raw_categoryname")]
        public string RawCategoryName { get; set; }

        [JsonProperty(PropertyName = "recommendticketcount")]
        public string RecommendTicketCount { get; set; }

        [JsonProperty(PropertyName = "releasestatus")]
        public string ReleaseStatus { get; set; }

        [JsonProperty(PropertyName = "reviewcount")]
        public string ReviewCount { get; set; }

        [JsonProperty(PropertyName = "rpid")]
        public string Rpid { get; set; }

        [JsonProperty(PropertyName = "sign_status")]
        public string SignStatus { get; set; }

        [JsonProperty(PropertyName = "siteid")]
        public string SiteId { get; set; }

        [JsonProperty(PropertyName = "updatetime")]
        public string UpdateTime { get; set; }

        [JsonProperty(PropertyName = "viewcount")]
        public string ViewCount { get; set; }

        [JsonProperty(PropertyName = "viewcount_curmnth")]
        public string ViewCountCurrentMonth { get; set; }

        [JsonProperty(PropertyName = "viewcount_curwk")]
        public string ViewCountCurrentWeek { get; set; }

        [JsonProperty(PropertyName = "wordscount")]
        public string WordCount { get; set; }

        [JsonProperty(PropertyName = "standardcategoryid")]
        public string StandardCategoryId { get; set; }

        [JsonProperty(PropertyName = "standardsubcategoryid")]
        public string StandardSubcategoryId { get; set; }

        [JsonProperty(PropertyName = "standardcategoryname")]
        public string StandardCategoryName { get; set; }

        [JsonProperty(PropertyName = "standardsubcategoryname")]
        public string StandardSubcategoryName { get; set; }

        [JsonProperty(PropertyName = "categoryid")]
        public string CategoryId { get; set; }

        [JsonProperty(PropertyName = "subcategoryid")]
        public string SubcategoryId { get; set; }

        [JsonProperty(PropertyName = "categoryname")]
        public string CategoryName { get; set; }

        [JsonProperty(PropertyName = "subcategoryname")]
        public string SubcategoryName { get; set; }

        [JsonProperty(PropertyName = "authorid")]
        public string AuthorId { get; set; }

        [JsonProperty(PropertyName = "bookinteralid")]
        public string BookInteralId { get; set; }

        [JsonProperty(PropertyName = "action_status_name")]
        public string ActionStatusName { get; set; }

        [JsonProperty(PropertyName = "sign_status_name")]
        public string SignStatusName { get; set; }

        [JsonProperty(PropertyName = "alg_info")]
        public string AlgInfo { get; set; }

    }

    public class SearchResponseEntity
    {
        [JsonProperty(PropertyName = "books")]
        public List<BookEntity> Books { get; set; }

        [JsonProperty(PropertyName = "totalcount")]
        public int Totalcount { get; set; }
    }

    public class SearchResultDataEntity
    {
        [JsonProperty(PropertyName = "blob")]
        public string Blob { get; set; }

        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "search_response")]
        public SearchResponseEntity SearchResponseEntity { get; set; }
    }

    public class QidianSearchResultEntity
    {
        [JsonProperty(PropertyName = "Flag")]
        public bool Flag { get; set; }

        [JsonProperty(PropertyName = "Data")]
        public SearchResultDataEntity Data { get; set; }

        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "ext_token")]
        public string ExtToken { get; set; }
    }
}