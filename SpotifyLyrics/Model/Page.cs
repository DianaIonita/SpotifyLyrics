using System.IO;
using System.Net;
using System.Text;
using HtmlAgilityPack;

namespace SpotifyLyrics.Model
{
    internal abstract class Page
    {
        protected string Html;
        protected HtmlDocument Document;

        protected Page(string searchPageUrl)
        {
            var client = new WebClient();
            Html = client.DownloadString(searchPageUrl);
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