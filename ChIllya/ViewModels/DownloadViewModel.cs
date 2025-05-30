﻿using ChIllya.Models;
using ChIllya.Services;
using ChIllya.Utils;
using ChIllya.Views.Popups;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ChIllya.ViewModels
{
#pragma warning disable IDE0079
#pragma warning disable MVVMTK0045
	public partial class DownloadViewModel : ObservableObject, IViewModel
    {
        private readonly ISpotifyService _spotifyService;
        private readonly IYoutubeService _youtubeService;

        public BindableCollection<Song> Songs { get; set; }

		public bool isLoading = false;
		public bool IsLoading
		{
			get => isLoading;
			set => SetProperty(ref isLoading, value);
        }

        #region Command Field

        public ICommand? LookupCommand { get; set; }
        public ICommand? TapCommand { get; set; }

        #endregion

        public DownloadViewModel(ISpotifyService spotifyService, IYoutubeService youtubeService)
        {
            _spotifyService = spotifyService;
            _youtubeService = youtubeService;

            Songs = [];
            Initialize();
        }

        public void Initialize()
        {
            Songs = [];

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

            DownloadProgressWindow popupProgress = new();
            App.Instance!.GetMainPage()?.ShowPopup(popupProgress);

            try
            {
				await _youtubeService.Download(song, popupProgress.TrackingDownload,
				    popupProgress.Cts.Token);
			}
            catch (Exception ex)
            {
                WarningPopup.DisplayError(ex.Message);
            }

            popupProgress.Close();
        }
    }
#pragma warning restore MVVMTK0045
#pragma warning restore IDE0079
}
