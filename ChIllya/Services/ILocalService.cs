using ChIllya.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //Task<IList<Song>> Search(string query);
    }
}
