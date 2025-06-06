﻿using CommunityToolkit.Mvvm.ComponentModel;
using Plugin.Maui.Audio;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya.Models
{
#pragma warning disable IDE0079
#pragma warning disable MVVMTK0045
	public partial class Song : ObservableObject
    {
        #region Song Properties
        public string Title { get; set; } = "";

        // file name and for display in UI
        [ObservableProperty]
        private string name = "";

        [ObservableProperty]
        private string directoryPath = "";
        public List<SimpleArtist> Artists { get; set; } = [];

        [ObservableProperty]
        private string spotifyID = "";

        [ObservableProperty]
        private string spotifyUri = "";

        [ObservableProperty]
        private double duration;

        [ObservableProperty]
        private TimeSpan durationText;

        public string Folder { get; set; } = "";

        #endregion

        public Song()
        {
            
        }
    }
#pragma warning restore MVVMTK0045
#pragma warning restore IDE0079
}
