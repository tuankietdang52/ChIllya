using ChIllya.ViewModels;

namespace ChIllya.Views
{
	public partial class DirectoryPage : ContentPage
	{
		public DirectoryPage(DirectoryViewModel viewModel)
		{
			InitializeComponent();
			BindingContext = viewModel;
		}
	}
}