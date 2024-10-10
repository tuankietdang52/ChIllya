using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Models.Mapper
{
    public class SongMapper : IMapper<FullTrack, Song>
    {
        public Song Map(FullTrack track)
        {
            StringBuilder sb = new();
            foreach (var artist in track.Artists)
            {
                sb.Append(artist.Name);
                sb.Append(", ");
            }

            sb.Remove(sb.Length - 2, 2);

            return new Song()
            {
                Title = $"{track.Name}",
                Name = $"{track.Name} - {sb}",
                Artists = track.Artists,
                SpotifyID = track.Id,
                Duration = track.DurationMs,
                DurationText = TimeSpan.FromMilliseconds(track.DurationMs),
                SpotifyUri = track.Uri,
            };
        }
    }
}
