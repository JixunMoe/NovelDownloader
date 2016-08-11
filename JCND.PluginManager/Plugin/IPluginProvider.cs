using System.Collections.Generic;
using System.Threading.Tasks;

namespace moe.Jixun.Plugin
{
    public interface IPluginProvider: IPluginBase
    {
        /// <summary>
        /// 搜索书籍
        /// </summary>
        /// <param name="bookName">书籍名称</param>
        /// <returns>
        /// <code>Chapters</code> 可以留空，如果不留空则获取最后的章节。
        /// </returns>
        Task<List<IBookMeta>> SearchBook(string bookName);

        /// <summary>
        /// 获取书籍的详细信息 (包括章节、分段信息)
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        Task<IBookMeta> GetBookMeta(string bookId);
    }
    
    public interface IBookMeta: IPluginData
    {
        IPluginProvider Plugin { get; }

        /// <summary>
        /// 书籍名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 书籍 ID
        /// </summary>
        string BookId { get; }
        
        /// <summary>
        /// 作者名称
        /// </summary>
        string Author { get; }

        /// <summary>
        /// 作者 ID，搜索作者时将传递该 ID。
        /// </summary>
        string AuthorId { get; }

        /// <summary>
        /// 章节列表，完整的。
        /// </summary>
        List<IBookChapter> Chapters { get; }

        /// <summary>
        /// 下载章节列表
        /// 下载后的结果应储存至 <see cref="Chapters"/> 内。
        /// </summary>
        Task DownloadChapterList();
    }

    public interface IBookChapter: IPluginData
    {
        /// <summary>
        /// 章节名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 章节 ID
        /// </summary>
        string ChapId { get; }

        /// <summary>
        /// 章节所在的书籍
        /// </summary>
        IBookMeta Book { get; }

        /// <summary>
        /// 下载章节并返回章节正文。
        /// </summary>
        /// <returns>章节正文</returns>
        Task<string> DownloadChapter();
    }
}
