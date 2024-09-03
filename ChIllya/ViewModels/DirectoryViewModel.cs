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
        public ObservableCollection<Song> Songs { get; set;}

        public ICommand? ChooseCommand { get; set; }

        public DirectoryViewModel()
        {
            Songs = new()
            {
                new Song("shinkai", "shinkai.mp3"),
                new Song("altair", "altair.mp3"),
                new Song("6h chill", "6H CHILL.mp3"),
                new Song("Under Bright Light", "underbrightlight.mp3")
            };

            GenerateCommand();
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
