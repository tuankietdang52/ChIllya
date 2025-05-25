using CommunityToolkit.Maui.Views;

namespace ChIllya.Views.Popups;

public partial class DownloadProgressWindow : Popup
{
	public CancellationTokenSource Cts = new();

	public DownloadProgressWindow()
	{
		Console.WriteLine("Start");
		InitializeComponent();
		closeButton.Clicked += Cancel!;
    }

    public void TrackingDownload(double progress)
	{
		progressBar.Progress = progress;
		if (progress >= 1)
		{
			closeButton.IsEnabled = false;
			progressState.Text = "Download Successful";
		}
	}

	private void Cancel(object sender, EventArgs e)
	{
		closeButton.IsEnabled = false;
		progressState.Text = "Canceling";
		Cts.Cancel();
	}
}