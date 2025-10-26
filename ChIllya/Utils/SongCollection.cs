using ChIllya.Models;

namespace ChIllya.Utils
{
    public class SongCollection
    {
        private bool isShuffle = false;

        private List<Song> songs;

        private int current = -1;
        private int shuffleIndex = 0;

        private List<int> shuffleOrder = [];

        public SongCollection()
        {
            songs = [];
        }

        public SongCollection(List<Song> songs)
        {
            this.songs = songs;
        }

        public bool SetCurrent(Song song)
        {
            var value = songs.Select((v, i) => new { Index = i, Value = v })
                             .FirstOrDefault(item => item.Value.DirectoryPath == song.DirectoryPath);

            if (value is null) return false;

            current = value.Index;
            return true;
        }

        public void Set(List<Song> songs)
        {
            isShuffle = false;
            current = -1;
            shuffleIndex = 0;
            shuffleOrder = [];

            this.songs = songs;
        }

        public bool HasSong() => songs.Count != 0;

        public void Shuffle()
        {
            shuffleIndex = 0;
            isShuffle = true;

            List<int> list = [.. Enumerable.Range(0, songs.Count)];
            list.Remove(current);
            list.Shuffle();

            shuffleOrder = [current];
            shuffleOrder.AddRange(list);
        }

        /// <summary>
        /// Activate shuffle mode, do nothing when already activate
        /// </summary>
        public void ActivateShuffle()
        {
            if (isShuffle) return;
            Shuffle();
        }

        public void DeactivateShuffle()
        {
            shuffleIndex = 0;
            isShuffle = false;
            shuffleOrder.Clear();
        }

        private Song? GetSongOrNull(int index)
        {
            try
            {
                return songs[index];
            }
            catch
            {
                return null;
            }
        }

        public Song? GetRandomSong()
        {
            int min = 0, max = songs.Count - 1;
            Random rand = new();

            current = rand.Next(min, max);
            return GetSongOrNull(current);
        }

        public Song? GetNext()
        {
            if (!isShuffle)
            {
                current = current + 1 >= songs.Count ? 0 : current + 1;
            }
            else
            {
                shuffleIndex = shuffleIndex + 1 >= shuffleOrder.Count ? 0 : shuffleIndex + 1;
                current = shuffleOrder[shuffleIndex];
            }

            return GetSongOrNull(current);
        }

        public Song? GetPrevious()
        {
            if (!isShuffle)
            {
                current = current - 1 < 0 ? songs.Count - 1 : current - 1;
            }
            else
            {
                shuffleIndex = shuffleIndex - 1 < 0 ? shuffleOrder.Count - 1 : shuffleIndex - 1;
                current = shuffleOrder[shuffleIndex];
            }

            return GetSongOrNull(current);
        }

        public Song? GetCurrent()
        {
            return GetSongOrNull(current);
        }
    }
}