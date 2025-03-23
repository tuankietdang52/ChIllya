using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Views;
using AndroidX.Core.View;
using Java.Lang;

namespace ChIllya
{
	[Activity(Theme = "@style/MainTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
	public class MainActivity : MauiAppCompatActivity
	{
		protected override void OnCreate(Bundle? savedInstanceState)
		{
            RequestWindowFeature(WindowFeatures.NoTitle);

            if (!Android.OS.Environment.IsExternalStorageManager)
			{
				Intent intent = new Intent();
				intent.SetAction(Android.Provider.Settings.ActionManageAppAllFilesAccessPermission);
				Android.Net.Uri? uri = Android.Net.Uri.FromParts("package", this.PackageName, null);
				intent.SetData(uri);
				StartActivity(intent);
			}
			
			base.OnCreate(savedInstanceState);

            SupportActionBar?.Hide();

			Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
			Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
		}
	}
}
