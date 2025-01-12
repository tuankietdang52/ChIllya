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
		SetBindingSearchResult();

		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

	public void SetBindingSearchResult()
	{
		var binding = new Binding()
		{
			Source = input,
			Path = "Text"
		};

		input.SetBinding(Entry.ReturnCommandParameterProperty, binding);
	}
}