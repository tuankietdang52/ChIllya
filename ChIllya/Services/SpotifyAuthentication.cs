using EmbedIO.Authentication;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;
using static System.Formats.Asn1.AsnWriter;

namespace ChIllya.Services
{
    public class SpotifyAuthentication
    {
        private readonly string clientID = "07041ec5137d4c168e766d7081db8758";
        private readonly string? clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");

        public SpotifyAuthentication()
        {
            if (string.IsNullOrEmpty(clientSecret))
            {
                throw new NullReferenceException("Client Secret is Empty");
            }
        }

        public SpotifyClient CreateSpotifyClient()
        {
            var config = SpotifyClientConfig
                        .CreateDefault()
                        .WithAuthenticator(new ClientCredentialsAuthenticator(clientID, clientSecret!));

            return new SpotifyClient(config);
        }
    }
}
