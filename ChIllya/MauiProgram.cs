using ChIllya.Services;
using ChIllya.Services.Implementations;
using ChIllya.Utils;
using ChIllya.ViewModels;
using ChIllya.Views;
using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;

namespace ChIllya
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .AddAudio()
                .RegisterViews()
                .RegisterViewModels()
                .RegisterServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            var setup = new AppSetup();
            setup
                .InitMusicManager()
                .LoadSecretFile();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        // dependency injection
        private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<HomePage>();
            builder.Services.AddScoped<DirectoryPage>();
            builder.Services.AddScoped<LocalSearchPage>();

            return builder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<LocalSearchViewModel>();
            builder.Services.AddScoped<DirectoryViewModel>();

            return builder;
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<ISpotifyService, SpotifyService>(provider =>
            {
                var client = new SpotifyAuthentication().CreateSpotifyClient();
                return new SpotifyService(client);
            });

            builder.Services.AddTransient<ILocalService, LocalService>();

            return builder;
        }
    }
}
