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
            if (query == "" || query is null) return [];

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
            catch (HttpRequestException ex)
            {
                WarningPopup.DisplayError($"{ex.Message}! Connect your Wifi and try again");
                return songs;
            }
            catch (Exception ex)
            {
                WarningPopup.DisplayError($"{ex.Message}");
                return songs;
            }

            /* 
             * We need to supply mapper function to get the correct result we want
             * cause some endpoints contain have multiple paginations objects
             * and it will return the root which contain multiple paging object (album, track,...)
             */
            try
            {
                await foreach (var track in client.Paginate(result.Tracks, (s) => s.Tracks))
				{
					if (count >= 50) break;

					Song song = mapper.Map(track);
					songs.Add(song);

					count++;
				}
            }
            catch (APIException)
            {
                return songs;
            }

            return songs;
        }
    }
}
