using ChIllya.Services;
using ChIllya.Services.Implementations;
using ChIllya.Utils;
using ChIllya.ViewModels;
using ChIllya.Views;
using ChIllya.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChIllya
{
    public static class AppSetup
    {
        public static MauiAppBuilder InitMusicManager(this MauiAppBuilder builder)
        {
            new MusicManager();
            return builder;
        }

        public static MauiAppBuilder LoadSecretFile(this MauiAppBuilder builder)
        {
            EnvLoader.Load("Secret/secret.env");
            return builder;
        }

		public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
		{
			builder.Services.AddScoped<HomePage>();
			builder.Services.AddScoped<DirectoryPage>();
			builder.Services.AddScoped<SpotifySearchPage>();

			return builder;
		}

		public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
		{
			builder.Services.AddScoped<SpotifySearchViewModel>();
			builder.Services.AddScoped<DirectoryViewModel>();

			return builder;
		}

		public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
		{
			builder.Services.AddTransient<ISpotifyService, SpotifyService>(provider =>
			{
				var client = new SpotifyAuthentication().CreateSpotifyClient();
				return new SpotifyService(client);
			});

			builder.Services.AddTransient<ISongService, SongService>();
			builder.Services.AddTransient<ILocalService, LocalService>();
			builder.Services.AddTransient<IYoutubeService, YoutubeService>();

			return builder;
		}
	}
}
