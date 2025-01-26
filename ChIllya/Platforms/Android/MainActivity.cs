using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.View;

namespace ChIllya
{
	[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
	public class MainActivity : MauiAppCompatActivity
	{
		protected override void OnCreate(Bundle? savedInstanceState)
		{
			if (!Android.OS.Environment.IsExternalStorageManager)
			{
				Intent intent = new Intent();
				intent.SetAction(Android.Provider.Settings.ActionManageAppAllFilesAccessPermission);
				Android.Net.Uri? uri = Android.Net.Uri.FromParts("package", this.PackageName, null);
				intent.SetData(uri);
				StartActivity(intent);
			}
			
			base.OnCreate(savedInstanceState);
			Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
		}
	}
}
