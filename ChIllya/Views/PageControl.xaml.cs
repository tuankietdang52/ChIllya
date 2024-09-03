using ChIllya.Utils;
using ChIllya.ViewModels;

namespace ChIllya.Views
{
	public partial class PageControl : ContentView
	{
		public PageControlViewModel? ViewModel { get; set; }

		public PageControl()
		{
			InitializeComponent();

			ViewModel = new();
			SetContentShortcut();
		}

		private void SetContentShortcut()
		{
			base.OnApplyTemplate();
			var shortcut = (Grid) GetTemplateChild("Shortcut");

			shortcut.BindingContext = ViewModel;
		}
	}
}