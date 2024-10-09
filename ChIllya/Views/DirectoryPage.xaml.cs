using ChIllya.Utils;
using ChIllya.ViewModels;

namespace ChIllya.Views
{
	public partial class DirectoryPage : ContentPage
	{
        private readonly DirectoryViewModel _viewModel;
        private Action<string>? searching;

		public DirectoryPage(DirectoryViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = viewModel;
            _viewModel = viewModel;

            SetupDebounce();
		}

        private void SetupDebounce()
        {
            Action<string> action = _viewModel.Searching;
            searching = action.Debounce(500);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (searching is null) return;
            if (_viewModel.DisplaySongs is null) return;

            searching(e.NewTextValue);
        }
    }
}