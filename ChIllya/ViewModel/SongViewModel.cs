using ChIllya.Model;
using ChIllya.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChIllya.ViewModel
{
    public partial class SongViewModel : ObservableObject, IViewModel
    {
        [ObservableProperty]
        private Song? current;

        private MusicPlayer player => MusicPlayer.GetInstance();

        #region Play/Stop/Replay Image

        private string statusImage = "Images/pause.png";
        public string StatusImage
        {
            get => statusImage;
            set
            {
                SetProperty(ref statusImage, value);
                SetScaleImage(value);
            }
        }

        [ObservableProperty]
        private Microsoft.Maui.Thickness scaleImage = new(30, 0, 30, 0);

        #endregion

        #region Command
        public ICommand? BackCommand { get; set; }

        [ObservableProperty]
        private ICommand? playCommand;

        #endregion

        public SongViewModel() {
            GenerateCommand();
        }

        public void GenerateCommand()
        {
            BackCommand = new RelayCommand(async () =>
            {
                await Shell.Current.Navigation.PopAsync();
            });

            PlayCommand = new RelayCommand(StopSong);
        }

        public void StartSong(Song song)
        {
            Current = song;
            player.CreateMusic(Current.SongPath, MusicEnding);
        }

        private void PlaySong()
        {
            player.PlayMusic();
            PlayCommand = new RelayCommand(StopSong);
            StatusImage = "Images/pause.png";
        }

        private void StopSong()
        {
            player.StopMusic();
            PlayCommand = new RelayCommand(PlaySong);
            StatusImage = "Images/play.png";
        }

        private void MusicEnding(object? sender, EventArgs e)
        {
            PlayCommand = new RelayCommand(() =>
            {
                player.ReplayMusic();
                StatusImage = "Images/pause.png";
            });

            StatusImage = "Images/replay.png";
        }

        private void SetScaleImage(string path)
        {
            switch (path)
            {
                case "Images/play.png":
                    ScaleImage = new(30, 0, 20, 0);
                    break;

                case "Images/pause.png":
                    ScaleImage = new(30, 0, 30, 0);
                    break;

                case "Images/replay.png":
                    ScaleImage = new(30, 0, 30, 0);
                    break;

                default:
                    break;
            }
        }
    }
}
