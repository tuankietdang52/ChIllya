using ChIllya.Services;
using ChIllya.Services.Implementations;
using ChIllya.Utils;
using ChIllya.ViewModels;
using ChIllya.Views;
using CommunityToolkit.Maui;
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
            builder.Services.AddScoped<SpotifySearchPage>();

            return builder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddScoped<SpotifySearchViewModel>();
            builder.Services.AddScoped<DirectoryViewModel>();

            return builder;
        }

        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<ISpotifyService, SpotifyService>(provider =>
            {
                var client = new SpotifyAuthentication().CreateSpotifyClient();
                return new SpotifyService(client);
            });

            builder.Services.AddTransient<ISongService, SongService>();
            builder.Services.AddTransient<ILocalService, LocalService>();

            return builder;
        }
    }
}
