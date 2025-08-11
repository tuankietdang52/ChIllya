using ChIllya.Models;
using ChIllya.Services;
using ChIllya.ViewModels;
using ChIllya.Views.Popups;

namespace ChIllya.Views;

public partial class PlaylistPage : ContentPage
{
	public PlaylistViewModel? ViewModel { get; set; }

	public PlaylistPage()
	{
		InitializeComponent();
		WarningPopup.DisplayError("Need to select playlist");
	}

    public PlaylistPage(Playlist playlist)
    {
        InitializeComponent();

		ViewModel = new PlaylistViewModel(playlist);
		BindingContext = ViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
	}
}