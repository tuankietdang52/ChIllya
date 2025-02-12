
using ChIllya.Utils;
using ChIllya.Views.Popups;
using Syncfusion.Licensing;

namespace ChIllya
{
    public partial class App : Application
    {
        public App()
        {
			string key = Environment.GetEnvironmentVariable("SYNCFUSION_LICENSE_KEY")!;

			if (key == null)
			{
				WarningPopup.DisplayError("Cant get Syncfusion key");
			}

			SyncfusionLicenseProvider.RegisterLicense(key);
            InitializeComponent();
		}

		protected override Window CreateWindow(IActivationState? activationState)
		{
            return new Window(new AppShell());
		}
	}
}
