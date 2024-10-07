using ChIllya.Models;
using ChIllya.Views.Popups;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace ChIllya.Services.Implementations
{
    public partial class LocalService : ILocalService
    { 
        public LocalService()
        {
            
        }

#if ANDROID
        private partial List<string> GetData();
#else
        private partial List<string> GetData();
        private partial List<string> GetData()
        {
            //PopUp.DisplayError("Only work on Android");
            //throw new NotImplementedException();
            List<string> data = new List<string>();
            string root = "C:/";
            var result = Directory.EnumerateFiles(root, "*.mp3", SearchOption.AllDirectories);
            var enumerator = result.GetEnumerator();

            FindPoint:
            try
            {
                while (enumerator.MoveNext())
                {
                    string file = enumerator.Current;
                    data.Add(file);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                goto FindPoint;
            }

            return data;
        }
#endif

        private ObservableCollection<Song> GettingData()
        {
            var paths = GetData();
            ObservableCollection<Song> list = new();

            foreach (var path in paths)
            {
                Song song = new(path);
                if (song.IsLoadedSuccessfully) list.Add(song);
            }

            return list;
        }

        // running in task for not frozing app while fetching data
        public Task<ObservableCollection<Song>> FetchSongOnDevice()
        {
            return Task.Run(() => GettingData());
        }
    }
}