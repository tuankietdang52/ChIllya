using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Models
{
    public partial class Song : ObservableObject
    {
        [ObservableProperty]
        private string name = "";

        [ObservableProperty]
        private string songPath = "";

        public bool IsLoadedSuccessfully = false;

        [ObservableProperty]
        private double duration;

        [ObservableProperty]
        private TimeSpan durationText;

        public Song(string path)
        {
            SongPath = path;         
            LoadFile();
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
            var file = TagLib.File.Create(SongPath);
            Name = GetNameFromPath(SongPath);

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
