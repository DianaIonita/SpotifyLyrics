using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using SpotifyLyrics;
using SpotifyLyrics.Model;
using SpotifyLyrics.Providers;
using SpotifyLyrics.Providers.AzLyrics;

namespace SpotifyLyricsViewer
{
    public partial class SpotifyLyricsViewer : Form
    {
        private Spotify _spotify;
        private Song _currentlyPlaying;
        private readonly IEnumerable<IProvideLyrics> _lyricsProviders = new List<IProvideLyrics>
        {
            new AzLyricsProvider()
        };

        public SpotifyLyricsViewer()
        {
            InitializeComponent();
        }

        private void SpotifyLyricsViewer_Load(object sender, EventArgs e)
        {
            _spotify = new Spotify();
            UpdateStatusLabel(_spotify);
        }

        private async void MainTimer_Tick(object sender, EventArgs e)
        {
            UpdateStatusLabel(_spotify);
            UpdateNowPlayingLabel(_spotify);
            if (SongChanged())
            {
                await UpdateLyrics(_spotify);
            }
        }

        private bool SongChanged()
        {
            if (_currentlyPlaying == null)
            {
                _currentlyPlaying = _spotify.CurrentlyPlayingSong;
                return true;
            }
            if (_currentlyPlaying.Equals(_spotify.CurrentlyPlayingSong))
            {
                return false;
            }
            _currentlyPlaying = _spotify.CurrentlyPlayingSong;
            return true;
        }

        private async Task UpdateLyrics(Spotify spotify)
        {
            foreach(var provider in _lyricsProviders)
            {
                var lyrics = await provider.ForSong(spotify.CurrentlyPlayingSong);
                if (lyrics != null)
                {
                    lyricsTextBox.Text = lyrics;
                    return;
                }
            }
            lyricsTextBox.Text = "Lyrics not found";
        }

        private void UpdateNowPlayingLabel(Spotify spotify)
        {
            if (spotify.CurrentlyPlayingSong != null)
            {
                nowPlayingLabel.Text = spotify.CurrentlyPlayingSong.ToString();
            }
            else
            {
                nowPlayingLabel.Text = string.Empty;
            }
        }

        private void UpdateStatusLabel(Spotify spotify)
        {
            var labelText = "Spotify is " + spotify.Status;

            spotifyStatusLabel.Text = labelText;
        }
    }
}
