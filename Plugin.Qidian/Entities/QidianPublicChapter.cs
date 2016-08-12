using Newtonsoft.Json;

namespace moe.jixun.Plugin.Qidian.Entities
{
    public class PublicChapterContent
    {
        [JsonProperty(PropertyName = "ChapterId")]
        public int ChapterId { get; set; }

        [JsonProperty(PropertyName = "ChapterName")]
        public string ChapterName { get; set; }

        [JsonProperty(PropertyName = "BookName")]
        public string BookName { get; set; }

        [JsonProperty(PropertyName = "AuthorId")]
        public object AuthorId { get; set; }

        [JsonProperty(PropertyName = "AuthorName")]
        public object AuthorName { get; set; }

        [JsonProperty(PropertyName = "LastUpdateTime")]
        public string LastUpdateTime { get; set; }

        [JsonProperty(PropertyName = "PreChapterId")]
        public int PreChapterId { get; set; }

        [JsonProperty(PropertyName = "NextChapterId")]
        public int NextChapterId { get; set; }

        [JsonProperty(PropertyName = "ChapterWordsCount")]
        public int ChapterWordsCount { get; set; }

    }

    public class QidianPublicChapter
    {
        [JsonProperty(PropertyName = "code")]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "isJump")]
        public bool IsJump { get; set; }

        [JsonProperty(PropertyName = "book")]
        public object Book { get; set; }

        [JsonProperty(PropertyName = "chapter")]
        public PublicChapterContent Chapter { get; set; }

        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "firstVipchapterId")]
        public int FirstVipChapterId { get; set; }

        [JsonProperty(PropertyName = "isbuy")]
        public int Isbuy { get; set; }

        [JsonProperty(PropertyName = "qRCode")]
        public string QrCode { get; set; }

    }
}