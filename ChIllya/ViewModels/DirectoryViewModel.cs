using ChIllya.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ChIllya.Error;
using ChIllya.Utils;
using ChIllya.Views;
using ChIllya.Views.Popups;
using Swan;
using ChIllya.Services;
using ChIllya.Services.Implementations;


namespace ChIllya.ViewModels
{
    public class DirectoryViewModel : IViewModel
    {
        private readonly string windowsPath = "C:/Users/ADMIN/ChIllyaData/";
        private readonly string androidPath = "/storage/emulated/0/music shiba/";

        public ObservableCollection<Song>? Songs { get; set;}

        public ICommand? ChooseCommand { get; set; }

        public DirectoryViewModel()
        {
            LoadSongs();
            GenerateCommand();
        }

        private async void LoadSongs()
        {
            Songs = new();
            ISongService songService = new SongService();

            await Shell.Current.Navigation.PushAsync(new LoadingPage());

            await songService.GetAllOnDevice(Songs);

            await Shell.Current.Navigation.PopAsync();
        }

        public void GenerateCommand()
        {
            ChooseCommand = new RelayCommand<Song>(DirectToSong);
        }

        private async void DirectToSong(Song? choice)
        {
            if (choice == null)
            {
                PopUp.DisplayError("Song is null");
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
}
