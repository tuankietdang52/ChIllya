using ChIllya.Models;
using ChIllya.Services;
using ChIllya.Utils;
using ChIllya.Views.Popups;
using CommunityToolkit.Maui.Views;
using SpotifyAPI.Web.Http;
using Swan;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		private readonly Regex pattern = new(@"headline\\x22:\\x7b\\x22runs\\x22:\\x5b\\x7b\\x22text\\x22:\\x22(.*?)\\x22.*?\\x22shortBylineText\\x22:\\x7b\\x22runs\\x22:\\x5b\\x7b\\x22text\\x22:\\x22(.*?)\\x22.*?\\/?(watch\?v.*?)\\u");

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

			song.Name = song.Name.SanitizeFileNameWith("_");
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
			query = query.Replace(' ', '+');

			string searchUrl = $"https://www.youtube.com/results?search_query={query}+official+audio";

			// get html text
			var response = await httpClient.GetStringAsync(searchUrl);
			string rawUrl = PrioritizeAudio(song, response);

			if (rawUrl == "")
			{
				WarningPopup.DisplayError("Cant find url");
				return "";
			}

			string url = GenerateUrl(rawUrl);
			return $"https://www.youtube.com/{url}";
		}

		private bool CheckArtist(Song song, string artist)
		{
			return song.Artists.Any(a => a.Name.Equals(artist, StringComparison.CurrentCultureIgnoreCase));
		}

		private string PrioritizeAudio(Song song, string response)
		{
			var matches = pattern.Matches(response).Take(3).ToList();
			string result = matches[0].Groups[3].Value;

			foreach (var m in matches)
			{
				string name = m.Groups[1].Value;
				string uploader = m.Groups[2].Value;
				string url = m.Groups[3].Value;

				if (!CheckArtist(song, uploader)) continue;

				if (name.Contains("audio", StringComparison.CurrentCultureIgnoreCase))
				{
					return url[..^1];
				}
			}

			return result[.. ^1];
		}

		~YoutubeService()
		{
			httpClient?.Dispose();
		}
    }
}