using ChIllya.Views.Contents;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ChIllya.Views;

public partial class MainPage : ContentPage
{
	private static MainPage? instance;
	public static MainPage Instance
	{
		get
		{
			instance ??= new MainPage();
			return instance;
		}
		set => instance = value;
	}

	public MainPage()
	{
		InitializeComponent();

		BindingContext = this;
		contentView.Content = new HomeView();
        tabBarContainer.OnNavigate = new RelayCommand<ContentView>(Navigate);
	}

	public void Navigate(ContentView? view)
	{
		if (view == null) return;
		contentView.Content = view;
    }
}