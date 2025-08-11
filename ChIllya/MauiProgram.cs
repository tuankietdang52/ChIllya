using ChIllya.Config;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.LifecycleEvents;
using Plugin.Maui.Audio;
using Syncfusion.Maui.Core.Hosting;

namespace ChIllya
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>()
                   .UseMauiCommunityToolkit()
                   .AddAudio()
                   .SetupFonts()
                   .RegisterViews()
                   .RegisterViewModels()
                   .RegisterServices()
                   .ConfigureSyncfusionCore()
                   .ConfigureAndroidUI()
                   .ConfigureLifecycleEvents(AndroidConfigure.ConfigureFragment);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
