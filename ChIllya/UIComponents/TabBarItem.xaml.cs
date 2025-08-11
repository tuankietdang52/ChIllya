using ChIllya.Views.Contents;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace ChIllya.UIComponents;

public delegate void OnTabBarNavigateHanlder(object sender, BaseView content);

public partial class TabBarItem : ContentView
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(TabBarItem));

    public static readonly BindableProperty GlyphProperty =
        BindableProperty.Create(nameof(Glyph), typeof(string), typeof(TabBarItem));

    public static readonly BindableProperty NavigationContentProperty =
        BindableProperty.Create(nameof(NavigationContent), typeof(BaseView), typeof(TabBarItem));

    public static readonly BindableProperty ForegroundProperty =
      BindableProperty.Create(nameof(Foreground), typeof(Color), typeof(TabBarItem));

    public event OnTabBarNavigateHanlder? Click;

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Glyph
    {
        get => (string)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }

    public ICommand TapCommand { get; set; }

    public Color Foreground
    {
        get => (Color)GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }

    public BaseView NavigationContent
    {
        get => (BaseView)GetValue(NavigationContentProperty);
        set => SetValue(NavigationContentProperty, value);
    }

    public TabBarItem()
	{
        TapCommand = new RelayCommand(OnClick);
		InitializeComponent();
	}

    private void OnClick()
    {
        Click?.Invoke(this, NavigationContent);
    }
}