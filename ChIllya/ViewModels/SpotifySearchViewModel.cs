using ChIllya.Models;
using ChIllya.Services;
using ChIllya.Services.Implementations;
using ChIllya.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChIllya.ViewModels
{
    public partial class SpotifySearchViewModel : ObservableObject, IViewModel
    {
        private readonly ISpotifyService _spotifyService;
        private readonly IYoutubeService _youtubeService;

        public BindableCollection<Song> Songs { get; set; }

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

        #region Command Field

        public ICommand? LookupCommand { get; set; }
        public ICommand? TapCommand { get; set; }

        #endregion

        public SpotifySearchViewModel(ISpotifyService spotifyService, IYoutubeService youtubeService)
        {
            _spotifyService = spotifyService;
            _youtubeService = youtubeService;

            Songs = new();
            GenerateCommand();
        }

        public void GenerateCommand()
        {
            LookupCommand = new RelayCommand<string>(Searching!);
            TapCommand = new RelayCommand<Song>(DownloadMusic!);
        }

        public async void Searching(string query)
        {
            IsLoading = true;

            var result = await _spotifyService.Search(query);
            Songs.ResetTo(result);

            IsLoading = false;
        }

        public async void DownloadMusic(Song song)
        {
            if (song is null) return;

            await _youtubeService.Download(song);
        }
    }
}
