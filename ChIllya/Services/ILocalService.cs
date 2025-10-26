using ChIllya.Models;

namespace ChIllya.Services
{
    /// <summary>
    /// Provide a service to handle device-related tasks for songs
    /// </summary>
    public interface ILocalService
    {
        /// <summary>
        /// Get all song on Device
        /// </summary>
        /// <returns></returns>
        Task<List<Song>> FetchSongOnDevice();
        Task<List<Playlist>> FetchPlaylistOnDevice();
        Task<List<Playlist>> OrganizeIntoPlaylist(IEnumerable<Song> list);
    }
}
