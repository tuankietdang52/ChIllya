using ChIllya.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChIllya.Services
{
    public interface ISongService
    {
        /// <summary>
        /// Get song file information
        /// </summary>
        /// <returns>Song object with information, null if fail to get information</returns>
        Song? GetInformationFromFile(string filePath);
    }
}
