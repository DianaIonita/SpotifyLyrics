using SpotifyLyrics.Model;

namespace SpotifyLyrics.Providers.AzLyrics
{
    internal class AzLyricsPage : LyricsPage
    {
        public AzLyricsPage(string pageUrl) : base(pageUrl)
        {
        }

        public override string Lyrics
        {
            get
            {
                var htmlLyrics = Document.DocumentNode.SelectSingleNode("//div[@class='container main-page']//div[@class='col-xs-12 col-lg-8 text-center']/div[5]").InnerHtml;
                var parsed = Parse(htmlLyrics);
                return parsed;
            }
        }

        private string Parse(string htmlLyrics)
        {
            var lyrics = htmlLyrics.Replace(
                "<!-- Usage of azlyrics.com content by any third-party lyrics provider is prohibited by our licensing agreement. Sorry about that. -->",
                "").Replace("<br>", "\r\n");

            return lyrics;
        }
    }
}