using ChIllya.Models;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ChIllya.Utils;
using ChIllya.Views;
using ChIllya.Views.Popups;
using ChIllya.Services;
using CommunityToolkit.Mvvm.ComponentModel;


namespace ChIllya.ViewModels
{
#pragma warning disable IDE0079
#pragma warning disable MVVMTK0045
	public partial class DirectoryViewModel : ObservableObject, IViewModel
    {
        private readonly string windowsPath = "C:/Users/ADMIN/ChIllyaData/";
        private readonly string androidPath = "/storage/emulated/0/music shiba/";

        private readonly ILocalService _localService;

        private CancellationTokenSource? cancellationTokenSource;

        private List<Song>? Songs;

        public BindableCollection<Song>? DisplaySongs { get; set; }
        
        public bool isLoading = false;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                SetProperty(ref isLoading, value);
                IsFree = !isLoading;
            }
        }

        [ObservableProperty]
        private bool isFree = false;

        public ICommand? TapCommand { get; set; }

        public DirectoryViewModel(ILocalService localService)
        {
            _localService = localService;
            GenerateCommand();

            LoadSongs();
        }

        private async void LoadSongs()
        {
            DisplaySongs = [];
            
            IsLoading = true;

            Songs = await _localService.FetchSongOnDevice();
            DisplaySongs.ResetTo(Songs);

            IsLoading = false;
        }

        public void GenerateCommand()
        {
            TapCommand = new RelayCommand<Song>(DirectToSong);
        }

        private async void DirectToSong(Song? choice)
        {
            if (choice == null)
            {
                WarningPopup.DisplayError("Song is null");
                return;
            }

            Song current = MusicManager.Instance!.Current;

            if (current == null || choice.DirectoryPath != current.DirectoryPath)
            {
                // User choose a song similar to the one currently playing
                await Shell.Current.Navigation.PushAsync(new SongPage(choice));
            }
            else await Shell.Current.Navigation.PushAsync(new SongPage());
        }

        public void Searching(string query)
        {
            if (Songs is null || DisplaySongs is null)
            {
                WarningPopup.DisplayError("Cannot Load Songs on Device");
                return;
            }

            MainThread.BeginInvokeOnMainThread(() => FilterSong(query));
        }

        private async void FilterSong(string query)
        {
            if (IsLoading) cancellationTokenSource?.Cancel();

            IsLoading = true;

            List<Song> temp = new();
            cancellationTokenSource = new();

            await Task.Run(() =>
            {
                temp = Songs!.FindAll(
                    song => song.Name
                                .Contains(query.Trim(), StringComparison.CurrentCultureIgnoreCase));

            }, cancellationTokenSource.Token);

            DisplaySongs?.ResetTo(temp);
            IsLoading = false;
        }
    }
#pragma warning restore MVVMTK0045
#pragma warning restore IDE0079
}
