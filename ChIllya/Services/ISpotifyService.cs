using ChIllya.Models;

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
