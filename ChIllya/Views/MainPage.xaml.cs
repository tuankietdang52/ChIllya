using ChIllya.Views.Contents;

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

	private readonly Stack<ContentView> contents = [];

	#region Bindable Property

	public static readonly BindableProperty MainContentProperty =
		BindableProperty.Create(nameof(MainContent), typeof(ContentView), typeof(MainPage));

	#endregion

	public ContentView MainContent
	{
		get => (ContentView)GetValue(MainContentProperty);
		set => SetValue(MainContentProperty, value);
	}

	public MainPage()
	{
		InitializeComponent();
		BindingContext = this;

		MainContent = new HomeView();
		contents.Push(MainContent);
	}

	public void PushContent(ContentView content)
	{
		contents.Push(content);
		MainContent = contents.Peek();
	}

	protected override bool OnBackButtonPressed()
	{
		if (contents.Count > 1)
		{
			contents.Pop();
			MainContent = contents.Peek();
		}
		else
		{
			MinimizeWindow();
		}

		return true;
	}

	private void MinimizeWindow()
	{
#if ANDROID
		var activity = Platform.CurrentActivity;
		activity?.MoveTaskToBack(true);
#endif
	}
}