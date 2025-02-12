using ChIllya.Models;
using ChIllya.ViewModels;
using ChIllya.Views.Popups;

namespace ChIllya.Views;

public partial class PlaylistView : ContentPage
{
	public PlaylistViewModel? ViewModel { get; set; }

	public PlaylistView()
	{
		InitializeComponent();
		WarningPopup.DisplayError("Need to select playlist");
	}

    public PlaylistView(Playlist playlist)
    {
        InitializeComponent();
		ViewModel = new PlaylistViewModel(playlist);
		BindingContext = ViewModel;
    }
}