﻿using ChIllya.Models;
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
            try { return FetchingInformation(filePath); }
            catch { return null!; }
        }

        public string GenerateNameFile(Song song)
        {
            string name = song.Name;
            string artists = GetArtistNameText(song);

            if (artists == "") return $"{name}.mp3";

            return $"{name} - {artists}.mp3";
        }

        private Song FetchingInformation(string filePath)
        {
            var file = TagLib.File.Create(filePath);

            Song song = new()
            {
                Name = $"{GetNameFromPath(filePath)}",
                DirectoryPath = filePath,
                Duration = (int)file.Properties.Duration.TotalSeconds,
                DurationText = new TimeSpan(file.Properties.Duration.Ticks),
            };

            file.Dispose();
            return song;
        }

        public string GetArtistNameText(Song song)
        {
            List<string> artists = [];
            foreach (var artist in song.Artists)
            {
                artists.Add(artist.Name);
            }

            if (artists.Count == 0) return "";

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
