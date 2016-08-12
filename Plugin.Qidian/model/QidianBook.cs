using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using moe.jixun.Plugin.Qidian.Entities;
using moe.Jixun.Plugin;
using QueryDict = System.Collections.Generic.Dictionary<string, string>;
using H = moe.Jixun.Plugin.PluginHelper;

namespace moe.jixun.Plugin.Qidian.model
{
    public class QidianBookMeta: IBookMeta
    {
        private const string BookInfoUrl = "http://3g.if.qidian.com/Client/IGetBookInfo.aspx";
        public static async Task<QidianBookMeta> FromBookId(string bookId)
        {
            return await FromBookId(new QidianBookMeta(), bookId);
        }

        internal static async Task<QidianBookMeta> FromBookId(QidianBookMeta instance, string bookId)
        {
            // 从起点抓取数据
            var query = new QueryDict()
            {
                ["bookId"] = bookId
            };

            var json = await H.RequestAsync(BookInfoUrl, query);
            var book = H.ParseJson<QidianBookInfo>(json);

            instance.Name = book.BookName;
            instance.BookId = book.BookId.ToString();
            instance.BookUrl = $"book-{book.BookId}@qidian";

            instance.Author = book.Author;
            instance.AuthorId = $"author-{book.Author}@qidian";

            // 读取章节信息
            // TODO: 添加 Vip 章节支持
            var chapters = book.Chapters
                .Where(chapter => chapter.IsVipChapter == 0)
                .Select(chapter => new QidianChapter(instance, chapter.ChapterId.ToString(), chapter.ChapterName, false))
                .ToList();

            instance.Chapters.Clear();
            instance.Chapters.AddRange(chapters);

            return instance;
        }

        private QidianBookMeta()
        {
            Chapters = new List<IBookChapter>();
        }

        public QidianBookMeta(BookEntity book): this()
        {
            Name = book.BookName;
            BookId = book.BookId;
            BookUrl = book.BookUrl;

            Author = book.AuthorName;
            AuthorId = book.AuthorId;
        }

        public IPluginProvider Plugin => QiDian.Instance;
        public string Name { get; private set; }
        public string BookId { get; private set; }
        internal string BookUrl { get; private set; }


        public string Author { get; private set; }
        public string AuthorId { get; private set; }
        public List<IBookChapter> Chapters { get; }

        public async Task DownloadChapterList()
        {
            await FromBookId(this, BookId);
        }
    }
}