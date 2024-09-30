using ChIllya.Models;
using ChIllya.ViewModels.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChIllya.Utils
{
    public partial class MusicManager : ObservableObject
    {
        public static MusicManager? Instance { get; private set; }

        private readonly IMessenger messenger = new StrongReferenceMessenger();

        private StatusImage? imageStatus;

        #region Music Field

        private readonly MusicPlayer player = new();

        private List<Song> playList = [];

        private Song? current;
        public Song Current { 
            get => current!;
            private set
            {
                if (current == value) return;

                current = value;
                SendMessage();
            }
        }

        #endregion

        private ICommand? musicCommand;
        public ICommand MusicCommand
        {
            get => musicCommand!;
            set
            {
                if (musicCommand == value) return;

                musicCommand = value;
                SendMessage();          
            }
        }

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

        #region Managing Messager

        public void Register(IRecipient<SongMessage> receiver)
        {
            messenger.Register(receiver);
            receiver.Receive(new SongMessage(Current, imageStatus!.Source!, musicCommand!));
        }

        public void Unregister(IRecipient<SongMessage> receiver)
        {
            messenger.Unregister<SongMessage>(receiver);
        }

        private void SendMessage()
        {
            imageStatus?.UpdateStatus();
            messenger.Send(new SongMessage(Current, imageStatus!.Source!, musicCommand!));
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
        }

        public void UnpauseSong()
        {
            player.ContinueToPlay();
            MusicCommand = new RelayCommand(PauseSong);
        }

        public void SeekSong(double position)
        {
            player.SeekMusic(position);
            MusicCommand = new RelayCommand(PauseSong);
        }

        public void PauseSong()
        {
            player.PausePlaying();
            MusicCommand = new RelayCommand(UnpauseSong);
        }

        public void ReplaySong()
        {
            player.ReplayMusic();
            MusicCommand = new RelayCommand(PauseSong);
        }

        private void MusicEnding(object? sender, EventArgs e)
        {
            if (!IsEnd()) return;
            MusicCommand = new RelayCommand(ReplaySong);
        }
    }
}
