using ChIllya.Config;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using Syncfusion.Maui.Core.Hosting;

namespace ChIllya
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiCommunityToolkit()
                   .UseMauiApp<App>()
                   .AddAudio()
                   .RegisterViews()
                   .RegisterViewModels()
                   .RegisterServices()
                   .SetupFonts()
                   .ConfigureSyncfusionCore();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
