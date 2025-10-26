using ChIllya.Models;
using ChIllya.Views.Popups;
using Plugin.Maui.Audio;

namespace ChIllya.Music
{
    /// <summary>
    /// Manager of playing music in app
    /// </summary>
    public class MusicPlayer
    {
        private IAudioPlayer? player;
        private bool isReady = false;

        public MusicPlayer()
        {

        }

        #region Get Set

        public bool IsPlaying() => isReady && player != null && player.IsPlaying;
        public double GetPosition()
        {
            return isReady && player != null ? player.CurrentPosition : 0;
        }
        public double GetDuration()
        {
            return isReady && player != null ? player.Duration : 0;
        }
        public bool IsEnd() => isReady && player != null && GetPosition() >= GetDuration();

        #endregion

        public void DisposePlayer(EventHandler playbackEndCallback)
        {
            RemovePlaybackEvent(playbackEndCallback);
            player?.Stop();
            player?.Dispose();
        }

        public void CreateMusic(Song song)
        {
            isReady = false;

            try
            {
                var stream = File.Open(song.DirectoryPath, FileMode.Open);
                player = AudioManager.Current.CreatePlayer(stream);
            }
            catch (FileNotFoundException ex)
            {
                WarningPopup.DisplayError(ex.Message);
                return;
            }

            if (player == null) throw new NullReferenceException("Cant create player");
            player.Play();

            isReady = true;
        }

        /// <summary>
        /// Need to create music first
        /// <para>Continue to play current song</para>
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public void ContinueToPlay()
        {
            if (!isReady) return;
            if (player == null) throw new NullReferenceException("Music Player is null");
            player.Play();
        }

        /// <summary>
        /// Set the current playback position (in seconds).
        /// </summary>
        public void SeekMusic(double position)
        {
            if (!isReady) return;

            player?.Stop();
            player?.Seek(position);
            player?.Play();
        }

        public void Pause()
        {
            if (!isReady) return;
            if (player == null) throw new NullReferenceException("Music Player is null");
            player.Pause();
        }

        public void ReplayMusic()
        {
            if (!isReady) return;
            if (player == null) throw new NullReferenceException("Music Player is null");

            SeekMusic(0);
            ContinueToPlay();
        }

        public void AddPlaybackEvent(EventHandler callback)
        {
            if (player == null) return;
            player.PlaybackEnded += callback;
        }

        public void RemovePlaybackEvent(EventHandler callback)
        {
            if (player == null) return;
            player.PlaybackEnded -= callback;
        }
    }
}
