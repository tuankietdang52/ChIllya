using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.Maui.Audio;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Models
{
    public partial class Song : ObservableObject
    {
        #region Song Properties
        public string Title { get; set; }

        // file name and for display in UI
        [ObservableProperty]
        private string name = "";

        [ObservableProperty]
        private string directoryPath = "";
        public List<SimpleArtist> Artists { get; set; } = [];

        [ObservableProperty]
        private string spotifyID = "";

        [ObservableProperty]
        private string spotifyUri = "";

        [ObservableProperty]
        private double duration;

        [ObservableProperty]
        private TimeSpan durationText;

        #endregion

        public Song()
        {

        }
    }
}
