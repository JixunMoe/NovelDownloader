using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using moe.jixun.Plugin.Qidian.Entities;
using moe.jixun.Plugin.Qidian.model;
using moe.Jixun.Plugin;
using H = moe.Jixun.Plugin.PluginHelper;

namespace moe.jixun.Plugin.Qidian
{
    class QiDian: IPluginProvider
    {
        public QiDian()
        {
            if (Instance == null)
                Instance = this;
        }

        public string PackageName => "Plugin.QiDian";
        public PluginType Type => PluginType.SiteProvider;
        public string DisplayName => "起点中文网";
        internal static IPluginProvider Instance { get; set; }

        // &keyword=xxx
        private const string SearchUrl
            = "http://sosu.qidian.com/ajax/search.ashx?method=Search&range=&ranker=lastchapterupdatetime&n=30";

        public async Task<List<IBookMeta>> SearchBook(string bookName)
        {
            var query = new Dictionary<string, string>
            {
                ["keyword"] = bookName
            };

            var json = await H.RequestAsync(SearchUrl, query, null, new QidianSearchClientHandler());
            var results = H.ParseJson<QidianSearchResultEntity>(json);
            var books = results.Data.SearchResponseEntity.Books;

            return books
                .Select(book => new QidianBookMeta(book))
                .Cast<IBookMeta>()
                .ToList();
        }

        public Task<IBookMeta> GetBookMeta(string bookId)
        {
            throw new NotImplementedException();
        }
    }

    public class QidianSearchClientHandler : DefaultPluginClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Referer", "http://sosu.qidian.com/searchresult.aspx");
            return base.SendAsync(request, cancellationToken);
        }
    }
}
