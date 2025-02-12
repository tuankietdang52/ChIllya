using ChIllya.Models;
using ChIllya.Views.Popups;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace ChIllya.Services.Implementations
{
    public partial class LocalService : ILocalService
    {
        private readonly ISongService _songService;

        public LocalService(ISongService songService)
        {
            this._songService = songService;
        }

#if ANDROID
        private partial List<string> GetData();
#else
        private partial List<string> GetData();
        private partial List<string> GetData()
        {
            //PopUp.DisplayError("Only work on Android");
            //throw new NotImplementedException();
        //    List<string> data = new List<string>();
        //    string root = "C:/";
        //    var result = Directory.EnumerateFiles(root, "*.mp3", SearchOption.AllDirectories);
        //    var enumerator = result.GetEnumerator();

        //FindPoint:
        //    try
        //    {
        //        while (enumerator.MoveNext())
        //        {
        //            string file = enumerator.Current;
        //            data.Add(file);
        //        }
        //    }
        //    catch (UnauthorizedAccessException ex)
        //    {
        //        goto FindPoint;
        //    }

        //    return data;

            return
            [
                "C:/Users/ADMIN/ChIllyaData/shinkai.mp3"
            ];
        }
#endif

        private List<Song> FetchingSongs()
        {
            var paths = GetData();
            List<Song> list = [];

            foreach (var path in paths)
            {
                Song? song = _songService.GetInformationFromFile(path);
                if (song is not null) list.Add(song);
            }

            return list;
        }

        // running in task for not frozing app while fetching data
        public async Task<List<Song>> FetchSongOnDevice()
        {
            var task = Task.Factory.StartNew(() => FetchingSongs());

            // time out is 10 second
            var result = await Task.WhenAny(task, Task.Delay(10000));

            if (result != task)
            {
                WarningPopup.DisplayError("Time Out! Something wrong");
                return [];
            }

            return task.Result;
        }

        public async Task<List<Playlist>> FetchPlaylistOnDevice()
        {
            Dictionary<string, Playlist> playlists = [];

			var songs = await FetchSongOnDevice();

            foreach (var song in songs)
            {
                string folderName = song.Folder;

                if (!playlists.TryGetValue(folderName, out Playlist? playlist))
                {
					playlist = new Playlist()
                    {
                        Name = folderName
                    };
					playlists[folderName] = playlist;
                }

				playlist.AddSong(song);
            }

            return new List<Playlist>(playlists.Values);
		}
    }
}