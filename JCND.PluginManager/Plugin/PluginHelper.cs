using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using Newtonsoft.Json;

namespace moe.Jixun.Plugin
{
    public class PluginHelper
    {
        private static readonly HtmlParser Parser;
        private static readonly Encoding GbkEncoding;

        static PluginHelper()
        {
            Parser = new HtmlParser(new HtmlParserOptions
            {
                IsScripting = false
            });
            GbkEncoding = Encoding.GetEncoding("gbk");
        }


        /// <summary>
        /// 解析 HTML
        /// </summary>
        /// <param name="html">从网页拉取的 HTML 文本。</param>
        /// <param name="baseUrl">拉取的页面来源。</param>
        /// <returns>解析对象，类似 jQuery。</returns>
        public static async Task<IDocument> ParseHtml(string html, string baseUrl = null)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                return await Parser.ParseAsync(html);

            var ctx = BrowsingContext.New();
            return await ctx.OpenAsync(res
                => res
                    // 强制 UTF-8 编码
                    // 否则页面声明了 GBK 会读不出内容。
                    .Header("Content-Type", "text/html; charset=UTF-8")
                    .Address(baseUrl)
                    .Content(html)
            );
        }


        /// <summary>
        /// 解析 JSON 字符串
        /// </summary>
        /// <typeparam name="T">解析后的类型，为 <code>class</code>，成员属性拥有 Set/Get 权限。</typeparam>
        /// <param name="json">待解析的 json 文本</param>
        /// <returns>解析后的类</returns>
        public static T ParseJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 发送请求 (UTF-8)
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="query">GET 请求参数</param>
        /// <param name="data">POST 请求数据</param>
        /// <param name="handler">处理层</param>
        /// <returns>抓取的网页数据</returns>
        public static async Task<string> RequestAsync(string url,
            Dictionary<string, string> query = null, 
            HttpContent data = null,
            HttpMessageHandler handler = null)
        {
            if (handler == null) handler = new DefaultPluginClientHandler();
            var client = new HttpClient(handler);
            url = BuildQueryString(url, query, Encoding.UTF8);

            if (data == null)
            {
                Debug.WriteLine($"GET(UTF8) to {url}");
                return await client.GetStringAsync(url);
            }

            Debug.WriteLine($"POST(UTF8) to {url} with {data}");
            var res = await client.PostAsync(url, data);
            return await res.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// 缓冲区大小，默认 4KB
        /// </summary>
        private const int BufferSize = 1024*4;

        /// <summary>
        /// 发送 GBK 请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="query">GET 请求参数</param>
        /// <param name="data">POST 数据</param>
        /// <param name="headers">额外的参数</param>
        /// <returns>抓取的网页数据</returns>
        public static async Task<string> RequestGbkAsync(string url,
            Dictionary<string, string> query = null,
            Dictionary<string, string> data  = null,
            NameValueCollection headers = null)
        {
            url = BuildQueryString(url, query, GbkEncoding);
            var req = (HttpWebRequest)WebRequest.Create(url);

            req.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            if (data == null)
            {
                req.Method = "GET";
                Debug.WriteLine($"GET(GBK) to {url}");
            }
            else
            {
                var postStr = DictParamToQueryString(data, GbkEncoding);
                Debug.WriteLine($"POST(GBK) to {url} with {postStr}");
                var postBin = Encoding.UTF8.GetBytes(postStr);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded;charset=gbk";
                req.ContentLength = postBin.Length;
                using (var reqStream = await req.GetRequestStreamAsync())
                {
                    await reqStream.WriteAsync(postBin, 0, postBin.Length);
                }
            }

            if (headers != null) req.Headers.Add(headers);

            using (var res = await req.GetResponseAsync())
            using (var stream = res.GetResponseStream())
            using (var memory = new MemoryStream())
            {
                if (stream == null)
                {
                    Debug.WriteLine("res.GetResponseStream got null.");
                    return "";
                }

                var buffer = new byte[BufferSize];
                while (stream.CanRead)
                {
                    var bytesRead = await stream.ReadAsync(buffer, 0, BufferSize);
                    if (bytesRead == 0) break;

                    await memory.WriteAsync(buffer, 0, bytesRead);
                }

                var actualSize = (int)memory.Position;
                memory.Seek(0, SeekOrigin.Begin);
                if (BufferSize < actualSize)
                    buffer = new byte[actualSize];
                await memory.ReadAsync(buffer, 0, actualSize);

                return GbkEncoding.GetString(buffer, 0, actualSize);
            }
        }

        /// <summary>
        /// 转换节点为纯文字。
        /// </summary>
        /// <param name="node">元素节点</param>
        /// <param name="digChild">挖掘子元素内容</param>
        /// <returns></returns>
        public static string NodeToString(INode node, bool digChild = true)
        {
            if (node.NodeType == NodeType.Text)
                return node.TextContent;

            if (node.NodeType == NodeType.Element)
            {
                var tag = node.NodeName.ToLowerInvariant();
                if (tag == "br") return "\n";
                if (tag == "p") return "\n" + node.TextContent;

                if (digChild)
                {
                    var sb = new StringBuilder();
                    foreach (var child in node.ChildNodes)
                    {
                        sb.Append(NodeToString(child, false));
                    }
                    return sb.ToString();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 构造添加了 QueryString 的新地址
        /// </summary>
        /// <param name="url">原始地址</param>
        /// <param name="query">请求参数</param>
        /// <param name="encoding">编码</param>
        /// <returns>新的访问地址</returns>
        private static string BuildQueryString(string url, Dictionary<string, string> query, Encoding encoding)
        {
            if (query != null)
            {
                if (url.Contains("?"))
                {
                    url += "&";
                }
                else
                {
                    url += "?";
                }

                url += DictParamToQueryString(query, encoding);
            }
            return url;
        }

        /// <summary>
        /// 编码 QueryString
        /// </summary>
        /// <param name="data">原始数据</param>
        /// <param name="encoding">编码</param>
        /// <returns>QueryString</returns>
        private static string DictParamToQueryString(Dictionary<string, string> data, Encoding encoding)
        {
            var paramsList = new List<string>(data.Count);

            paramsList.AddRange(data.Select(param =>
                HttpUtility.UrlEncode(param.Key, encoding) + "="
                + HttpUtility.UrlEncode(param.Value, encoding)));

            return string.Join("&", paramsList);
        }
    }

    public class DefaultPluginClientHandler : HttpClientHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            if (request.Headers.All(kv => kv.Key != "Referer"))
                request.Headers.Add("Referer", request.RequestUri.AbsoluteUri);

            if (request.Headers.All(kv => kv.Key != "User-Agent"))
                request.Headers.Add("User-Agent", "JCND 1.0 (.Net)");

            if (request.Headers.All(kv => kv.Key != "Accept"))
                request.Headers.Add("Accept", "*/*");

            return base.SendAsync(request, cancellationToken);
        }
    }
}