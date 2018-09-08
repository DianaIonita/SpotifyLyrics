# Spotify Lyrics
Spotify Lyrics is a Windows application which displays the lyrics to the currently playing Spotify song. It works by detecting the Spotify process and getting the currently playing song from the window title, then it looks up the lyrics in its providers.
Currently there is only one lyrics provider defined, `AZLyrics`.

## Build

Clone the repository and run the following command

```powershell
.\build.ps1
```

## Limitations
- Only works on Windows
- The UI is very bare-bones, '90s style. Feel free to raise PRs to improve it.
