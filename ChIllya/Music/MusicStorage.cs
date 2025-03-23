using ChIllya.Models;
using ChIllya.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Music
{
    /// <summary>
    /// Storage for all song in device
    /// </summary>
    public class MusicStorage
    {
        private static MusicStorage? instance;
        public static MusicStorage Instance
        {
            get
            {
                instance ??= new MusicStorage();
                return instance;
            }
        }

        private ILocalService? _localService;

        private List<Song>? songs;
        private List<Playlist>? playlists;

        public MusicStorage()
        {
            Initialize();
            FetchSongs();
        }

        private void Initialize()
        {
            var app = (App)App.Current!;
            var services = app.ServiceProvider;
            _localService = services?.GetService<ILocalService>();
        }

        private async void FetchSongs()
        {
            if (_localService == null) return;

            songs = await _localService.FetchSongOnDevice();
            playlists = await _localService.OrganizeIntoPlaylist(songs);
        }

        public List<Song>? GetAllSongs() => songs;
        public List<Playlist>? GetAllPlaylists() => playlists;
    }
}
