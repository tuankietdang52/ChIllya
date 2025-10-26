namespace ChIllya.Models
{
	public class Playlist
	{
		public string Name { get; set; } = "";
		private List<Song> Songs { get; set; } = [];

		public int SongCount => Songs.Count;

		public Playlist() { }

		public void AddSong(Song song)
		{
			Songs.Add(song);
		}

		public void RemoveSong(Song song) {
			Songs.Remove(song); 
		}

		public List<Song> GetSongs() => Songs;
	}
}
