using ChIllya.Models;
using ChIllya.Utils;

namespace ChIllya.Music
{
    public delegate void CurrentSongChangedHandler(object sender, Song current);

    public class PlaylistController
    {
        private bool isShuffle = false;
        private bool isLoop = false;

        private readonly SongCollection songs = new();

        public event CurrentSongChangedHandler? CurrentSongChanged;

        public PlaylistController()
        {

        }

        public Song? GetCurrent() => songs.GetCurrent();
        public bool IsShuffle() => isShuffle;
        public bool IsLoop() => isLoop;

        public void OnCurrentSongChanged()
        {
            var current = songs.GetCurrent();
            if (current != null) CurrentSongChanged?.Invoke(this, current);
        }

        /// <summary>
        /// Shuffle the list and get the first song of the list
        /// </summary>
        /// <returns></returns>
        public Song? PrepareShuffle()
        {
            var song = songs.GetRandomSong();
            songs.Shuffle();

            return song;
        }

        public void SetShuffleMode(bool isShuffle)
        {
            this.isShuffle = isShuffle;
        }

        public void SetLoopMode(bool isLoop)
        {
            this.isLoop = isLoop;
        }

        public void SetCurrentSong(Song song)
        {
            if (songs.SetCurrent(song)) OnCurrentSongChanged();

            //TODO: Show error when fail to set current
        }

        public void SetPlaylist(Playlist playlist)
        {
            songs.Set(playlist.GetSongs());
            isShuffle = false;
            isLoop = false;
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
            if (!songs.HasSong()) return;
            if (songs.GetCurrent() is null) return;

            if (isShuffle)
            {
                songs.ActivateShuffle();
            }
            else songs.DeactivateShuffle();

            _ = GetAdjacentSong(adjacent);
            OnCurrentSongChanged();
        }

        private Song? GetAdjacentSong(EAdjacent adjacent)
        {
            return adjacent switch
            {
                EAdjacent.Previous => songs.GetPrevious(),
                EAdjacent.Next => songs.GetNext(),
                _ => null,
            };
        }
    }

    public enum EAdjacent
    {
        Next,
        Previous
    }
}
