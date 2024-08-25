using ChIllya.Model;
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


namespace ChIllya.ViewModel
{
    public class DirectoryViewModel
    {
        public ObservableCollection<Song> Songs { get; set;}

        public ICommand ChooseCommand { get; set; }

        public DirectoryViewModel()
        {
            Songs ??= new()
            {
                new Song("shinkai", "shinkai.mp3"),
                new Song("altair", "altair.mp3")
            };

            ChooseCommand = new RelayCommand<string>(PlayingSong);
        }

        private void PlayingSong(string? path)
        {
            MusicPlayer media = MusicPlayer.GetInstance();
            media.CreateMusic(path!);
        }
    }
}
