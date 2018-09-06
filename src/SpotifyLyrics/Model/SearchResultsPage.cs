namespace SpotifyLyrics.Model
{
    internal abstract class SearchResultsPage : Page
    {
        protected SearchResultsPage(string pageUrl) : base(pageUrl)
        {
        }

        public abstract bool HasResults { get; }
        public abstract string FirstResultUrl { get; }

    }
}