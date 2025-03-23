using ChIllya.Models;
using System.Windows.Input;

namespace ChIllya.UIComponents;

public partial class SongShorcut : ContentView
{
    public static readonly BindableProperty TapCommandProperty =
        BindableProperty
        .Create(nameof(TapCommand), typeof(ICommand), typeof(SongShorcut));

    public static readonly BindableProperty SongCommandProperty =
        BindableProperty
        .Create(nameof(SongCommand), typeof(ICommand), typeof(SongShorcut));

    public static readonly BindableProperty CurrentSongProperty =
        BindableProperty
        .Create(nameof(CurrentSong), typeof(Song), typeof(SongShorcut));

    public static readonly BindableProperty ImageStatusSourceProperty =
        BindableProperty
        .Create(nameof(ImageStatusSource), typeof(string), typeof(SongShorcut));

    public ICommand TapCommand
	{
		get => (ICommand)GetValue(TapCommandProperty);
		set => SetValue(TapCommandProperty, value);
	}

	public ICommand SongCommand
    {
        get => (ICommand)GetValue(SongCommandProperty);
        set => SetValue(SongCommandProperty, value); 
    }

	public Song CurrentSong
    {
        get => (Song)GetValue(CurrentSongProperty);
        set => SetValue(CurrentSongProperty, value);
    }

    public string ImageStatusSource
    {
        get => (string)GetValue(ImageStatusSourceProperty);
        set => SetValue(ImageStatusSourceProperty, value);
    }

    public SongShorcut()
	{
		InitializeComponent();
	}
}