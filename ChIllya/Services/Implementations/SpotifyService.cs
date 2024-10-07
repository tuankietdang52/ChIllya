using ChIllya.Models;
using ChIllya.Models.Mapper;
using ChIllya.Views.Popups;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Services.Implementations
{
    public class SpotifyService : ISpotifyService
    {
        private readonly ISpotifyClient client;

        public SpotifyService(ISpotifyClient client)
        {
            this.client = client;
        }

        public async Task<List<Song>> Search(string query)
        {
            List<Song> songs = [];
            SongMapper mapper = new();

            SearchRequest request;
            SearchResponse result;

            int count = 0;

            try
            {
                request = new(SearchRequest.Types.Track, query);
                result = await client.Search.Item(request);
            }
            catch (Exception ex)
            {
                PopUp.DisplayError(ex.Message);
                return songs;
            }

            // we need to supply mapper function to get the correct result we want
            // cause some endpoints contain have multiple paginations objects
            // and it will return the root which contain multiple paging object (album, track,...)
            await foreach (var track in client.Paginate(result.Tracks, (s) => s.Tracks))
            {
                if (count >= 50) break;

                Song song = mapper.Map(track);
                songs.Add(song);
                count++;
            }

            return songs;
        }
    }
}
