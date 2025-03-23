using ChIllya.Config;
using ChIllya.Utils;
using System.Threading.Tasks;

namespace ChIllya.Views;

public partial class SplashScreenPage : ContentPage
{
    private AppSetup appSetup = new();
    private bool isShowing = false;

	public SplashScreenPage()
	{
		InitializeComponent();
        PrepareConfigure();
    }

    private void OnProgressChanged(object? sender, ProgressReport e)
    {
        double percent = e.PercentComplete;
        progressBar.Progress = percent;

        if (percent == 100) ToApplication();
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
    }

    private async void StartConfigure()
    {
        await Task.Delay(1000);
        appSetup.Start();
    }

    private async void ToApplication()
    {
        var app = App.Current!;
        if (app is null) return;

        await Task.Delay(500);

        app.CloseWindow(GetParentWindow());
        app.OpenWindow(new Window(new AppShell()));
    }
}