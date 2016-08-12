using System.Collections.Generic;
using Newtonsoft.Json;

namespace moe.jixun.Plugin.Qidian.Entities
{
    public class Chapter
    {
        [JsonProperty(PropertyName = "c")]
        public int ChapterId { get; set; }

        [JsonProperty(PropertyName = "n")]
        public string ChapterName { get; set; }

        [JsonProperty(PropertyName = "v")]
        public int IsVipChapter { get; set; }

        [JsonProperty(PropertyName = "p")]
        public int Price { get; set; }

        [JsonProperty(PropertyName = "t")]
        public long TimeStamp { get; set; }

        [JsonProperty(PropertyName = "w")]
        public int WordCount { get; set; }

        [JsonProperty(PropertyName = "vc")]
        public string VolumeId { get; set; }

        // 下面的参数不懂

        [JsonProperty(PropertyName = "ui")]
        public int Ui { get; set; }

        [JsonProperty(PropertyName = "pn")]
        public int Pn { get; set; }

        [JsonProperty(PropertyName = "ccs")]
        public int Ccs { get; set; }

        [JsonProperty(PropertyName = "cci")]
        public int Cci { get; set; }

    }

    public class Volume
    {
        [JsonProperty(PropertyName = "VolumeCode")]
        public string VolumeCode { get; set; }

        [JsonProperty(PropertyName = "VolumeName")]
        public string VolumeName { get; set; }

    }

    public class QidianBookInfo
    {
        [JsonProperty(PropertyName = "BookId")]
        public int BookId { get; set; }

        [JsonProperty(PropertyName = "BookName")]
        public string BookName { get; set; }

        [JsonProperty(PropertyName = "Author")]
        public string Author { get; set; }

        [JsonProperty(PropertyName = "ImageStatus")]
        public int ImageStatus { get; set; }

        [JsonProperty(PropertyName = "BookStatus")]
        public string BookStatus { get; set; }

        [JsonProperty(PropertyName = "CategoryId")]
        public int CategoryId { get; set; }

        [JsonProperty(PropertyName = "CategoryName")]
        public string CategoryName { get; set; }

        [JsonProperty(PropertyName = "SubCategoryId")]
        public int SubCategoryId { get; set; }

        [JsonProperty(PropertyName = "SubCategoryName")]
        public string SubCategoryName { get; set; }

        [JsonProperty(PropertyName = "SignStatus")]
        public string SignStatus { get; set; }

        // 投“赞”
        [JsonProperty(PropertyName = "IscanPraise")]
        public int CanUpVote { get; set; }

        [JsonProperty(PropertyName = "LastChapterId")]
        public int LastChapterId { get; set; }

        [JsonProperty(PropertyName = "LastChapterName")]
        public string LastChapterName { get; set; }

        [JsonProperty(PropertyName = "LastChapterTime")]
        public long LastChapterTime { get; set; }

        [JsonProperty(PropertyName = "LastVipChapterId")]
        public int LastVipChapterId { get; set; }

        [JsonProperty(PropertyName = "LastVipChapterName")]
        public string LastVipChapterName { get; set; }

        [JsonProperty(PropertyName = "LastVipChapterTime")]
        public long LastVipChapterTime { get; set; }

        [JsonProperty(PropertyName = "WordsCount")]
        public int WordsCount { get; set; }

        // 全勤?
        [JsonProperty(PropertyName = "IsQin")]
        public int IsQin { get; set; }

        [JsonProperty(PropertyName = "Chapters")]
        public List<Chapter> Chapters { get; set; }

        [JsonProperty(PropertyName = "Volumes")]
        public List<Volume> Volumes { get; set; }

        [JsonProperty(PropertyName = "EnableBookUnitLease")]
        public int EnableBookUnitLease { get; set; }

        [JsonProperty(PropertyName = "EnableBookUnitBuy")]
        public int EnableBookUnitBuy { get; set; }

        [JsonProperty(PropertyName = "Units")]
        public object Units { get; set; }

        [JsonProperty(PropertyName = "WholeSale")]
        public int WholeSale { get; set; }

        [JsonProperty(PropertyName = "TotalPrice")]
        public int TotalPrice { get; set; }

        [JsonProperty(PropertyName = "ret")]
        public int Ret { get; set; }

        [JsonProperty(PropertyName = "msg")]
        public string Msg { get; set; }

    }
}