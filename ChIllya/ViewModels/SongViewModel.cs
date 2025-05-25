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

        [ObservableProperty]
        private double songLength;

        private readonly System.Timers.Timer timer = new(1000);

        #region Command

        public ICommand? BackCommand { get; set; }

        [ObservableProperty]
        private ICommand? musicCommand;
        public ICommand? NextSongCommand { get; set; }
        public ICommand? PreviousSongCommand { get; set; }
        public ICommand? LoopCommand {  get; set; }
        public ICommand? ShuffleCommand { get; set; }

        #endregion

        [ObservableProperty]
        private Color? loopButtonColor;

        [ObservableProperty]
        private Color? shuffleButtonColor;

        // User return to song page by shortcut or in directory
        public SongViewModel() {
            Initialize();
            UpdateSliderPosition();
        }

        public void Initialize()
        {
            Manager.Register(this);

            UpdatePlayState();
            SetTimer();
            GenerateCommand();
        }

        // update information of current song
        public void Receive(SongMessage message)
        {
            (Current, ImageStatus, MusicCommand) = message.GetData();
            UpdateInformation();
        }

        private async void UpdateInformation()
        {
            UpdatePlayState();
            SongLength = Manager.GetDuration();
            UpdateTimer(Manager.IsPlaying());

            await Task.Delay(500).ContinueWith(task =>
            {
                if (!Manager.IsPlaying()) UpdateSliderPosition();
            });
        }

        private void UpdatePlayState()
        {
            Color selectedColor = (Color)App.Instance!.Resources["IconSelected"];
            Color unSelectedColor = (Color)App.Instance!.Resources["Icon"];

            LoopButtonColor = Manager.IsLoop() ? selectedColor : unSelectedColor;
            ShuffleButtonColor = Manager.IsShuffle() ? selectedColor : unSelectedColor;
        }

        private void OnLoopButtonClicked()
        {
            Manager.SwtichLoopMode();
        }

        private void OnShuffleButtonClicked()
        {
            Manager.SwitchShuffleMode();
        }

        private void SetTimer()
        {
            timer.Elapsed += (source, e) => UpdateSliderPosition();
        }

        /// <summary>
        ///  Tracks remain time of music and convert it to string
        /// </summary>
        private void UpdateSliderPosition()
        {
            double remain = Manager.GetDuration() - Manager.GetPosition();
            Position = TimeSpan.FromSeconds((int)remain).ToString("hh\\:mm\\:ss");

            if (!Manager.IsEnd()) SliderValue = Manager.GetPosition();
            else SliderValue = 0;
        }

        private void UpdateTimer(bool isPlaying)
        {
            if (!isPlaying) timer.Stop();
            else timer.Start();

            UpdateSliderPosition();
        }

        private void GenerateCommand()
        {
            BackCommand = new RelayCommand(async () =>
            {
                Manager.Unregister(this);
                timer.Dispose();

                await App.Instance!.PopAsync();
            });

            var nextAction = ToNextSong;
            var previousAction = ToPreviousSong;

            NextSongCommand = new RelayCommand(nextAction.Debounce(500));
            PreviousSongCommand = new RelayCommand(previousAction.Debounce(500));
            LoopCommand = new RelayCommand(OnLoopButtonClicked);
            ShuffleCommand = new RelayCommand(OnShuffleButtonClicked);
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

            UpdateSliderPosition();
        }
    }
}
