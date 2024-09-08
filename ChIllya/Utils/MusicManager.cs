using ChIllya.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChIllya.Utils
{
    public partial class MusicManager : ObservableObject
    {
        public static MusicManager? Instance { get; private set; }


        private readonly List<IObserveSong> observers = new();

        [ObservableProperty]
        private StatusImage? imageStatus;

        #region Music Field

        private MusicPlayer player = new();

        private List<Song> playList = new();

        private Song? current;
        public Song Current { 
            get => current!;
            private set
            {
                current = value;
                Notify();
            }
        }

        #endregion

        [ObservableProperty]
        private ICommand? musicCommand;

        public MusicManager()
        {
            Instance ??= this;
            imageStatus = new(this);
        }

        #region Get Set

        public bool IsPlaying() => player.IsPlaying();
        public double GetPosition() => player.GetPosition();
        public double GetDuration() => player.GetDuration();
        public bool IsEnd() => player.IsEnd();

        #endregion

        #region Managing List of Observer

        public void Subscribe(IObserveSong observer)
        {
            observers.Add(observer);
            observer.Update();
        }

        public void Unsubscribe(IObserveSong observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IObserveSong observe in observers)
            {
                observe.Update();
            }
        }

        private void UpdateStatus()
        {
            ImageStatus?.UpdateStatus();
            Notify();
        }

        #endregion

        private void ResetPlayer()
        {
            player.RemovePlaybackEvent(MusicEnding);
        }

        public void ReadyToPlay(Song song)
        {
            ResetPlayer();

            Current = song;
            player.CreateMusic(song);
            player.AddPlaybackEvent(MusicEnding);

            MusicCommand = new RelayCommand(PauseSong);
            UpdateStatus();
        }

        public void UnpauseSong()
        {
            player.ContinueToPlay();
            MusicCommand = new RelayCommand(PauseSong);
            UpdateStatus();
        }

        public void SeekSong(double position)
        {
            player.SeekMusic(position);
        }

        public void PauseSong()
        {
            player.PausePlaying();
            MusicCommand = new RelayCommand(UnpauseSong);
            UpdateStatus();
        }

        public void ReplaySong()
        {
            player.ReplayMusic();

            MusicCommand = new RelayCommand(PauseSong);
            UpdateStatus();
        }

        private void MusicEnding(object? sender, EventArgs e)
        {
            if (!IsEnd()) return;

            MusicCommand = new RelayCommand(ReplaySong);
            UpdateStatus();
        }
    }
}
