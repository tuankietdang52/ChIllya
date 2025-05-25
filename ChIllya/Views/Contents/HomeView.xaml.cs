using ChIllya.Services;
using ChIllya.Services.Implementations;
using ChIllya.Views.Popups;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Platform;
using CommunityToolkit.Maui.Views;
using SpotifyAPI.Web;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace ChIllya.Views.Contents
{
	public partial class HomeView : BaseView
	{
        public HomeView()
		{
            InitializeComponent();
		}
    }
}