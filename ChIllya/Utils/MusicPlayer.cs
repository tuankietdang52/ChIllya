using ChIllya.Error;
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
        private static MusicPlayer? instance;

        private IAudioPlayer? player;

        /// <summary>
        /// for replay music after stop
        /// </summary>
        private double currentPosition = 0;

        public MusicPlayer()
        {
            instance ??= this;
        }

        /// <summary>
        /// Get instance of Media Manager
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public static MusicPlayer GetInstance()
        {
            if (instance == null) throw new NullReferenceException("Player is null");
            return instance;
        }

        public async void CreateMusic(string path, EventHandler? endEvent = null)
        {
            player?.Dispose();
            currentPosition = 0;

            player = AudioManager.Current.CreatePlayer(
                await MauiStorage.FileSystem.OpenAppPackageFileAsync(path));

            if (player == null) throw new NullReferenceException("Instance is null");
            if (endEvent != null) player.PlaybackEnded += endEvent;

            PlayMusic();
        }

        /// <summary>
        /// Need to create music first
        /// <para>Playing current song</para>
        /// </summary>
        /// <exception cref="NullReferenceException"></exception>
        public void PlayMusic()
        {
            if (player == null) throw new NullReferenceException("Music Player is null");

            SeekMusic(currentPosition);
            player.Play();
        }

        /// <summary>
        /// Set the current playback position (in seconds).
        /// </summary>
        public void SeekMusic(double position)
        {
            player?.Seek(position);
        }

        public void StopMusic()
        {
            if (player == null) throw new NullReferenceException("Music Player is null");

            currentPosition = player.CurrentPosition;
            player.Stop();
        }

        public void ReplayMusic()
        {
            if (player == null) throw new NullReferenceException("Music Player is null");

            SeekMusic(0);
            PlayMusic();
        }
    }
}
