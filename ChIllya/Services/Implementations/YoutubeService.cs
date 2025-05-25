using ChIllya.Models;
using ChIllya.Services;
using ChIllya.Utils;
using ChIllya.Views.Popups;
using CommunityToolkit.Maui.Views;
using SpotifyAPI.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YoutubeExplode.Videos.Streams;

namespace ChIllya.Services
{
	public class YoutubeService : IYoutubeService
	{
		private readonly string androidPath = "/storage/emulated/0/ChIllya";

		private readonly ISongService _songService;
		private readonly HttpClient httpClient;

		//only god know what this regex is
		//only thing i know is it find string match the pattern '/watch/?v=code until meet a comma'
		private readonly Regex videoUrlRegex = new(@"\/watch\?v[^,]*");

		private readonly YoutubeClient client = new();

		public YoutubeService(ISongService songService)
		{
			httpClient = new HttpClient();
			_songService = songService;
		}

		public async Task Download(Song song, Action<double> handlerProgress, CancellationToken cancellationToken)
		{
			string url = await GetVideoUrl(song);

			if (url == "") return;

			string dir = androidPath;

			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}

			var streamManifest = Task.Run(async () => await client.Videos.Streams.GetManifestAsync(url)).Result;
			var streamInfo = streamManifest.GetAudioStreams().GetWithHighestBitrate();

			song.Name = song.Name.SanitizeFileName("_");
            song.DirectoryPath = Path.Combine(dir, $"{song.Name}.mp3");

			Progress<double> progress = new(handlerProgress);
			string path = song.DirectoryPath;

			try
			{
				await Task.Run(async () => await client.Videos.Streams.DownloadAsync(streamInfo, path, progress), cancellationToken);
			}
			catch (TaskCanceledException)
			{
				if (File.Exists(path)) File.Delete(path);
				return;
			}
		}

		private string GetArtistsQuery(Song song)
		{
			string artists = _songService.GetArtistNameText(song);
			string[] arr = artists.ToLower().Split(',');

			StringBuilder queryArtists = new();

			foreach (var artist in arr)
			{
				queryArtists.Append(artist.Trim());
				queryArtists.Append('+');
			}

			queryArtists.Remove(queryArtists.Length - 1, 1);

			return queryArtists.ToString();
		}

		private string GenerateUrl(string url)
		{
			if (string.IsNullOrEmpty(url)) return "";

			url = url.Replace("\\x3d", "=");
			url = url.Replace("\\\\u0026", "&");
			url = url.Replace("\\x22", "");

			return url;
		}

		private async Task<string> GetVideoUrl(Song song)
		{
			string query = $"{song.Title}+{GetArtistsQuery(song)}";
			string searchUrl = $"https://www.youtube.com/results?search_query={query}+audio";

			// get html text
			var response = await httpClient.GetStringAsync(searchUrl);
			Match match = videoUrlRegex.Match(response);

			if (!match.Success)
			{
				WarningPopup.DisplayError("Cant find url");
				return "";
			}

			var url = GenerateUrl(match.Value);
			return $"https://www.youtube.com{url}";
		}

		~YoutubeService()
		{
			httpClient?.Dispose();
		}
    }
}
