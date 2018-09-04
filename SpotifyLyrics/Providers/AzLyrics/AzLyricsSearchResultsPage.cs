using System.Linq;
using SpotifyLyrics.Model;

namespace SpotifyLyrics.Providers.AzLyrics
{
    internal class AzLyricsSearchResultsPage : SearchResultsPage
    {
        public AzLyricsSearchResultsPage(string searchPageUrl)
            : base(searchPageUrl)
        {
        }

        public override bool HasResults
        {
            get
            {
                var noResultsDiv = Document.DocumentNode.SelectSingleNode("//div[@class='container main-page']//div[@class='alert alert-warning']");
                if (noResultsDiv == null && FirstResultUrl != null)
                {
                    return true;
                }
                return false;
            }
        }

        public override string FirstResultUrl
        {
            get
            {
                var anchor = Document.DocumentNode.SelectSingleNode("//div[@class='container main-page']//div[@class='panel']/table/tr/td[@class='text-left visitedlyr']/a");
                if (anchor != null)
                {
                    var url = anchor.Attributes.Single(a => a.Name == "href").Value;
                    return url;
                }
                return null;
            }
        }
    }
}