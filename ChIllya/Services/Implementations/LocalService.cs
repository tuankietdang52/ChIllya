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

        private List<Song> GettingData()
        {
            var paths = GetData();
            List<Song> list = new();

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
            var task = Task.Factory.StartNew(() => GettingData());

            // checking if task done first or the time out first
            var result = await Task.WhenAny(task, Task.Delay(10000));

            if (result != task)
            {
                PopUp.DisplayError("Time Out! Something wrong");
                return [];
            }

            return task.Result;
        }
    }
}