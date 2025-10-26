#if ANDROID
using AndroidX.Activity;
using FragmentManager = AndroidX.Fragment.App.FragmentManager;
using Microsoft.Maui.Platform;
using Android.Views;
using AndroidX.Core.View;
#endif

using Microsoft.Maui.LifecycleEvents;

namespace ChIllya.Config
{
    public static class AndroidConfigure
    {
        public static MauiAppBuilder ConfigureAndroidUI(this MauiAppBuilder builder)
        {
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.BackgroundTintList =
                    Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

            return builder;
        }

        public static void ConfigureFragment(ILifecycleBuilder lifeCycleBuilder)
        {
#if ANDROID
            lifeCycleBuilder.AddAndroid(builder =>
            {
                builder.OnCreate((activity, bundle) =>
                {
                    if (activity is not ComponentActivity componentActivity)
                    {
                        return;
                    }

                    componentActivity.RequestWindowFeature(WindowFeatures.NoTitle);
                    componentActivity.GetFragmentManager()?.RegisterFragmentLifecycleCallbacks(new MyFragmentLifecycleCallbacks((fm, f) =>
                    {
                        if (f is AndroidX.Fragment.App.DialogFragment dialogFragment)
                        {
                            var activity = Platform.CurrentActivity;

                            if (activity is null)
                                return;

                            var dialog = dialogFragment.Dialog;
                            var window = dialog?.Window;

                            if (window is null) return;

                            //window?.ClearFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                            window?.ClearFlags(WindowManagerFlags.TranslucentStatus);
                            window?.ClearFlags(WindowManagerFlags.Fullscreen);

                            window?.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);

                            window?.SetStatusBarColor(Android.Graphics.Color.Transparent);
                            WindowCompat.SetDecorFitsSystemWindows(window, false);
                        }

                    }), false);
                });
            });
#endif
        }

#if ANDROID

        public class MyFragmentLifecycleCallbacks(Action<AndroidX.Fragment.App.FragmentManager, AndroidX.Fragment.App.Fragment> onFragmentStarted) : FragmentManager.FragmentLifecycleCallbacks
        {
            public override void OnFragmentStarted(FragmentManager fm, AndroidX.Fragment.App.Fragment f)
            {
                onFragmentStarted?.Invoke(fm, f);
                base.OnFragmentStarted(fm, f);
            }
        }
#endif
    }
}
