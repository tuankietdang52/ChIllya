using ChIllya.Model;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChIllya.ViewModel
{
    public class DirectoryViewModel
    {
        public ObservableCollection<Song> Songs { get; set;}

        public ICommand ChooseCommand { get; set; }

        public DirectoryViewModel()
        {
            Songs ??= new();

            test();

            ChooseCommand = new RelayCommand(() =>
            {
                Debug.WriteLine("Playing");
            });
        }

        public void test()
        {
            Song song = new Song("shinkai", "/Assets/Songs/shinkai.mp3");
            Songs.Add(song);
        }
    }
}
