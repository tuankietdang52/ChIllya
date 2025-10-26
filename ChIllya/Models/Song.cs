using CommunityToolkit.Mvvm.ComponentModel;
using SpotifyAPI.Web;

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

        [ObservableProperty]
        private bool isPlaying = false;

        public Song()
        {

        }
    }
#pragma warning restore MVVMTK0045
#pragma warning restore IDE0079
}
