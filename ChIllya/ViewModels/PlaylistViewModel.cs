using ChIllya.Models;
using ChIllya.Utils;
using ChIllya.Views;
using ChIllya.Views.Popups;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ChIllya.Music;
using System.Windows.Input;

namespace ChIllya.ViewModels
{
#pragma warning disable IDE0079
#pragma warning disable MVVMTK0045
    public partial class PlaylistViewModel : ObservableObject, IViewModel
    {
        [ObservableProperty]
        private Playlist current;

        public BindableCollection<Song> DisplaySongs { get; private set; } = [];

        [ObservableProperty]
        public bool isLoading = false;

        private bool isBack = false;

        public ICommand? TapCommand { get; set; }
        public ICommand? PlayShuffleCommand { get; set; }
        public ICommand? BackCommand { get; set; }

        public PlaylistViewModel(Playlist playlist)
        {
            Current = playlist;
            Initialize();
        }

        public void Initialize()
        {
            TapCommand = new RelayCommand<Song>(DirectToSong);
            PlayShuffleCommand = new RelayCommand(PlayShuffle);
            BackCommand = new RelayCommand(OnBackButtonTap);

            LoadSongs();

            MusicManager manager = MusicManager.Instance!;
            if (manager == null) throw new NullReferenceException(nameof(manager));

            manager.SetPlaylist(Current);
        }

        private async void LoadSongs()
        {
            IsLoading = true;
            await Task.Delay(900)
                      .ContinueWith(task => DisplaySongs.ResetTo(Current.GetSongs()))
                      .ContinueWith(task => IsLoading = false);
        }

        private async void PlayShuffle()
        {
            MusicManager manager = MusicManager.Instance!;
            if (manager == null) throw new NullReferenceException(nameof(manager));

            if (!manager.PlayShuffle())
            {
                //TODO: Display Error
                WarningPopup.DisplayError("An error occur when shuffle songs");
                return;
            }

            var app = App.Instance!;
            await app.PushAsync(new SongPage()).ContinueWith(task => manager.UnpauseSong());
        }

        private async void DirectToSong(Song? choice)
        {
            if (choice == null)
            {
                WarningPopup.DisplayError("Song is null");
                return;
            }

            MusicManager manager = MusicManager.Instance!;
            if (manager == null) throw new NullReferenceException(nameof(manager));

            Song currentSong = manager.GetCurrentSong()!;
            var app = App.Instance!;

            if (currentSong == null || choice.DirectoryPath != currentSong.DirectoryPath)
            {
                manager.SetCurrentSong(choice);
                await app.PushAsync(new SongPage()).ContinueWith(task => manager.UnpauseSong());
            }
            // User choose a song similar to the one currently playing
            else await app.PushAsync(new SongPage());
        }

        private async void OnBackButtonTap()
        {
            if (isBack || IsLoading) return;

            isBack = true;
            await App.Instance!.PopAsync();
        }
    }
#pragma warning restore MVVMTK0045
#pragma warning restore IDE0079
}
