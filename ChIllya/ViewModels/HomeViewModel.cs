using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChIllya.Models;
using ChIllya.Music;
using ChIllya.Utils;
using ChIllya.Views.Popups;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ChIllya.ViewModels
{
    public class HomeViewModel : ObservableObject, IViewModel
    {
        private List<Playlist>? playlists;

        public BindableCollection<Playlist>? DisplayPlaylists { get; set; }

        public HomeViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            LoadPlaylists();
        }

        private async void LoadPlaylists()
        {
            DisplayPlaylists = [];
            playlists = MusicStorage.Instance.GetAllPlaylists();
            
            if (playlists is null)
            {
                WarningPopup.DisplayError("Cannot fetch playlist in device");
                return;
            }

            await Task.Delay(700)
                      .ContinueWith(task => DisplayPlaylists.ResetTo(playlists));
        }
    }
}