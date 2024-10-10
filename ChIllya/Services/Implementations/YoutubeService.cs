using ChIllya.Models;
using ChIllya.Services;
using ChIllya.Views.Popups;
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
        private readonly string windowsPath = "C:/Users/ADMIN/ChIllyaData/";

        private readonly ISongService _songService;
        private readonly HttpClient httpClient;

        //only god know what this regex is
        //only thing i know is it find string match the pattern '/watch/?v=code with size 11'
        private readonly Regex videoUrlRegex = new(@"\/watch\?v=[A-Za-z0-9-_]{11}");

        private readonly YoutubeClient client = new();

        public YoutubeService(ISongService songService)
        {
            httpClient = new HttpClient();
            _songService = songService;
        }


        public async Task Download(Song song)
        {
            string url = await GetVideoUrl(song);

            string dir = windowsPath;

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var streamManifest = await client.Videos.Streams.GetManifestAsync(url);
            var streamInfo = streamManifest.GetAudioStreams().GetWithHighestBitrate();

            song.DirectoryPath = Path.Combine(dir, $"{song.Name}.mp3");
            await client.Videos.Streams.DownloadAsync(streamInfo, song.DirectoryPath);
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

        private async Task<string> GetVideoUrl(Song song)
        {
            string query = $"{song.Title}+{GetArtistsQuery(song)}";

            string path = $"https://www.youtube.com/results?search_query={query}";
            
            // get html text
            var response = await httpClient.GetStringAsync(path);

            Match match = videoUrlRegex.Match(response);

            if (!match.Success)
            {
                PopUp.DisplayError("Cant find url");
                return "";
            }

           return $"https://www.youtube.com{match.Value}";
        }
    }
}
