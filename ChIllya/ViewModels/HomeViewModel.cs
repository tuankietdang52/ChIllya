using System.Windows.Input;
using ChIllya.Models;
using ChIllya.Music;
using ChIllya.Utils;
using ChIllya.Views;
using ChIllya.Views.Contents;
using ChIllya.Views.Popups;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChIllya.ViewModels
{
    public class HomeViewModel : ObservableObject, IViewModel
    {
        private List<Playlist>? playlists;

        public BindableCollection<Playlist>? DisplayPlaylists { get; set; }

        public ICommand? DownloadNavigate { get; set; }

        public HomeViewModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            DownloadNavigate = new RelayCommand(ToDownloadPage);
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

        private void ToDownloadPage() {
            MainPage.Instance!.PushContent(new DownloadView());
        }
    }
}