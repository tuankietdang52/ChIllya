using System.Windows.Input;

namespace ChIllya.Controls
{
	public partial class ChIllyaImageButton : ContentView
	{
        public static readonly BindableProperty TapCommandProperty =
            BindableProperty
            .Create(nameof(TapCommand), typeof(ICommand), typeof(ChIllyaImageButton));

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty
            .Create(nameof(ImageSource), typeof(string), typeof(ChIllyaImageButton));

        public static readonly BindableProperty ImageWidthProperty =
            BindableProperty
            .Create(nameof(ImageWidth), typeof(double), typeof(ChIllyaImageButton));

        public static readonly BindableProperty ImageHeightProperty =
            BindableProperty
            .Create(nameof(ImageHeight), typeof(double), typeof(ChIllyaImageButton));

        public static readonly BindableProperty CornerRadiusProperty =
            BindableProperty
            .Create(nameof(CornerRadius), typeof(int), typeof(ChIllyaImageButton));

        public static new readonly BindableProperty BackgroundColorProperty =
            BindableProperty
            .Create(nameof(BackgroundColor), typeof(Color), typeof(ChIllyaImageButton));

        public static readonly BindableProperty BorderColorProperty =
            BindableProperty
            .Create(nameof(BorderColor), typeof(Color), typeof(ChIllyaImageButton));

        public ICommand? TapCommand
        {
            get => (ICommand)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }

        public string ImageSource
        {
            get => (string)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        public double ImageWidth
        {
            get => (double)GetValue(ImageWidthProperty);
            set => SetValue(ImageWidthProperty, value);
        }

        public double ImageHeight
        {
            get => (double)GetValue(ImageHeightProperty);
            set => SetValue(ImageHeightProperty, value);
        }

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public Color BorderColor
        {
            get => (Color)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public ChIllyaImageButton()
		{
			InitializeComponent();
		}

	}
}