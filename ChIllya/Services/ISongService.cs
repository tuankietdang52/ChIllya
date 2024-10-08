﻿using ChIllya.Models;
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

        /// <summary>
        /// Get artists name
        /// </summary>
        /// <param name="song"></param>
        /// <returns>Return a string with artists name seperate by ,</returns>
        string GetArtistNameText(Song song);

        /// <summary>
        /// Generate a name for mp3 file
        /// </summary>
        /// <param name="song"></param>
        /// <returns>Name file with artists (if any)</returns>
        string GenerateNameFile(Song song);
    }
}
