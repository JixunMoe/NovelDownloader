using System.Collections.Generic;
using System.Threading.Tasks;
using moe.Jixun.model;
using moe.Jixun.Plugin;

namespace moe.Jixun.Dummies
{
    internal class DummyBookMeta : IBookMeta
    {
        private static int _counter;
        public DummyBookMeta()
        {
            _counter++;

            Name = $"小说 {_counter}";
            BookId = $"book_{_counter}";
            Author = "Jixun";
            AuthorId = "jixun";
        }

        public IPluginProvider Plugin { get; }
        public string Name { get; }
        public string BookId { get; }
        public string Author { get; }
        public string AuthorId { get; }
        public List<IBookChapter> Chapters { get; private set; }
        public async Task DownloadChapterList()
        {
            Chapters = new List<IBookChapter>();
            for (var i = 1; i < 15; i++)
            {
                Chapters.Add(new DummyBookChapter($"第 {i} 章", this));
            }
        }
    }

    internal class DummyBookEntity : BookEntity
    {
        public override string Site { get; } = "测试数据";

        public DummyBookEntity() : base(new DummyBookMeta())
        {

        }
    }

    internal class DummyBookWithChapter : BookEntity
    {
        public override string Site { get; } = "测试数据";
        public sealed override List<ChapterEntity> Chapters { get; }


        public DummyBookWithChapter() : base(new DummyBookMeta())
        {
            Chapters = new List<ChapterEntity>();
            for (var i = 1; i < 15; i++)
            {
                Chapters.Add(new DummyBookChapterEntity(this, $"第 {i} 章"));
            }
        }
    }

    internal class DummyBookChapter: IBookChapter
    {
        public DummyBookChapter(string name, IBookMeta book)
        {
            Name = name;
            Book = book;
        }

        public string Name { get; }
        public IBookMeta Book { get; }
        public string Download()
        {
            throw new System.NotImplementedException();
        }
    }

    internal class DummyBookChapterEntity : ChapterEntity
    {
        private readonly BookEntity _book;
        public DummyBookChapterEntity(BookEntity book, string name)
        {
            _book = book;

            SetChapter(new DummyBookChapter(name, book.BookMeta));
        }
    }
}