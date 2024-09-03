using ChIllya.Error;
using ChIllya.Models;
using ChIllya.ViewModels;
using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoLibrary;

using MauiStorage = Microsoft.Maui.Storage;

namespace ChIllya.Utils
{
    /// <summary>
    /// Manager of playing music in app
    /// </summary>
    public class MusicPlayer
    {
        private IAudioPlayer? player;

        public MusicPlayer()
        {

        }

        #region Get Set

        public bool IsPlaying() => player != null && player.IsPlaying;
        public double GetPosition() => player != null ? player.CurrentPosition : 0;
        public double GetDuration() => player != null ? player.Duration : 0;
        public bool IsEnd() => player != null && GetPosition() >= GetDuration();

        #endregion

        public void CreateMusic(Song song)
        {
            player?.Stop();
            player?.Dispose();

            Task<Stream> task =
                MauiStorage.FileSystem.OpenAppPackageFileAsync(song.SongPath);
            task.Wait();

            player = AudioManager.Current.CreatePlayer(task.Result);

            if (player == null) throw new NullReferenceException("Cant create player");
            player.Play();

            // wait to load information
            Thread.Sleep(250);
        }

        /// <summary>
        /// Need to create music first
        /// <para>Continue to play current song</para>
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public void ContinueToPlay()
        {
            if (player == null) throw new NullReferenceException("Music Player is null");
            player.Play();
        }

        /// <summary>
        /// Set the current playback position (in seconds).
        /// </summary>
        public void SeekMusic(double position)
        {
            player?.Seek(position);
        }

        public void PausePlaying()
        {
            if (player == null) throw new NullReferenceException("Music Player is null");
            player.Pause();
        }

        public void ReplayMusic()
        {
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
