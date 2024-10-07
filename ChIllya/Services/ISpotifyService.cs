using ChIllya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Services
{
    public interface ISpotifyService
    {
        /// <summary>
        /// Searching a song on Spotify
        /// </summary>
        /// <param name="query">Name of the song</param>
        /// <returns>List of result</returns>
        Task<List<Song>> Search(string query);
    }
}
