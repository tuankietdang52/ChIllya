using ChIllya.Services;
using ChIllya.Services.Implementations;
using ChIllya.ViewModels;
using ChIllya.Views;
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
            builder
                .UseMauiCommunityToolkit()
                .UseMauiApp<App>()
                .AddAudio()
                .RegisterViews()
                .RegisterViewModels()
                .RegisterServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Raleway-Regular.ttf", "RalewayRegular");
					fonts.AddFont("Raleway-SemiBold.ttf", "RalewaySemibold");
                    fonts.AddFont("Inter_24pt-Regular.tff", "InterRegular");
					fonts.AddFont("Inter_24pt-SemiBold.tff", "InterSemibold");
				})
                .InitMusicManager()
                .LoadSecretFile()
                .ConfigureSyncfusionCore();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
