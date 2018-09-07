using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using SpotifyLyrics.Model;

namespace SpotifyLyrics
{
    public class Spotify
    {
        public Process SpotifyProcess { get; private set; }
        private HashSet<int> _children;
        public string WindowName { get; private set; }

        private readonly Timer _refreshTimer;


        public Spotify()
        {
            _refreshTimer = new Timer((e) =>
            {
                if (IsRunning())
                {
                    WindowName = SpotifyProcess.MainWindowTitle;
                }
                else
                {
                    ClearHooks();
                    HookSpotify();
                }
            }, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(500));
        }

        public bool IsRunning()
        {
            if (SpotifyProcess == null)
                return false;

            SpotifyProcess.Refresh();
            return !SpotifyProcess.HasExited;
        }

        public bool IsPaused()
        {
            if (IsRunning() && CurrentlyPlayingSong == null)
            {
                return true;
            }
            return false;
        }

        public SpotifyStatus Status
        {
            get
            {
                if (IsRunning())
                {
                    if (IsPaused())
                    {
                        return SpotifyStatus.Paused;
                    }
                    else
                    {
                        return SpotifyStatus.Playing;
                    }
                }
                return SpotifyStatus.NotRunning;
            }
        }

        public Song CurrentlyPlayingSong
        {
            get
            {
                if (GetArtist() != null && GetSongTitle() != null)
                {
                    return new Song
                    {
                        Artist = GetArtist(),
                        Title = GetSongTitle(),
                    };
                }
                return null;
            }
        }

        public string GetArtist()
        {
            if (IsRunning() && WindowName.Contains(" - "))
            {
                return WindowName.Split(new[] { " - " }, StringSplitOptions.None)[0];
            }

            return null;
        }

        public string GetSongTitle()
        {
            if (IsRunning() && WindowName.Contains(" - "))
            {
                return String.Join(" - ", WindowName.Split(new[] { " - " }, StringSplitOptions.None).Skip(1).ToArray());
            }

            return null;
        }

        private void ClearHooks()
        {
            SpotifyProcess = null;
            WindowName = "";
        }

        private bool HookSpotify()
        {
            _children = new HashSet<int>();

            foreach (Process p in Process.GetProcessesByName("spotify"))
            {
                _children.Add(p.Id);
                SpotifyProcess = p;
                if (p.MainWindowTitle.Length > 1)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
