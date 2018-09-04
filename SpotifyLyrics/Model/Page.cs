using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SpotifyLyrics.Model
{
    internal abstract class Page
    {
        private readonly string _pageUrl;
        protected string Html;
        protected HtmlDocument Document;

        protected Page(string pageUrl)
        {
            _pageUrl = pageUrl;
        }

        public async Task Load()
        {
            var client = new WebClient();
            Html = await client.DownloadStringTaskAsync(_pageUrl);
            client.Dispose();
            Document = HtmlDocumentFromString(Html);
        }

        private HtmlDocument HtmlDocumentFromString(string html)
        {
            var doc = new HtmlDocument
            {
                OptionFixNestedTags = true,
                OptionCheckSyntax = true,
                OptionAutoCloseOnEnd = true
            };
            doc.Load(new MemoryStream(Encoding.UTF8.GetBytes(html ?? "")));
            return doc;
        }
    }
}