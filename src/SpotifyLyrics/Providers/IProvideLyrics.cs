using System.Threading.Tasks;
using SpotifyLyrics.Model;

namespace SpotifyLyrics.Providers
{
    public interface IProvideLyrics
    {
        Task<string> ForSong(Song song);
    }
}