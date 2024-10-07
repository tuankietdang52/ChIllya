using ChIllya.ViewModels;

namespace ChIllya.Views;

public partial class LocalSearchPage : ContentPage
{
	private readonly LocalSearchViewModel _viewModel;

	public LocalSearchPage(LocalSearchViewModel viewModel)
	{
		InitializeComponent();

		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}