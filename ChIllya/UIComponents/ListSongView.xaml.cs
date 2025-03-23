using ChIllya.Models;
using System.Windows.Input;

namespace ChIllya.UIComponents;

public partial class ListSongView : ContentView
{
    public static readonly BindableProperty ItemTapCommandProperty =
        BindableProperty
        .Create(nameof(ItemTapCommand), typeof(ICommand), typeof(ListSongView));

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty
        .Create(nameof(ItemsSource), typeof(IEnumerable<Song>), typeof(ListSongView));

    public ICommand ItemTapCommand
    {
        get => (ICommand)GetValue(ItemTapCommandProperty);
        set => SetValue(ItemTapCommandProperty, value);
    }

    public IEnumerable<Song> ItemsSource
    {
        get => (IEnumerable<Song>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public ListSongView()
	{
		InitializeComponent();
	}
}