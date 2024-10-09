using ChIllya.Utils;
using ChIllya.ViewModels;
using System.Diagnostics;

namespace ChIllya.Views;

public partial class SpotifySearchPage : ContentPage
{
	private readonly SpotifySearchViewModel _viewModel;

	public SpotifySearchPage(SpotifySearchViewModel viewModel)
	{
		InitializeComponent();

		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}