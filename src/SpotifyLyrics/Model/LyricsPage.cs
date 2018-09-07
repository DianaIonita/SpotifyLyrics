namespace SpotifyLyrics.Model
{
    internal abstract class LyricsPage : Page
    {
        protected LyricsPage(string pageUrl) : base(pageUrl)
        {
        }

        public abstract string Lyrics { get; }
    }
}