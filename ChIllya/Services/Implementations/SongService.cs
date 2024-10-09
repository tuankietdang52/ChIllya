using ChIllya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChIllya.Services.Implementations
{
    public class SongService : ISongService
    {
        public SongService()
        {

        }

        public Song? GetInformationFromFile(string filePath)
        {
            Song song;

            try
            {
                song = FetchingInformation(filePath);
            }
            catch
            {
                return null!;
            }

            return song;
        }

        private Song FetchingInformation(string filePath)
        {

            var file = TagLib.File.Create(filePath);

            Song song = new()
            {
                Name = GetNameFromPath(filePath),
                DirectoryPath = filePath,
                Duration = (int)file.Properties.Duration.TotalSeconds,
                ArtistName = GetArtistNameText(file.Tag.Performers),
                DurationText = new TimeSpan(file.Properties.Duration.Ticks),
            };

            file.Dispose();
            return song;
        }

        private string GetArtistNameText(string[] artists)
        {
            if (artists.Length == 0) return "Unknown";

            StringBuilder sb = new();
            foreach (var artist in artists)
            {
                sb.Append(artist + ", ");
            }

            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
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
