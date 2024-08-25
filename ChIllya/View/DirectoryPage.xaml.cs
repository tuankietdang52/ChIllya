using ChIllya.ViewModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ChIllya.View
{
	public partial class DirectoryPage : ContentPage
	{
		public DirectoryViewModel ViewModel { get; set; }

		public DirectoryPage()
		{
			InitializeComponent();
			ViewModel = new();

			BindingContext = ViewModel;
		}
	}
}