using ChIllya.ViewModels;
using ChIllya.Views.Popups;

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