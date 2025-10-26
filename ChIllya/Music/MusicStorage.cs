using ChIllya.Models;
using ChIllya.Services;

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
            var services = App.Instance!.ServiceProvider;
            _localService = services?.GetService<ILocalService>();
        }

        private void FetchSongs()
        {
            if (_localService == null) return;

            Task.Run(async () =>
            {
                songs = await _localService.FetchSongOnDevice();
                playlists = await _localService.OrganizeIntoPlaylist(songs);
            }).Wait();
        }

        public List<Song>? GetAllSongs() => songs;
        public List<Playlist>? GetAllPlaylists() => playlists;
    }
}
