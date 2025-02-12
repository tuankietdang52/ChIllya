namespace ChIllya.UIComponents;
using ChIllya.Models;
using ChIllya.Views.Popups;
using System.Windows.Input;

public partial class ListDirectoryView : ContentView
{
	public static readonly BindableProperty ItemTapCommandProperty =
		BindableProperty
		.Create(nameof(ItemTapCommand), typeof(ICommand), typeof(ChIllyaImageButton));

	public static readonly BindableProperty ItemsSourceProperty =
		BindableProperty
		.Create(nameof(ItemsSource), typeof(IEnumerable<Playlist>), typeof(ListDirectoryView));

	public static readonly BindableProperty IsHaveItemProperty = 
		BindableProperty
		.Create(nameof(IsHaveItem), typeof(bool), typeof(ListDirectoryView));

    public ICommand ItemTapCommand
	{
		get => (ICommand)GetValue(ItemTapCommandProperty);
		set => SetValue(ItemTapCommandProperty, value);
	}
	
	public IEnumerable<Playlist> ItemsSource
	{
		get => (IEnumerable<Playlist>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
	}

	public bool IsHaveItem
	{
		get => (bool)GetValue(IsHaveItemProperty);
		set => SetValue(IsHaveItemProperty, value);
	}
    public ListDirectoryView()
	{
        InitializeComponent();
	}
}