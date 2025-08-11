using ChIllya.Config;
using ChIllya.Utils;
using System.Threading.Tasks;

namespace ChIllya.Views;

public partial class SplashScreenPage : ContentPage
{
    private readonly AppSetup appSetup = new();

	public SplashScreenPage()
	{
		InitializeComponent();
        PrepareConfigure();
    }

    private void OnProgressChanged(object? sender, ProgressReport e)
    {
        double percent = e.PercentComplete;
        MainThread.BeginInvokeOnMainThread(() => progressBar.Progress = percent);

        if (percent == 1) ToApplication();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        StartConfigure();
    }

    private void PrepareConfigure()
    {
        Progress<ProgressReport> progress = appSetup.GetProgress();
        progress.ProgressChanged += OnProgressChanged;

        appSetup.AddConfigure(new AppConfigure());
        appSetup.AddConfigure(new KeyConfigure());
        appSetup.AddConfigure(new ContentConfigure());
    }

    private async void StartConfigure()
    {
        await Task.Delay(100);
        appSetup.Start();
    }

    private async void ToApplication()
    {
        var app = App.Instance!;
        if (app is null) return;

        await Task.Delay(200);
        app.Windows[0].Page = MainPage.Instance;
    }
}