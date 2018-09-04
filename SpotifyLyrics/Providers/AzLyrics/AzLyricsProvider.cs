using System;
using SpotifyLyrics.Model;

namespace SpotifyLyrics.Providers.AzLyrics
{
    public class AzLyricsProvider : IProvideLyrics
    {
        public string ForSong(Song song)
        {
            if (song == null)
            {
                return string.Empty;
            }
            var searchResultsPage = new AzLyricsSearchResultsPage(CreateSearchUrlFor(song));
            if (!searchResultsPage.HasResults)
            {
                return null;
            }
            var lyricsPage = new AzLyricsPage(searchResultsPage.FirstResultUrl);
            return lyricsPage.Lyrics;
        }

        private string CreateSearchUrlFor(Song song)
        {
            var artist = song.Artist.Replace(' ', '+');
            var songTitle = song.Title.Split(new string[] { " - " }, StringSplitOptions.None)[0].Replace(' ', '+');
            return "https://search.azlyrics.com/search.php?q=" + artist + '+' + songTitle;
        }
    }
}
