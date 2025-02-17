using ChIllya.Models;
using ChIllya.Music;
using ChIllya.Utils;
using ChIllya.ViewModels.Components;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;

namespace ChIllya.ViewModels
{
    /// <summary>
    /// View Model for Song Page
    /// <para>
    /// If move to another page, current Song View Model Instance 
    /// will be deleted
    /// </para>
    /// <para>
    ///     When move to Song Page in anyway, 
    ///     always create the new Song VM instance
    ///     and setup properties depend on song status through Music Player Instance 
    /// </para>
    /// </summary>
    public partial class SongViewModel : ObservableObject, IViewModel, IRecipient<SongMessage>
    {
        [ObservableProperty]
        private Song? current;

        private MusicManager Manager => MusicManager.Instance!;

        [ObservableProperty]
        private string? imageStatus;

        [ObservableProperty]
        private string? position;

        [ObservableProperty]
        private double sliderValue;

        private readonly System.Timers.Timer timer = new(1000);

        #region Command

        public ICommand? BackCommand { get; set; }

        [ObservableProperty]
        private ICommand? musicCommand;

        public ICommand? NextSongCommand { get; set; }
        public ICommand? PreviousSongCommand { get; set; }

        #endregion

        // User return to song page by shortcut or in directory
        public SongViewModel() {
            Setup();
            SetViewPosition();
        }

        public SongViewModel(Song song)
        {
            Setup();
            Manager.SetCurrentSong(song);
            Manager.StartSong();
        }

        // update information of current song
        public void Receive(SongMessage message)
        {
            (Current, ImageStatus, MusicCommand) = message.GetData();
            TimerControl(Manager.IsPlaying());
        }

        private void Setup()
        {
            Manager.Register(this);
            SetTimer();
            GenerateCommand();
        }

        private void SetTimer()
        {
            timer.Elapsed += (source, e) => SetViewPosition();
        }

        /// <summary>
        ///  Tracks remain time of music and convert it to string
        /// </summary>
        private void SetViewPosition()
        {
            double remain = Manager.GetDuration() - Manager.GetPosition();
            Position = TimeSpan.FromSeconds((int)remain).ToString("hh\\:mm\\:ss");

            if (!Manager.IsEnd()) SliderValue = Manager.GetPosition();
            else SliderValue = 0;
        }

        private void TimerControl(bool isPlaying)
        {
            if (!isPlaying) timer.Stop();
            else timer.Start();

            SetViewPosition();
        }

        public void GenerateCommand()
        {
            BackCommand = new RelayCommand(async () =>
            {
                Manager.Unregister(this);
                timer.Dispose();

                await Shell.Current.Navigation.PopAsync();
            });

            var nextAction = ToNextSong;
            var previousAction = ToPreviousSong;

            NextSongCommand = new RelayCommand(nextAction.Debounce(500));
            PreviousSongCommand = new RelayCommand(previousAction.Debounce(500));
        }

        private void ToNextSong()
        {
            Manager.NextSong();
        }

        private void ToPreviousSong()
        {
            Manager.PreviousSong();
        }

        public void SliderValueChanged(double value)
        {
            SliderValue = value;
            Manager.SeekSong(SliderValue);

            SetViewPosition();
        }
    }
}
