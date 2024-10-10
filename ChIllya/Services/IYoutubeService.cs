using ChIllya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Services
{
    public interface IYoutubeService
    {
        // Download a song from youtube
        Task Download(Song song);
    }
}
