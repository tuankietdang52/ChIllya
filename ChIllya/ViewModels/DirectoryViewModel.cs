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
            AndroidSongInit();
            GenerateCommand();
        }

        //test test
        private void WindowsSongInit()
        {
            Songs = new()
            {
                new Song($"{windowsPath}shinkai.mp3"),
                new Song($"{windowsPath}altair.mp3"),
                new Song($"{windowsPath}6H CHILL.mp3"),
                new Song($"{windowsPath}underbrightlight.mp3")
            };

            foreach (var song in Songs)
            {
                if (!song.IsLoadedSuccessfully) Songs.Remove(song);
            }
        }

        private void AndroidSongInit()
        {
            Songs = new()
            {
                new Song($"{androidPath}starlog.mp3")
            };

            //foreach (var song in Songs)
            //{
            //    if (!song.IsLoadedSuccessfully) Songs.Remove(song);
            //}
        }

        public void GenerateCommand()
        {
            ChooseCommand = new RelayCommand<Song>(DirectToSong);
        }

        private async void DirectToSong(Song? choice)
        {
            ArgumentNullException.ThrowIfNull(choice, nameof(choice));
            Song current = MusicManager.Instance!.Current;

            if (current == null || choice.SongPath != current.SongPath)
            {
                // User choose a song similar to the one currently playing
                await Shell.Current.Navigation.PushAsync(new SongPage(choice));
            }
            else await Shell.Current.Navigation.PushAsync(new SongPage());
        }
    }
}
