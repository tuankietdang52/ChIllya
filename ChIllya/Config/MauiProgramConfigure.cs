using ChIllya.Services;
using ChIllya.Services.Implementations;
using ChIllya.Utils;
using ChIllya.ViewModels;
using ChIllya.Views;
using ChIllya.Music;

namespace ChIllya.Config
{
	public static class MauiProgramConfigure
	{
		public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
		{
			builder.Services.AddSingleton<HomePage>();
			builder.Services.AddSingleton<DirectoryPage>();
			builder.Services.AddSingleton<SpotifySearchPage>();

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

		public static MauiAppBuilder SetupFonts(this MauiAppBuilder builder)
		{
			builder.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Raleway-Regular.ttf", "RalewayRegular");
				fonts.AddFont("Raleway-SemiBold.ttf", "RalewaySemibold");
				fonts.AddFont("Inter_24pt-Regular.tff", "InterRegular");
				fonts.AddFont("Inter_24pt-SemiBold.tff", "InterSemibold");
				fonts.AddFont("Font-Awesome-6-Brands-Regular-400.otf", "FontAwesomeBrandRegular");
				fonts.AddFont("Font-Awesome-6-Free-Regular-400.otf", "FontAwesomeRegular");
				fonts.AddFont("Font-Awesome-6-Free-Solid-900.otf", "FontAwesomeSolid");
			});

			return builder;
		}
    }
}