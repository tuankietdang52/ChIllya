using System.Windows.Input;

namespace ChIllya.UIComponents;

public partial class Icon : ContentView
{
	public static readonly BindableProperty GlyphProperty =
		BindableProperty.Create(nameof(Glyph), typeof(string), typeof(Icon));

	public static readonly BindableProperty ClickProperty =
		BindableProperty.Create(nameof(Click), typeof(ICommand), typeof(Icon));

	public static readonly BindableProperty ColorProperty = 
		BindableProperty.Create(nameof(Color), typeof(Color), typeof(Icon));

	public string Glyph
	{
		get => (string)GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }

	public ICommand? Click
	{
		get => (ICommand?)GetValue(ClickProperty);
		set => SetValue(ClickProperty, value);
    }

	public Color Color
	{
		get => (Color)GetValue(ColorProperty);
		set => SetValue(ColorProperty, value);
	}
	
	public Icon()
	{
		InitializeComponent();
	}
}