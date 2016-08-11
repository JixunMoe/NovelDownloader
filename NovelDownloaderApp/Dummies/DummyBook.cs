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

        public DummyBookMeta(bool addChapters): this()
        {
            if (addChapters)
                Chapters = new List<IBookChapter>();
        }

        public IPluginProvider Plugin { get; }
        public string Name { get; }
        public string BookId { get; }
        public string Author { get; }
        public string AuthorId { get; }
        public List<IBookChapter> Chapters { get; private set; }
        public async Task DownloadChapterList()
        {
            await Task.Run(() => { });

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

        public DummyBookWithChapter() : base(new DummyBookMeta(true))
        {
            for (var i = 1; i < 15; i++)
            {
                BookMeta.Chapters.Add(new DummyBookChapter($"第 {i} 章", BookMeta));
            }

            UpdateChapters();
        }
    }

    internal class DummyBookChapter: IBookChapter
    {
        public DummyBookChapter(string name, IBookMeta book)
        {
            Name = name;
            Book = book;
            ChapId = name;
        }

        public string Name { get; }
        public string ChapId { get; }
        public IBookMeta Book { get; }
        public Task<string> DownloadChapter()
        {
            throw new System.NotImplementedException();
        }
    }
}