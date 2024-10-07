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
using CommunityToolkit.Mvvm.ComponentModel;


namespace ChIllya.ViewModels
{
    public partial class DirectoryViewModel : ObservableObject, IViewModel
    {
        private readonly string windowsPath = "C:/Users/ADMIN/ChIllyaData/";
        private readonly string androidPath = "/storage/emulated/0/music shiba/";

        private readonly ILocalService _localService;

        public ObservableCollection<Song>? Songs { get; set;}

        // for loading view
        public bool isLoading = false;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                SetProperty(ref isLoading, value);
                IsHaveResult = !isLoading;
            }
        }

        // for list view
        // im just too lazy to do the convert :D 
        [ObservableProperty]
        public bool isHaveResult = false;

        public ICommand? ChooseCommand { get; set; }

        public DirectoryViewModel(ILocalService localService)
        {
            _localService = localService;
            LoadSongs();
            GenerateCommand();
        }

        private async void LoadSongs()
        {
            Songs = new();

            IsLoading = true;

            var task =  _localService.FetchSongOnDevice();

            // checking if task done first or the time out first
            var result = await Task.WhenAny(task, Task.Delay(40000));

            if (result != task)
            {
                PopUp.DisplayError("Time Out! Something wrong");
            }

            foreach (var song in task.Result)
            {
                Songs.Add(song);
            }

            IsLoading = false;
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
