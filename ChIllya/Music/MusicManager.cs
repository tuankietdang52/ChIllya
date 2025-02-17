using ChIllya.Models;
using ChIllya.Views.Popups;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using System.Windows.Input;

namespace ChIllya.Music
{
    public partial class MusicManager : ObservableObject
    {
        public static MusicManager? Instance { get; private set; }

        private readonly IMessenger messenger = new StrongReferenceMessenger();

        private readonly StatusImage? imageStatus;

        private readonly PlaylistController playlistController;

        #region Music Field

        private readonly MusicPlayer player = new();

        #endregion

        private EMusicState songState;
        public EMusicState SongState
        {
            get => songState;
            private set
            {
                songState = value;
                UpdateCommand();
            }
        }

        public ICommand? MusicCommand;
        private ICommand? playCommand;
        private ICommand? pauseCommand;
        private ICommand? replayCommand;

        public MusicManager()
        {
            Instance ??= this;
            imageStatus = new(this);
            playlistController = new();

            SetupCommand();
            playlistController.CurrentSongChanged += OnCurrentSongChanged;
        }

        private void SetupCommand()
        {
            playCommand = new RelayCommand(UnpauseSong);
            pauseCommand = new RelayCommand(PauseSong);
            replayCommand = new RelayCommand(ReplaySong);
        }

        private void UpdateCommand()
        {
            switch (songState)
            {
                case EMusicState.Playing:
                    MusicCommand = pauseCommand;
                    break;

                case EMusicState.Stop:
                    MusicCommand = playCommand;
                    break;

                case EMusicState.Ending:
                    MusicCommand = replayCommand;
                    break;

                case EMusicState.ReadyToChange:
                    MusicCommand = null;
                    break;
            }
        }

        #region Get Set

        public bool IsPlaying() => player.IsPlaying();
        public double GetPosition() => player.GetPosition();
        public double GetDuration() => player.GetDuration();
        public bool IsEnd() => player.IsEnd();
        public Song? GetCurrentSong() => playlistController.GetCurrent();

        #endregion

        #region Managing Messager

        public void Register(IRecipient<SongMessage> receiver)
        {
            messenger.Register(receiver);
            receiver.Receive(new SongMessage(playlistController.GetCurrent()!, imageStatus!.Source!, MusicCommand!));
        }

        public void Unregister(IRecipient<SongMessage> receiver)
        {
            messenger.Unregister<SongMessage>(receiver);
        }

        private void SendMessage()
        {
            imageStatus?.UpdateStatus();
            messenger.Send(new SongMessage(playlistController.GetCurrent()!, imageStatus!.Source!, MusicCommand!));
        }

        #endregion

        private void ResetPlayer()
        {
            player.RemovePlaybackEvent(MusicEnding);
        }

        public void StartSong()
        {
            try
            {
                ResetPlayer();

                var song = playlistController.GetCurrent();
                if (song == null) return;

                player.CreateMusic(song);
                player.AddPlaybackEvent(MusicEnding);

                SongState = EMusicState.Playing;
                SendMessage();
            }
            catch (Exception ex)
            {
                WarningPopup.DisplayError(ex.Message);
            }
        }

        public void UnpauseSong()
        {
            player.ContinueToPlay();

            SongState = EMusicState.Playing;
            SendMessage();
        }

        public void SeekSong(double position)
        {
            player.SeekMusic(position);

            songState = EMusicState.Playing;
            SendMessage();
        }

        public void PauseSong()
        {
            player.PausePlaying();
            
            SongState = EMusicState.Stop;
            SendMessage();
        }

        public void ReplaySong()
        {
            player.ReplayMusic();

            SongState = EMusicState.Playing;
            SendMessage();
        }

        private void MusicEnding(object? sender, EventArgs e)
        {
            if (!IsEnd()) return;
            //MusicCommand = replayCommand;
            //SendMessage();

            SongState = EMusicState.ReadyToChange;
            NextSong();
        }

        public void SetPlaylist(Playlist playlist)
        {
            playlistController.SetPlaylist(playlist);
        }

        public void SetCurrentSong(Song song)
        {
            playlistController.SetCurrentSong(song);
        }

        public async void NextSong()
        {
            playlistController.NextSong();

            await Task.Delay(400);
            StartSong();
        }

        public async void PreviousSong()
        {
            playlistController.PreviousSong();

            await Task.Delay(400);
            StartSong();
        }

        /// <summary>
        /// Call when current song change by playlist controller
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="song"></param>
        private void OnCurrentSongChanged(object sender, Song song)
        {
            SendMessage();
        }
    }

    public enum EMusicState
    {
        Playing,
        Stop,
        Ending,
        ReadyToChange
    }
}
