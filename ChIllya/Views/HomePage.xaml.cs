using ChIllya.Services;
using ChIllya.Services.Implementations;
using ChIllya.Views.Popups;
using SpotifyAPI.Web;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace ChIllya.Views
{
	public partial class HomePage : ContentPage
	{
        public HomePage()
		{
			InitializeComponent();
			test();
		}

		private void test()
		{
			var spotify = new SpotifyAuthentication().CreateSpotifyClient();
			var service = new SpotifyService(spotify);

			var list = service.Search("Die For You");
        }
    }
}