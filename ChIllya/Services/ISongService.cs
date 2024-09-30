using ChIllya.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Services
{
    public interface ISongService
    {
        Task<List<Song>> GetAllOnDevice();
        Task GetAllOnDevice(Collection<Song> list);
    }
}
