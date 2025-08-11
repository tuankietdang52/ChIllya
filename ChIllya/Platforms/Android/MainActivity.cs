using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Hardware.Lights;
using Android.OS;
using Android.Views;
using AndroidX.Core.View;

namespace ChIllya
{
	[Activity(Theme = "@style/MainTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : MauiAppCompatActivity
	{
		public static MainActivity? Current { get; private set; }

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
			Current = this;

            SupportActionBar?.Hide();
			SetStatusBarTransparent();
		}

		private void SetStatusBarTransparent()
		{
			Window?.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);
			Window?.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            Window?.ClearFlags(WindowManagerFlags.TranslucentStatus);
            WindowCompat.SetDecorFitsSystemWindows(Window, false);

            Window?.SetStatusBarColor(Android.Graphics.Color.Transparent);
        }
    }
}
