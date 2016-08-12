using System;
using System.Threading.Tasks;
using moe.jixun.Plugin.Qidian.Entities;
using moe.Jixun.Plugin;
using H = moe.Jixun.Plugin.PluginHelper;

namespace moe.jixun.Plugin.Qidian.model
{
    public class QidianChapter: IBookChapter
    {
        private const string PublicChapterUrl = "http://ap.if.qidian.com/AjaxCache.aspx?opName=getchaptercontent&bookId={0}&chapterId={1}&sk=undefined&isbig5=0";
        public QidianChapter(QidianBookMeta book, string chapId, string chapName, bool isVip)
        {
            Book = book;
            ChapId = chapId;
            Name = chapName;
            IsVipChapter = isVip;
        }

        public bool IsVipChapter { get; set; }

        public string Name { get; }
        public string ChapId { get; }
        public QidianBookMeta Book;
        IBookMeta IBookChapter.Book => Book;
        public async Task<string> DownloadChapter()
        {
            if (IsVipChapter)
                return await DownloadVipChapter();

            return await DownloadPublicChapter();
        }

        public async Task<string> DownloadPublicChapter()
        {
            var url = string.Format(PublicChapterUrl, Book.BookId, ChapId);
            var json = await H.RequestAsync(url);
            var chapter = H.ParseJson<QidianPublicChapter>(json);
            var doc = await H.ParseHtml($"<div id='content'>{chapter.Content}</div>");

            var elements = doc.QuerySelectorAll("a");
            foreach (var element in elements)
                element.ParentElement.RemoveChild(element);

            return H.NodeToString(doc.GetElementById("content")).TrimEnd(';').TrimEnd();
        }

        public async Task<string> DownloadVipChapter()
        {
            await Task.Run(() => { });
            throw new NotImplementedException("未实现 Vip 章节下载。");
        }
    }
}