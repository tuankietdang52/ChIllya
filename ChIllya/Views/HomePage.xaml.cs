using ChIllya.Services;
using ChIllya.Services.Implementations;
using ChIllya.Views.Popups;
using CommunityToolkit.Maui.Views;
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
		}

		//private async void open(object sender, EventArgs args)
		//{
		//	var popupWindow = new DownloadProgressWindow();
		//	await Shell.Current.ShowPopupAsync(popupWindow);
		//}

    }
}