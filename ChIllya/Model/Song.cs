using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Model
{
    public partial class Song : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string songPath;

        public Song()
        {
            Name = "";
            SongPath = "";
        }

        public Song(string name, string path)
        {
            Name = name;
            SongPath = path;
        }
    }
}
