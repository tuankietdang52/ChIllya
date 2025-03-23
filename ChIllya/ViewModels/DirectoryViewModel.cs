using ChIllya.Models;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using ChIllya.Utils;
using ChIllya.Views;
using ChIllya.Views.Popups;
using ChIllya.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using ChIllya.Music;


namespace ChIllya.ViewModels
{
#pragma warning disable IDE0079
#pragma warning disable MVVMTK0045
	public partial class DirectoryViewModel : ObservableObject, IViewModel
    {
        //private readonly string windowsPath = "C:/Users/ADMIN/ChIllyaData/";
        //private readonly string androidPath = "/storage/emulated/0/music shiba/";

        private readonly ILocalService _localService;

        private CancellationTokenSource? cancellationTokenSource;

        private List<Playlist>? Playlists;
        public BindableCollection<Playlist>? DisplayPlaylists { get; set; }

        [ObservableProperty]        
        public bool isLoading = false;

        [ObservableProperty]
        private bool isHaveItem = false;

        public ICommand? TapCommand { get; set; }

        public DirectoryViewModel(ILocalService localService)
        {
            _localService = localService;
            Initialize();
        }

        public void Initialize()
        {
            TapCommand = new RelayCommand<Playlist>(DirectToPlaylistView!);
            LoadPlaylists();
        }

        private async void LoadPlaylists()
        {
            DisplayPlaylists = [];
            IsLoading = true;

            Playlists = MusicStorage.Instance.GetAllPlaylists();
            ArgumentNullException.ThrowIfNull(Playlists);

            await Task.Delay(700)
                      .ContinueWith(task => DisplayPlaylists.ResetTo(Playlists));

            IsHaveItem = DisplayPlaylists.Any();
            IsLoading = false;
        }

        private async void DirectToPlaylistView(Playlist playlist)
        {
            if (playlist == null)
            {
                WarningPopup.DisplayError("Playlist is null");
                return;
            }

            await Shell.Current.Navigation.PushAsync(new PlaylistView(playlist));
        }

        public void Searching(string query)
        {
            if (Playlists is null || DisplayPlaylists is null)
            {
                WarningPopup.DisplayError("Cannot Load Playlists on device");
                return;
            }

            FilterSong(query);
        }

        private async void FilterSong(string query)
        {
            if (DisplayPlaylists is null) return;
            if (IsLoading) cancellationTokenSource?.Cancel();

            IsLoading = true;

            List<Playlist> temp = [];
            cancellationTokenSource = new();

            await Task.Run(() =>
            {
                temp = Playlists!.FindAll(
                    playlist => playlist.Name
                                .Contains(query.Trim(), StringComparison.CurrentCultureIgnoreCase));

            }, cancellationTokenSource.Token);

            IsLoading = false;
            MainThread.BeginInvokeOnMainThread(() => DisplayPlaylists.ResetTo(temp));
        }
    }
#pragma warning restore MVVMTK0045
#pragma warning restore IDE0079
}
