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
using ChIllya.Utils;
using ChIllya.Views;
using ChIllya.Views.Popups;
using Swan;
using ChIllya.Services;
using ChIllya.Services.Implementations;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using CommunityToolkit.Maui.Core.Extensions;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ChIllya.ViewModels
{
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
                IsHaveResult = !isLoading;
            }
        }

        [ObservableProperty]
        private bool isHaveResult = false;

        public ICommand? ChooseCommand { get; set; }

        public DirectoryViewModel(ILocalService localService)
        {
            _localService = localService;
            GenerateCommand();

            LoadSongs();
        }

        private async void LoadSongs()
        {
            DisplaySongs = new();
            
            IsLoading = true;

            Songs = await _localService.FetchSongOnDevice();

            DisplaySongs.ResetTo(Songs);

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

        public void Searching(string query)
        {
            if (Songs is null || DisplaySongs is null)
            {
                PopUp.DisplayError("Cannot Load Songs on Device");
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
                temp = Songs!.FindAll(song => song.Name
                                                  .ToLower()
                                                  .Contains(query.Trim().ToLower()));

            }, cancellationTokenSource.Token);

            DisplaySongs?.ResetTo(temp);
            IsLoading = false;
        }

    }
}
