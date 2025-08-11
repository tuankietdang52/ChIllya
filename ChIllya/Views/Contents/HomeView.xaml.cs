using ChIllya.Services;
using ChIllya.Services.Implementations;
using ChIllya.ViewModels;
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
		private readonly HomeViewModel? _viewModel;

		public HomeView()
		{
			_viewModel = App.Instance!.ServiceProvider?.GetService<HomeViewModel>()!;

            if (_viewModel == null)
            {
                WarningPopup.DisplayError("Cannot get Download View Model");
                return;
            }

			InitializeComponent();
			BindingContext = _viewModel;
		}

		public HomeView(HomeViewModel viewModel)
		{
			InitializeComponent();
			_viewModel = viewModel;

			BindingContext = _viewModel;
		}
    }
}