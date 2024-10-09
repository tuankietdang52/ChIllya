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

        [ObservableProperty]
        private string name = "";

        [ObservableProperty]
        private string directoryPath = "";

        [ObservableProperty]
        private string artistName = "Unknown";

        private List<SimpleArtist> artists = [];
        public List<SimpleArtist> Artists {
            get => artists;
            set
            {
                artists = value;
                SetArtistNameText();
            } 
        }

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

        private void SetArtistNameText()
        {
            if (Artists.Count == 0) ArtistName = "Unknown";

            StringBuilder sb = new();
            foreach (var artist in Artists)
            {
                sb.Append(artist.Name + ", ");
            }

            sb.Remove(sb.Length - 2, 2);
            ArtistName = sb.ToString();
        }
    }
}
