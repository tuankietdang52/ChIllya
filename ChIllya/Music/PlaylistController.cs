using ChIllya.Models;
using ChIllya.Utils;

namespace ChIllya.Music
{
    public delegate void CurrentSongChangedHandler(object sender, Song current);

    public class PlaylistController
    {
        private Song? current;
        
        private Playlist? playlist;
        private bool isShuffle = false;
        private bool isLoop = false;

        private List<Song> shuffleSongs = [];

        public event CurrentSongChangedHandler? CurrentSongChanged;

        public PlaylistController()
        {

        }

        public Song? GetCurrent() => current;
        public bool IsShuffle() => isShuffle;
        public bool IsLoop() => isLoop;

        public void OnCurrentSongChanged()
        {
            if (current != null) CurrentSongChanged?.Invoke(this, current);
        }

        public void SetShuffleMode(bool isShuffle)
        {
            this.isShuffle = isShuffle;
            if (!isShuffle || playlist == null) shuffleSongs.Clear();
        }

        public void SetLoopMode(bool isLoop)
        {
            this.isLoop = isLoop;
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

        public async Task NextSong()
        {
            await Task.Run(() => ToAdjacentSong(EAdjacent.Next));
        }

        public async Task PreviousSong()
        {
            await Task.Run(() => ToAdjacentSong(EAdjacent.Previous));
        }

        private void ToAdjacentSong(EAdjacent adjacent)
        {
            if (isLoop) return;
            if (playlist == null) return;

            if (isShuffle && shuffleSongs.IsEmpty()) OnShuffleSongs();

            var songs = isShuffle ? shuffleSongs : playlist.GetSongs();
            if (songs == null) return;

            for (int i = 0; i < songs.Count; i++)
            {
                var song = songs[i];

                if (current == null) return;
                if (current.DirectoryPath != song.DirectoryPath) continue;

                current = GetAdjacentSong(adjacent, songs, i);
                break;
            }

            if (current == null) return;
            OnCurrentSongChanged();
        }

        private void OnShuffleSongs()
        {
            if (playlist == null) return;

            shuffleSongs = new List<Song>(playlist.GetSongs());
            shuffleSongs.Shuffle();
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
