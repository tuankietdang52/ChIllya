using ChIllya.Models;
using ChIllya.Utils;
using ChIllya.Views;
using ChIllya.Views.Popups;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ICommand? TapCommand { get; set; }

        public PlaylistViewModel(Playlist playlist)
        {
            Current = playlist;

            LoadSongs();
            GenerateCommand();
        }
        
        public void GenerateCommand()
        {
            TapCommand = new RelayCommand<Song>(DirectToSong);
        }

        private async void LoadSongs()
        {
            IsLoading = true;
            await Task.Delay(500);

            DisplaySongs.ResetTo(Current.GetSongs());
            IsLoading = false;
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
    }
#pragma warning restore MVVMTK0045
#pragma warning restore IDE0079
}
