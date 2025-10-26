using ChIllya.Models;

namespace ChIllya.Services
{
    public interface IYoutubeService
    {
        // Download a song from youtube
        Task Download(Song song, Action<double> handlerProgress, CancellationToken cancellationToken);
    }
}
