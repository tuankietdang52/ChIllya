using ChIllya.Models;
using ChIllya.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Music
{
    public delegate void CurrentSongChangedHandler(object sender, Song current);

    public class PlaylistController
    {
        private Song? current;
        
        private Playlist? playlist;
        private bool isRandom = false;

        private List<Song> randomSongs = [];

        public event CurrentSongChangedHandler? CurrentSongChanged;

        public PlaylistController()
        {

        }

        public Song? GetCurrent() => current;

        public void OnCurrentSongChanged()
        {
            if (current != null) CurrentSongChanged?.Invoke(this, current);
        }

        public void SetRandomMode(bool isRandom)
        {
            this.isRandom = isRandom;

            if (isRandom && playlist != null)
            {
                randomSongs = playlist.GetSongs();
                randomSongs.Shuffle();
            }
            else randomSongs.Clear();
        }

        public void SetCurrentSong(Song song)
        {
            current = song;
            OnCurrentSongChanged();
        }

        public void SetPlaylist(Playlist playlist)
        {
            this.playlist = playlist;
        }

        public void NextSong()
        {
            ToAdjacentSong(EAdjacent.Next);
        }

        public void PreviousSong()
        {
            ToAdjacentSong(EAdjacent.Previous);
        }

        private void ToAdjacentSong(EAdjacent adjacent)
        {
            if (playlist == null) return;
            var songs = isRandom ? randomSongs : playlist.GetSongs();

            if (songs == null) return;

            for (int i = 0; i < songs.Count; i++)
            {
                var song = songs[i];
                if (current != song) continue;

                current = GetAdjacentSong(adjacent, songs, i);
                break;
            }

            if (current == null) return;
            OnCurrentSongChanged();
        }

        private Song? GetAdjacentSong(EAdjacent adjacent, List<Song> songs, int i)
        {
            switch (adjacent)
            {
                case EAdjacent.Previous:
                    if (i - 1 < 0) return songs[^1];
                    else return songs[i - 1];

                case EAdjacent.Next:
                    if (i + 1 >= songs.Count) return songs[0];
                    else return songs[i + 1];

                default: 
                    return null;
            }
        }
    }

    public enum EAdjacent
    {
        Next,
        Previous
    }
}
