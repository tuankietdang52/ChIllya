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
        private string artistName = "None";

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

        public bool IsLoadedSuccessfully = false;

        public Song()
        {

        }

        public Song(string path)
        {
            DirectoryPath = path;
            LoadFile();
        }

        private void SetArtistNameText()
        {
            if (Artists.Count == 0) ArtistName = "None";

            StringBuilder sb = new();
            foreach (var artist in Artists)
            {
                sb.Append(artist.Name + ", ");
            }

            sb.Remove(sb.Length - 2, 2);
            ArtistName = sb.ToString();
        }

        private void LoadFile()
        {
            try
            {
                LoadInformation();
                IsLoadedSuccessfully = true;
            }
            catch
            {
                IsLoadedSuccessfully = false;
            }
        }

        private void LoadInformation()
        {
            var file = TagLib.File.Create(DirectoryPath);
            Name = GetNameFromPath(DirectoryPath);

            Duration = (int) file.Properties.Duration.TotalSeconds;
            DurationText = new TimeSpan(file.Properties.Duration.Ticks);

            file.Dispose();
        }

        private string GetNameFromPath(string path)
        {
            char[] filter = ['/', '.'];
            string[] words = path.Split(filter);

            int length = words.Length;

            return words[length - 2];
        }
    }
}
