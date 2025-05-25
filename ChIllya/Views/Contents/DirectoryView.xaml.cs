using ChIllya.Utils;
using ChIllya.ViewModels;
using ChIllya.Views.Popups;

namespace ChIllya.Views.Contents
{
	public partial class DirectoryView : BaseView
	{
        private readonly DirectoryViewModel? _viewModel;
        private Action<string>? searching;

        public DirectoryView()
        {
            _viewModel = App.Instance!.ServiceProvider?.GetService<DirectoryViewModel>()!;

            if (_viewModel == null)
            {
                WarningPopup.DisplayError("Cannot get Download View Model");
                return;
            }

            InitializeComponent();
            BindingContext = _viewModel;
            SetupDebounce();
        }

		public DirectoryView(DirectoryViewModel viewModel)
		{
			InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;

            SetupDebounce();
		}

        private void SetupDebounce()
        {
            if (_viewModel == null) return;

            Action<string> action = _viewModel.Searching;
            searching = action.Debounce(500);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_viewModel == null) return;
            if (searching is null) return;

            if (_viewModel.DisplayPlaylists is null) return;
            searching(e.NewTextValue);
        }
    }
}