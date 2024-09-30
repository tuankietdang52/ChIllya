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
            return new Song()
            {
                Name = track.Name,
                Artists = track.Artists,
                SpotifyID = track.Id,
                Duration = track.DurationMs,
                SpotifyUri = track.Uri,
            };
        }
    }
}
