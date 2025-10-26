using ChIllya.Utils;
using SpotifyAPI.Web;
using System.Text;

namespace ChIllya.Models.Mapper
{
    public class SongMapper : IMapper<FullTrack, Song>
    {
        private string GetArtistsText(FullTrack track)
        {
            if (track.Artists.IsEmpty()) return "";

            StringBuilder sb = new();
            foreach (var artist in track.Artists)
            {
                sb.Append(artist.Name);
                sb.Append(", ");
            }

            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }

        public Song Map(FullTrack track)
        {
            string artists = GetArtistsText(track);
            string displayName = artists == "" ? track.Name : $"{track.Name} - {artists}";

            return new Song()
            {
                Title = $"{track.Name}",
                Name = displayName,
                Artists = track.Artists,
                SpotifyID = track.Id,
                Duration = track.DurationMs,
                DurationText = TimeSpan.FromMilliseconds(track.DurationMs),
                SpotifyUri = track.Uri,
            };
        }
    }
}
