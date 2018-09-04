using SpotifyLyrics.Model;

namespace SpotifyLyrics.Providers
{
    public interface IProvideLyrics
    {
        string ForSong(Song song);
    }
}